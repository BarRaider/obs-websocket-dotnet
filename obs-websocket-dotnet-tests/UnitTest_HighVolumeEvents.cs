using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using OBSWebsocketDotNet.Types.Events;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_HighVolumeEvents : OBSWebsocket
    {
        [TestMethod]
        public void EventSubscriptions_DefaultsToAll()
        {
            Assert.AreEqual(EventSubscription.All, EventSubscriptions);
        }

        [TestMethod]
        public void SubscribingToInputVolumeMeters_SetsSubscriptionBit_WithoutThrowingWhenDisconnected()
        {
            EventHandler<InputVolumeMetersEventArgs> handler = (s, e) => { };

            InputVolumeMeters += handler;

            Assert.IsTrue(EventSubscriptions.HasFlag(EventSubscription.InputVolumeMeters));

            InputVolumeMeters -= handler;
        }

        [TestMethod]
        public void UnsubscribingLastHandler_ClearsSubscriptionBit()
        {
            EventHandler<InputVolumeMetersEventArgs> handler = (s, e) => { };
            InputVolumeMeters += handler;

            InputVolumeMeters -= handler;

            Assert.IsFalse(EventSubscriptions.HasFlag(EventSubscription.InputVolumeMeters));
        }

        [TestMethod]
        public void SecondHandler_DoesNotClearBitWhenFirstIsRemoved()
        {
            EventHandler<InputVolumeMetersEventArgs> handler1 = (s, e) => { };
            EventHandler<InputVolumeMetersEventArgs> handler2 = (s, e) => { };
            InputVolumeMeters += handler1;
            InputVolumeMeters += handler2;

            InputVolumeMeters -= handler1;

            Assert.IsTrue(EventSubscriptions.HasFlag(EventSubscription.InputVolumeMeters));

            InputVolumeMeters -= handler2;
            Assert.IsFalse(EventSubscriptions.HasFlag(EventSubscription.InputVolumeMeters));
        }

        [TestMethod]
        public void SubscribingToInputActiveStateChanged_SetsSubscriptionBit()
        {
            EventHandler<InputActiveStateChangedEventArgs> handler = (s, e) => { };

            InputActiveStateChanged += handler;
            Assert.IsTrue(EventSubscriptions.HasFlag(EventSubscription.InputActiveStateChanged));

            InputActiveStateChanged -= handler;
            Assert.IsFalse(EventSubscriptions.HasFlag(EventSubscription.InputActiveStateChanged));
        }

        [TestMethod]
        public void SubscribingToInputShowStateChanged_SetsSubscriptionBit()
        {
            EventHandler<InputShowStateChangedEventArgs> handler = (s, e) => { };

            InputShowStateChanged += handler;
            Assert.IsTrue(EventSubscriptions.HasFlag(EventSubscription.InputShowStateChanged));

            InputShowStateChanged -= handler;
            Assert.IsFalse(EventSubscriptions.HasFlag(EventSubscription.InputShowStateChanged));
        }

        [TestMethod]
        public void SubscribingToSceneItemTransformChanged_SetsSubscriptionBit()
        {
            EventHandler<SceneItemTransformEventArgs> handler = (s, e) => { };

            SceneItemTransformChanged += handler;
            Assert.IsTrue(EventSubscriptions.HasFlag(EventSubscription.SceneItemTransformChanged));

            SceneItemTransformChanged -= handler;
            Assert.IsFalse(EventSubscriptions.HasFlag(EventSubscription.SceneItemTransformChanged));
        }

        [TestMethod]
        public void ProcessEventType_InputVolumeMeters_RaisesTypedEvent()
        {
            var body = new JObject
            {
                {
                    "eventData", new JObject
                    {
                        {
                            "inputs", new JArray
                            {
                                new JObject
                                {
                                    { "inputName", "Mic/Aux" },
                                    { "inputLevelsMul", new JArray { new JArray { 0.1, 0.2, 0.3 } } }
                                }
                            }
                        }
                    }
                }
            };

            InputVolumeMetersEventArgs received = null;
            InputVolumeMeters += (sender, args) => received = args;

            ProcessEventType(nameof(InputVolumeMeters), body);

            Assert.IsNotNull(received);
            Assert.AreEqual(1, received.inputs.Count);
            Assert.AreEqual("Mic/Aux", received.inputs[0].InputName);
            Assert.AreEqual(0.1f, received.inputs[0].InputLevels[0].MagnitudeWithVolume);
        }
    }
}
