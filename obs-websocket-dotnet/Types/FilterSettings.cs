using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Filter settings
    /// </summary>
    public class FilterSettings
    {
        /// <summary>
        /// Name of the filter
        /// </summary>
        [JsonProperty(PropertyName = "filterName")]
        public string Name { set; get; }

        /// <summary>
        /// Type of the specified filter
        /// </summary>
        [JsonProperty(PropertyName = "filterKind")]
        public string Type { set; get; }

        /// <summary>
        /// Index of the specified filter
        /// </summary>
        [JsonProperty(PropertyName = "filterIndex")]
        public int Index { set; get; }

        /// <summary>
        /// Status of the specified filter
        /// </summary>
        [JsonProperty(PropertyName = "filterEnabled")]
        public bool IsEnabled { set; get; }

        /// <summary>
        /// Settings for the filter
        /// </summary>
        [JsonProperty(PropertyName = "filterSettings")]
        public JObject Settings { set; get; }
    }
}