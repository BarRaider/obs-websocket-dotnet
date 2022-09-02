using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Settings for a source item
    /// </summary>
    public class InputSettings
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string InputName { set; get; }

        /// <summary>
        /// Kind of source
        /// </summary>
        [JsonProperty(PropertyName = "inputKind")]
        public string InputKind { set; get; }

        /// <summary>
        /// Type of the specified source. Useful for type-checking if you expect a specific settings schema.
        /// </summary>
        [JsonProperty(PropertyName = "inputType")]
        public string InputType { set; get; }

        /// <summary>
        /// Settings for the source
        /// </summary>
        [JsonProperty(PropertyName = "inputSettings")]
        public JObject Settings { set; get; }

        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public InputSettings(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public InputSettings() { }
    }
}