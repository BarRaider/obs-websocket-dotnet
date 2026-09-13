using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Request_Typed : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void GetOutputList_SendsNoRequestData_AndReturnsRawOutputs()
        {
            // The obs-websocket protocol documents the "outputs" field only as Array<Object>, with no
            // per-item field list - so, like GetGroupSceneItemList, this returns raw JObjects rather
            // than a guessed-at typed DTO.
            Arrange(new JObject { ["outputs"] = new JArray(new JObject { ["outputName"] = "virtualcam_output" }) });

            List<JObject> outputs = GetOutputList();

            Server.AssertRequest(nameof(GetOutputList), null);
            Assert.AreEqual(1, outputs.Count);
            Assert.AreEqual("virtualcam_output", (string)outputs[0]["outputName"]);
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetSceneItemSource_SendsSceneNameAndItemId_AndMapsResponse()
        {
            Arrange(new JObject { ["sourceName"] = "Webcam", ["sourceUuid"] = "abc-123" });

            SceneItemSource source = GetSceneItemSource("Scene 1", 5);

            Server.AssertRequest(nameof(GetSceneItemSource), new JObject { ["sceneName"] = "Scene 1", ["sceneItemId"] = 5 });
            Assert.AreEqual("Webcam", source.SourceName);
            Assert.AreEqual("abc-123", source.SourceUuid);
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetInputDeinterlaceMode_SendsInputName_AndReturnsMode()
        {
            Arrange(new JObject { ["inputDeinterlaceMode"] = "OBS_DEINTERLACE_MODE_YADIF" });

            string mode = GetInputDeinterlaceMode("Capture Card");

            Server.AssertRequest(nameof(GetInputDeinterlaceMode), new JObject { ["inputName"] = "Capture Card" });
            Assert.AreEqual("OBS_DEINTERLACE_MODE_YADIF", mode);
        }

        [TestMethod]
        [Timeout(10000)]
        public void SetInputDeinterlaceMode_WithString_SendsInputNameAndMode()
        {
            Arrange();

            SetInputDeinterlaceMode("Capture Card", "OBS_DEINTERLACE_MODE_YADIF");

            Server.AssertRequest(nameof(SetInputDeinterlaceMode), new JObject
            {
                ["inputName"] = "Capture Card",
                ["inputDeinterlaceMode"] = "OBS_DEINTERLACE_MODE_YADIF"
            });
        }

        [TestMethod]
        [Timeout(10000)]
        public void SetInputDeinterlaceMode_WithEnum_SendsInputNameAndMode()
        {
            Arrange();

            SetInputDeinterlaceMode("Capture Card", DeinterlaceMode.OBS_DEINTERLACE_MODE_YADIF_2X);

            Server.AssertRequest(nameof(SetInputDeinterlaceMode), new JObject
            {
                ["inputName"] = "Capture Card",
                ["inputDeinterlaceMode"] = "OBS_DEINTERLACE_MODE_YADIF_2X"
            });
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetInputDeinterlaceFieldOrder_SendsInputName_AndReturnsFieldOrder()
        {
            Arrange(new JObject { ["inputDeinterlaceFieldOrder"] = "OBS_DEINTERLACE_FIELD_ORDER_TOP" });

            string order = GetInputDeinterlaceFieldOrder("Capture Card");

            Server.AssertRequest(nameof(GetInputDeinterlaceFieldOrder), new JObject { ["inputName"] = "Capture Card" });
            Assert.AreEqual("OBS_DEINTERLACE_FIELD_ORDER_TOP", order);
        }

        [TestMethod]
        [Timeout(10000)]
        public void SetInputDeinterlaceFieldOrder_WithString_SendsInputNameAndOrder()
        {
            Arrange();

            SetInputDeinterlaceFieldOrder("Capture Card", "OBS_DEINTERLACE_FIELD_ORDER_BOTTOM");

            Server.AssertRequest(nameof(SetInputDeinterlaceFieldOrder), new JObject
            {
                ["inputName"] = "Capture Card",
                ["inputDeinterlaceFieldOrder"] = "OBS_DEINTERLACE_FIELD_ORDER_BOTTOM"
            });
        }

        [TestMethod]
        [Timeout(10000)]
        public void SetInputDeinterlaceFieldOrder_WithEnum_SendsInputNameAndOrder()
        {
            Arrange();

            SetInputDeinterlaceFieldOrder("Capture Card", DeinterlaceFieldOrder.OBS_DEINTERLACE_FIELD_ORDER_TOP);

            Server.AssertRequest(nameof(SetInputDeinterlaceFieldOrder), new JObject
            {
                ["inputName"] = "Capture Card",
                ["inputDeinterlaceFieldOrder"] = "OBS_DEINTERLACE_FIELD_ORDER_TOP"
            });
        }
    }
}
