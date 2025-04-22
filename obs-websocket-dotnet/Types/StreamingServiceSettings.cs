using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Streaming server settings
    /// </summary>
    public class StreamingServiceSettings
    {
        /// <summary>
        /// The publish URL
        /// </summary>
        [JsonProperty(PropertyName = "server")]
        public string Server { set; get; }

        /// <summary>
        /// The publish key of the stream
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string Key { set; get; }

        /// <summary>
        /// Indicates whether authentication should be used when connecting to the streaming server
        /// </summary>
        [JsonProperty(PropertyName = "use_auth")]
        public bool UseAuth { set; get; }

        /// <summary>
        /// The username to use when accessing the streaming server. Only present if use-auth is true
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { set; get; }

        /// <summary>
        /// The password to use when accessing the streaming server. Only present if use-auth is true
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { set; get; }

        /// <summary>
        /// The service being used to stream
        /// </summary>
        [JsonProperty(PropertyName = "service")]
        public string Service { get; set; }

        /// <summary>
        /// The protocol to use for the stream
        /// </summary>
        [JsonProperty(PropertyName = "protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// Other values not covered by the class
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JToken> OtherValues { get; set; }
    }
}