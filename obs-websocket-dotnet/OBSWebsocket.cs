using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using OBSWebsocketDotNet.Types;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.WebSockets;
using Websocket.Client;
using OBSWebsocketDotNet.Communication;

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket
    {
        /// <summary>
        /// WebSocket request timeout, represented as a TimeSpan object
        /// </summary>
        public TimeSpan WSTimeout
        {
            get
            {
                return WSConnection?.ReconnectTimeout ?? wsTimeout;
            }
            set
            {
                wsTimeout = value;

                if (WSConnection != null)
                {
                    WSConnection.ReconnectTimeout = wsTimeout;
                }
            }
        }

        #region Private Members
        private const string WEBSOCKET_URL_PREFIX = "ws://";
        private const int SUPPORTED_RPC_VERSION = 1;
        private TimeSpan wsTimeout = TimeSpan.FromSeconds(60);
        private string connectionPassword = null;

        // Random should never be created inside a function
        private static readonly Random random = new Random();

        #endregion

        /// <summary>
        /// Current connection state
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (WSConnection != null && WSConnection.IsRunning);
            }
        }

        /// <summary>
        /// Underlying WebSocket connection to an obs-websocket server. Value is null when disconnected.
        /// </summary>
        public WebsocketClient WSConnection { get; private set; }

        private delegate void RequestCallback(OBSWebsocket sender, JObject body);
        private readonly ConcurrentDictionary<string, TaskCompletionSource<JObject>> responseHandlers;

        /// <summary>
        /// Constructor
        /// </summary>
        public OBSWebsocket()
        {
            responseHandlers = new ConcurrentDictionary<string, TaskCompletionSource<JObject>>();
        }

        /// <summary>
        /// Connect this instance to the specified URL, and authenticate (if needed) with the specified password
        /// </summary>
        /// <param name="url">Server URL in standard URL format.</param>
        /// <param name="password">Server password</param>
        public void Connect(string url, string password)
        {
            if (!url.ToLower().StartsWith(WEBSOCKET_URL_PREFIX))
            {
                throw new ArgumentException($"Invalid url, must start with '{WEBSOCKET_URL_PREFIX}'");
            }

            if (WSConnection != null && WSConnection.IsRunning)
            {
                Disconnect();
            }

            WSConnection = new WebsocketClient(new Uri(url));
            WSConnection.MessageReceived.Subscribe(m => WebsocketMessageHandler(this, m));
            WSConnection.DisconnectionHappened.Subscribe(d => Disconnected?.Invoke(this, d));

            connectionPassword = password;
            WSConnection.StartOrFail();
        }

        /// <summary>
        /// Disconnect this instance from the server
        /// </summary>
        public void Disconnect()
        {
            connectionPassword = null;
            if (WSConnection != null)
            {
                // Attempt to both close and dispose the existing connection
                try
                {
                    WSConnection.Stop(WebSocketCloseStatus.NormalClosure, "User requested disconnect");
                    ((IDisposable)WSConnection).Dispose();
                }
                catch { }
                WSConnection = null;
            }
            
            var unusedHandlers = responseHandlers.ToArray();
            responseHandlers.Clear();
            foreach (var cb in unusedHandlers)
            {
                var tcs = cb.Value;
                tcs.TrySetCanceled();
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
                    Connected?.Invoke(this, EventArgs.Empty);
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
                    //string eventType = body["update-type"].ToString();
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
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="additionalFields">additional JSON fields if required by the request type</param>
        /// <returns>The server's JSON response as a JObject</returns>
        internal JObject SendRequest(MessageTypes operationCode, string requestType, JObject additionalFields = null, bool waitForReply = true)
        {
            
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
            WSConnection.Send(message.ToString());
            if (!waitForReply)
            {
                return null;
            }

            // Wait for a response (received and notified by the websocket response handler)
            tcs.Task.Wait();

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
        protected bool SendIdentify(string password, OBSAuthInfo authInfo = null)
        {
            var requestFields = new JObject
            {
                { "rpcVersion", SUPPORTED_RPC_VERSION }
            };

            if (authInfo != null)
            {
                // Authorization required

                string secret = HashEncode(password + authInfo.PasswordSalt);
                string authResponse = HashEncode(secret + authInfo.Challenge);
                requestFields.Add("authentication", authResponse);
            }

            try
            {
                // Throws ErrorResponseException if auth fails
                SendRequest(MessageTypes.Identify, null, requestFields, false);
            }
            catch (ErrorResponseException)
            {
                Disconnected?.Invoke(this, new DisconnectionInfo(DisconnectionType.Error, WebSocketCloseStatus.ProtocolError, "Auth Failed", String.Empty, new AuthFailureException()));
                Disconnect();
                return false;
            }

            return true;
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
            if (!WSConnection.IsStarted)
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
