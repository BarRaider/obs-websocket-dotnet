using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Settings for a source item
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SourceSettings
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; } = null!;

        /// <summary>
        /// Kind of source
        /// </summary>
        [JsonProperty(PropertyName = "sourceKind")]
        public string SourceKind { set; get; } = null!;

        /// <summary>
        /// Type of the specified source. Useful for type-checking if you expect a specific settings schema.
        /// </summary>
        [JsonProperty(PropertyName = "sourceType")]
        public string SourceType { set; get; } = null!;

        /// <summary>
        /// Settings for the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceSettings")]
        public JObject Settings { set; get; } = null!;

    }
}