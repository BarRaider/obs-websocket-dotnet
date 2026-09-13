using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Request_Outputs : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void GetOutputStatus_SendsOutputName_AndMapsResponse()
        {
            Arrange(new JObject
            {
                ["outputActive"] = true,
                ["outputReconnecting"] = false,
                ["outputTimecode"] = "00:00:12.345",
                ["outputDuration"] = 12345,
                ["outputCongestion"] = 0.5,
                ["outputBytes"] = 999,
                ["outputSkippedFrames"] = 1,
                ["outputTotalFrames"] = 300
            });

            OutputStatus status = GetOutputStatus("virtualcam_output");

            Server.AssertRequest(nameof(GetOutputStatus), new JObject { ["outputName"] = "virtualcam_output" });
            Assert.IsTrue(status.IsActive);
            Assert.AreEqual("00:00:12.345", status.TimeCode);
            Assert.AreEqual(300L, status.TotalFrames);
        }

        [TestMethod]
        [Timeout(10000)]
        public void ToggleOutput_SendsOutputName_AndReturnsOutputActive()
        {
            Arrange(new JObject { ["outputActive"] = true });

            bool active = ToggleOutput("virtualcam_output");

            Server.AssertRequest(nameof(ToggleOutput), new JObject { ["outputName"] = "virtualcam_output" });
            Assert.IsTrue(active);
        }

        [TestMethod]
        [Timeout(10000)]
        public void StartOutput_SendsOutputName()
        {
            Arrange();

            StartOutput("virtualcam_output");

            Server.AssertRequest(nameof(StartOutput), new JObject { ["outputName"] = "virtualcam_output" });
        }

        [TestMethod]
        [Timeout(10000)]
        public void StopOutput_SendsOutputName()
        {
            Arrange();

            StopOutput("virtualcam_output");

            Server.AssertRequest(nameof(StopOutput), new JObject { ["outputName"] = "virtualcam_output" });
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetOutputSettings_SendsOutputName_AndReturnsSettings()
        {
            Arrange(new JObject { ["outputSettings"] = new JObject { ["server"] = "rtmp://example.com" } });

            JObject settings = GetOutputSettings("virtualcam_output");

            Server.AssertRequest(nameof(GetOutputSettings), new JObject { ["outputName"] = "virtualcam_output" });
            Assert.AreEqual("rtmp://example.com", (string)settings["server"]);
        }

        [TestMethod]
        [Timeout(10000)]
        public void SetOutputSettings_SendsOutputNameAndSettings()
        {
            Arrange();
            var settings = new JObject { ["server"] = "rtmp://example.com" };

            SetOutputSettings("virtualcam_output", settings);

            Server.AssertRequest(nameof(SetOutputSettings), new JObject
            {
                ["outputName"] = "virtualcam_output",
                ["outputSettings"] = new JObject { ["server"] = "rtmp://example.com" }
            });
        }
    }
}
