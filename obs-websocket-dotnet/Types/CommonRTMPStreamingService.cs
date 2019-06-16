﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Common RTMP settings (predefined streaming services list)
    /// </summary>
    public class CommonRTMPStreamingService
    {
        /// <summary>
        /// Streaming provider name
        /// </summary>
        [JsonProperty(PropertyName = "service")]
        public string ServiceName;

        /// <summary>
        /// Streaming server URL;
        /// </summary>
        [JsonProperty(PropertyName = "server")]
        public string ServerUrl;

        /// <summary>
        /// Stream key
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string StreamKey;

        /// <summary>
        /// Construct object from data provided by <see cref="StreamingService.Settings"/>
        /// </summary>
        /// <param name="settings"></param>
        public CommonRTMPStreamingService(JObject settings)
        {
            JsonConvert.PopulateObject(settings.ToString(), this);
        }

        /// <summary>
        /// Convert to JSON object
        /// </summary>
        /// <returns></returns>
        public JObject ToJSONOLD()
        {
            var obj = new JObject();
            obj.Add("service", ServiceName);
            obj.Add("server", ServerUrl);
            obj.Add("key", StreamKey);
            return obj;
        }
    }
}