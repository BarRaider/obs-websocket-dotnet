using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_InputVolumeMeter
    {
        [TestMethod]
        public void Deserialize_SingleInput_ReadsNameAndChannelCount()
        {
            string json = @"{
                ""inputName"": ""Mic/Aux"",
                ""inputLevelsMul"": [[0.5, 0.7, 0.9], [0.3, 0.6, 0.8]]
            }";

            var meter = JsonConvert.DeserializeObject<InputVolumeMeter>(json);

            Assert.AreEqual("Mic/Aux", meter.InputName);
            Assert.AreEqual(2, meter.InputLevels.Count);
        }

        [TestMethod]
        public void Deserialize_ChannelLevel_PreservesFieldOrder()
        {
            // obs-websocket emits each channel as [magnitude, peak, inputPeak]
            string json = @"{
                ""inputName"": ""Desktop Audio"",
                ""inputLevelsMul"": [[0.1, 0.2, 0.3]]
            }";

            var meter = JsonConvert.DeserializeObject<InputVolumeMeter>(json);

            Assert.AreEqual(0.1f, meter.InputLevels[0].MagnitudeWithVolume);
            Assert.AreEqual(0.2f, meter.InputLevels[0].PeakWithVolume);
            Assert.AreEqual(0.3f, meter.InputLevels[0].PeakRaw);
        }

        [TestMethod]
        public void Deserialize_ListOfInputs_ReadsEachIndependently()
        {
            string json = @"[
                { ""inputName"": ""Mic/Aux"", ""inputLevelsMul"": [[0.4, 0.5, 0.6]] },
                { ""inputName"": ""Desktop Audio"", ""inputLevelsMul"": [[0.1, 0.2, 0.3], [0.7, 0.8, 0.9]] }
            ]";

            var meters = JsonConvert.DeserializeObject<List<InputVolumeMeter>>(json);

            Assert.AreEqual(2, meters.Count);
            Assert.AreEqual("Mic/Aux", meters[0].InputName);
            Assert.AreEqual(1, meters[0].InputLevels.Count);
            Assert.AreEqual("Desktop Audio", meters[1].InputName);
            Assert.AreEqual(2, meters[1].InputLevels.Count);
        }

        [TestMethod]
        public void Deserialize_NoActiveChannels_ReturnsEmptyLevelsList()
        {
            string json = @"{ ""inputName"": ""Silent Source"", ""inputLevelsMul"": [] }";

            var meter = JsonConvert.DeserializeObject<InputVolumeMeter>(json);

            Assert.AreEqual("Silent Source", meter.InputName);
            Assert.AreEqual(0, meter.InputLevels.Count);
        }

        [TestMethod]
        public void Deserialize_ChannelWithWrongElementCount_Throws()
        {
            string json = @"{ ""inputName"": ""Bad Source"", ""inputLevelsMul"": [[0.1, 0.2]] }";

            Assert.ThrowsException<ProtocolViolationException>(
                () => JsonConvert.DeserializeObject<InputVolumeMeter>(json));
        }
    }
}
