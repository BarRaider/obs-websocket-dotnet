using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Gets the Active and Showing state of a video source
    /// </summary>
    public class SourceActiveInfo
    {
        /// <summary>
        /// Whether the source is showing in Program
        /// </summary>
        [JsonPropertyName("videaActive")]
        public bool VideoActive { get; set; }

        /// <summary>
        /// Whether the source is showing in the UI (Preview, Projector, Properties)
        /// </summary>
        [JsonPropertyName("videoShowing")]
        public bool VideoShowing { get; set; }        /// <summary>
        /// Auto populate constructor
        /// </summary>
        /// <param name="data"></param>
        public SourceActiveInfo(JsonObject data)
        {
            VideoActive = data["videaActive"]?.GetValue<bool>() ?? false;
            VideoShowing = data["videoShowing"]?.GetValue<bool>() ?? false;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SourceActiveInfo() { }
    }
}
