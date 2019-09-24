/*
    The MIT License (MIT)

    Copyright (c) 2017 Stéphane Lepin

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using OBSWebsocketDotNet.Types;
using Newtonsoft.Json;

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
        public event SceneItemUpdateCallback SceneItemVisibilityChanged;

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
        public event EventHandler TransitionBegin;

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
        public event EventHandler Disconnected;

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
        /// A source has been muted or unmuted
        /// </summary>
        public event SourceMuteStateChangedCallback SourceMuteStateChanged;

        /// <summary>
        /// A source has been renamed
        /// </summary>
        public event SourceRenamedCallback SourceRenamed;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        public event SourceVolumeChangedCallback SourceVolumeChanged;

        #endregion

        /// <summary>
        /// WebSocket request timeout, represented as a TimeSpan object
        /// </summary>
        public TimeSpan WSTimeout
        {
            get
            {
                if (WSConnection != null)
                    return WSConnection.WaitTime;
                else
                    return _pWSTimeout;
            }
            set
            {
                _pWSTimeout = value;

                if (WSConnection != null)
                    WSConnection.WaitTime = _pWSTimeout;
            }
        }
        private TimeSpan _pWSTimeout;

        /// <summary>
        /// Current connection state
        /// </summary>
        public bool IsConnected
        {
            get {
                return (WSConnection != null ? WSConnection.IsAlive : false);
            }
        }

        /// <summary>
        /// Underlying WebSocket connection to an obs-websocket server. Value is null when disconnected.
        /// </summary>
        public WebSocket WSConnection { get; private set; }

        private delegate void RequestCallback(OBSWebsocket sender, JObject body);
        private Dictionary<string, TaskCompletionSource<JObject>> _responseHandlers;

        /// <summary>
        /// Constructor
        /// </summary>
        public OBSWebsocket()
        {
            _responseHandlers = new Dictionary<string, TaskCompletionSource<JObject>>();
        }

        /// <summary>
        /// Connect this instance to the specified URL, and authenticate (if needed) with the specified password
        /// </summary>
        /// <param name="url">Server URL in standard URL format</param>
        /// <param name="password">Server password</param>
        public void Connect(string url, string password)
        {
            if (WSConnection != null && WSConnection.IsAlive)
                Disconnect();

            WSConnection = new WebSocket(url);
            WSConnection.WaitTime = _pWSTimeout;
            WSConnection.OnMessage += WebsocketMessageHandler;
            WSConnection.OnClose += (s, e) =>
            {
                if (Disconnected != null)
                    Disconnected(this, e);
            };
            WSConnection.Connect();

            if (!WSConnection.IsAlive)
                return;

            OBSAuthInfo authInfo = GetAuthInfo();

            if (authInfo.AuthRequired)
                Authenticate(password, authInfo);

            if (Connected != null)
                Connected(this, null);
        }

        /// <summary>
        /// Disconnect this instance from the server
        /// </summary>
        public void Disconnect()
        {
            if (WSConnection != null)
                WSConnection.Close();

            WSConnection = null;

            foreach (var cb in _responseHandlers)
            {
                var tcs = cb.Value;
                tcs.TrySetCanceled();
            }
        }



        // This callback handles incoming JSON messages and determines if it's
        // a request response or an event ("Update" in obs-websocket terminology)
        private void WebsocketMessageHandler(object sender, MessageEventArgs e)
        {
            if (!e.IsText)
                return;

            JObject body = JObject.Parse(e.Data);

            if (body["message-id"] != null)
            {
                // Handle a request :
                // Find the response handler based on
                // its associated message ID
                string msgID = (string)body["message-id"];
                var handler = _responseHandlers[msgID];

                if (handler != null)
                {
                    // Set the response body as Result and notify the request sender
                    handler.SetResult(body);

                    // The message with the given ID has been processed,
                    // so its handler can be discarded
                    _responseHandlers.Remove(msgID);
                }
            }
            else if(body["update-type"] != null)
            {
                // Handle an event
                string eventType = body["update-type"].ToString();
                ProcessEventType(eventType, body);
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
            string messageID;

            // Generate a random message id and make sure it is unique within the handlers dictionary
            do { messageID = NewMessageID(); }
            while (_responseHandlers.ContainsKey(messageID));

            // Build the bare-minimum body for a request
            var body = new JObject();
            body.Add("request-type", requestType);
            body.Add("message-id", messageID);

            // Add optional fields if provided
            if (additionalFields != null)
            {
                var mergeSettings = new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                };

                body.Merge(additionalFields);
            }

            // Prepare the asynchronous response handler
            var tcs = new TaskCompletionSource<JObject>();
            _responseHandlers.Add(messageID, tcs);

            // Send the message and wait for a response
            // (received and notified by the websocket response handler)
            WSConnection.Send(body.ToString());
            tcs.Task.Wait();

            if (tcs.Task.IsCanceled)
                throw new ErrorResponseException("Request canceled");

            // Throw an exception if the server returned an error.
            // An error occurs if authentication fails or one if the request body is invalid.
            var result = tcs.Task.Result;

            if ((string)result["status"] == "error")
                throw new ErrorResponseException((string)result["error"]);

            return result;
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
        public bool Authenticate(string password, OBSAuthInfo authInfo)
        {
            string secret = HashEncode(password + authInfo.PasswordSalt);
            string authResponse = HashEncode(secret + authInfo.Challenge);

            var requestFields = new JObject();
            requestFields.Add("auth", authResponse);

            try
            {
                // Throws ErrorResponseException if auth fails
                SendRequest("Authenticate", requestFields);
            }
            catch(ErrorResponseException)
            {
                throw new AuthFailureException();
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
                    if(SceneChanged != null)
                        SceneChanged(this, (string)body["scene-name"]);
                    break;

                case "ScenesChanged":
                    if (SceneListChanged != null)
                        SceneListChanged(this, EventArgs.Empty);
                    break;

                case "SourceOrderChanged":
                    if (SourceOrderChanged != null)
                        SourceOrderChanged(this, (string)body["scene-name"]);
                    break;

                case "SceneItemAdded":
                    if (SceneItemAdded != null)
                        SceneItemAdded(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneItemRemoved":
                    if (SceneItemRemoved != null)
                        SceneItemRemoved(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneItemVisibilityChanged":
                    if (SceneItemVisibilityChanged != null)
                        SceneItemVisibilityChanged(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneCollectionChanged":
                    if (SceneCollectionChanged != null)
                        SceneCollectionChanged(this, EventArgs.Empty);
                    break;

                case "SceneCollectionListChanged":
                    if (SceneCollectionListChanged != null)
                        SceneCollectionListChanged(this, EventArgs.Empty);
                    break;

                case "SwitchTransition":
                    if (TransitionChanged != null)
                        TransitionChanged(this, (string)body["transition-name"]);
                    break;

                case "TransitionDurationChanged":
                    if (TransitionDurationChanged != null)
                        TransitionDurationChanged(this, (int)body["new-duration"]);
                    break;

                case "TransitionListChanged":
                    if (TransitionListChanged != null)
                        TransitionListChanged(this, EventArgs.Empty);
                    break;

                case "TransitionBegin":
                    if (TransitionBegin != null)
                        TransitionBegin(this, EventArgs.Empty);
                    break;

                case "ProfileChanged":
                    if (ProfileChanged != null)
                        ProfileChanged(this, EventArgs.Empty);
                    break;

                case "ProfileListChanged":
                    if (ProfileListChanged != null)
                        ProfileListChanged(this, EventArgs.Empty);
                    break;

                case "StreamStarting":
                    if (StreamingStateChanged != null)
                        StreamingStateChanged(this, OutputState.Starting);
                    break;

                case "StreamStarted":
                    if (StreamingStateChanged != null)
                        StreamingStateChanged(this, OutputState.Started);
                    break;

                case "StreamStopping":
                    if (StreamingStateChanged != null)
                        StreamingStateChanged(this, OutputState.Stopping);
                    break;

                case "StreamStopped":
                    if (StreamingStateChanged != null)
                        StreamingStateChanged(this, OutputState.Stopped);
                    break;

                case "RecordingStarting":
                    if (RecordingStateChanged != null)
                        RecordingStateChanged(this, OutputState.Starting);
                    break;

                case "RecordingStarted":
                    if (RecordingStateChanged != null)
                        RecordingStateChanged(this, OutputState.Started);
                    break;

                case "RecordingStopping":
                    if (RecordingStateChanged != null)
                        RecordingStateChanged(this, OutputState.Stopping);
                    break;

                case "RecordingStopped":
                    if (RecordingStateChanged != null)
                        RecordingStateChanged(this, OutputState.Stopped);
                    break;

                case "StreamStatus":
                    if (StreamStatus != null)
                    {
                        status = new StreamStatus(body);
                        StreamStatus(this, status);
                    }
                    break;

                case "PreviewSceneChanged":
                    if(PreviewSceneChanged != null)
                        PreviewSceneChanged(this, (string)body["scene-name"]);
                    break;

                case "StudioModeSwitched":
                    if (StudioModeSwitched != null)
                        StudioModeSwitched(this, (bool)body["new-state"]);
                    break;

                case "ReplayStarting":
                    if (ReplayBufferStateChanged != null)
                        ReplayBufferStateChanged(this, OutputState.Starting);
                    break;

                case "ReplayStarted":
                    if (ReplayBufferStateChanged != null)
                        ReplayBufferStateChanged(this, OutputState.Started);
                    break;

                case "ReplayStopping":
                    if (ReplayBufferStateChanged != null)
                        ReplayBufferStateChanged(this, OutputState.Stopping);
                    break;

                case "ReplayStopped":
                    if (ReplayBufferStateChanged != null)
                        ReplayBufferStateChanged(this, OutputState.Stopped);
                    break;

                case "Exiting":
                    if (OBSExit != null)
                        OBSExit(this, EventArgs.Empty);
                    break;

                case "Heartbeat":
                    if (Heartbeat != null)
                        Heartbeat(this, new Heartbeat(body));
                    break;
                case "SceneItemDeselected":
                    if (SceneItemDeselected != null)
                        SceneItemDeselected(this, (string)body["scene-name"], (string)body["item-name"], (string)body["item-id"]);
                    break;
                case "SceneItemSelected":
                    if (SceneItemSelected != null)
                        SceneItemSelected(this, (string)body["scene-name"], (string)body["item-name"], (string)body["item-id"]);
                    break;
                case "SceneItemTransformChanged":
                    if (SceneItemTransformChanged != null)
                        SceneItemTransformChanged(this, new SceneItemTransformInfo(body));
                    break;
                case "SourceAudioMixersChanged":
                    if (SourceAudioMixersChanged != null)
                        SourceAudioMixersChanged(this, new AudioMixersChangedInfo(body));
                    break;
                case "SourceAudioSyncOffsetChanged":
                    if (SourceAudioSyncOffsetChanged != null)
                        SourceAudioSyncOffsetChanged(this, (string)body["sourceName"], (int)body["syncOffset"]);
                    break;
                case "SourceCreated":
                    if (SourceCreated != null)
                        SourceCreated(this, new SourceSettings(body));
                    break;
                case "SourceDestroyed":
                    if (SourceDestroyed != null)
                        SourceDestroyed(this, (string)body["sourceName"], (string)body["sourceType"], (string)body["sourceKind"]);
                    break;
                case "SourceRenamed":
                    if (SourceRenamed != null)
                        SourceRenamed(this, (string)body["newName"], (string)body["previousName"]);
                    break;

                case "SourceMuteStateChanged":
                    if (SourceMuteStateChanged != null)
                        SourceMuteStateChanged(this, (string)body["sourceName"], (bool)body["muted"]);
                    break;
                case "SourceVolumeChanged":
                    if (SourceVolumeChanged != null)
                        SourceVolumeChanged(this, (string)body["sourceName"], (float)body["volume"]);
                    break;
                case "SourceFilterAdded":
                    if (SourceFilterAdded != null)
                        SourceFilterAdded(this, (string)body["sourceName"], (string)body["filterName"], (string)body["filterType"], (JObject)body["filterSettings"]);
                    break;
                case "SourceFilterRemoved":
                    if (SourceFilterRemoved != null)
                        SourceFilterRemoved(this, (string)body["sourceName"], (string)body["filterName"]);
                    break;
                case "SourceFiltersReordered":
                    List<FilterReorderItem> filters = new List<FilterReorderItem>();
                    JsonConvert.PopulateObject(body["filters"].ToString(), filters);

                    if (SourceFiltersReordered != null)
                        SourceFiltersReordered(this, (string)body["sourceName"], filters);
                    break;
                /*
                default:
                    var header = "-----------" + eventType + "-------------";
                    Console.WriteLine(header);
                    Console.WriteLine(body);
                    Console.WriteLine("".PadLeft(header.Length,'-'));
                    break;
                 */
            }
        }

        /// <summary>
        /// Encode a Base64-encoded SHA-256 hash
        /// </summary>
        /// <param name="input">source string</param>
        /// <returns></returns>
        protected string HashEncode(string input)
        {
            var sha256 = new SHA256Managed();

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
            var random = new Random();

            string result = "";
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, pool.Length - 1);
                result += pool[index];
            }

            return result;
        }
    }
}
