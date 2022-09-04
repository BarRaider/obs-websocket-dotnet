using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Types
    {
        [TestMethod]
        public void OBSScene_BuildFromJSON()
        {
            string sceneName = "Scene name äëôû";
            bool isGroup = true;
            string itemName = "First item name äëôû";
            SceneItemSourceType sourceType = SceneItemSourceType.OBS_SOURCE_TYPE_INPUT;
            string inputKind = "BarRaider";
            int sceneItemId = 22;
            

            JObject itemData = new JObject
            {
                { "sourceName", itemName },
                { "sourceType", (int)sourceType },
                { "inputKind", inputKind},
                { "sceneItemId", sceneItemId },
                { "volume", 1.0f },
                { "x", 0.0f },
                { "y", 0.0f },
                { "source_cx", 1280 },
                { "source_cy", 720 },
                { "cx", 1280.0f },
                { "cy", 720.0F }
            };

            var items = new JArray
            {
                itemData
            };

            var data = new JObject
            {
                { "sceneName", sceneName },
                { "sources", items },
                { "isGroup", isGroup }
            };

            var scene = new ObsScene(data);

            Assert.AreEqual(sceneName, scene.Name);
            Assert.AreEqual(1, scene.Items.Count);
            Assert.AreEqual(isGroup, scene.IsGroup);
            Assert.AreEqual(itemName, scene.Items[0].SourceName);
            Assert.AreEqual(sourceType, scene.Items[0].SourceType);
            Assert.AreEqual(inputKind, scene.Items[0].SourceKind);
            Assert.AreEqual(sceneItemId, scene.Items[0].ItemId);

        }

        [TestMethod]
        public void OBSAuthInfo_BuildFromJSON()
        {
            string challenge = "pBWv82hj";
            string salt = "B9fL8CF7";

            var data = new JObject
            {
                { "authRequired", true },
                { "challenge", challenge },
                { "salt", salt }
            };

            var authInfo = new OBSAuthInfo(data);

            Assert.AreEqual(challenge, authInfo.Challenge);
            Assert.AreEqual(salt, authInfo.PasswordSalt);
        }

        [TestMethod]
        public void OBSVersion_BuildFromJSON()
        {
            string pluginVersion = "5.0.1";
            string obsVersion = "28.0.1";
            double rpcVersion = 1.1;
            string availableRequests = "GetVersion,BarRaider,Test";
            string platform = "windows";
            string supportedImageFormats = "png,jpg";

            var requests = new JArray(availableRequests.Split(','));
            var images = new JArray(supportedImageFormats.Split(','));

            var data = new JObject
            {
                { "obsWebSocketVersion", pluginVersion },
                { "obsVersion", obsVersion },
                { "rpcVersion", rpcVersion},
                { "availableRequests", requests},
                { "platform", platform},
                { "supportedImageFormats", images}
            };

            var version = new ObsVersion(data);

            Assert.AreEqual(pluginVersion, version.PluginVersion);
            Assert.AreEqual(obsVersion, version.OBSStudioVersion);
            Assert.AreEqual(rpcVersion, version.Version);
            Assert.AreEqual(platform, version.Platform);
            Assert.AreEqual(3, version.AvailableRequests.Count);
            Assert.AreEqual(2, version.SupportedImageFormats.Count);
        }

        [TestMethod]
        public void OBSStreamStatus_BuildFromJSON()
        {
            string outputTimecode = "00:01:22.666";
            int outputDuration = 230;
            double outputCongestion = 23.32;
            int outputBytes = 451241;
            int outputSkippedFrames = 120;
            int outputTotalFrames = 2000;

            var data = new JObject
            {
                { "outputActive", true },
                { "outputReconnecting", true },
                { "outputTimecode", outputTimecode },
                { "outputDuration", outputDuration },
                { "outputCongestion", outputCongestion },
                { "outputBytes", outputBytes },
                { "outputSkippedFrames", outputSkippedFrames },
                { "outputTotalFrames", outputTotalFrames }
            };

            var streamStatus = new OutputStatus(data);

            Assert.IsTrue(streamStatus.IsActive);
            Assert.IsTrue(streamStatus.IsReconnecting);
            Assert.AreEqual(outputTimecode, streamStatus.TimeCode);
            Assert.AreEqual(outputDuration, streamStatus.Duration);
            Assert.AreEqual(outputCongestion, streamStatus.Congestion);
            Assert.AreEqual(outputBytes, streamStatus.BytesSent);
            Assert.AreEqual(outputSkippedFrames, streamStatus.SkippedFrames);
            Assert.AreEqual(outputTotalFrames, streamStatus.TotalFrames);
        }

        [TestMethod]
        public void OBSOutputStatus_BuildFromJSON()
        {
            var data = new JObject
            {
                { "outputActive", true }
            };

            var outputState = new OutputStatus(data);
            var recordState = new RecordingStatus(data);

            Assert.IsTrue(outputState.IsActive);
            Assert.IsTrue(recordState.IsRecording);
        }

        [TestMethod]
        public void OBSCurrentTransitionInfo_BuildFromJSON()
        {
            string transitionName = "Transition name éèïöü";
            int duration = 2000;
            string kind = "TBD";
            bool transitionFixed = true;
            bool transitionConfigurable = true;

            var data = new JObject
            {
                { "transitionName", transitionName },
                { "transitionDuration", duration },
                { "transitionKind", kind },
                { "transitionFixed", transitionFixed },
                { "transitionConfigurable", transitionConfigurable }
            };

            var transitionInfo = new TransitionSettings(data);

            Assert.AreEqual(transitionName, transitionInfo.Name);
            Assert.AreEqual(duration, transitionInfo.Duration);
            Assert.AreEqual(kind, transitionInfo.Kind);
            Assert.AreEqual(transitionFixed, transitionInfo.IsFixed);
            Assert.AreEqual(transitionConfigurable, transitionInfo.IsConfigurable);
        }

        [TestMethod]
        public void OBSVolumeInfo_BuildFromJSON()
        {
            float volumeMul = 0.50f;
            float volumeDB = 45.4f;

            var data = new JObject
            {
                { "inputVolumeMul", volumeMul },
                { "inputVolumeDb", volumeDB }
            };

            var volumeInfo = new VolumeInfo(data);

            Assert.AreEqual(volumeMul, volumeInfo.VolumeMul);
            Assert.AreEqual(volumeDB, volumeInfo.VolumeDb);
        }
    }
}
