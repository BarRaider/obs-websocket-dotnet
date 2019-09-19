using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Source information returned by GetSourcesList
    /// </summary>
    public class SourceInfo
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; }

        /// <summary>
        /// Non-unique source internal type(a.k.a type id)
        /// </summary>
        [JsonProperty(PropertyName = "typeId")]
        public string TypeID { set; get; }

        /// <summary>
        /// Source type.Value is one of the following: "input", "filter", "transition", "scene" or "unknown"
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; }
    }
}