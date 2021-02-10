using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Custom RTMP settings (fully customizable RTMP credentials)
    /// </summary>
    public class CustomRTMPStreamingService : IValidatedResponse
    {
        /// <inheritdoc/>
        public bool ResponseValid => !string.IsNullOrEmpty(ServerAddress);
        /// <summary>
        /// RTMP server URL
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "server")]
        public string ServerAddress = null!;

        /// <summary>
        /// RTMP stream key (URL suffix)
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string? StreamKey;

        /// <summary>
        /// Tell OBS' RTMP client to authenticate to the server
        /// </summary>
        [JsonProperty(PropertyName = "use_auth")]
        public bool UseAuthentication;

        /// <summary>
        /// Username used if authentication is enabled
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string? AuthUsername;

        /// <summary>
        /// Password used if authentication is enabled
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string? AuthPassword;
    }
}