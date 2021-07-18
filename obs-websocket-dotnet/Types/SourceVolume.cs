using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Source volume values
    /// </summary>
    public class SourceVolume
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; }
        /// <summary>
        /// The source volume in percent
        /// </summary>
        [JsonProperty(PropertyName = "volume")]
        public float Volume { get; set; }
        /// <summary>
        /// The source volume in decibels
        /// </summary>
        [JsonProperty(PropertyName = "volumeDb")]
        public float VolumeDb { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public SourceVolume(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Empty constructor for jsonconvert
        /// </summary>
        public SourceVolume() { }
    }
}