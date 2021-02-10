using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes a scene in OBS, along with its items
    /// </summary>
    public class OBSScene
    {
        /// <summary>
        /// OBS Scene name
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// Scene item list
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sources")]
        public List<SceneItem> Items = null!;
    }
}