using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Status of a Media Input
    /// </summary>
    public class MediaInputStatus
    {
        /// <summary>
        /// State of the media input
        /// </summary>
        [JsonProperty(PropertyName = "mediaState")]
        public string State { get; set; }

        /// <summary>
        /// Total duration of the playing media in milliseconds. `null` if not playing
        /// </summary>
        [JsonProperty(PropertyName = "mediaDuration")]
        public int? Duration { get; set; }

        /// <summary>
        /// Position of the cursor in milliseconds. `null` if not playing
        /// </summary>
        [JsonProperty(PropertyName = "mediaCursor")]
        public int Cursor { get; set; }

        /// <summary>
        /// Instantiate from JObject
        /// </summary>
        /// <param name="body"></param>
        public MediaInputStatus(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MediaInputStatus() { }
    }
}
