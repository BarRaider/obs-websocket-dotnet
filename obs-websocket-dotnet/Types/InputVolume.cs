using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("inputName")]
        public string InputName { set; get; }
        /// <summary>
        /// The source volume in percent
        /// </summary>
        [JsonPropertyName("inputVolumeMul")]
        public float InputVolumeMul { get; set; }
        /// <summary>
        /// The source volume in decibels
        /// </summary>
        [JsonPropertyName("inputVolumeDb")]
        public float InputVolumeDb { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public InputVolume(JsonObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Empty constructor for jsonconvert
        /// </summary>
        public InputVolume() { }
    }
}