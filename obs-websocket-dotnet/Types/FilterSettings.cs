using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Filter settings
    /// </summary>
    public class FilterSettings
    {
        /// <summary>
        /// Name of the filter
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; }

        /// <summary>
        /// Type of the specified filter
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { set; get; }

        /// <summary>
        /// Settings for the filter
        /// </summary>
        [JsonProperty(PropertyName = "settings")]
        public JObject Settings { set; get; }
    }
}
