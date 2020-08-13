using Newtonsoft.Json;

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
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; } = null!;

        /// <summary>
        /// Type of filter
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; } = null!;
    }
}