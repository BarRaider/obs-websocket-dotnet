using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Deserialization test for CustomEvent, emitted by BroadcastCustomEvent.
    ///
    /// The protocol's data-fields table lists a single "eventData: Object" field, which reads as if
    /// the payload is nested one level deeper (d.eventData.eventData). Verified against a live OBS
    /// 32.2.2 (obs-websocket 5.x) instance that this is NOT the case: the payload passed to
    /// BroadcastCustomEvent arrives directly as d.eventData, with no extra nesting - matching the
    /// existing VendorEvent dispatch, which has the same characteristic.
    /// </summary>
    [TestClass]
    public class UnitTest_Events_CustomEvent : OBSWebsocket
    {
        [TestMethod]
        public void CustomEvent_Test()
        {
            var body = JObject.Parse(@"{ ""eventData"": { ""testKey"": ""testValue"", ""nested"": { ""a"": 1 } } }");

            Types.Events.CustomEventArgs eventArgs = null;
            CustomEvent += (sender, e) => eventArgs = e;
            ProcessEventType(nameof(CustomEvent), body);

            Assert.IsNotNull(eventArgs);
            Assert.AreEqual("testValue", (string)eventArgs.EventData["testKey"]);
            Assert.AreEqual(1, (int)eventArgs.EventData["nested"]["a"]);
        }
    }
}
