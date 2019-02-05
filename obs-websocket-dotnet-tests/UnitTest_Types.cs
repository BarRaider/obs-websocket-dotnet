using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    [TestClass]
    public class UnitTest_Types
    {
        [TestMethod]
        public void OBSAuthInfo_BuildFromJSON()
        {
            string challenge = "pBWv82hj";
            string salt = "B9fL8CF7";

            var data = new JObject();
            data.Add("authRequired", true);
            data.Add("challenge", challenge);
            data.Add("salt", salt);

            var authInfo = new OBSAuthInfo(data);

            Assert.IsTrue(authInfo.AuthRequired);
            Assert.AreEqual(challenge, authInfo.Challenge);
            Assert.AreEqual(salt, authInfo.PasswordSalt);
        }

        [TestMethod]
        public void OBSVersion_BuildFromJSON()
        {
            string pluginVersion = "4.0.0";
            string obsVersion = "18.0.1";

            var data = new JObject();
            data.Add("obs-websocket-version", pluginVersion);
            data.Add("obs-studio-version", obsVersion);

            var version = new OBSVersion(data);

            Assert.AreEqual(pluginVersion, version.PluginVersion);
            Assert.AreEqual(obsVersion, version.OBSStudioVersion);
        }
    }
}
