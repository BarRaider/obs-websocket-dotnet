using System.Text.Json;using System.Text.Json.Serialization;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Filter list item
    /// </summary>
    public class FilterReorderItem
    {
        /// <summary>
        /// Name of filter
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { set; get; }

        /// <summary>
        /// Type of filter
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { set; get; }
    }
}