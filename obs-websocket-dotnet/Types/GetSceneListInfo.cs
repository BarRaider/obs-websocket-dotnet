using System.Text.Json;using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Response from <see cref="OBSWebsocket.GetSceneList"/>
    /// </summary>
    public class GetSceneListInfo
    {
        /// <summary>
        /// Name of the currently active program scene
        /// </summary>
        [JsonPropertyName("currentProgramSceneName")]
        public string CurrentProgramSceneName { set; get; }

        /// <summary>
        /// Name of the currently active preview/studio scene
        /// Note: Will return null if not in studio mode
        /// </summary>
        [JsonPropertyName("currentPreviewSceneName")]
        public string CurrentPreviewSceneName { set; get; }

        /// <summary>
        /// Ordered list of the current profile's scenes
        /// </summary>
        [JsonPropertyName("scenes")]
        public List<SceneBasicInfo> Scenes { set; get; }
    }
}