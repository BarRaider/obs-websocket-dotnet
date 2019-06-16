using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        [JsonProperty(PropertyName = "type")]
        public string Type;

        /// <summary>
        /// Streaming service settings (JSON data)
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public StreamingServiceSettings Settings;
    }
}
