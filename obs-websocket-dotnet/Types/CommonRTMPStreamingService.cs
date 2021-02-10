using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Common RTMP settings (predefined streaming services list)
    /// </summary>
    public class CommonRTMPStreamingService : IValidatedResponse
    {
        /// <inheritdoc/>
        public bool ResponseValid => !string.IsNullOrEmpty(ServiceName) && ServerUrl != null;
        /// <summary>
        /// Streaming provider name
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "service")]
        public string ServiceName = null!;

        /// <summary>
        /// Streaming server URL;
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "server")]
        public string ServerUrl = null!;

        /// <summary>
        /// Stream key
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string? StreamKey;
    }
}