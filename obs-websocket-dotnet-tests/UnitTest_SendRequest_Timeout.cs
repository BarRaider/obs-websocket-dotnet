using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_SendRequest_Timeout : OBSWebsocket
    {
        // Raw opcode for a "RequestResponse" message, per the obs-websocket protocol.
        private const int OpCodeRequestResponse = 7;

        private TestWebSocketServer server;

        [TestCleanup]
        public void Cleanup()
        {
            Disconnect();
            server?.Dispose();
        }

        [TestMethod]
        [Timeout(5000)]
        public void SendRequest_UsesFullTimeout_NotJustMillisecondComponent()
        {
            // A whole-second TimeSpan has a zero ".Milliseconds" component (e.g. 2000ms -> Milliseconds == 0),
            // while ".TotalMilliseconds" correctly reports 2000. The original bug waited on the former,
            // which made the request time out immediately regardless of the configured timeout.
            WSTimeout = TimeSpan.FromSeconds(2);
            ConnectToServerThatNeverReplies();

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var ex = Assert.ThrowsException<ErrorResponseException>(() => SendRequest("GetVersion"));
            sw.Stop();

            Assert.AreEqual(1, ex.ErrorCode);
            StringAssert.Contains(ex.Message, "timed out");
            Assert.IsTrue(sw.ElapsedMilliseconds >= 1800,
                $"Expected the request to wait close to 2000ms before timing out, only waited {sw.ElapsedMilliseconds}ms. " +
                "This suggests only the Milliseconds component of the timeout (not TotalMilliseconds) is being honored.");
        }

        [TestMethod]
        [Timeout(5000)]
        public void SendRequest_RemovesPendingHandler_OnTimeout()
        {
            WSTimeout = TimeSpan.FromMilliseconds(150);
            ConnectToServerThatNeverReplies();

            Assert.ThrowsException<ErrorResponseException>(() => SendRequest("GetVersion"));

            var handlers = GetResponseHandlers();
            Assert.AreEqual(0, handlers.Count,
                "The timed-out request's handler was not removed from responseHandlers - this leaks a TaskCompletionSource per timed-out request.");
        }

        [TestMethod]
        [Timeout(5000)]
        public void SendRequest_ReturnsResponseData_WhenServerRepliesInTime()
        {
            WSTimeout = TimeSpan.FromSeconds(5);
            ConnectToServerThatEchoesSuccess();

            JObject response = SendRequest("GetVersion");

            Assert.IsNotNull(response);
            Assert.AreEqual("1.0.0", (string)response["obsVersion"]);
            Assert.AreEqual(0, GetResponseHandlers().Count);
        }

        private System.Collections.ICollection GetResponseHandlers()
        {
            var field = typeof(OBSWebsocket).GetField("responseHandlers",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (System.Collections.ICollection)field.GetValue(this);
        }

        private void ConnectToServerThatNeverReplies()
        {
            server = TestWebSocketServer.Start(async (socket, ct) =>
            {
                // Accept the connection but never send a response back.
                await Task.Delay(Timeout.Infinite, ct).ContinueWith(_ => { });
            });
            ConnectAsync($"ws://127.0.0.1:{server.Port}/", string.Empty);
        }

        private void ConnectToServerThatEchoesSuccess()
        {
            server = TestWebSocketServer.Start(async (socket, ct) =>
            {
                string json = await TestWebSocketServer.ReceiveTextAsync(socket, ct);
                var request = JObject.Parse(json);
                string requestId = (string)request["d"]["requestId"];

                var response = new JObject
                {
                    ["op"] = OpCodeRequestResponse,
                    ["d"] = new JObject
                    {
                        ["requestId"] = requestId,
                        ["requestStatus"] = new JObject { ["result"] = true },
                        ["responseData"] = new JObject { ["obsVersion"] = "1.0.0" }
                    }
                };
                await TestWebSocketServer.SendTextAsync(socket, response.ToString(Newtonsoft.Json.Formatting.None), ct);
            });
            ConnectAsync($"ws://127.0.0.1:{server.Port}/", string.Empty);
        }
    }
}
