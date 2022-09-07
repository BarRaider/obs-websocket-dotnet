using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Settings for a source item
    /// </summary>
    public class InputSettings : Input
    {
        /// <summary>
        /// Settings for the source
        /// </summary>
        [JsonProperty(PropertyName = "inputSettings")]
        public JObject Settings { set; get; }

        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public InputSettings(JObject data) : base(data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public InputSettings() { }
    }
}