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
        [JsonProperty(PropertyName = "name")]
        public string Name { internal set; get; }

        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { internal set; get; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public TransitionSettings(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Constructor for jsonconverter
        /// </summary>
        public TransitionSettings()
        {
        }

    }
}