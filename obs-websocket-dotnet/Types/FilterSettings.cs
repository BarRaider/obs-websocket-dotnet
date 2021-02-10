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
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; } = null!;

        /// <summary>
        /// Type of the specified filter
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; } = null!;

        /// <summary>
        /// Status of the specified filter
        /// </summary>
        [JsonProperty(PropertyName = "enabled")]
        public bool IsEnabled { set; get; }

        /// <summary>
        /// Settings for the filter
        /// </summary>
        [JsonProperty(PropertyName = "settings")]
        public JObject? Settings { set; get; }
    }
}