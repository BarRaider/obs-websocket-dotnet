using System.Net.WebSockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_IsIdentified : OBSWebsocket
    {
        // Raw opcodes, per the obs-websocket protocol.
        private const int OpCodeHello = 0;
        private const int OpCodeIdentified = 2;

        private TestWebSocketServer server;

        [TestCleanup]
        public void Cleanup()
        {
            Disconnect();
            server?.Dispose();
        }

        [TestMethod]
        public void IsIdentified_IsFalse_BeforeConnecting()
        {
            Assert.IsFalse(IsIdentified);
        }

        [TestMethod]
        [Timeout(5000)]
        public void IsIdentified_BecomesTrue_OnceServerConfirmsIdentification()
        {
            var identifiedReceivedByClient = new System.Threading.ManualResetEventSlim(false);
            Connected += (s, e) => identifiedReceivedByClient.Set();

            server = TestWebSocketServer.Start(async (socket, ct) =>
            {
                await SendHello(socket, ct);
                await TestWebSocketServer.ReceiveTextAsync(socket, ct); // Identify
                await SendIdentified(socket, ct);
            });

            Assert.IsFalse(IsIdentified);
            ConnectAsync($"ws://127.0.0.1:{server.Port}/", string.Empty);

            Assert.IsTrue(identifiedReceivedByClient.Wait(4000), "Connected event was never raised.");
            Assert.IsTrue(IsIdentified);
        }

        [TestMethod]
        [Timeout(5000)]
        public void IsIdentified_ResetsToFalse_OnDisconnect()
        {
            var identifiedReceivedByClient = new System.Threading.ManualResetEventSlim(false);
            Connected += (s, e) => identifiedReceivedByClient.Set();

            server = TestWebSocketServer.Start(async (socket, ct) =>
            {
                await SendHello(socket, ct);
                await TestWebSocketServer.ReceiveTextAsync(socket, ct); // Identify
                await SendIdentified(socket, ct);
            });

            ConnectAsync($"ws://127.0.0.1:{server.Port}/", string.Empty);
            Assert.IsTrue(identifiedReceivedByClient.Wait(4000), "Connected event was never raised.");
            Assert.IsTrue(IsIdentified);

            Disconnect();

            Assert.IsFalse(IsIdentified);
        }

        private static System.Threading.Tasks.Task SendHello(WebSocket socket, System.Threading.CancellationToken ct)
        {
            var hello = new JObject
            {
                ["op"] = OpCodeHello,
                ["d"] = new JObject { ["rpcVersion"] = 1 }
            };
            return TestWebSocketServer.SendTextAsync(socket, hello.ToString(Newtonsoft.Json.Formatting.None), ct);
        }

        private static System.Threading.Tasks.Task SendIdentified(WebSocket socket, System.Threading.CancellationToken ct)
        {
            var identified = new JObject
            {
                ["op"] = OpCodeIdentified,
                ["d"] = new JObject { ["negotiatedRpcVersion"] = 1 }
            };
            return TestWebSocketServer.SendTextAsync(socket, identified.ToString(Newtonsoft.Json.Formatting.None), ct);
        }
    }
}
