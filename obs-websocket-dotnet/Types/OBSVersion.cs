using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Version info of the plugin, the API and OBS Studio
    /// </summary>
    public class OBSVersion
    {
        /// <summary>
        /// obs-websocket plugin version
        /// </summary>
        [JsonProperty(PropertyName = "obs-websocket-version")]
        public readonly string PluginVersion;

        /// <summary>
        /// OBS Studio version
        /// </summary>
        [JsonProperty(PropertyName = "obs-studio-version")]
        public readonly string OBSStudioVersion;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSVersion(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}
