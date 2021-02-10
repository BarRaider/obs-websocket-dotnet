using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Streaming settings
    /// </summary>
    public class StreamingService : IValidatedResponse
    {
        /// <inheritdoc/>
        public bool ResponseValid => !string.IsNullOrEmpty(Type) && Settings != null;
        /// <summary>
        /// Type of streaming service
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; } = null!;

        /// <summary>
        /// Streaming service settings (JSON data)
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "source")]
        public StreamingServiceSettings Settings { set; get; } = null!;
    }
}