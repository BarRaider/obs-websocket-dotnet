using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    public class SourceActiveInfo
    {
        /// <summary>
        /// Whether the source is showing in Program
        /// </summary>
        [JsonProperty(PropertyName = "videaActive")]
        public bool VideoActive { get; set; }

        /// <summary>
        /// Whether the source is showing in the UI (Preview, Projector, Properties)
        /// </summary>
        [JsonProperty(PropertyName = "videoShowing")]
        public bool VideoShowing { get; set; }

        public SourceActiveInfo(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        public SourceActiveInfo() { }
    }
}
