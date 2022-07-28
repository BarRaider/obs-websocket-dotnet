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
using System.Security;
using OBSWebsocketDotNet.Communication;

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket
    {
        #region Events
        /// <summary>
        /// Triggered when switching to another scene
        /// </summary>
        public event SceneChangeCallback SceneChanged;

        /// <summary>
        /// Triggered when a scene is created, deleted or renamed
        /// </summary>
        public event EventHandler SceneListChanged;

        /// <summary>
        /// Triggered when the scene item list of the specified scene is reordered
        /// </summary>
        public event SourceOrderChangeCallback SourceOrderChanged;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        public event SceneItemUpdateCallback SceneItemAdded;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        public event SceneItemUpdateCallback SceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        public event SceneItemVisibilityChangedCallback SceneItemVisibilityChanged;

        /// <summary>
        /// Triggered when the lock status of a scene item changes
        /// </summary>
        public event SceneItemLockChangedCallback SceneItemLockChanged;      

        /// <summary>
        /// Triggered when switching to another scene collection
        /// </summary>
        public event EventHandler SceneCollectionChanged;

        /// <summary>
        /// Triggered when a scene collection is created, deleted or renamed
        /// </summary>
        public event EventHandler SceneCollectionListChanged;

        /// <summary>
        /// Triggered when switching to another transition
        /// </summary>
        public event TransitionChangeCallback TransitionChanged;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        public event TransitionDurationChangeCallback TransitionDurationChanged;

        /// <summary>
        /// Triggered when a transition is created or removed
        /// </summary>
        public event EventHandler TransitionListChanged;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="SceneChanged"/>
        /// </summary>
        public event TransitionBeginCallback TransitionBegin;

        /// <summary>
        /// Triggered when a transition (other than "cut") has ended. Please note that the from-scene field is not available in TransitionEnd
        /// </summary>
        public event TransitionEndCallback TransitionEnd;

        /// <summary>
        /// Triggered when a stinger transition has finished playing its video
        /// </summary>
        public event TransitionVideoEndCallback TransitionVideoEnd;

        /// <summary>
        /// Triggered when switching to another profile
        /// </summary>
        public event EventHandler ProfileChanged;

        /// <summary>
        /// Triggered when a profile is created, imported, removed or renamed
        /// </summary>
        public event EventHandler ProfileListChanged;

        /// <summary>
        /// Triggered when the streaming output state changes
        /// </summary>
        public event OutputStateCallback StreamingStateChanged;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        public event OutputStateCallback RecordingStateChanged;

        /// <summary>
        /// Triggered when the recording output is paused
        /// </summary>
        public event EventHandler RecordingPaused;

        /// <summary>
        /// Triggered when the recording output is resumed
        /// </summary>
        public event EventHandler RecordingResumed;

        /// <summary>
        /// Triggered when state of the replay buffer changes
        /// </summary>
        public event OutputStateCallback ReplayBufferStateChanged;

        /// <summary>
        /// Triggered every 2 seconds while streaming is active
        /// </summary>
        public event StreamStatusCallback StreamStatus;

        /// <summary>
        /// Triggered when the preview scene selection changes (Studio Mode only)
        /// </summary>
        public event SceneChangeCallback PreviewSceneChanged;

        /// <summary>
        /// Triggered when Studio Mode is turned on or off
        /// </summary>
        public event StudioModeChangeCallback StudioModeSwitched;

        /// <summary>
        /// Triggered when OBS exits
        /// </summary>
        public event EventHandler OBSExit;

        /// <summary>
        /// Triggered when connected successfully to an obs-websocket server
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Triggered when disconnected from an obs-websocket server
        /// </summary>
        public event EventHandler<DisconnectionInfo> Disconnected;

        /// <summary>
        /// Emitted every 2 seconds after enabling it by calling SetHeartbeat
        /// </summary>
        public event HeartBeatCallback Heartbeat;

        /// <summary>
        /// A scene item is deselected
        /// </summary>
        public event SceneItemDeselectedCallback SceneItemDeselected;

        /// <summary>
        /// A scene item is selected
        /// </summary>
        public event SceneItemSelectedCallback SceneItemSelected;

        /// <summary>
        /// A scene item transform has changed
        /// </summary>
        public event SceneItemTransformCallback SceneItemTransformChanged;

        /// <summary>
        /// Audio mixer routing changed on a source
        /// </summary>
        public event SourceAudioMixersChangedCallback SourceAudioMixersChanged;

        /// <summary>
        /// The audio sync offset of a source has changed
        /// </summary>
        public event SourceAudioSyncOffsetCallback SourceAudioSyncOffsetChanged;

        /// <summary>
        /// A source has been created. A source can be an input, a scene or a transition.
        /// </summary>
        public event SourceCreatedCallback SourceCreated;

        /// <summary>
        /// A source has been destroyed/removed. A source can be an input, a scene or a transition.
        /// </summary>
        public event SourceDestroyedCallback SourceDestroyed;

        /// <summary>
        /// A filter was added to a source
        /// </summary>
        public event SourceFilterAddedCallback SourceFilterAdded;

        /// <summary>
        /// A filter was removed from a source
        /// </summary>
        public event SourceFilterRemovedCallback SourceFilterRemoved;

        /// <summary>
        /// Filters in a source have been reordered
        /// </summary>
        public event SourceFiltersReorderedCallback SourceFiltersReordered;

        /// <summary>
        /// Triggered when the visibility of a filter has changed
        /// </summary>
        public event SourceFilterVisibilityChangedCallback SourceFilterVisibilityChanged;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event SourceMuteStateChangedCallback SourceMuteStateChanged;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event SourceAudioDeactivatedCallback SourceAudioDeactivated;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event SourceAudioActivatedCallback SourceAudioActivated;

        /// <summary>
        /// A source has been renamed
        /// </summary>
        public event SourceRenamedCallback SourceRenamed;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        public event SourceVolumeChangedCallback SourceVolumeChanged;

        /// <summary>
        /// A custom broadcast message was received
        /// </summary>
        public event BroadcastCustomMessageCallback BroadcastCustomMessageReceived;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file ends. The behavior depends on the type of media source being used.
        /// </summary>
        public event MediaEndedCallback MediaEnded;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file starts playing. The behavior depends on the type of media source being used.
        /// </summary>
        public event MediaStartedCallback MediaStarted;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaPreviousCallback MediaPrevious;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaNextCallback MediaNext;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaStoppedCallback MediaStopped;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaRestartedCallback MediaRestarted;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaPausedCallback MediaPaused;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaPlayingCallback MediaPlaying;

        /// <summary>
        /// The virtual camera has been started.
        /// </summary>
        public event EventHandler VirtualCameraStarted;

        /// <summary>
        /// The virtual camera has been stopped.
        /// </summary>
        public event EventHandler VirtualCameraStopped;

        #endregion

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
                    WSConnection.Stop(WebSocketCloseStatus.Empty, string.Empty);
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
                    string eventType = body["update-type"].ToString();
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
            WSConnection.SendInstant(message.ToString());
            if (!waitForReply)
            {
                return null;
            }

            // Wait for a response (received and notified by the websocket response handler)
            tcs.Task.Wait();

            if (tcs.Task.IsCanceled)
                throw new ErrorResponseException("Request canceled");

            // Throw an exception if the server returned an error.
            // An error occurs if authentication fails or one if the request body is invalid.
            var result = tcs.Task.Result;
            
            if ((string)result["status"] == "error")
                throw new ErrorResponseException((string)result["error"]);

            return result["responseData"].ToObject<JObject>();
        }

        /// <summary>
        /// Requests version info regarding obs-websocket, the API and OBS Studio
        /// </summary>
        /// <returns>Version info in an <see cref="OBSVersion"/> object</returns>
        public OBSVersion GetVersion()
        {
            JObject response = SendRequest("GetVersion");
            return new OBSVersion(response);
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
        /// Update message handler
        /// </summary>
        /// <param name="eventType">Value of "event-type" in the JSON body</param>
        /// <param name="body">full JSON message body</param>
        protected void ProcessEventType(string eventType, JObject body)
        {
            StreamStatus status;

            switch (eventType)
            {
                case "SwitchScenes":
                    SceneChanged?.Invoke(this, (string)body["scene-name"]);
                    break;

                case "ScenesChanged":
                    SceneListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "SourceOrderChanged":
                    SourceOrderChanged?.Invoke(this, (string)body["scene-name"]);
                    break;

                case "SceneItemAdded":
                    SceneItemAdded?.Invoke(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneItemRemoved":
                    SceneItemRemoved?.Invoke(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneItemVisibilityChanged":
                    SceneItemVisibilityChanged?.Invoke(this, (string)body["scene-name"], (string)body["item-name"], (bool)body["item-visible"]);
                    break;
                case "SceneItemLockChanged":
                    SceneItemLockChanged?.Invoke(this, (string)body["scene-name"], (string)body["item-name"], (int)body["item-id"], (bool)body["item-locked"]);
                    break;
                case "SceneCollectionChanged":
                    SceneCollectionChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "SceneCollectionListChanged":
                    SceneCollectionListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "SwitchTransition":
                    TransitionChanged?.Invoke(this, (string)body["transition-name"]);
                    break;

                case "TransitionDurationChanged":
                    TransitionDurationChanged?.Invoke(this, (int)body["new-duration"]);
                    break;

                case "TransitionListChanged":
                    TransitionListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "TransitionBegin":
                    TransitionBegin?.Invoke(this, (string)body["name"], (string)body["type"], (int)body["duration"], (string)body["from-scene"], (string)body["to-scene"]);
                    break;
                case "TransitionEnd":
                    TransitionEnd?.Invoke(this, (string)body["name"], (string)body["type"], (int)body["duration"], (string)body["to-scene"]);
                    break;
                case "TransitionVideoEnd":
                    TransitionVideoEnd?.Invoke(this, (string)body["name"], (string)body["type"], (int)body["duration"], (string)body["from-scene"], (string)body["to-scene"]);
                    break;
                case "ProfileChanged":
                    ProfileChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "ProfileListChanged":
                    ProfileListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "StreamStarting":
                    StreamingStateChanged?.Invoke(this, OutputState.Starting);
                    break;

                case "StreamStarted":
                    StreamingStateChanged?.Invoke(this, OutputState.Started);
                    break;

                case "StreamStopping":
                    StreamingStateChanged?.Invoke(this, OutputState.Stopping);
                    break;

                case "StreamStopped":
                    StreamingStateChanged?.Invoke(this, OutputState.Stopped);
                    break;

                case "RecordingStarting":
                    RecordingStateChanged?.Invoke(this, OutputState.Starting);
                    break;

                case "RecordingStarted":
                    RecordingStateChanged?.Invoke(this, OutputState.Started);
                    break;

                case "RecordingStopping":
                    RecordingStateChanged?.Invoke(this, OutputState.Stopping);
                    break;

                case "RecordingStopped":
                    RecordingStateChanged?.Invoke(this, OutputState.Stopped);
                    break;
                case "RecordingPaused":
                    RecordingPaused?.Invoke(this, EventArgs.Empty);
                    break;
                case "RecordingResumed":
                    RecordingResumed?.Invoke(this, EventArgs.Empty);
                    break;
                case "StreamStatus":
                    if (StreamStatus != null)
                    {
                        status = new StreamStatus(body);
                        StreamStatus(this, status);
                    }
                    break;

                case "PreviewSceneChanged":
                    PreviewSceneChanged?.Invoke(this, (string)body["scene-name"]);
                    break;

                case "StudioModeSwitched":
                    StudioModeSwitched?.Invoke(this, (bool)body["new-state"]);
                    break;

                case "ReplayStarting":
                    ReplayBufferStateChanged?.Invoke(this, OutputState.Starting);
                    break;

                case "ReplayStarted":
                    ReplayBufferStateChanged?.Invoke(this, OutputState.Started);
                    break;

                case "ReplayStopping":
                    ReplayBufferStateChanged?.Invoke(this, OutputState.Stopping);
                    break;

                case "ReplayStopped":
                    ReplayBufferStateChanged?.Invoke(this, OutputState.Stopped);
                    break;

                case "Exiting":
                    OBSExit?.Invoke(this, EventArgs.Empty);
                    break;

                case "Heartbeat":
                    Heartbeat?.Invoke(this, new Heartbeat(body));
                    break;
                case "SceneItemDeselected":
                    SceneItemDeselected?.Invoke(this, (string)body["scene-name"], (string)body["item-name"], (string)body["item-id"]);
                    break;
                case "SceneItemSelected":
                    SceneItemSelected?.Invoke(this, (string)body["scene-name"], (string)body["item-name"], (string)body["item-id"]);
                    break;
                case "SceneItemTransformChanged":
                    SceneItemTransformChanged?.Invoke(this, new SceneItemTransformInfo(body));
                    break;
                case "SourceAudioMixersChanged":
                    SourceAudioMixersChanged?.Invoke(this, new AudioMixersChangedInfo(body));
                    break;
                case "SourceAudioSyncOffsetChanged":
                    SourceAudioSyncOffsetChanged?.Invoke(this, (string)body["sourceName"], (int)body["syncOffset"]);
                    break;
                case "SourceCreated":
                    SourceCreated?.Invoke(this, new SourceSettings(body));
                    break;
                case "SourceDestroyed":
                    SourceDestroyed?.Invoke(this, (string)body["sourceName"], (string)body["sourceType"], (string)body["sourceKind"]);
                    break;
                case "SourceRenamed":
                    SourceRenamed?.Invoke(this, (string)body["newName"], (string)body["previousName"]);
                    break;

                case "SourceMuteStateChanged":
                    SourceMuteStateChanged?.Invoke(this, (string)body["sourceName"], (bool)body["muted"]);
                    break;
                case "SourceAudioDeactivated":
                    SourceAudioDeactivated?.Invoke(this, (string)body["sourceName"]);
                    break;
                case "SourceAudioActivated":
                    SourceAudioActivated?.Invoke(this, (string)body["sourceName"]);
                    break;
                case "SourceVolumeChanged":
                    SourceVolumeChanged?.Invoke(this, new SourceVolume(body));
                    break;
                case "SourceFilterAdded":
                    SourceFilterAdded?.Invoke(this, (string)body["sourceName"], (string)body["filterName"], (string)body["filterType"], (JObject)body["filterSettings"]);
                    break;
                case "SourceFilterRemoved":
                    SourceFilterRemoved?.Invoke(this, (string)body["sourceName"], (string)body["filterName"]);
                    break;
                case "SourceFiltersReordered":
                    if (SourceFiltersReordered != null)
                    {
                        List<FilterReorderItem> filters = new List<FilterReorderItem>();
                        JsonConvert.PopulateObject(body["filters"].ToString(), filters);

                        SourceFiltersReordered?.Invoke(this, (string)body["sourceName"], filters);
                    }
                    break;
                case "SourceFilterVisibilityChanged":
                    SourceFilterVisibilityChanged?.Invoke(this, (string)body["sourceName"], (string)body["filterName"], (bool)body["filterEnabled"]);
                    break;
                case "BroadcastCustomMessage":
                    BroadcastCustomMessageReceived?.Invoke(this, (string)body["realm"], (JObject)body["data"]);
                    break;

                case "MediaEnded":
                    MediaEnded?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaStarted":
                    MediaStarted?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaPrevious":
                    MediaPrevious?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaNext":
                    MediaNext?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaStopped":
                    MediaStopped?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaRestarted":
                    MediaRestarted?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaPaused":
                    MediaPaused?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "MediaPlaying":
                    MediaPlaying?.Invoke(this, (string)body["sourceName"], (string)body["sourceKind"]);
                    break;
                case "VirtualCamStarted":
                    VirtualCameraStarted?.Invoke(this, EventArgs.Empty);
                    break;
                case "VirtualCamStopped":
                    VirtualCameraStopped?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                        var message = $"Unsupported Event: {eventType}\n{body}";
                        Console.WriteLine(message);
                        Debug.WriteLine(message);
                        break;
            }
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
