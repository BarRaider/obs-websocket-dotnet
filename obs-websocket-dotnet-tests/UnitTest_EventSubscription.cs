using Microsoft.VisualStudio.TestTools.UnitTesting;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_EventSubscription
    {
        [TestMethod]
        public void All_ExcludesHighVolumeEvents()
        {
            var all = EventSubscription.All;

            Assert.IsFalse(all.HasFlag(EventSubscription.InputVolumeMeters));
            Assert.IsFalse(all.HasFlag(EventSubscription.InputActiveStateChanged));
            Assert.IsFalse(all.HasFlag(EventSubscription.InputShowStateChanged));
            Assert.IsFalse(all.HasFlag(EventSubscription.SceneItemTransformChanged));
        }

        [TestMethod]
        public void All_IncludesEveryStandardCategory()
        {
            var all = EventSubscription.All;

            Assert.IsTrue(all.HasFlag(EventSubscription.General));
            Assert.IsTrue(all.HasFlag(EventSubscription.Config));
            Assert.IsTrue(all.HasFlag(EventSubscription.Scenes));
            Assert.IsTrue(all.HasFlag(EventSubscription.Inputs));
            Assert.IsTrue(all.HasFlag(EventSubscription.Transitions));
            Assert.IsTrue(all.HasFlag(EventSubscription.Filters));
            Assert.IsTrue(all.HasFlag(EventSubscription.Outputs));
            Assert.IsTrue(all.HasFlag(EventSubscription.SceneItems));
            Assert.IsTrue(all.HasFlag(EventSubscription.MediaInputs));
            Assert.IsTrue(all.HasFlag(EventSubscription.Vendors));
            Assert.IsTrue(all.HasFlag(EventSubscription.Ui));
            Assert.IsTrue(all.HasFlag(EventSubscription.Canvases));
        }

        [TestMethod]
        public void CanCombineAllWithAHighVolumeEvent()
        {
            var combined = EventSubscription.All | EventSubscription.InputVolumeMeters;

            Assert.IsTrue(combined.HasFlag(EventSubscription.All));
            Assert.IsTrue(combined.HasFlag(EventSubscription.InputVolumeMeters));
            Assert.IsFalse(combined.HasFlag(EventSubscription.InputActiveStateChanged));
        }

        [TestMethod]
        public void BitwiseRemove_ClearsOnlyTheTargetedFlag()
        {
            var subscription = EventSubscription.All | EventSubscription.InputVolumeMeters;
            subscription &= ~EventSubscription.InputVolumeMeters;

            Assert.IsTrue(subscription.HasFlag(EventSubscription.All));
            Assert.IsFalse(subscription.HasFlag(EventSubscription.InputVolumeMeters));
        }

        [TestMethod]
        public void MatchesObsWebsocketProtocolBitValues()
        {
            // https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#eventsubscription
            Assert.AreEqual((uint)0, (uint)EventSubscription.None);
            Assert.AreEqual((uint)(1 << 0), (uint)EventSubscription.General);
            Assert.AreEqual((uint)(1 << 1), (uint)EventSubscription.Config);
            Assert.AreEqual((uint)(1 << 2), (uint)EventSubscription.Scenes);
            Assert.AreEqual((uint)(1 << 3), (uint)EventSubscription.Inputs);
            Assert.AreEqual((uint)(1 << 4), (uint)EventSubscription.Transitions);
            Assert.AreEqual((uint)(1 << 5), (uint)EventSubscription.Filters);
            Assert.AreEqual((uint)(1 << 6), (uint)EventSubscription.Outputs);
            Assert.AreEqual((uint)(1 << 7), (uint)EventSubscription.SceneItems);
            Assert.AreEqual((uint)(1 << 8), (uint)EventSubscription.MediaInputs);
            Assert.AreEqual((uint)(1 << 9), (uint)EventSubscription.Vendors);
            Assert.AreEqual((uint)(1 << 10), (uint)EventSubscription.Ui);
            Assert.AreEqual((uint)(1 << 11), (uint)EventSubscription.Canvases);
            Assert.AreEqual((uint)0xFFF, (uint)EventSubscription.All);
            Assert.AreEqual((uint)(1 << 16), (uint)EventSubscription.InputVolumeMeters);
            Assert.AreEqual((uint)(1 << 17), (uint)EventSubscription.InputActiveStateChanged);
            Assert.AreEqual((uint)(1 << 18), (uint)EventSubscription.InputShowStateChanged);
            Assert.AreEqual((uint)(1 << 19), (uint)EventSubscription.SceneItemTransformChanged);
        }
    }
}
