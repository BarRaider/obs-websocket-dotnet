using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;

namespace OBSWebsocketDotNet.Tests
{
    /// <summary>
    /// Regression test for #142: GetInputAudioTracks always returned false for every track.
    /// The GetInputAudioTracks response nests the track states under an "inputAudioTracks" field
    /// (`{"inputAudioTracks": {"1": true, "2": false, ...}}`), but the response was being populated
    /// directly onto SourceTracks without unwrapping that field first, so none of the "1".."6"
    /// properties on the response ever matched.
    /// </summary>
    [TestClass]
    public class UnitTest_Request_InputAudioTracks : ObsRequestTestBase
    {
        [TestMethod]
        [Timeout(10000)]
        public void GetInputAudioTracks_UnwrapsInputAudioTracksField()
        {
            Arrange(new JObject
            {
                ["inputAudioTracks"] = new JObject
                {
                    ["1"] = true,
                    ["2"] = false,
                    ["3"] = false,
                    ["4"] = false,
                    ["5"] = false,
                    ["6"] = false
                }
            });

            SourceTracks tracks = GetInputAudioTracks("Mic/Aux");

            Server.AssertRequest(nameof(GetInputAudioTracks), new JObject { ["inputName"] = "Mic/Aux" });
            Assert.IsTrue(tracks.IsTrack1Active);
            Assert.IsFalse(tracks.IsTrack2Active);
            Assert.IsFalse(tracks.IsTrack3Active);
            Assert.IsFalse(tracks.IsTrack4Active);
            Assert.IsFalse(tracks.IsTrack5Active);
            Assert.IsFalse(tracks.IsTrack6Active);
        }
    }
}
