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
        protected delegate void RequestCallback(OBSWebsocket sender, JObject body);

        public SceneChangeCallback OnSceneChange;
        public EventHandler OnSceneListChange;
        public SourceOrderChangeCallback OnSourceOrderChange;
        public SceneItemUpdateCallback OnSceneItemAdded;
        public SceneItemUpdateCallback OnSceneItemRemoved;
        public SceneItemUpdateCallback OnSceneItemVisibilityChange;
        public EventHandler OnSceneCollectionChange;
        public EventHandler OnSceneCollectionListChange;
        public TransitionChangeCallback OnTransitionChange;
        public TransitionDurationChangeCallback OnTransitionDurationChange;
        public EventHandler OnTransitionListChange;
        public EventHandler OnTransitionBegin;
        public EventHandler OnProfileChange;
        public EventHandler OnProfileListChange;
        public OutputStateCallback OnStreamingStateChange;
        public OutputStateCallback OnRecordingStateChange;
        public StreamStatusCallback OnStreamStatus;
        public EventHandler OnExit;

        protected WebSocket _ws;
        protected Dictionary<string, TaskCompletionSource<JObject>> _responseHandlers;

        public OBSWebsocket()
        {
            _responseHandlers = new Dictionary<string, TaskCompletionSource<JObject>>();
        }

        public void Connect(string url, string password) {
            if (_ws != null && _ws.IsAlive)
            {
                Disconnect();
            }

            _ws = new WebSocket("ws://" + url);
            _ws.OnMessage += WSMessageHandler;

            _ws.Connect();

            OBSAuthInfo authInfo = GetAuthInfo();
            if (authInfo.AuthRequired)
            {
                Authenticate(password, authInfo);
            }
        }

        public void Disconnect()
        {
            if (_ws != null)
            {
                _ws.Close();
            }
            _ws = null;

            foreach (var cb in _responseHandlers)
            {
                var tcs = cb.Value;
                tcs.TrySetCanceled();
            }
        }

        protected void WSMessageHandler(object sender, MessageEventArgs e)
        {
            if (!e.IsText)
            {
                return;
            }

            JObject body = JObject.Parse(e.Data);

            if (body["message-id"] != null)
            {
                // Handle a request
                string msgID = (string)body["message-id"];
                TaskCompletionSource<JObject> _handler = _responseHandlers[msgID];

                if (_handler != null)
                {
                    _handler.SetResult(body);
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

        public JObject SendRequest(string requestType, JObject additionalFields = null) {
            string messageID = NewMessageID();

            var body = new JObject();
            body.Add(new JProperty("request-type", requestType));
            body.Add(new JProperty("message-id", messageID));

            if (additionalFields != null)
            {
                var mergeSettings = new JsonMergeSettings 
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                };

                body.Merge(additionalFields);
            }

            var tcs = new TaskCompletionSource<JObject>();
            _responseHandlers.Add(messageID, tcs);

            _ws.Send(body.ToString());
            tcs.Task.Wait();

            var result = tcs.Task.Result;
            if ((string)result["status"] == "error")
                throw new ArgumentException((string)result["message"]);

            return result;
        }

        public OBSVersion GetVersion()
        {
            JObject response = SendRequest("GetVersion");
            return new OBSVersion(response);
        }

        public OBSAuthInfo GetAuthInfo()
        {
            JObject response = SendRequest("GetAuthRequired");
            return new OBSAuthInfo(response);
        }

        public bool Authenticate(string password, OBSAuthInfo authInfo)
        {
            string secret = HashEncode(password + authInfo.PasswordSalt);
            string authResponse = HashEncode(secret + authInfo.Challenge);

            var requestFields = new JObject();
            requestFields.Add(new JProperty("auth", authResponse));

            // ArgumentException thrown here if auth fails
            SendRequest("Authenticate", requestFields);

            return true;
        }

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
                        OnStreamingStateChange(this, OutputStateUpdate.Starting);
                    break;

                case "StreamStarted":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputStateUpdate.Started);
                    break;

                case "StreamStopping":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputStateUpdate.Stopping);
                    break;

                case "StreamStopped":
                    if (OnStreamingStateChange != null)
                        OnStreamingStateChange(this, OutputStateUpdate.Stopped);
                    break;

                case "RecordingStarting":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputStateUpdate.Starting);
                    break;

                case "RecordingStarted":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputStateUpdate.Started);
                    break;

                case "RecordingStopping":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputStateUpdate.Stopping);
                    break;

                case "RecordingStopped":
                    if (OnRecordingStateChange != null)
                        OnRecordingStateChange(this, OutputStateUpdate.Stopped);
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

        protected string HashEncode(string input)
        {
            var sha256 = new SHA256Managed();

            byte[] textBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha256.ComputeHash(textBytes);

            return System.Convert.ToBase64String(hash);
        }

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
