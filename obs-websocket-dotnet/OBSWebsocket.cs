using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using Websocket.Client;
using OBSWebsocketDotNet.Communication;

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket : IOBSWebsocket
    {
        #region Private Members
        private const string WEBSOCKET_URL_PREFIX = "ws://";
        private const int SUPPORTED_RPC_VERSION = 1;
        private TimeSpan wsTimeout = TimeSpan.FromSeconds(10);
        private string connectionPassword = null;
        private WebsocketClient wsConnection;

        private delegate void RequestCallback(OBSWebsocket sender, JObject body);
        private readonly ConcurrentDictionary<string, TaskCompletionSource<JObject>> responseHandlers;

        // Random should never be created inside a function
        private static readonly Random random = new Random();

        #endregion

        /// <summary>
        /// WebSocket request timeout, represented as a TimeSpan object
        /// </summary>
        public TimeSpan WSTimeout
        {
            get
            {
                return wsConnection?.ReconnectTimeout ?? wsTimeout;
            }
            set
            {
                wsTimeout = value;

                if (wsConnection != null)
                {
                    wsConnection.ReconnectTimeout = wsTimeout;
                }
            }
        }
      
        /// <summary>
        /// Current connection state
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (wsConnection != null && wsConnection.IsRunning);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OBSWebsocket()
        {
            responseHandlers = new ConcurrentDictionary<string, TaskCompletionSource<JObject>>();
        }

        /// <summary>
        /// Connect this instance to the specified URL, and authenticate (if needed) with the specified password.
        /// NOTE: Please subscribe to the Connected/Disconnected events (or atleast check the IsConnected property) to determine when the connection is actually fully established
        /// </summary>
        /// <param name="url">Server URL in standard URL format.</param>
        /// <param name="password">Server password</param>
        [Obsolete("Please use ConnectAsync, this function will be removed in the next version")]
        public void Connect(string url, string password)
        {
            ConnectAsync(url, password);
        }

        /// <summary>
        /// Connect this instance to the specified URL, and authenticate (if needed) with the specified password.
        /// NOTE: Please subscribe to the Connected/Disconnected events (or atleast check the IsConnected property) to determine when the connection is actually fully established
        /// </summary>
        /// <param name="url">Server URL in standard URL format.</param>
        /// <param name="password">Server password</param>
        public void ConnectAsync(string url, string password)
        {
            if (!url.ToLower().StartsWith(WEBSOCKET_URL_PREFIX))
            {
                throw new ArgumentException($"Invalid url, must start with '{WEBSOCKET_URL_PREFIX}'");
            }

            if (wsConnection != null && wsConnection.IsRunning)
            {
                Disconnect();
            }

            wsConnection = new WebsocketClient(new Uri(url));
            wsConnection.IsReconnectionEnabled = false;
            wsConnection.ReconnectTimeout = null;
            wsConnection.ErrorReconnectTimeout = null;
            wsConnection.MessageReceived.Subscribe(m => Task.Run(() => WebsocketMessageHandler(this, m)));
            wsConnection.DisconnectionHappened.Subscribe(d => Task.Run(() => OnWebsocketDisconnect(this, d)));

            connectionPassword = password;
            wsConnection.StartOrFail();
        }

        /// <summary>
        /// Disconnect this instance from the server
        /// </summary>
        public void Disconnect()
        {
            connectionPassword = null;
            if (wsConnection != null)
            {
                // Attempt to both close and dispose the existing connection
                try
                {
                    wsConnection.Stop(WebSocketCloseStatus.NormalClosure, "User requested disconnect");
                    ((IDisposable)wsConnection).Dispose();
                }
                catch { }
                wsConnection = null;
            }

            var unusedHandlers = responseHandlers.ToArray();
            responseHandlers.Clear();
            foreach (var cb in unusedHandlers)
            {
                var tcs = cb.Value;
                tcs.TrySetCanceled();
            }
        }

        // This callback handles a websocket disconnection
        private void OnWebsocketDisconnect(object sender, DisconnectionInfo d)
        {
            if (d == null || d.CloseStatus == null)
            {
                Disconnected?.Invoke(sender, new ObsDisconnectionInfo(ObsCloseCodes.UnknownReason, null, d));
            }
            else
            {
                Disconnected?.Invoke(sender, new ObsDisconnectionInfo((ObsCloseCodes)d.CloseStatus, d.CloseStatusDescription, d));
            }
        }

        // This callback handles incoming JSON messages and determines if it's
        // a request response or an event ("Update" in obs-websocket terminology)
        private void WebsocketMessageHandler(object sender, ResponseMessage e)
        {
            if (e.MessageType != WebSocketMessageType.Text)
            {
                return;
            }

            ServerMessage msg = JsonConvert.DeserializeObject<ServerMessage>(e.Text);
            JObject body = msg.Data;

            switch (msg.OperationCode)
            {
                case MessageTypes.Hello:
                    // First message received after connection, this may ask us for authentication
                    HandleHello(body);
                    break;
                case MessageTypes.Identified:
                    Task.Run(() => Connected?.Invoke(this, EventArgs.Empty));
                    break;
                case MessageTypes.RequestResponse:
                case MessageTypes.RequestBatchResponse:
                    // Handle response to previous request
                    if (body.ContainsKey("requestId"))
                    {
                        // Handle a request :
                        // Find the response handler based on
                        // its associated message ID
                        string msgID = (string)body["requestId"];

                        if (responseHandlers.TryRemove(msgID, out TaskCompletionSource<JObject> handler))
                        {
                            // Set the response body as Result and notify the request sender
                            handler.SetResult(body);
                        }
                    }
                    break;
                case MessageTypes.Event:
                    // Handle events
                    string eventType = body["eventType"].ToString();
                    Task.Run(() => { ProcessEventType(eventType, body); });
                    break;
                default:
                    // Unsupported message type
                    break;

            }
        }

        /// <summary>
        /// Sends a message to the websocket API with the specified request type and optional parameters
        /// </summary>
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="additionalFields">additional JSON fields if required by the request type</param>
        /// <returns>The server's JSON response as a JObject</returns>
        public JObject SendRequest(string requestType, JObject additionalFields = null)
        {
            return SendRequest(MessageTypes.Request, requestType, additionalFields, true);
        }

        /// <summary>
        /// Internal version which allows to set the opcode
        /// Sends a message to the websocket API with the specified request type and optional parameters
        /// </summary>
        /// <param name="operationCode">Type/OpCode for this messaage</param>
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="additionalFields">additional JSON fields if required by the request type</param>
        /// <param name="waitForReply">Should wait for reply vs "fire and forget"</param>
        /// <returns>The server's JSON response as a JObject</returns>
        internal JObject SendRequest(MessageTypes operationCode, string requestType, JObject additionalFields = null, bool waitForReply = true)
        {
            if (wsConnection == null)
            {
                throw new NullReferenceException("Websocket is not initialized");
            }

            // Prepare the asynchronous response handler
            var tcs = new TaskCompletionSource<JObject>();
            JObject message = null;
            do
            {
                // Generate a random message id
                message = MessageFactory.BuildMessage(operationCode, requestType, additionalFields, out string messageId);
                if (!waitForReply || responseHandlers.TryAdd(messageId, tcs))
                {
                    break;
                }
                // Message id already exists, retry with a new one.
            } while (true);
            // Send the message 
            wsConnection.Send(message.ToString());
            if (!waitForReply)
            {
                return null;
            }

            // Wait for a response (received and notified by the websocket response handler)
            tcs.Task.Wait(wsTimeout.Milliseconds);

            if (tcs.Task.IsCanceled)
                throw new ErrorResponseException("Request canceled", 0);

            // Throw an exception if the server returned an error.
            // An error occurs if authentication fails or one if the request body is invalid.
            var result = tcs.Task.Result;

            if (!(bool)result["requestStatus"]["result"])
            {
                var status = (JObject)result["requestStatus"];
                throw new ErrorResponseException($"ErrorCode: {status["code"]}{(status.ContainsKey("comment") ? $", Comment: {status["comment"]}" : "")}", (int)status["code"]);
            }

            if (result.ContainsKey("responseData")) // ResponseData is optional
                return result["responseData"].ToObject<JObject>();

            return new JObject();
        }

        /// <summary>
        /// Request authentication data. You don't have to call this manually.
        /// </summary>
        /// <returns>Authentication data in an <see cref="OBSAuthInfo"/> object</returns>
        public OBSAuthInfo GetAuthInfo()
        {
            JObject response = SendRequest("GetAuthRequired");
            return new OBSAuthInfo(response);
        }

        /// <summary>
        /// Authenticates to the Websocket server using the challenge and salt given in the passed <see cref="OBSAuthInfo"/> object
        /// </summary>
        /// <param name="password">User password</param>
        /// <param name="authInfo">Authentication data</param>
        /// <returns>true if authentication succeeds, false otherwise</returns>
        protected void SendIdentify(string password, OBSAuthInfo authInfo = null)
        {
            var requestFields = new JObject
            {
                { "rpcVersion", SUPPORTED_RPC_VERSION },
                { "eventSubscriptions", (uint)registeredEvents }
            };

            if (authInfo != null)
            {
                // Authorization required

                string secret = HashEncode(password + authInfo.PasswordSalt);
                string authResponse = HashEncode(secret + authInfo.Challenge);
                requestFields.Add("authentication", authResponse);
            }

            SendRequest(MessageTypes.Identify, null, requestFields, false);
        }

        /// <summary>
        /// Encode a Base64-encoded SHA-256 hash
        /// </summary>
        /// <param name="input">source string</param>
        /// <returns></returns>
        protected string HashEncode(string input)
        {
            using var sha256 = new SHA256Managed();

            byte[] textBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha256.ComputeHash(textBytes);

            return System.Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Generate a message ID
        /// </summary>
        /// <param name="length">(optional) message ID length</param>
        /// <returns>A random string of alphanumerical characters</returns>
        protected string NewMessageID(int length = 16)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string result = "";
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, pool.Length - 1);
                result += pool[index];
            }

            return result;
        }

        private void HandleHello(JObject payload)
        {
            if (!wsConnection.IsStarted)
            {
                return;
            }

            OBSAuthInfo authInfo = null;
            if (payload.ContainsKey("authentication"))
            {
                // Authentication required
                authInfo = new OBSAuthInfo((JObject)payload["authentication"]);
            }

            SendIdentify(connectionPassword, authInfo);

            connectionPassword = null;
        }
    }
}
