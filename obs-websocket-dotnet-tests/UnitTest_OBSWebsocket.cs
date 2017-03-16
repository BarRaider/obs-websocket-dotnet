using Microsoft.VisualStudio.TestTools.UnitTesting;
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
