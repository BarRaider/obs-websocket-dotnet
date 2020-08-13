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
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; } = null!;

        /// <summary>
        /// Non-unique source internal type(a.k.a type id)
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "typeId")]
        public string TypeID { set; get; } = null!;

        /// <summary>
        /// Source type.Value is one of the following: "input", "filter", "transition", "scene" or "unknown"
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; } = null!;
    }
}