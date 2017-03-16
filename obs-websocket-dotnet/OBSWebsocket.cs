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

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket
    {
        #region Events
        /// <summary>
        /// Triggered when switching to another scene
        /// </summary>
        public SceneChangeCallback OnSceneChange;

        /// <summary>
        /// Triggered when a scene is created, deleted or renamed
        /// </summary>
        public EventHandler OnSceneListChange;

        /// <summary>
        /// Triggered when the scene item list of the specified scene is reordered
        /// </summary>
        public SourceOrderChangeCallback OnSourceOrderChange;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        public SceneItemUpdateCallback OnSceneItemAdded;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        public SceneItemUpdateCallback OnSceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        public SceneItemUpdateCallback OnSceneItemVisibilityChange;

        /// <summary>
        /// Triggered when switching to another scene collection
        /// </summary>
        public EventHandler OnSceneCollectionChange;

        /// <summary>
        /// Triggered when a scene collection is created, deleted or renamed
        /// </summary>
        public EventHandler OnSceneCollectionListChange;

        /// <summary>
        /// Triggered when switching to another transition
        /// </summary>
        public TransitionChangeCallback OnTransitionChange;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        public TransitionDurationChangeCallback OnTransitionDurationChange;

        /// <summary>
        /// Triggered when a transition is created or removed
        /// </summary>
        public EventHandler OnTransitionListChange;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="OnSceneChange"/> 
        /// </summary>
        public EventHandler OnTransitionBegin;

        /// <summary>
        /// Triggered when switching to another profile
        /// </summary>
        public EventHandler OnProfileChange;

        /// <summary>
        /// Triggered when a profile is created, imported, removed or renamed
        /// </summary>
        public EventHandler OnProfileListChange;

        /// <summary>
        /// Triggered when the streaming output state changes
        /// </summary>
        public OutputStateCallback OnStreamingStateChange;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        public OutputStateCallback OnRecordingStateChange;

        /// <summary>
        /// Triggered every 2 seconds while streaming is active
        /// </summary>
        public StreamStatusCallback OnStreamStatus;

        /// <summary>
        /// Triggered when OBS exits
        /// </summary>
        public EventHandler OnExit;
        #endregion

        private delegate void RequestCallback(OBSWebsocket sender, JObject body);

        private WebSocket _ws;
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
            if (_ws != null && _ws.IsAlive)
                Disconnect();

            _ws = new WebSocket(url);
            _ws.OnMessage += WebsocketMessageHandler;
            _ws.Connect();

            OBSAuthInfo authInfo = GetAuthInfo();

            if (authInfo.AuthRequired)
                Authenticate(password, authInfo);
        }

        /// <summary>
        /// Disconnect this instance from the server
        /// </summary>
        public void Disconnect()
        {
            if (_ws != null)
                _ws.Close();

            _ws = null;

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
            _ws.Send(body.ToString());
            tcs.Task.Wait();

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
            OBSStreamStatus status;

            switch (eventType)
            {
                case "SwitchScenes":
                    if(OnSceneChange != null)
                        OnSceneChange(this, (string)body["scene-name"]);
                    break;

                case "ScenesChanged":
                    if (OnSceneListChange != null)
                        OnSceneListChange(this, EventArgs.Empty);
                    break;

                case "SourceOrderChanged":
                    if (OnSourceOrderChange != null)
                        OnSourceOrderChange(this, (string)body["scene-name"]);
                    break;

                case "SceneItemAdded":
                    if (OnSceneItemAdded != null)
                        OnSceneItemAdded(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneItemRemoved":
                    if (OnSceneItemRemoved != null)
                        OnSceneItemRemoved(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneItemVisibilityChanged":
                    if (OnSceneItemVisibilityChange != null)
                        OnSceneItemVisibilityChange(this, (string)body["scene-name"], (string)body["item-name"]);
                    break;

                case "SceneCollectionChanged":
                    if (OnSceneCollectionChange != null)
                        OnSceneCollectionChange(this, EventArgs.Empty);
                    break;

                case "SceneCollectionListChanged":
                    if (OnSceneCollectionListChange != null)
                        OnSceneCollectionListChange(this, EventArgs.Empty);
                    break;

                case "SwitchTransition":
                    if (OnTransitionChange != null)
                        OnTransitionChange(this, (string)body["transition-name"]);
                    break;

                case "TransitionDurationChanged":
                    if (OnTransitionDurationChange != null)
                        OnTransitionDurationChange(this, (int)body["new-duration"]);
                    break;

                case "TransitionListChanged":
                    if (OnTransitionListChange != null)
                        OnTransitionListChange(this, EventArgs.Empty);
                    break;

                case "TransitionBegin":
                    if (OnTransitionBegin != null)
                        OnTransitionBegin(this, EventArgs.Empty);
                    break;

                case "ProfileChanged":
                    if (OnProfileChange != null)
                        OnProfileChange(this, EventArgs.Empty);
                    break;

                case "ProfileListChanged":
                    if (OnProfileListChange != null)
                        OnProfileListChange(this, EventArgs.Empty);
                    break;

                case "StreamStarting":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputState.Starting);
                    break;

                case "StreamStarted":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputState.Started);
                    break;

                case "StreamStopping":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputState.Stopping);
                    break;

                case "StreamStopped":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputState.Stopped);
                    break;

                case "RecordingStarting":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputState.Starting);
                    break;

                case "RecordingStarted":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputState.Started);
                    break;

                case "RecordingStopping":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputState.Stopping);
                    break;

                case "RecordingStopped":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputState.Stopped);
                    break;

                case "StreamStatus":
                    if (OnStreamStatus != null)
                    {
                        status = new OBSStreamStatus(body);
                        OnStreamStatus(this, status);
                    }
                    break;

                case "Exiting":
                    OnExit(this, EventArgs.Empty);
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
