using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Streaming settings
    /// </summary>
    public class StreamingService : IValidatedResponse
    {
        public bool ResponseValid => !string.IsNullOrEmpty(Type) && Settings != null;
        /// <summary>
        /// Type of streaming service
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; } = null!;

        /// <summary>
        /// Streaming service settings (JSON data)
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public StreamingServiceSettings Settings { set; get; } = null!;
    }
}