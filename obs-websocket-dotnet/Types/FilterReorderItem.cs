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
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; }

        /// <summary>
        /// Type of filter
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; }
    }
}