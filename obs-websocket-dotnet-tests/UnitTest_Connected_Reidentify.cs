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
    /// Regression test for Connected being raised on every Identified message. The server answers each
    /// ReIdentify with another Identified, so subscribing to or unsubscribing from a high-volume event
    /// used to raise Connected again on an already-established session - consumers treating Connected as
    /// "new session" then re-ran their full initial-state load every time a meter was shown or hidden.
    /// </summary>
    [TestClass]
    public class UnitTest_Connected_Reidentify : OBSWebsocket
    {
        private const int OpCodeHello = 0;
        private const int OpCodeIdentified = 2;
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
        public void Connected_IsNotRaisedAgain_WhenServerAnswersReidentify()
        {
            int connectedCount = 0;
            var firstConnected = new ManualResetEventSlim(false);
            var reidentifyAnswered = new ManualResetEventSlim(false);
            Connected += (s, e) =>
            {
                Interlocked.Increment(ref connectedCount);
                firstConnected.Set();
            };

            server = TestWebSocketServer.Start(async (socket, ct) =>
            {
                await SendHello(socket, ct);
                await TestWebSocketServer.ReceiveTextAsync(socket, ct); // Identify
                await SendIdentified(socket, ct);

                string next = await TestWebSocketServer.ReceiveTextAsync(socket, ct);
                if ((int)JObject.Parse(next)["op"] == OpCodeReIdentify)
                {
                    // Per protocol, the server confirms a ReIdentify with another Identified.
                    await SendIdentified(socket, ct);
                    reidentifyAnswered.Set();
                }
            });

            ConnectAsync($"ws://127.0.0.1:{server.Port}/", string.Empty);
            Assert.IsTrue(firstConnected.Wait(2000), "Connected event was never raised.");

            EventHandler<InputVolumeMetersEventArgs> handler = (s, e) => { };
            InputVolumeMeters += handler;

            Assert.IsTrue(reidentifyAnswered.Wait(2000), "Server never received the client's ReIdentify.");

            // Give a (wrongly) re-raised Connected time to be dispatched before asserting it wasn't.
            Thread.Sleep(500);

            Assert.AreEqual(1, Volatile.Read(ref connectedCount));
            Assert.IsTrue(IsIdentified);

            InputVolumeMeters -= handler;
        }

        private static Task SendHello(WebSocket socket, CancellationToken ct)
        {
            var hello = new JObject
            {
                ["op"] = OpCodeHello,
                ["d"] = new JObject { ["rpcVersion"] = 1 }
            };
            return TestWebSocketServer.SendTextAsync(socket, hello.ToString(Newtonsoft.Json.Formatting.None), ct);
        }

        private static Task SendIdentified(WebSocket socket, CancellationToken ct)
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
