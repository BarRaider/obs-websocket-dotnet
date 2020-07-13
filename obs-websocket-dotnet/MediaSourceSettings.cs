using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// 
    /// </summary>
    public class MediaSourceSettings
    {
        /// <summary>
        /// Source Name
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { get; set; }

        /// <summary>
        /// Source Type
        /// </summary>
        [JsonProperty(PropertyName = "sourceType")]
        public string SourceType { get; set; }

        /// <summary>
        /// Media settings
        /// </summary>
        [JsonProperty(PropertyName = "sourceSettings")]
        public FFMpegSourceSettings Media { get; set; }


    }
}
