using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Request_Record : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void SetRecordDirectory_SendsRecordDirectory()
        {
            Arrange();

            SetRecordDirectory(@"C:\Recordings");

            Server.AssertRequest(nameof(SetRecordDirectory), new JObject { ["recordDirectory"] = @"C:\Recordings" });
        }

        [TestMethod]
        [Timeout(10000)]
        public void SplitRecordFile_SendsNoRequestData()
        {
            Arrange();

            SplitRecordFile();

            Server.AssertRequest(nameof(SplitRecordFile), null);
        }

        [TestMethod]
        [Timeout(10000)]
        public void CreateRecordChapter_WithName_SendsChapterName()
        {
            Arrange();

            CreateRecordChapter("Chapter 1");

            Server.AssertRequest(nameof(CreateRecordChapter), new JObject { ["chapterName"] = "Chapter 1" });
        }

        [TestMethod]
        [Timeout(10000)]
        public void CreateRecordChapter_WithoutName_SendsNoRequestData()
        {
            Arrange();

            CreateRecordChapter();

            Server.AssertRequest(nameof(CreateRecordChapter), null);
        }
    }
}
