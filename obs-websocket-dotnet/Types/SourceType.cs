using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// OBS Source Type definitions
    /// </summary>
    public class SourceType : IValidatedResponse
    {
        /// <inheritdoc/>
        public bool ResponseValid => !string.IsNullOrEmpty(TypeID) && DisplayName != null;
        /// <summary>
        /// Non-unique internal source type ID
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "typeId")]
        public string TypeID { set; get; } = null!;

        /// <summary>
        /// Display name of the source type
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { set; get; } = null!;

        /// <summary>
        /// Type.Value is one of the following: "input", "filter", "transition" or "other"
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; } = null!;

        /// <summary>
        /// Default settings of the source type
        /// </summary>
        [JsonProperty(PropertyName = "defaultSettings")]
        public JObject? DefaultSettings { set; get; }

        /// <summary>
        /// Source type capabilities
        /// </summary>
        [JsonProperty(PropertyName = "caps")]
        public SourceTypeCapabilities? Capabilities { set; get; }
    }
}