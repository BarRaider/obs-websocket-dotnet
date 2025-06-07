using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Volume settings of an OBS source
    /// </summary>
    public class VolumeInfo
    {
        /// <summary>
        /// Source volume in linear scale (0.0 to 1.0)
        /// </summary>
        [JsonPropertyName("inputVolumeMul")]
        public float VolumeMul { internal set; get; }

        /// <summary>
        /// Volume setting in dB
        /// </summary>
        [JsonPropertyName("inputVolumeDb")]
        public float VolumeDb { internal set; get; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public VolumeInfo(JsonObject data)
        {
            JsonSerializer2.PopulateObject(data.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public VolumeInfo() { }
    }
}