using System.Collections.Generic;
using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("server")]
        public string Server { set; get; }

        /// <summary>
        /// The publish key of the stream
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { set; get; }

        /// <summary>
        /// Indicates whether authentication should be used when connecting to the streaming server
        /// </summary>
        [JsonPropertyName("use_auth")]
        public bool UseAuth { set; get; }

        /// <summary>
        /// The username to use when accessing the streaming server. Only present if use-auth is true
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { set; get; }

        /// <summary>
        /// The password to use when accessing the streaming server. Only present if use-auth is true
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { set; get; }

        /// <summary>
        /// The service being used to stream
        /// </summary>
        [JsonPropertyName("service")]
        public string Service { get; set; }

        /// <summary>
        /// The protocol to use for the stream
        /// </summary>
        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }        /// <summary>
        /// Other values not covered by the class
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement> OtherValues { get; set; }
    }
}