using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Deserialization tests for the Canvases category, added to the obs-websocket protocol in v5.7.0.
    /// </summary>
    [TestClass]
    public class UnitTest_Events_Canvases : OBSWebsocket
    {
        [TestMethod]
        public void CanvasCreated_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""canvasName"": ""Vertical"", ""canvasUuid"": ""abc-123"" } }");

            Types.Events.CanvasCreatedEventArgs eventArgs = null;
            CanvasCreated += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(CanvasCreated), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("Vertical", eventArgs.CanvasName);
            Assert.AreEqual("abc-123", eventArgs.CanvasUuid);
        }

        [TestMethod]
        public void CanvasRemoved_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""canvasName"": ""Vertical"", ""canvasUuid"": ""abc-123"" } }");

            Types.Events.CanvasRemovedEventArgs eventArgs = null;
            CanvasRemoved += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(CanvasRemoved), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("Vertical", eventArgs.CanvasName);
            Assert.AreEqual("abc-123", eventArgs.CanvasUuid);
        }

        [TestMethod]
        public void CanvasNameChanged_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""canvasUuid"": ""abc-123"", ""oldCanvasName"": ""Old"", ""canvasName"": ""New"" } }");

            Types.Events.CanvasNameChangedEventArgs eventArgs = null;
            CanvasNameChanged += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(CanvasNameChanged), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("abc-123", eventArgs.CanvasUuid);
            Assert.AreEqual("Old", eventArgs.OldCanvasName);
            Assert.AreEqual("New", eventArgs.CanvasName);
        }
    }
}
