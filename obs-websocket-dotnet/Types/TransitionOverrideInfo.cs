using System.Text.Json;using System.Text.Json.Serialization;

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
        [JsonPropertyName("transitionName")]
        public string Name { internal set; get; }

        /// <summary>
        /// Transition duration in milliseconds. -1 if no override is set.
        /// </summary>
        [JsonPropertyName("transitionDuration")]
        public int Duration { internal set; get; }
    }
}
