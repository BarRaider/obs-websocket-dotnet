using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Message received from the server
    /// </summary>
    internal class ServerMessage
    {
        /// <summary>
        /// Server Message's operation code
        /// </summary>
        [JsonProperty(PropertyName = "op")]
        public MessageTypes OperationCode { set; get; }

        /// <summary>
        /// Server Data
        /// </summary>
        [JsonProperty(PropertyName = "d")]
        public JObject Data { get; set; }
    }
}
