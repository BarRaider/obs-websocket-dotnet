using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Abstract class with information on a specific Input
    /// </summary>
    public abstract class Input
    {
        /// <summary>
        /// Name of the Input
        /// </summary>
        [JsonPropertyName("inputName")]
        public string InputName { get; set; }

        /// <summary>
        /// Kind of the Input
        /// </summary>
        [JsonPropertyName("inputKind")]
        public string InputKind { get; set; }

        /// <summary>
        /// Instantiate object from response data
        /// </summary>
        /// <param name="body"></param>
        public Input(JsonObject body)
        {
            JsonSerializer2.PopulateObject(body.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Input() { }
    }
}
