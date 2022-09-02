using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [JsonProperty(PropertyName = "videaActive")]
        public bool VideoActive { get; set; }

        /// <summary>
        /// Whether the source is showing in the UI (Preview, Projector, Properties)
        /// </summary>
        [JsonProperty(PropertyName = "videoShowing")]
        public bool VideoShowing { get; set; }

        /// <summary>
        /// Auto populate constructor
        /// </summary>
        /// <param name="data"></param>
        public SourceActiveInfo(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SourceActiveInfo() { }
    }
}
