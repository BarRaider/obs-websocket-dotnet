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
        [JsonProperty(PropertyName = "sourceName")]
        public string sourceName { set; get; }

        /// <summary>
        /// Kind of source
        /// </summary>
        [JsonProperty(PropertyName = "sourceKind")]
        public string SourceKind { set; get; }

        /// <summary>
        /// Type of the specified source. Useful for type-checking if you expect a specific settings schema.
        /// </summary>
        [JsonProperty(PropertyName = "sourceType")]
        public string sourceType { set; get; }

        /// <summary>
        /// Settings for the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceSettings")]
        public JObject sourceSettings { set; get; }

    }
}