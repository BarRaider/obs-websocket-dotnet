using System.Text.Json;using System.Text.Json.Serialization;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Streaming settings
    /// </summary>
    public class StreamingService
    {
        /// <summary>
        /// Type of streaming service
        /// </summary>
        [JsonPropertyName("streamServiceType")]
        public string Type { set; get; }

        /// <summary>
        /// Streaming service settings (JSON data)
        /// </summary>
        [JsonPropertyName("streamServiceSettings")]
        public StreamingServiceSettings Settings { set; get; }
    }
}