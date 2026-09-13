using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Request_Filters : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void GetSourceFilterKindList_SendsNoRequestData_AndReturnsKinds()
        {
            Arrange(new JObject { ["sourceFilterKinds"] = new JArray("color_filter", "chroma_key_filter") });

            List<string> kinds = GetSourceFilterKindList();

            Server.AssertRequest(nameof(GetSourceFilterKindList), null);
            CollectionAssert.AreEqual(new[] { "color_filter", "chroma_key_filter" }, kinds);
        }

        [TestMethod]
        [Timeout(10000)]
        public void GetSourceFilterKindList_EmptyResponse_ReturnsEmptyList()
        {
            Arrange(new JObject());

            List<string> kinds = GetSourceFilterKindList();

            Assert.AreEqual(0, kinds.Count);
        }
    }
}
