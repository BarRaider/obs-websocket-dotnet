using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Current transition settings
    /// </summary>
    public class TransitionSettings : IValidatedResponse
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public bool ResponseValid => !string.IsNullOrEmpty(Name);
        /// <summary>
        /// Transition name
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; internal set; }
    }
}