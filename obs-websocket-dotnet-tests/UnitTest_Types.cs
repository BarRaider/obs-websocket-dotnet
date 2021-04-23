using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
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
            string itemName = "First item name äëôû";

            JObject itemData = new JObject
            {
                { "name", itemName },
                { "type", "dummy_source" },
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
                { "name", sceneName },
                { "sources", items }
            };

            var scene = new OBSScene(data);

            Assert.AreEqual(sceneName, scene.Name);
            Assert.AreEqual(1, scene.Items.Count);
            Assert.AreEqual(itemName, scene.Items[0].SourceName);
        }

        [TestMethod]
        public void OBSSceneItem_BuildFromJSON()
        {
            string name = "Source name éèüïöîô";
            string type = "dummy_source";
            float volume = 0.5f;
            float x = 10.0005f;
            float y = 15.0002f;
            int sourceWidth = 1280;
            int sourceHeight = 720;
            float width = sourceWidth * 2.002f;
            float height = sourceHeight * 2.002f;

            var data = new JObject
            {
                { "name", name },
                { "type", type },
                { "volume", volume },
                { "x", x },
                { "y", y },
                { "source_cx", sourceWidth },
                { "source_cy", sourceHeight },
                { "cx", width },
                { "cy", height }
            };

            var item = new SceneItem(data);

            Assert.AreEqual(name, item.SourceName);
            Assert.AreEqual(type, item.InternalType);
            Assert.AreEqual(volume, item.AudioVolume);
            Assert.AreEqual(x, item.XPos);
            Assert.AreEqual(y, item.YPos);
            Assert.AreEqual(sourceWidth, item.SourceWidth);
            Assert.AreEqual(sourceHeight, item.SourceHeight);
            Assert.AreEqual(width, item.Width);
            Assert.AreEqual(height, item.Height);
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

            Assert.IsTrue(authInfo.AuthRequired);
            Assert.AreEqual(challenge, authInfo.Challenge);
            Assert.AreEqual(salt, authInfo.PasswordSalt);
        }

        [TestMethod]
        public void OBSVersion_BuildFromJSON()
        {
            string pluginVersion = "4.0.0";
            string obsVersion = "18.0.1";

            var data = new JObject
            {
                { "obs-websocket-version", pluginVersion },
                { "obs-studio-version", obsVersion }
            };

            var version = new OBSVersion(data);

            Assert.AreEqual(pluginVersion, version.PluginVersion);
            Assert.AreEqual(obsVersion, version.OBSStudioVersion);
        }

        [TestMethod]
        public void OBSStreamStatus_BuildFromJSON()
        {
            int bytesPerSec = 294400;
            int kbitsPerSec = 2300;
            float strain = 0.5f;
            int streamTime = 120;
            int totalFrames = 2000;
            int droppedFrames = 12;
            float fps = 29.97f;

            var data = new JObject
            {
                { "streaming", true },
                { "recording", true },
                { "bytes-per-sec", bytesPerSec },
                { "kbits-per-sec", kbitsPerSec },
                { "strain", strain },
                { "total-stream-time", streamTime },
                { "num-total-frames", totalFrames },
                { "num-dropped-frames", droppedFrames },
                { "fps", fps }
            };

            var streamStatus = new StreamStatus(data);

            Assert.IsTrue(streamStatus.Streaming);
            Assert.IsTrue(streamStatus.Recording);
            Assert.AreEqual(bytesPerSec, streamStatus.BytesPerSec);
            Assert.AreEqual(kbitsPerSec, streamStatus.KbitsPerSec);
            Assert.AreEqual(strain, streamStatus.Strain);
            Assert.AreEqual(streamTime, streamStatus.TotalStreamTime);
            Assert.AreEqual(totalFrames, streamStatus.TotalFrames);
            Assert.AreEqual(droppedFrames, streamStatus.DroppedFrames);
            Assert.AreEqual(fps, streamStatus.FPS);
        }

        [TestMethod]
        public void OBSOutputStatus_BuildFromJSON()
        {
            var data = new JObject
            {
                { "streaming", true },
                { "recording", true }
            };

            var outputState = new OutputStatus(data);

            Assert.IsTrue(outputState.IsStreaming);
            Assert.IsTrue(outputState.IsRecording);
        }

        [TestMethod]
        public void OBSCurrentTransitionInfo_BuildFromJSON()
        {
            string transitionName = "Transition name éèïöü";
            int duration = 2000;

            var data = new JObject
            {
                { "name", transitionName },
                { "duration", duration }
            };

            var transitionInfo = new TransitionSettings(data);

            Assert.AreEqual(transitionName, transitionInfo.Name);
            Assert.AreEqual(duration, transitionInfo.Duration);
        }

        [TestMethod]
        public void OBSVolumeInfo_BuildFromJSON()
        {
            float volumeLevel = 0.50f;

            var data = new JObject
            {
                { "volume", volumeLevel },
                { "muted", true }
            };

            var volumeInfo = new VolumeInfo(data);

            Assert.AreEqual(volumeLevel, volumeInfo.Volume);
            Assert.IsTrue(volumeInfo.Muted);
        }
    }
}
