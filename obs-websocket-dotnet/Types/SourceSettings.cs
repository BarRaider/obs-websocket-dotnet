using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Settings for a source item
    /// </summary>
    public class SourceSettings
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceName")]
        public string sourceName { set; get; } = null!;

        /// <summary>
        /// Kind of source
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceKind")]
        public string SourceKind { set; get; } = null!;

        /// <summary>
        /// Type of the specified source. Useful for type-checking if you expect a specific settings schema.
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceType")]
        public string sourceType { set; get; } = null!;

        /// <summary>
        /// Settings for the source
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceSettings")]
        public JObject sourceSettings { set; get; } = null!;

    }
}