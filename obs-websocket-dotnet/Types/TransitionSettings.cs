using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Current transition settings
    /// </summary>
    public class TransitionSettings
    {
        /// <summary>
        /// Transition name
        /// </summary>
        [JsonProperty(PropertyName = "transitionName")]
        public string Name { internal set; get; }

        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        [JsonProperty(PropertyName = "transitionDuration")]
        public int? Duration { internal set; get; }

        /// <summary>
        /// Kind of the transition
        /// </summary>
        [JsonProperty(PropertyName = "transitionKind")]
        public string Kind { internal set; get; }

        /// <summary>
        /// Whether the transition uses a fixed (unconfigurable) duration
        /// </summary>
        [JsonProperty(PropertyName = "transitionFixed")]
        public bool IsFixed { internal set; get; }

        /// <summary>
        /// Whether the transition supports being configured
        /// </summary>
        [JsonProperty(PropertyName = "transitionConfigurable")]
        public bool IsConfigurable { internal set; get; }

        /// <summary>
        /// Object of settings for the transition. 'null' if transition is not configurable
        /// </summary>
        [JsonProperty(PropertyName = "transitionSettings")]
        public JObject Settings { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public TransitionSettings(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public TransitionSettings() { }

    }
}