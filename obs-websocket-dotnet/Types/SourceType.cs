using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// OBS SOurce Type definitions
    /// </summary>
    public class SourceType
    {
        /// <summary>
        /// Non-unique internal source type ID
        /// </summary>
        [JsonProperty(PropertyName = "typeId")]
        public string TypeID { set; get; }

        /// <summary>
        /// Display name of the source type
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { set; get; }

        /// <summary>
        /// Type.Value is one of the following: "input", "filter", "transition" or "other"
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; }

        /// <summary>
        /// Default settings of the source type
        /// </summary>
        [JsonProperty(PropertyName = "defaultSettings")]
        public JObject DefaultSettings { set; get; }

        /// <summary>
        /// Source type capabilities
        /// </summary>
        [JsonProperty(PropertyName = "caps")]
        public SourceTypeCapabilities Capabilities { set; get; }
    }
}