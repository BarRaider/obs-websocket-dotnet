using Newtonsoft.Json;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Get Scene Info response
    /// </summary>
    public class GetSceneListInfo
    {
        /// <summary>
        /// Name of the currently active scene
        /// </summary>
        [JsonProperty(PropertyName = "current-scene")]
        public string CurrentScene { set; get; }

        /// <summary>
        /// Ordered list of the current profile's scenes
        /// </summary>
        [JsonProperty(PropertyName = "scenes")]
        public List<OBSScene> Scenes { set; get; }
    }
}