using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("filterName")]
        public string Name { set; get; }

        /// <summary>
        /// Type of the specified filter
        /// </summary>
        [JsonPropertyName("filterKind")]
        public string Kind { set; get; }

        /// <summary>
        /// Index of the filter in the list, beginning at 0
        /// </summary>
        [JsonPropertyName("filterIndex")]
        public int Index { get; set; }

        /// <summary>
        /// Status of the specified filter
        /// </summary>
        [JsonPropertyName("filterEnabled")]
        public bool IsEnabled { set; get; }

        /// <summary>
        /// Settings for the filter
        /// </summary>
        [JsonPropertyName("filterSettings")]
        public JsonObject Settings { set; get; }
    }
}