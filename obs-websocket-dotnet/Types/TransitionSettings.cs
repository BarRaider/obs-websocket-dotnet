using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("transitionName")]
        public string Name { internal set; get; }

        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        [JsonPropertyName("transitionDuration")]
        public int? Duration { internal set; get; }

        /// <summary>
        /// Kind of the transition
        /// </summary>
        [JsonPropertyName("transitionKind")]
        public string Kind { internal set; get; }

        /// <summary>
        /// Whether the transition uses a fixed (unconfigurable) duration
        /// </summary>
        [JsonPropertyName("transitionFixed")]
        public bool IsFixed { internal set; get; }

        /// <summary>
        /// Whether the transition supports being configured
        /// </summary>
        [JsonPropertyName("transitionConfigurable")]
        public bool IsConfigurable { internal set; get; }

        /// <summary>
        /// Object of settings for the transition. 'null' if transition is not configurable
        /// </summary>
        [JsonPropertyName("transitionSettings")]
        public JsonObject Settings { get; set; }        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public TransitionSettings(JsonObject data)
        {
            JsonSerializer2.PopulateObject(data.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public TransitionSettings() { }

    }
}