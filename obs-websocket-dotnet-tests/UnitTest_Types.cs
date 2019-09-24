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

            var itemData = new JObject();
            itemData.Add("name", itemName);
            itemData.Add("type", "dummy_source");
            itemData.Add("volume", 1.0f);
            itemData.Add("x", 0.0f);
            itemData.Add("y", 0.0f);
            itemData.Add("source_cx", 1280);
            itemData.Add("source_cy", 720);
            itemData.Add("cx", 1280.0f);
            itemData.Add("cy", 720.0F);

            var items = new JArray();
            items.Add(itemData);

            var data = new JObject();
            data.Add("name", sceneName);
            data.Add("sources", items);

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

            var data = new JObject();
            data.Add("name", name);
            data.Add("type", type);
            data.Add("volume", volume);
            data.Add("x", x);
            data.Add("y", y);
            data.Add("source_cx", sourceWidth);
            data.Add("source_cy", sourceHeight);
            data.Add("cx", width);
            data.Add("cy", height);

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

            var data = new JObject();
            data.Add("authRequired", true);
            data.Add("challenge", challenge);
            data.Add("salt", salt);

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

            var data = new JObject();
            data.Add("obs-websocket-version", pluginVersion);
            data.Add("obs-studio-version", obsVersion);

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

            var data = new JObject();
            data.Add("streaming", true);
            data.Add("recording", true);
            data.Add("bytes-per-sec", bytesPerSec);
            data.Add("kbits-per-sec", kbitsPerSec);
            data.Add("strain", strain);
            data.Add("total-stream-time", streamTime);
            data.Add("num-total-frames", totalFrames);
            data.Add("num-dropped-frames", droppedFrames);
            data.Add("fps", fps);

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
            var data = new JObject();
            data.Add("streaming", true);
            data.Add("recording", true);

            var outputState = new OutputStatus(data);

            Assert.IsTrue(outputState.IsStreaming);
            Assert.IsTrue(outputState.IsRecording);
        }

        [TestMethod]
        public void OBSCurrentTransitionInfo_BuildFromJSON()
        {
            string transitionName = "Transition name éèïöü";
            int duration = 2000;

            var data = new JObject();
            data.Add("name", transitionName);
            data.Add("duration", duration);

            var transitionInfo = new TransitionSettings(data);

            Assert.AreEqual(transitionName, transitionInfo.Name);
            Assert.AreEqual(duration, transitionInfo.Duration);
        }

        [TestMethod]
        public void OBSVolumeInfo_BuildFromJSON()
        {
            float volumeLevel = 0.50f;

            var data = new JObject();
            data.Add("volume", volumeLevel);
            data.Add("muted", true);

            var volumeInfo = new VolumeInfo(data);

            Assert.AreEqual(volumeLevel, volumeInfo.Volume);
            Assert.IsTrue(volumeInfo.Muted);
        }
    }
}
