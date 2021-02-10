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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;
using System.Diagnostics;

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket
    {
        /// <summary>
        /// Default <see cref="JsonSerializerSettings"/>.
        /// </summary>
        protected static JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings()
        {
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore
        };

        #region Events

        /// <summary>
        /// Exceptions thrown that are not passed up to the caller will be passed through this event.
        /// </summary>
        public event EventHandler<OBSErrorEventArgs>? OBSError;

        /// <summary>
        /// Raised when a request is sent.
        /// </summary>
        public event EventHandler<RequestData>? RequestSent;

        /// <summary>
        /// Raised when any "update-type" event is received.
        /// </summary>
        public event EventHandler<JObject>? EventReceived;

        /// <summary>
        /// Raised when a request's response is received.
        /// </summary>
        public event EventHandler<JObject>? ResponseReceived;

        /// <summary>
        /// Triggered when switching to another scene
        /// </summary>
        public event EventHandler<SceneChangeEventArgs>? SceneChanged;

        /// <summary>
        /// Triggered when a scene is created, deleted or renamed
        /// </summary>
        public event EventHandler? SceneListChanged;

        /// <summary>
        /// Scene items within a scene have been reordered.
        /// </summary>
        public event EventHandler<SourceOrderChangedEventArgs>? SourceOrderChanged;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        public event EventHandler<SceneItemUpdatedEventArgs>? SceneItemAdded;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        public event EventHandler<SceneItemUpdatedEventArgs>? SceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        public event EventHandler<SceneItemVisibilityChangedEventArgs>? SceneItemVisibilityChanged;

        /// <summary>
        /// Triggered when the lock status of a scene item changes
        /// </summary>
        public event EventHandler<SceneItemLockChangedEventArgs>? SceneItemLockChanged;

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
        public event EventHandler<TransitionChangeEventArgs>? TransitionChanged;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        public event EventHandler<TransitionDurationChangeEventArgs>? TransitionDurationChanged;

        /// <summary>
        /// Triggered when a transition is created or removed
        /// </summary>
        public event EventHandler? TransitionListChanged;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="SceneChanged"/>
        /// </summary>
        public event EventHandler<TransitionBeginEventArgs>? TransitionBegin;

        /// <summary>
        /// A transition (other than "cut") has ended. Added in v4.8.0
        /// </summary>
        public event EventHandler<TransitionEndEventArgs>? TransitionEnd;

        /// <summary>
        /// A stinger transition has finished playing its video. Added in v4.8.0
        /// </summary>
        public event EventHandler<TransitionVideoEndEventArgs>? TransitionVideoEnd;

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
        public event EventHandler<OutputStateChangedEventArgs>? StreamingStateChanged;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        public event EventHandler<OutputStateChangedEventArgs>? RecordingStateChanged;

        /// <summary>
        /// Triggered when state of the replay buffer changes
        /// </summary>
        public event EventHandler<OutputStateChangedEventArgs>? ReplayBufferStateChanged;

        /// <summary>
        /// Triggered every 2 seconds while streaming is active
        /// </summary>
        public event EventHandler<StreamStatusEventArgs>? StreamStatus;

        /// <summary>
        /// Triggered when the preview scene selection changes (Studio Mode only)
        /// </summary>
        public event EventHandler<SceneChangeEventArgs>? PreviewSceneChanged;

        /// <summary>
        /// Triggered when Studio Mode is turned on or off
        /// </summary>
        public event EventHandler<StudioModeChangeEventArgs>? StudioModeSwitched;

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
        public event EventHandler<HeartBeatEventArgs>? Heartbeat;

        /// <summary>
        /// A scene item is deselected
        /// </summary>
        public event EventHandler<SceneItemSelectionEventArgs>? SceneItemDeselected;

        /// <summary>
        /// A scene item is selected
        /// </summary>
        public event EventHandler<SceneItemSelectionEventArgs>? SceneItemSelected;

        /// <summary>
        /// A scene item transform has changed
        /// </summary>
        public event EventHandler<SceneItemTransformEventArgs>? SceneItemTransformChanged;

        /// <summary>
        /// Audio mixer routing changed on a source
        /// </summary>
        public event EventHandler<SourceAudioMixersChangedEventArgs>? SourceAudioMixersChanged;

        /// <summary>
        /// The audio sync offset of a source has changed
        /// </summary>
        public event EventHandler<SourceAudioSyncOffsetEventArgs>? SourceAudioSyncOffsetChanged;

        /// <summary>
        /// A source has been created. A source can be an input, a scene or a transition.
        /// </summary>
        public event EventHandler<SourceCreatedEventArgs>? SourceCreated;

        /// <summary>
        /// A source has been destroyed/removed. A source can be an input, a scene or a transition.
        /// </summary>
        public event EventHandler<SourceDestroyedEventArgs>? SourceDestroyed;

        /// <summary>
        /// A filter was added to a source
        /// </summary>
        public event EventHandler<SourceFilterAddedEventArgs>? SourceFilterAdded;

        /// <summary>
        /// A filter was removed from a source
        /// </summary>
        public event EventHandler<SourceFilterRemovedEventArgs>? SourceFilterRemoved;

        /// <summary>
        /// Filters in a source have been reordered
        /// </summary>
        public event EventHandler<SourceFiltersReorderedEventArgs>? SourceFiltersReordered;

        /// <summary>
        /// Triggered when the visibility of a filter has changed
        /// </summary>
        public event EventHandler<SourceFilterVisibilityChangedEventArgs>? SourceFilterVisibilityChanged;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event EventHandler<SourceMuteStateChangedEventArgs>? SourceMuteStateChanged;

        /// <summary>
        /// A source has been renamed
        /// </summary>
        public event EventHandler<SourceRenamedEventArgs>? SourceRenamed;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        public event EventHandler<SourceVolumeChangedEventArgs>? SourceVolumeChanged;

        /// <summary>
        /// An event was received that obs-websocket-dotnet does not have a defined event handler for.
        /// </summary>
        public event EventHandler<JObject>? UnhandledEvent;
        /// <summary>
        /// A custom broadcast message was received
        /// </summary>
        public event EventHandler<BroadcastCustomMessageReceivedEventArgs>? BroadcastCustomMessageReceived;

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

        /// <summary>
        /// Returns true if the connection is open.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        protected static bool CheckConnection(WebSocket connection) => connection.State == WebSocketState.Open;

        /// <summary>
        /// URL used to connect to the server.
        /// </summary>
        public string? ConnectionUrl { get; protected set; }

        /// <summary>
        /// Underlying WebSocket connection to an obs-websocket server. Value is null when disconnected.
        /// </summary>
        protected WebSocket? WSConnection { get; private set; }

        private delegate void RequestCallback(OBSWebsocket sender, JObject body);
        /// <summary>
        /// Dictionary of response handlers waiting for a response.
        /// </summary>
        protected ConcurrentDictionary<string, TaskCompletionSource<JObject>> responseHandlers;
        /// <summary>
        /// <see cref="TaskCompletionSource{TResult}"/> used to wait for a connection.
        /// </summary>
        protected TaskCompletionSource<bool>? ConnectingTaskSource;

        /// <summary>
        /// Creates a new <see cref="OBSWebsocket"/>. A URL must be set before connecting.
        /// </summary>
        public OBSWebsocket()
        {
            responseHandlers = new ConcurrentDictionary<string, TaskCompletionSource<JObject>>();
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
        public async Task<bool> Connect(string? password, CancellationToken cancellationToken = default)
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
            using CancellationTokenRegistration cancelRegistration = cancellationToken.Register(() =>
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

        /// <summary>
        /// Connect to the given URL and authenticate (if needed) with the specified password
        /// </summary>
        /// <param name="url"></param>
        /// <param name="password">Server password</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AuthFailureException"></exception>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="SocketErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<bool> Connect(string url, string? password, CancellationToken cancellationToken = default)
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

        /// <summary>
        /// Sets events used by obs-websocket-dotnet on a <see cref="WebSocket"/> connection.
        /// </summary>
        /// <param name="connection"></param>
        protected void SetEvents(WebSocket connection)
        {
            RemoveEvents(connection);
            connection.MessageReceived += WebsocketMessageHandler;
            connection.Opened += OnConnectionOpened;
            connection.Closed += OnConnectionClosed;
            connection.Error += OnConnectionError;
        }

        /// <summary>
        /// Removes events used by obs-websocket-dotnet on a <see cref="WebSocket"/> connection.
        /// </summary>
        /// <param name="connection"></param>
        protected void RemoveEvents(WebSocket connection)
        {
            connection.MessageReceived -= WebsocketMessageHandler;
            connection.Opened -= OnConnectionOpened;
            connection.Closed -= OnConnectionClosed;
            connection.Error -= OnConnectionError;
        }

        private void OnConnectionOpened(object sender, EventArgs e)
        {
            TaskCompletionSource<bool>? connectingTcs = ConnectingTaskSource;
            if (connectingTcs != null)
                connectingTcs.TrySetResult(true);
        }

        private void OnConnectionClosed(object sender, EventArgs e)
        {
            TaskCompletionSource<bool>? connectingTcs = ConnectingTaskSource;
            if (connectingTcs != null)
                connectingTcs.TrySetResult(false);
            EventHandler? disconnectHandler = Disconnected;
            disconnectHandler?.Invoke(this, e);
            CancelAllHandlers();
        }

        private void OnConnectionError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (e?.Exception != null)
            {
                OBSError?.Invoke(this, new OBSErrorEventArgs($"WebSocket connection error: {e.Exception.Message}", e.Exception));
            }
            else
                OBSError?.Invoke(this, new OBSErrorEventArgs("Unknown error in WebSocket connection."));
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

        /// <summary>
        /// Cancels all waiting response handlers. Optionally, sets an <see cref="Exception"/> on them.
        /// </summary>
        /// <param name="exception"></param>
        protected void CancelAllHandlers(Exception? exception = null)
        {
            KeyValuePair<string, TaskCompletionSource<JObject>>[]? unusedHandlers = responseHandlers.ToArray();
            responseHandlers.Clear();
            foreach (KeyValuePair<string, TaskCompletionSource<JObject>> pair in unusedHandlers)
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
#pragma warning disable CA1031 // Do not catch general exception types
        private void WebsocketMessageHandler(object sender, MessageReceivedEventArgs e)
        {
            JObject? body = null;
            try
            {
                body = JObject.Parse(e.Message);
            }
            catch (Exception ex)
            {
                OBSError?.Invoke(this, new OBSErrorEventArgs($"Error parsing received message: {ex.Message}.", ex, body));
                OBSLogger.Debug($"Invalid message: {e.Message}");
                return;
            }
            string? msgID = (string?)body["message-id"];
            string? eventType = body["update-type"]?.ToString();
            if (msgID != null && msgID.Length > 0)
            {
                // Handle a request :
                // Find the response handler based on
                // its associated message ID

                if (responseHandlers.TryRemove(msgID, out TaskCompletionSource<JObject> handler))
                {
                    // Set the response body as Result and notify the request sender
                    handler.SetResult(body);
                    try
                    {
                        ResponseReceived?.Invoke(this, body);
                    }
                    catch (Exception ex)
                    {
                        OBSLogger.Error($"Error in {nameof(ResponseReceived)} handler: {ex.Message}");
                        OBSLogger.Debug(ex);
                    }
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
#pragma warning restore CA1031 // Do not catch general exception types

        /// <summary>
        /// Sends a message to the websocket API with the specified request type.
        /// </summary>
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server's JSON response as a JObject</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public Task<JObject> SendRequest(string requestType, CancellationToken cancellationToken) => SendRequest(requestType, null, cancellationToken);
        /// <summary>
        /// Sends a message to the websocket API with the specified request type and optional parameters
        /// </summary>
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="additionalFields">additional JSON fields if required by the request type</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server's JSON response as a JObject</returns>
        /// <exception cref="ErrorResponseException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<JObject> SendRequest(string requestType, JObject? additionalFields, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            string messageID;

            // Build the bare-minimum body for a request
            JObject? body = new JObject
            {
                { "request-type", requestType }
            };

            // Add optional fields if provided
            if (additionalFields != null)
            {
                JsonMergeSettings? mergeSettings = new JsonMergeSettings
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
            TaskCompletionSource<JObject>? tcs = new TaskCompletionSource<JObject>();
            using CancellationTokenRegistration tokenRegistration = cancellationToken.Register(() => tcs.TrySetCanceled(cancellationToken));
            do
            {
                // Generate a random message id
                messageID = NewMessageID();
                if (responseHandlers.TryAdd(messageID, tcs))
                {
                    body.Add("message-id", messageID);
                    break;
                }
                // Message id already exists, retry with a new one.
            } while (true);
            // Send the message and wait for a response
            // (received and notified by the websocket response handler)

            connection.Send(body.ToString());
            RequestSent?.Invoke(this, new RequestData(requestType, messageID, body));
            JObject result;

            result = await tcs.Task.ConfigureAwait(false);

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
        public async Task<OBSVersion> GetVersion(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetVersion", cancellationToken).ConfigureAwait(false);
            return new OBSVersion(response);
        }

        /// <summary>
        /// Request authentication data. You don't have to call this manually.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Authentication data in an <see cref="OBSAuthInfo"/> object</returns>
        /// <exception cref="ErrorResponseException"></exception>
        public async Task<OBSAuthInfo> GetAuthInfo(CancellationToken cancellationToken = default)
        {
            JObject response = await SendRequest("GetAuthRequired", cancellationToken).ConfigureAwait(false);
            try
            {
                OBSAuthInfo info = response?.ToObject<OBSAuthInfo>() ?? throw new ErrorResponseException($"Invalid response for 'GetAuthRequired'.", response);
                return info;
            }
            catch (JsonException ex)
            {
                throw new ErrorResponseException($"Invalid response for 'GetAuthRequired': {ex.Message}", response, ex);
            }
        }

        /// <summary>
        /// Authenticates to the Websocket server using the challenge and salt given in the passed <see cref="OBSAuthInfo"/> object
        /// </summary>
        /// <param name="password">User password</param>
        /// <param name="authInfo">Authentication data</param>
        /// <param name="cancellationToken"></param>
        /// <returns>true if authentication succeeds</returns>
        /// <exception cref="AuthFailureException">Thrown if authentication fails.</exception>
        public async Task<bool> Authenticate(string password, OBSAuthInfo authInfo, CancellationToken cancellationToken = default)
        {
            string secret = HashEncode(password + authInfo.PasswordSalt);
            string authResponse = HashEncode(secret + authInfo.Challenge);

            JObject? requestFields = new JObject
            {
                { "auth", authResponse }
            };

            try
            {
                // Throws ErrorResponseException if auth fails
                await SendRequest("Authenticate", requestFields, cancellationToken).ConfigureAwait(false);
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
            try
            {
                EventReceived?.Invoke(this, body);
            }
            catch (Exception ex)
            {
                OBSError?.Invoke(this, new OBSErrorEventArgs($"Error invoking 'OnEvent' event.", ex, body));
            }
            try
            {
                switch (eventType)
                {
                    case "SwitchScenes":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneChangeEventArgs args))
                                SceneChanged?.Invoke(this, args);
                            break;
                        }
                    case "ScenesChanged":
                        SceneListChanged?.Invoke(this, EventArgs.Empty);
                        break;
                    case "SourceOrderChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceOrderChangedEventArgs args))
                                SourceOrderChanged?.Invoke(this, args);
                            break;
                        }
                    case "SceneItemAdded":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemUpdatedEventArgs args))
                            {
                                args.ChangeType = SceneItemChangeType.Added;
                                SceneItemAdded?.Invoke(this, args);
                            }
                            break;
                        }
                    case "SceneItemRemoved":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemUpdatedEventArgs args))
                            {
                                args.ChangeType = SceneItemChangeType.Removed;
                                SceneItemRemoved?.Invoke(this, args);
                            }
                            break;
                        }
                    case "SceneItemVisibilityChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemVisibilityChangedEventArgs args))
                                SceneItemVisibilityChanged?.Invoke(this, args);
                            break;
                        }
                    case "SceneItemLockChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemLockChangedEventArgs args))
                                SceneItemLockChanged?.Invoke(this, args);
                            break;
                        }
                    case "SceneCollectionChanged":
                        {
                            SceneCollectionChanged?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    case "SceneCollectionListChanged":
                        {
                            SceneCollectionListChanged?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    case "SwitchTransition":
                        {
                            if (TryCreateEventArgs(eventType, body, out TransitionChangeEventArgs args))
                                TransitionChanged?.Invoke(this, args);
                            break;
                        }
                    case "TransitionDurationChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out TransitionDurationChangeEventArgs args))
                                TransitionDurationChanged?.Invoke(this, args);
                            break;
                        }
                    case "TransitionListChanged":
                        {
                            TransitionListChanged?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    case "TransitionBegin":
                        {
                            if (TryCreateEventArgs(eventType, body, out TransitionBeginEventArgs args))
                                TransitionBegin?.Invoke(this, args);
                            break;
                        }
                    case "TransitionEnd":
                        {
                            if (TryCreateEventArgs(eventType, body, out TransitionEndEventArgs args))
                                TransitionEnd?.Invoke(this, args);
                            break;
                        }
                    case "TransitionVideoEnd":
                        {
                            if (TryCreateEventArgs(eventType, body, out TransitionVideoEndEventArgs args))
                                TransitionVideoEnd?.Invoke(this, args);
                            break;
                        }
                    case "ProfileChanged":
                        {
                            ProfileChanged?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    case "ProfileListChanged":
                        {
                            ProfileListChanged?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    case "StreamStarting":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                StreamingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "StreamStarted":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                StreamingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "StreamStopping":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                StreamingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "StreamStopped":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                StreamingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "RecordingStarting":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                RecordingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "RecordingStarted":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                RecordingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "RecordingStopping":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                RecordingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "RecordingStopped":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                RecordingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "RecordingPaused":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                RecordingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "RecordingResumed":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                RecordingStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "ReplayStarting":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                ReplayBufferStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "ReplayStarted":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                ReplayBufferStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "ReplayStopping":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                ReplayBufferStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "ReplayStopped":
                        {
                            if (TryCreateEventArgs(eventType, body, out OutputStateChangedEventArgs args))
                                ReplayBufferStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "StreamStatus":
                        {
                            if (TryCreateEventArgs(eventType, body, out StreamStatusEventArgs args))
                                StreamStatus?.Invoke(this, args);
                            break;
                        }
                    case "PreviewSceneChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneChangeEventArgs args))
                                PreviewSceneChanged?.Invoke(this, args);
                            break;
                        }
                    case "StudioModeSwitched":
                        {
                            if (TryCreateEventArgs(eventType, body, out StudioModeChangeEventArgs args))
                                StudioModeSwitched?.Invoke(this, args);
                            break;
                        }
                    case "Exiting":
                        {
                            OBSExit?.Invoke(this, EventArgs.Empty);
                            break;
                        }
                    case "Heartbeat":
                        {
                            if (TryCreateEventArgs(eventType, body, out HeartBeatEventArgs args))
                                Heartbeat?.Invoke(this, args);
                            break;
                        }
                    case "SceneItemDeselected":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemSelectionEventArgs args))
                                SceneItemDeselected?.Invoke(this, args);
                            break;
                        }
                    case "SceneItemSelected":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemSelectionEventArgs args))
                                SceneItemSelected?.Invoke(this, args);
                            break;
                        }
                    case "SceneItemTransformChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SceneItemTransformEventArgs args))
                                SceneItemTransformChanged?.Invoke(this, args);
                            break;
                        }
                    case "SourceAudioMixersChanged":
                        {

                            if (TryCreateEventArgs(eventType, body, out SourceAudioMixersChangedEventArgs args))
                                SourceAudioMixersChanged?.Invoke(this, args);
                            break;
                        }
                    case "SourceAudioSyncOffsetChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceAudioSyncOffsetEventArgs args))
                                SourceAudioSyncOffsetChanged?.Invoke(this, args);
                            break;
                        }
                    case "SourceCreated":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceCreatedEventArgs args))
                                SourceCreated?.Invoke(this, args);
                            break;
                        }
                    case "SourceDestroyed":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceDestroyedEventArgs args))
                                SourceDestroyed?.Invoke(this, args);
                            break;
                        }
                    case "SourceRenamed":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceRenamedEventArgs args))
                                SourceRenamed?.Invoke(this, args);
                            break;
                        }
                    case "SourceMuteStateChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceMuteStateChangedEventArgs args))
                                SourceMuteStateChanged?.Invoke(this, args);
                            break;
                        }
                    case "SourceVolumeChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceVolumeChangedEventArgs args))
                                SourceVolumeChanged?.Invoke(this, args);
                            break;
                        }
                    case "SourceFilterAdded":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceFilterAddedEventArgs args))
                                SourceFilterAdded?.Invoke(this, args);
                            break;
                        }
                    case "SourceFilterRemoved":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceFilterRemovedEventArgs args))
                                SourceFilterRemoved?.Invoke(this, args);
                            break;
                        }
                    case "SourceFiltersReordered":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceFiltersReorderedEventArgs args))
                                SourceFiltersReordered?.Invoke(this, args);
                            break;
                        }
                    case "SourceFilterVisibilityChanged":
                        {
                            if (TryCreateEventArgs(eventType, body, out SourceFilterVisibilityChangedEventArgs args))
                                SourceFilterVisibilityChanged?.Invoke(this, args);
                            break;
                        }
                    case "BroadcastCustomMessage":
                        {
                            if (TryCreateEventArgs(eventType, body, out BroadcastCustomMessageReceivedEventArgs args))
                                BroadcastCustomMessageReceived?.Invoke(this, args);
                            break;
                        }
                    default:
                        {
                            UnhandledEvent?.Invoke(this, body);
                            break;
                        }
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                OBSError?.Invoke(this, new OBSErrorEventArgs($"Error invoking '{eventType}' event.", ex, body));
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        /// <summary>
        /// Attempts to create <see cref="EventArgs"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventName"></param>
        /// <param name="body"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected bool TryCreateEventArgs<T>(string eventName, JObject body, out T args) where T : EventArgs, new()
        {
            args = null!;
            try
            {
                args = body.ToObject<T>()!;
                if (args != null)
                    return true;
                OBSError?.Invoke(this, new OBSErrorEventArgs($"Received '{eventName}' event, but associated data was missing.", body));
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                OBSError?.Invoke(this, new OBSErrorEventArgs($"Error on '{eventName}' event.", ex, body));
            }
#pragma warning restore CA1031 // Do not catch general exception types
            return false;
        }

        /// <summary>
        /// Encode a Base64-encoded SHA-256 hash
        /// </summary>
        /// <param name="input">source string</param>
        /// <returns></returns>
        protected static string HashEncode(string input)
        {
            SHA256Managed? sha256 = new SHA256Managed();

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
