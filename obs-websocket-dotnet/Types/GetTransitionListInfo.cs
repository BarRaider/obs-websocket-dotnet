using System.Text.Json;using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Response from <see cref="OBSWebsocket.GetSceneTransitionList"/>
    /// </summary>
    public class GetTransitionListInfo
    {
        /// <summary>
        /// Name of the currently active transition
        /// </summary>
        [JsonPropertyName("currentSceneTransitionName")]
        public string CurrentTransition { set; get; }

        /// <summary>
        /// Kind of the currently active transition
        /// </summary>
        [JsonPropertyName("currentSceneTransitionKind")]
        public string CurrentTransitionKing { set; get; }

        /// <summary>
        /// List of transitions.
        /// </summary>
        [JsonPropertyName("transitions")]
        public List<TransitionSettings> Transitions { set; get; }
    }
}
