using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Characterization tests proving ObsRequestRecorder against already-shipped request behavior,
    /// before any new request/event code is added on top of it.
    /// </summary>
    [TestClass]
    public class UnitTest_Request_Harness : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void GetStreamStatus_SendsNoRequestData_AndMapsResponse()
        {
            Arrange(new JObject
            {
                ["outputActive"] = true,
                ["outputReconnecting"] = false,
                ["outputTimecode"] = "00:00:05.000",
                ["outputDuration"] = 5000,
                ["outputCongestion"] = 0.1,
                ["outputBytes"] = 1234,
                ["outputSkippedFrames"] = 0,
                ["outputTotalFrames"] = 150
            });

            OutputStatus status = GetStreamStatus();

            Server.AssertRequest(nameof(GetStreamStatus), null);
            Assert.IsTrue(status.IsActive);
            Assert.AreEqual(150L, status.TotalFrames);
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetRecordDirectory_SendsNoRequestData_AndMapsResponse()
        {
            Arrange(new JObject { ["recordDirectory"] = @"C:\Recordings" });

            string dir = GetRecordDirectory();

            Server.AssertRequest(nameof(GetRecordDirectory), null);
            Assert.AreEqual(@"C:\Recordings", dir);
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetInputKindList_Unversioned_SendsRequestData_AndMapsResponse()
        {
            Arrange(new JObject { ["inputKinds"] = new JArray("ffmpeg_source", "browser_source") });

            List<string> kinds = GetInputKindList(unversioned: true);

            Server.AssertRequest(nameof(GetInputKindList), new JObject { ["unversioned"] = true });
            CollectionAssert.AreEqual(new[] { "ffmpeg_source", "browser_source" }, kinds);
        }

        [TestMethod]
        [Timeout(10000)]
        public void SendRequest_MapsServerError_ToErrorResponseException()
        {
            Arrange();
            Server.EnqueueError(600, "Something went wrong");

            var ex = Assert.ThrowsException<ErrorResponseException>(() => GetVersion());

            Assert.AreEqual(600, ex.ErrorCode);
            StringAssert.Contains(ex.Message, "Something went wrong");
        }
    }
}
