using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Base class for tests that need a real (loopback) OBSWebsocket connection with control
    /// over the server's responses. Inherits OBSWebsocket to reach protected members, matching
    /// the pattern used by the other test classes in this project.
    /// </summary>
    public abstract class ObsRequestTestBase : OBSWebsocket
    {
        /// <summary>
        /// The recorder/fake server for the current test. Set by <see cref="Arrange"/>.
        /// </summary>
        protected ObsRequestRecorder Server { get; private set; }

        [TestInitialize]
        public void ObsRequestTestBase_Initialize()
        {
            WSTimeout = TimeSpan.FromSeconds(5);
        }

        [TestCleanup]
        public void ObsRequestTestBase_Cleanup()
        {
            Disconnect();
            Server?.Dispose();
        }

        /// <summary>
        /// Starts a fake server, connects this instance to it, and waits for identification.
        /// </summary>
        /// <param name="responseData">responseData to reply with on the first request</param>
        protected void Arrange(JObject responseData = null)
        {
            Server = ObsRequestRecorder.StartAndConnect(this, responseData);
        }
    }
}
