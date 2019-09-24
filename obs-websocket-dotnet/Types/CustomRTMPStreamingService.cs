using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Custom RTMP settings (fully customizable RTMP credentials)
    /// </summary>
    public class CustomRTMPStreamingService
    {
        /// <summary>
        /// RTMP server URL
        /// </summary>
        [JsonProperty(PropertyName = "server")]
        public string ServerAddress;

        /// <summary>
        /// RTMP stream key (URL suffix)
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string StreamKey;

        /// <summary>
        /// Tell OBS' RTMP client to authenticate to the server
        /// </summary>
        [JsonProperty(PropertyName = "use_auth")]
        public bool UseAuthentication;

        /// <summary>
        /// Username used if authentication is enabled
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string AuthUsername;

        /// <summary>
        /// Password used if authentication is enabled
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string AuthPassword;

        /// <summary>
        /// Construct object from data provided by <see cref="StreamingService.Settings"/>
        /// </summary>
        /// <param name="settings"></param>
        public CustomRTMPStreamingService(JObject settings)
        {
            JsonConvert.PopulateObject(settings.ToString(), this);
        }
    }
}