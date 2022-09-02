using Newtonsoft.Json;

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
        [JsonProperty(PropertyName = "streamServiceType")]
        public string Type { set; get; }

        /// <summary>
        /// Streaming service settings (JSON data)
        /// </summary>
        [JsonProperty(PropertyName = "streamServiceSettings")]
        public StreamingServiceSettings Settings { set; get; }
    }
}