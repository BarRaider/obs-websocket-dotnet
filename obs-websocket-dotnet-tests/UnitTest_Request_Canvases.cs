using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Request_Canvases : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void GetCanvasList_SendsNoRequestData_AndReturnsRawCanvases()
        {
            // Like GetOutputList, the obs-websocket protocol documents "canvases" only as
            // Array<Object> with no per-item field list, so these are returned as raw JObjects.
            Arrange(new JObject { ["canvases"] = new JArray(new JObject { ["canvasName"] = "Vertical" }) });

            List<JObject> canvases = GetCanvasList();

            Server.AssertRequest(nameof(GetCanvasList), null);
            Assert.AreEqual(1, canvases.Count);
            Assert.AreEqual("Vertical", (string)canvases[0]["canvasName"]);
        }
    }
}
