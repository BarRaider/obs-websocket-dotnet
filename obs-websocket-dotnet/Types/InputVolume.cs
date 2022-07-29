using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Source volume values
    /// </summary>
    public class InputVolume
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string InputName { set; get; }
        /// <summary>
        /// The source volume in percent
        /// </summary>
        [JsonProperty(PropertyName = "inputVolumeMul")]
        public float InputVolumeMul { get; set; }
        /// <summary>
        /// The source volume in decibels
        /// </summary>
        [JsonProperty(PropertyName = "inputVolumeDb")]
        public float InputVolumeDb { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public InputVolume(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Empty constructor for jsonconvert
        /// </summary>
        public InputVolume() { }
    }
}