using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Loopback server that performs the full Hello/Identify/Identified handshake with a real
    /// <see cref="OBSWebsocket"/> client, then records every outgoing request frame and auto-replies
    /// to it, so tests can assert the exact wire shape of a request and control its response.
    /// </summary>
    public sealed class ObsRequestRecorder : IDisposable
    {
        // Raw opcodes, per the obs-websocket protocol.
        private const int OpCodeHello = 0;
        private const int OpCodeIdentify = 1;
        private const int OpCodeIdentified = 2;
        private const int OpCodeRequest = 6;
        private const int OpCodeRequestResponse = 7;

        private readonly TestWebSocketServer server;
        private readonly ConcurrentQueue<JObject> queuedResponses = new ConcurrentQueue<JObject>();
        private readonly ConcurrentQueue<(int Code, string Comment)> queuedErrors = new ConcurrentQueue<(int, string)>();
        private readonly object requestsLock = new object();
        private readonly List<JObject> requests = new List<JObject>();

        /// <summary>
        /// The `d` payload of the op-1 Identify frame the client sent.
        /// </summary>
        public JObject IdentifyPayload { get; private set; }

        /// <summary>
        /// The `d` payload of every op-6 request frame received so far, in order.
        /// </summary>
        public IReadOnlyList<JObject> Requests
        {
            get { lock (requestsLock) { return requests.ToArray(); } }
        }

        /// <summary>
        /// The `d` payload of the most recently received request, or null if none yet.
        /// </summary>
        public JObject LastRequest
        {
            get { lock (requestsLock) { return requests.Count > 0 ? requests[requests.Count - 1] : null; } }
        }

        /// <summary>
        /// The `requestType` field of <see cref="LastRequest"/>.
        /// </summary>
        public string LastRequestType => (string)LastRequest?["requestType"];

        /// <summary>
        /// The `requestData` field of <see cref="LastRequest"/>.
        /// </summary>
        public JToken LastRequestData => LastRequest?["requestData"];

        private ObsRequestRecorder(JObject defaultResponseData)
        {
            if (defaultResponseData != null)
            {
                queuedResponses.Enqueue(defaultResponseData);
            }

            server = TestWebSocketServer.Start(HandleConnectionAsync);
        }

        /// <summary>
        /// Starts a loopback server, connects the given client to it, and blocks until the client
        /// has completed identification.
        /// </summary>
        /// <param name="client">The OBSWebsocket instance to connect</param>
        /// <param name="defaultResponseData">responseData to reply with on the first request. Every request beyond that (or when this is null) gets an empty object, unless overridden via <see cref="EnqueueResponse"/>/<see cref="EnqueueError"/>.</param>
        public static ObsRequestRecorder StartAndConnect(OBSWebsocket client, JObject defaultResponseData = null)
        {
            var recorder = new ObsRequestRecorder(defaultResponseData);
            var identified = new ManualResetEventSlim(false);
            void OnConnected(object s, EventArgs e) => identified.Set();

            client.Connected += OnConnected;
            try
            {
                client.ConnectAsync($"ws://127.0.0.1:{recorder.server.Port}/", string.Empty);
                if (!identified.Wait(4000))
                {
                    throw new TimeoutException("Client never completed identification.");
                }
            }
            finally
            {
                client.Connected -= OnConnected;
            }

            return recorder;
        }

        /// <summary>
        /// Queues responseData to reply with on the next request.
        /// </summary>
        public void EnqueueResponse(JObject responseData) => queuedResponses.Enqueue(responseData ?? new JObject());

        /// <summary>
        /// Makes the next reply a protocol error, so callers can assert ErrorResponseException mapping.
        /// </summary>
        public void EnqueueError(int code, string comment = null) => queuedErrors.Enqueue((code, comment));

        /// <summary>
        /// Asserts that <see cref="LastRequest"/> has the given requestType and requestData.
        /// Pass null for expectedRequestData to assert that no request data was sent.
        /// </summary>
        public void AssertRequest(string expectedRequestType, JObject expectedRequestData)
        {
            var lastRequest = LastRequest;
            Assert.IsNotNull(lastRequest, "No request was captured.");
            Assert.AreEqual(expectedRequestType, LastRequestType, "Unexpected requestType.");

            var actualData = LastRequestData;
            if (expectedRequestData == null)
            {
                bool isEmpty = actualData == null
                    || actualData.Type == JTokenType.Null
                    || (actualData.Type == JTokenType.Object && !((JObject)actualData).HasValues);
                Assert.IsTrue(isEmpty, $"Expected no request data, got: {actualData}");
            }
            else
            {
                Assert.IsTrue(JToken.DeepEquals(expectedRequestData, actualData),
                    $"Expected requestData {expectedRequestData}, got {actualData}");
            }
        }

        private async Task HandleConnectionAsync(WebSocket socket, CancellationToken ct)
        {
            await SendAsync(socket, new JObject
            {
                ["op"] = OpCodeHello,
                ["d"] = new JObject { ["rpcVersion"] = 1 }
            }, ct);

            string identifyJson = await TestWebSocketServer.ReceiveTextAsync(socket, ct);
            IdentifyPayload = (JObject)JObject.Parse(identifyJson)["d"];

            await SendAsync(socket, new JObject
            {
                ["op"] = OpCodeIdentified,
                ["d"] = new JObject { ["negotiatedRpcVersion"] = 1 }
            }, ct);

            while (true)
            {
                string json;
                try
                {
                    json = await TestWebSocketServer.ReceiveTextAsync(socket, ct);
                }
                catch
                {
                    // Socket closed/disposed during test teardown - stop serving.
                    return;
                }

                var message = JObject.Parse(json);
                if ((int)message["op"] != OpCodeRequest)
                {
                    continue;
                }

                var requestBody = (JObject)message["d"];
                lock (requestsLock)
                {
                    requests.Add(requestBody);
                }

                string requestId = (string)requestBody["requestId"];
                JObject responseMessage;
                if (queuedErrors.TryDequeue(out var error))
                {
                    var status = new JObject { ["result"] = false, ["code"] = error.Code };
                    if (error.Comment != null)
                    {
                        status["comment"] = error.Comment;
                    }

                    responseMessage = new JObject
                    {
                        ["requestId"] = requestId,
                        ["requestStatus"] = status
                    };
                }
                else
                {
                    queuedResponses.TryDequeue(out var responseData);
                    responseMessage = new JObject
                    {
                        ["requestId"] = requestId,
                        ["requestStatus"] = new JObject { ["result"] = true },
                        ["responseData"] = responseData ?? new JObject()
                    };
                }

                await SendAsync(socket, new JObject
                {
                    ["op"] = OpCodeRequestResponse,
                    ["d"] = responseMessage
                }, ct);
            }
        }

        private static Task SendAsync(WebSocket socket, JObject message, CancellationToken ct) =>
            TestWebSocketServer.SendTextAsync(socket, message.ToString(Newtonsoft.Json.Formatting.None), ct);

        public void Dispose() => server.Dispose();
    }
}
