using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Capabilities for a SourceType
    /// </summary>
    public class SourceTypeCapabilities
    {
        /// <summary>
        /// True if source of this type provide frames asynchronously
        /// </summary>
        [JsonProperty(PropertyName = "isAsync")]
        public bool IsAsync { set; get; }

        /// <summary>
        /// True if source of this type is deprecated
        /// </summary>
        [JsonProperty(PropertyName = "isDeprecated")]
        public bool IsDeprecated { set; get; }

        /// <summary>
        /// True if sources of this type provide video
        /// </summary>
        [JsonProperty(PropertyName = "hasVideo")]
        public bool HasVideo { set; get; }

        /// <summary>
        /// True if sources of this type provide audio
        /// </summary>
        [JsonProperty(PropertyName = "hasAudio")]
        public bool HasAudio { set; get; }

        /// <summary>
        /// True if interaction with this sources of this type is possible
        /// </summary>
        [JsonProperty(PropertyName = "canInteract")]
        public bool CanInteract { set; get; }

        /// <summary>
        /// True if sources of this type composite one or more sub-sources
        /// </summary>
        [JsonProperty(PropertyName = "isComposite")]
        public bool IsComposite { set; get; }

        /// <summary>
        /// True if sources of this type should not be fully duplicated
        /// </summary>
        [JsonProperty(PropertyName = "doNotDuplicate")]
        public bool DoNotDuplicate { set; get; }

        /// <summary>
        /// True if sources of this type may cause a feedback loop if it's audio is monitored and shouldn't be
        /// </summary>
        [JsonProperty(PropertyName = "doNotSelfMonitor")]
        public bool DoNotSelfMonitor { set; get; }
    }
}