using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Deserialization tests for events added to the obs-websocket protocol after this library's
    /// initial 5.0 parity (v5.1.0 - v5.5.0). See UnitTest_Conformance for the dispatch-coverage gate
    /// that would otherwise let one of these silently fall through to UnsupportedEvent.
    /// </summary>
    [TestClass]
    public class UnitTest_Events_v5x : OBSWebsocket
    {
        [TestMethod]
        public void InputSettingsChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""inputName"": ""Mic/Aux"", ""inputUuid"": ""abc-123"", ""inputSettings"": { ""device_id"": ""default"" } } }");

            Types.Events.InputSettingsChangedEventArgs eventArgs = null;
            InputSettingsChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(InputSettingsChanged), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("Mic/Aux", eventArgs.InputName);
            Assert.AreEqual("abc-123", eventArgs.InputUuid);
            Assert.AreEqual("default", (string)eventArgs.InputSettings["device_id"]);
        }

        [TestMethod]
        public void SourceFilterSettingsChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""sourceName"": ""Webcam"", ""filterName"": ""Color Correction"", ""filterSettings"": { ""gamma"": 0.5 } } }");

            Types.Events.SourceFilterSettingsChangedEventArgs eventArgs = null;
            SourceFilterSettingsChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(SourceFilterSettingsChanged), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("Webcam", eventArgs.SourceName);
            Assert.AreEqual("Color Correction", eventArgs.FilterName);
            Assert.AreEqual(0.5, (double)eventArgs.FilterSettings["gamma"]);
        }

        [TestMethod]
        public void RecordFileChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""newOutputPath"": ""C:\\Recordings\\video_000.mkv"" } }");

            Types.Events.RecordFileChangedEventArgs eventArgs = null;
            RecordFileChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(RecordFileChanged), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(@"C:\Recordings\video_000.mkv", eventArgs.NewOutputPath);
        }

        [TestMethod]
        public void ScreenshotSaved_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""savedScreenshotPath"": ""C:\\Screenshots\\shot.png"" } }");

            Types.Events.ScreenshotSavedEventArgs eventArgs = null;
            ScreenshotSaved += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(ScreenshotSaved), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(@"C:\Screenshots\shot.png", eventArgs.SavedScreenshotPath);
        }
    }
}
