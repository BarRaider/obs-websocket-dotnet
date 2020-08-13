﻿/*
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
using WebSocket4Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using OBSWebsocketDotNet.Types;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket
    {

        protected static JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings()
        {
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore
        };

        #region Events
        /// <summary>
        /// Triggered when switching to another scene
        /// </summary>
        public event SceneChangeCallback? SceneChanged;

        /// <summary>
        /// Triggered when a scene is created, deleted or renamed
        /// </summary>
        public event EventHandler? SceneListChanged;

        /// <summary>
        /// Triggered when the scene item list of the specified scene is reordered
        /// </summary>
        public event SourceOrderChangeCallback? SourceOrderChanged;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        public event SceneItemUpdateCallback? SceneItemAdded;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        public event SceneItemUpdateCallback? SceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        public event SceneItemVisibilityChangedCallback? SceneItemVisibilityChanged;

        /// <summary>
        /// Triggered when switching to another scene collection
        /// </summary>
        public event EventHandler? SceneCollectionChanged;

        /// <summary>
        /// Triggered when a scene collection is created, deleted or renamed
        /// </summary>
        public event EventHandler? SceneCollectionListChanged;

        /// <summary>
        /// Triggered when switching to another transition
        /// </summary>
        public event TransitionChangeCallback? TransitionChanged;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        public event TransitionDurationChangeCallback? TransitionDurationChanged;

        /// <summary>
        /// Triggered when a transition is created or removed
        /// </summary>
        public event EventHandler? TransitionListChanged;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="SceneChanged"/>
        /// </summary>
        public event EventHandler? TransitionBegin;

        /// <summary>
        /// Triggered when switching to another profile
        /// </summary>
        public event EventHandler? ProfileChanged;

        /// <summary>
        /// Triggered when a profile is created, imported, removed or renamed
        /// </summary>
        public event EventHandler? ProfileListChanged;

        /// <summary>
        /// Triggered when the streaming output state changes
        /// </summary>
        public event OutputStateCallback? StreamingStateChanged;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        public event OutputStateCallback? RecordingStateChanged;

        /// <summary>
        /// Triggered when state of the replay buffer changes
        /// </summary>
        public event OutputStateCallback? ReplayBufferStateChanged;

        /// <summary>
        /// Triggered every 2 seconds while streaming is active
        /// </summary>
        public event StreamStatusCallback? StreamStatus;

        /// <summary>
        /// Triggered when the preview scene selection changes (Studio Mode only)
        /// </summary>
        public event SceneChangeCallback? PreviewSceneChanged;

        /// <summary>
        /// Triggered when Studio Mode is turned on or off
        /// </summary>
        public event StudioModeChangeCallback? StudioModeSwitched;

        /// <summary>
        /// Triggered when OBS exits
        /// </summary>
        public event EventHandler? OBSExit;

        /// <summary>
        /// Triggered when connected successfully to an obs-websocket server
        /// </summary>
        public event EventHandler? Connected;

        /// <summary>
        /// Triggered when disconnected from an obs-websocket server
        /// </summary>
        public event EventHandler? Disconnected;

        /// <summary>
        /// Emitted every 2 seconds after enabling it by calling SetHeartbeat
        /// </summary>
        public event HeartBeatCallback? Heartbeat;

        /// <summary>
        /// A scene item is deselected
        /// </summary>
        public event SceneItemDeselectedCallback? SceneItemDeselected;

        /// <summary>
        /// A scene item is selected
        /// </summary>
        public event SceneItemSelectedCallback? SceneItemSelected;

        /// <summary>
        /// A scene item transform has changed
        /// </summary>
        public event SceneItemTransformCallback? SceneItemTransformChanged;

        /// <summary>
        /// Audio mixer routing changed on a source
        /// </summary>
        public event SourceAudioMixersChangedCallback? SourceAudioMixersChanged;

        /// <summary>
        /// The audio sync offset of a source has changed
        /// </summary>
        public event SourceAudioSyncOffsetCallback? SourceAudioSyncOffsetChanged;

        /// <summary>
        /// A source has been created. A source can be an input, a scene or a transition.
        /// </summary>
        public event SourceCreatedCallback? SourceCreated;

        /// <summary>
        /// A source has been destroyed/removed. A source can be an input, a scene or a transition.
        /// </summary>
        public event SourceDestroyedCallback? SourceDestroyed;

        /// <summary>
        /// A filter was added to a source
        /// </summary>
        public event SourceFilterAddedCallback? SourceFilterAdded;

        /// <summary>
        /// A filter was removed from a source
        /// </summary>
        public event SourceFilterRemovedCallback? SourceFilterRemoved;

        /// <summary>
        /// Filters in a source have been reordered
        /// </summary>
        public event SourceFiltersReorderedCallback? SourceFiltersReordered;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event SourceMuteStateChangedCallback? SourceMuteStateChanged;

        /// <summary>
        /// A source has been renamed
        /// </summary>
        public event SourceRenamedCallback? SourceRenamed;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        public event SourceVolumeChangedCallback? SourceVolumeChanged;

        #endregion

        // Random should never be created inside a function
        private static readonly Random random = new Random();

        /// <summary>
        /// Current connection state
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (WSConnection != null && CheckConnection(WSConnection));
            }
        }

        protected static bool CheckConnection(WebSocket connection) => connection.State == WebSocketState.Open;

        public string? ConnectionUrl { get; protected set; }

        /// <summary>
        /// Underlying WebSocket connection to an obs-websocket server. Value is null when disconnected.
        /// </summary>
        protected WebSocket? WSConnection { get; private set; }

        private delegate void RequestCallback(OBSWebsocket sender, JObject body);
        protected ConcurrentDictionary<string, TaskCompletionSource<JObject>> _responseHandlers;
        protected TaskCompletionSource<bool>? ConnectingTaskSource;

        public OBSWebsocket()
        {
            _responseHandlers = new ConcurrentDictionary<string, TaskCompletionSource<JObject>>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OBSWebsocket(string url)
            : this()
        {
            WSConnection = new WebSocket(url);
            SetEvents(WSConnection);
        }

        /// <summary>
        /// Connect and authenticate (if needed) with the specified password
        /// </summary>
        /// <param name="password">Server password</param>
        public Task<bool> Connect(string? password = null) => Connect(password, CancellationToken.None);

        /// <summary>
        /// Connect and authenticate (if needed) with the specified password
        /// </summary>
        /// <param name="password">Server password</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="AuthFailureException"></exception>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="SocketErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<bool> Connect(string? password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (WSConnection != null
                && (WSConnection.State == WebSocketState.Open
                    || WSConnection.State == WebSocketState.Connecting))
                Disconnect();

            TaskCompletionSource<bool>? tcs = ConnectingTaskSource;
            ConnectingTaskSource = null;
            if (tcs != null)
            {
                tcs.TrySetCanceled();
            }
            tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(() =>
            {
                tcs.TrySetCanceled(cancellationToken);
            });
            ConnectingTaskSource = tcs;
            if (WSConnection == null) throw new InvalidOperationException("A URL has not been specified for the connection.");
            WSConnection.Open();
            bool connected = await tcs.Task.ConfigureAwait(false); // Will throw exception if error occurs
            if (!connected)
                return false;
            ConnectingTaskSource = null;
            OBSAuthInfo authInfo = await GetAuthInfo().ConfigureAwait(false);

            if (authInfo.AuthRequired)
            {
                try
                {
                    await Authenticate(password ?? string.Empty, authInfo).ConfigureAwait(false);
                }
                catch (AuthFailureException)
                {
                    WSConnection.Close(1008, "Authentication failed, incorrect password.");
                    throw;
                }
            }
            Connected?.Invoke(this, null);
            return true;
        }

        public Task<bool> Connect(string url, string password) => Connect(url, password, CancellationToken.None);

        /// <summary>
        /// Connect to the given URL and authenticate (if needed) with the specified password
        /// </summary>
        /// <param name="url"></param>
        /// <param name="password">Server password</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"
        /// <exception cref="AuthFailureException"></exception>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="SocketErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<bool> Connect(string url, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "url cannot be null in Connect.");
            if (ConnectionUrl != url)
            {
                Disconnect();
                WebSocket? connection = WSConnection;
                WSConnection = null;
                if (connection != null)
                {
                    RemoveEvents(connection);
                    connection?.Dispose();
                }
                connection = new WebSocket(url);
                WSConnection = connection;
                SetEvents(connection);
            }
            return Connect(password, cancellationToken);
        }

        protected void SetEvents(WebSocket connection)
        {
            RemoveEvents(connection);
            connection.MessageReceived += WebsocketMessageHandler;
            connection.Opened += OnConnectionOpened;
            connection.Closed += OnConnectionClosed;
            connection.Error += OnConnectionError;
        }

        protected void RemoveEvents(WebSocket connection)
        {
            connection.MessageReceived -= WebsocketMessageHandler;
            connection.Opened -= OnConnectionOpened;
            connection.Closed -= OnConnectionClosed;
            connection.Error -= OnConnectionError;
        }

        protected void OnConnectionOpened(object sender, EventArgs e)
        {
            TaskCompletionSource<bool>? connectingTcs = ConnectingTaskSource;
            if (connectingTcs != null)
                connectingTcs.TrySetResult(true);
        }

        protected void OnConnectionClosed(object sender, EventArgs e)
        {
            TaskCompletionSource<bool>? connectingTcs = ConnectingTaskSource;
            if (connectingTcs != null)
                connectingTcs.TrySetResult(false);
            EventHandler? disconnectHandler = Disconnected;
            disconnectHandler?.Invoke(this, e);
            CancelAllHandlers();
        }

        protected void OnConnectionError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (e?.Exception != null)
                OBSLogger.Error(e.Exception);
            else
                OBSLogger.Error("Unknown error in WebSocket connection.");
            TaskCompletionSource<bool>? connectingTcs = ConnectingTaskSource;
            ConnectingTaskSource = null;
            if (connectingTcs != null)
            {
                if (e?.Exception == null)
                    connectingTcs.TrySetResult(false);
                else
                {
                    Exception exception = e.Exception;
                    if (exception is SocketException socketException)
                        connectingTcs.TrySetException(new SocketErrorResponseException(e.Exception.Message, socketException));
                    else
                        connectingTcs.TrySetException(new ErrorResponseException(e.Exception.Message, e.Exception));
                }
            }
            CancelAllHandlers(e?.Exception);
        }

        /// <summary>
        /// Disconnect this instance from the server
        /// </summary>
        public void Disconnect()
        {
            WebSocket? connection = WSConnection;
            if (connection != null
                && (connection.State == WebSocketState.Open
                    || connection.State == WebSocketState.Connecting))
            {
                connection.Close();
            }
            CancelAllHandlers();
        }

        protected void CancelAllHandlers(Exception? exception = null)
        {
            var unusedHandlers = _responseHandlers.ToArray();
            _responseHandlers.Clear();
            foreach (var pair in unusedHandlers)
            {
                if (exception != null)
                {
                    pair.Value.TrySetException(new ErrorResponseException(exception.Message, exception));
                }
                else
                    pair.Value.TrySetCanceled();
            }
        }



        // This callback handles incoming JSON messages and determines if it's
        // a request response or an event ("Update" in obs-websocket terminology)
        private void WebsocketMessageHandler(object sender, MessageReceivedEventArgs e)
        {
            JObject body;
            try
            {
                body = JObject.Parse(e.Message);
            }
            catch (Exception ex)
            {
                OBSLogger.Error($"Error parsing received message: {ex.Message}.");
                OBSLogger.Debug($"Invalid message: {e.Message}");
                return;
            }
            string? msgID = (string?)body["message-id"];
            string? eventType = body["update-type"]?.ToString();
            if (msgID != null)
            {
                if (_responseHandlers.TryRemove(msgID, out TaskCompletionSource<JObject> handler))
                {
                    // Set the response body as Result and notify the request sender
                    handler.SetResult(body);
                }
                else
                {
                    OBSLogger.Debug($"No handler for message-id. body is {body.ToString(Formatting.Indented)}");
                }
            }
            else if (eventType != null)
            {
                // Handle an event
                ProcessEventType(eventType, body);
            }
        }

        /// <summary>
        /// Sends a message to the websocket API with the specified request type and optional parameters
        /// </summary>
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="additionalFields">additional JSON fields if required by the request type</param>
        /// <returns>The server's JSON response as a JObject</returns>
        public async Task<JObject> SendRequest(string requestType, JObject? additionalFields = null)
        {
            string messageID;

            // Build the bare-minimum body for a request
            var body = new JObject
            {
                { "request-type", requestType }
            };

            // Add optional fields if provided
            if (additionalFields != null)
            {
                var mergeSettings = new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                };

                body.Merge(additionalFields, mergeSettings);
            }
            WebSocket? connection = WSConnection;
            if (connection == null || !CheckConnection(connection))
            {
                throw new ErrorResponseException("Not connected to OBS");
            }

            // Prepare the asynchronous response handler
            var tcs = new TaskCompletionSource<JObject>();
            do
            {
                // Generate a random message id
                messageID = NewMessageID();
                if (_responseHandlers.TryAdd(messageID, tcs))
                {
                    body.Add("message-id", messageID);
                    break;
                }
                // Message id already exists, retry with a new one.
            } while (true);
            // Send the message and wait for a response
            // (received and notified by the websocket response handler)

            connection.Send(body.ToString());
            JObject result;
            try
            {
                result = await tcs.Task.ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                throw new ErrorResponseException("Request canceled");
            }


            // Throw an exception if the server returned an error.
            // An error occurs if authentication fails or one if the request body is invalid.
            if ((string?)result["status"] == "error")
                throw new ErrorResponseException((string?)result["error"] ?? "Response indicated an error.", result);

            return result;
        }

        /// <summary>
        /// Requests version info regarding obs-websocket, the API and OBS Studio
        /// </summary>
        /// <returns>Version info in an <see cref="OBSVersion"/> object</returns>
        public async Task<OBSVersion> GetVersion()
        {
            JObject response = await SendRequest("GetVersion").ConfigureAwait(false);
            return new OBSVersion(response);
        }

        /// <summary>
        /// Request authentication data. You don't have to call this manually.
        /// </summary>
        /// <returns>Authentication data in an <see cref="OBSAuthInfo"/> object</returns>
        public async Task<OBSAuthInfo> GetAuthInfo()
        {
            JObject response = await SendRequest("GetAuthRequired").ConfigureAwait(false);
            return new OBSAuthInfo(response);
        }

        /// <summary>
        /// Authenticates to the Websocket server using the challenge and salt given in the passed <see cref="OBSAuthInfo"/> object
        /// </summary>
        /// <param name="password">User password</param>
        /// <param name="authInfo">Authentication data</param>
        /// <returns>true if authentication succeeds</returns>
        /// <exception cref="AuthFailureException">Thrown if authentication fails.</exception>
        public async Task<bool> Authenticate(string password, OBSAuthInfo authInfo)
        {
            string secret = HashEncode(password + authInfo.PasswordSalt);
            string authResponse = HashEncode(secret + authInfo.Challenge);

            var requestFields = new JObject
            {
                { "auth", authResponse }
            };

            try
            {
                // Throws ErrorResponseException if auth fails
                await SendRequest("Authenticate", requestFields).ConfigureAwait(false);
            }
            catch (ErrorResponseException)
            {
                throw new AuthFailureException("Authentication failed.");
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
                    SceneChanged?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty);
                    break;

                case "ScenesChanged":
                    SceneListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "SourceOrderChanged":
                    SourceOrderChanged?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty); // TODO: More info available.
                    break;

                case "SceneItemAdded":
                    SceneItemAdded?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty,
                        (string?)body["item-name"] ?? string.Empty); // TODO: int 'item-id' available.
                    break;

                case "SceneItemRemoved":
                    SceneItemRemoved?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty,
                        (string?)body["item-name"] ?? string.Empty);
                    break;

                case "SceneItemVisibilityChanged":
                    SceneItemVisibilityChanged?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty,
                        (string?)body["item-name"] ?? string.Empty,
                        (bool?)body["item-visible"] ?? false);
                    break;

                case "SceneCollectionChanged":
                    SceneCollectionChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "SceneCollectionListChanged":
                    SceneCollectionListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "SwitchTransition":
                    TransitionChanged?.Invoke(this,
                        (string?)body["transition-name"] ?? string.Empty);
                    break;

                case "TransitionDurationChanged":
                    TransitionDurationChanged?.Invoke(this,
                        (int?)body["new-duration"] ?? -1);
                    break;

                case "TransitionListChanged":
                    TransitionListChanged?.Invoke(this, EventArgs.Empty);
                    break;

                case "TransitionBegin":
                    TransitionBegin?.Invoke(this, EventArgs.Empty); // TODO: More info available.
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
                    StreamingStateChanged?.Invoke(this, OutputState.Stopping); // TODO: Undocumented 'stream-timecode' available. (TimeSpan?)
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
                    RecordingStateChanged?.Invoke(this, OutputState.Stopping); // TODO: Undocumented 'rec-timecode' available. (TimeSpan? ex: '00:00:02.183')
                    break;

                case "RecordingStopped":
                    RecordingStateChanged?.Invoke(this, OutputState.Stopped);
                    break;

                case "RecordingPaused":
                    RecordingStateChanged?.Invoke(this, OutputState.Paused); // TODO: Undocumented 'rec-timecode' available. (TimeSpan?)
                    break;

                case "RecordingResumed":
                    RecordingStateChanged?.Invoke(this, OutputState.Resumed); // TODO: Undocumented 'rec-timecode' available. (TimeSpan?)
                    break;

                case "StreamStatus":
                    if (StreamStatus != null)
                    {
                        status = new StreamStatus(body);
                        StreamStatus(this, status);
                    }
                    break;

                case "PreviewSceneChanged":
                    PreviewSceneChanged?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty); // TODO: 'sources' Array<SceneItem> available.
                    break;

                case "StudioModeSwitched":
                    StudioModeSwitched?.Invoke(this,
                        (bool?)body["new-state"] ?? false);
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
                    SceneItemDeselected?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty,
                        (string?)body["item-name"] ?? string.Empty,
                        (string?)body["item-id"] ?? string.Empty);
                    break;
                case "SceneItemSelected":
                    SceneItemSelected?.Invoke(this,
                        (string?)body["scene-name"] ?? string.Empty,
                        (string?)body["item-name"] ?? string.Empty,
                        (string?)body["item-id"] ?? string.Empty);
                    break;
                case "SceneItemTransformChanged":
                    SceneItemTransformChanged?.Invoke(this, new SceneItemTransformInfo(body));
                    break;
                case "SourceAudioMixersChanged":
                    try
                    {
                        AudioMixersChangedInfo? info = body?.ToObject<AudioMixersChangedInfo>();
                        if (info != null)
                            SourceAudioMixersChanged?.Invoke(this, info);
                        else
                            OBSLogger.Warning($"Received '' event, but associated data was missing.");
                    }
                    catch (Exception ex)
                    {
                        OBSLogger.Error(ex);
                    }
                    break;
                case "SourceAudioSyncOffsetChanged":
                    SourceAudioSyncOffsetChanged?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        (int?)body["syncOffset"] ?? 0);
                    break;
                case "SourceCreated":
                    try
                    {
                        SourceSettings settings = body.ToObject<SourceSettings>() 
                            ?? throw new ErrorResponseException("Could not parse body from event 'SourceCreated'.", body);

                        SourceCreated?.Invoke(this, settings);
                    }
                    catch (JsonException ex)
                    {
                        throw new ErrorResponseException("Could not parse body from event 'SourceCreated'.", body, ex);
                    }
                    break;
                case "SourceDestroyed":
                    SourceDestroyed?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        (string?)body["sourceType"] ?? string.Empty,
                        (string?)body["sourceKind"] ?? string.Empty);
                    break;
                case "SourceRenamed":
                    SourceRenamed?.Invoke(this,
                        (string?)body["newName"] ?? string.Empty,
                        (string?)body["previousName"] ?? string.Empty);
                    break;

                case "SourceMuteStateChanged":
                    SourceMuteStateChanged?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        (bool?)body["muted"] ?? false);
                    break;
                case "SourceVolumeChanged":
                    SourceVolumeChanged?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        (float?)body["volume"] ?? -1f);
                    break;
                case "SourceFilterAdded":
                    SourceFilterAdded?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        (string?)body["filterName"] ?? string.Empty,
                        (string?)body["filterType"] ?? string.Empty,
                        (JObject?)body["filterSettings"]);
                    break;
                case "SourceFilterRemoved":
                    SourceFilterRemoved?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        (string?)body["filterName"] ?? string.Empty);
                    break;
                case "SourceFiltersReordered":
                    List<FilterReorderItem> filters = new List<FilterReorderItem>();
                    string? filtersStr = body["filters"]?.ToString();
                    if (filtersStr != null)
                        JsonConvert.PopulateObject(filtersStr, filters);

                    SourceFiltersReordered?.Invoke(this,
                        (string?)body["sourceName"] ?? string.Empty,
                        filters);
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
        protected static string HashEncode(string input)
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
        protected static string NewMessageID(int length = 16)
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
    }
}
