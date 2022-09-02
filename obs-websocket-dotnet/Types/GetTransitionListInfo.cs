using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "currentSceneTransitionName")]
        public string CurrentTransition { set; get; }

        /// <summary>
        /// Kind of the currently active transition
        /// </summary>
        [JsonProperty(PropertyName = "currentSceneTransitionKind")]
        public string CurrentTransitionKing { set; get; }

        /// <summary>
        /// List of transitions.
        /// </summary>
        [JsonProperty(PropertyName = "transitions")]
        public List<TransitionSettings> Transitions { set; get; }
    }
}
