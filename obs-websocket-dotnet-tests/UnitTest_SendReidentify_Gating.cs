using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types.Events;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Regression test for gating SendReidentify on IsIdentified rather than wsConnection.IsStarted.
    /// IsStarted only means Start() was called, not that the server has completed the
    /// Hello/Identify/Identified handshake - so subscribing to a high-volume event during that window
    /// used to send a ReIdentify before the client was actually identified, violating protocol ordering.
    /// </summary>
    [TestClass]
    public class UnitTest_SendReidentify_Gating : OBSWebsocket
    {
        private const int OpCodeReIdentify = 3;

        private TestWebSocketServer server;

        [TestCleanup]
        public void Cleanup()
        {
            Disconnect();
            server?.Dispose();
        }

        [TestMethod]
        [Timeout(5000)]
        public void RegisterEvent_DoesNotSendReidentify_BeforeServerConfirmsIdentification()
        {
            var identifyReceivedByServer = new ManualResetEventSlim(false);
            var reidentifyReceivedByServer = new ManualResetEventSlim(false);

            server = TestWebSocketServer.Start(async (socket, ct) =>
            {
                await SendHello(socket, ct);
                await TestWebSocketServer.ReceiveTextAsync(socket, ct); // Identify
                identifyReceivedByServer.Set();

                // Deliberately withhold Identified to hold the client in the connected-but-not-identified
                // window this test is guarding. If the client (incorrectly) sends a ReIdentify here, catch it.
                string next = await TestWebSocketServer.ReceiveTextAsync(socket, ct);
                if ((int)JObject.Parse(next)["op"] == OpCodeReIdentify)
                {
                    reidentifyReceivedByServer.Set();
                }
            });

            ConnectAsync($"ws://127.0.0.1:{server.Port}/", string.Empty);
            Assert.IsTrue(identifyReceivedByServer.Wait(2000), "Server never received the client's Identify.");
            Assert.IsFalse(IsIdentified);

            EventHandler<InputVolumeMetersEventArgs> handler = (s, e) => { };
            InputVolumeMeters += handler;

            Assert.IsFalse(reidentifyReceivedByServer.Wait(500),
                "A ReIdentify was sent before the server confirmed identification.");

            InputVolumeMeters -= handler;
        }

        private static Task SendHello(WebSocket socket, CancellationToken ct)
        {
            var hello = new JObject
            {
                ["op"] = 0,
                ["d"] = new JObject { ["rpcVersion"] = 1 }
            };
            return TestWebSocketServer.SendTextAsync(socket, hello.ToString(Newtonsoft.Json.Formatting.None), ct);
        }
    }
}
