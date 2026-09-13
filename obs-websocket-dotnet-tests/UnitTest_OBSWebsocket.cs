using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_OBSWebsocket : OBSWebsocket
    {
        [TestMethod]
        public void NewMessageID_Test()
        {
            int idLength = 22;
            string msgID = NewMessageID(idLength);

            Assert.IsFalse(msgID.IsNullOrEmpty());
            Assert.AreEqual(idLength, msgID.Length);
        }

        [TestMethod]
        public void SceneListChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""scenes"": [ { ""sceneName"": ""Scene 1"" }, { ""sceneName"": ""Scene 2"" } ] } }");

            Types.Events.SceneListChangedEventArgs eventArgs = null;
            SceneListChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(SceneListChanged), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(2, eventArgs.Scenes.Count);
            Assert.AreEqual("Scene 1", (string)eventArgs.Scenes[0]["sceneName"]);
        }

        [TestMethod]
        public void SceneItemListReindexed_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""sceneName"": ""Scene 1"", ""sceneItems"": [ { ""sceneItemId"": 1 }, { ""sceneItemId"": 2 } ] } }");

            Types.Events.SceneItemListReindexedEventArgs eventArgs = null;
            SceneItemListReindexed += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(SceneItemListReindexed), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("Scene 1", eventArgs.SceneName);
            Assert.AreEqual(2, eventArgs.SceneItems.Count);
            Assert.AreEqual(1, (int)eventArgs.SceneItems[0]["sceneItemId"]);
        }

        [TestMethod]
        public void SceneCollectionListChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""sceneCollections"": [ ""Collection 1"", ""Collection 2"" ] } }");

            Types.Events.SceneCollectionListChangedEventArgs eventArgs = null;
            SceneCollectionListChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(SceneCollectionListChanged), body);

            Assert.IsNotNull(eventArgs);
            CollectionAssert.AreEqual(new[] { "Collection 1", "Collection 2" }, eventArgs.SceneCollections);
        }

        [TestMethod]
        public void ProfileListChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""profiles"": [ ""Profile 1"", ""Profile 2"" ] } }");

            Types.Events.ProfileListChangedEventArgs eventArgs = null;
            ProfileListChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(ProfileListChanged), body);

            Assert.IsNotNull(eventArgs);
            CollectionAssert.AreEqual(new[] { "Profile 1", "Profile 2" }, eventArgs.Profiles);
        }

        [TestMethod]
        public void InputVolumeMeters_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""inputs"": [ { ""inputName"": ""Mic/Aux"" } ] } }");

            Types.Events.InputVolumeMetersEventArgs eventArgs = null;
            InputVolumeMeters += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(InputVolumeMeters), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(1, eventArgs.inputs.Count);
            Assert.AreEqual("Mic/Aux", (string)eventArgs.inputs[0]["inputName"]);
        }

        [TestMethod]
        public void HashEncode_Test()
        {
            string sourceText, expectedResult, result;

            // First test
            sourceText = "The quick brown fox jumps over the lazy dog.";
            expectedResult = "71N/JciVv6eCUmUpqbY9l6pjFWTV14nCt2VEjIY1+2w=";

            result = HashEncode(sourceText);

            Assert.AreEqual(expectedResult, result);

            // Second test : consecutive calls produce same output
            // for a given source text
            Assert.AreEqual(result, HashEncode(sourceText));

            // Third test : another source text
            sourceText = "Pack my box with five dozen liquor jugs.";
            expectedResult = "I+7lK59W6o/nkmE65aY5TE13mBTi/AfgndWWjUqu3cw=";

            result = HashEncode(sourceText);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
