using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("inputSettings")]
        public JsonObject Settings { set; get; }

        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public InputSettings(JsonObject data) : base(data)
        {
            JsonSerializer2.PopulateObject(data.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public InputSettings() { }
    }
}