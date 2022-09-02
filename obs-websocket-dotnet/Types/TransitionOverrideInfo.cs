using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Scene transition override settings
    /// </summary>
    public class TransitionOverrideInfo
    {
        /// <summary>
        /// Name of the current overriding transition. Empty string if no override is set.
        /// </summary>
        [JsonProperty(PropertyName = "transitionName")]
        public string Name { internal set; get; }

        /// <summary>
        /// Transition duration in milliseconds. -1 if no override is set.
        /// </summary>
        [JsonProperty(PropertyName = "transitionDuration")]
        public int Duration { internal set; get; }
    }
}
