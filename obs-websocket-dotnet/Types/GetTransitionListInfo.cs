using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Response from <see cref="OBSWebsocket.GetTransitionList"/>
    /// </summary>
    public class GetTransitionListInfo
    {
        /// <summary>
        /// Name of the currently active transition
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "current-transition")]
        public string CurrentTransition { set; get; } = null!;

        /// <summary>
        /// List of transitions.
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "transitions")]
        public List<TransitionSettings> Transitions { set; get; } = null!;
    }
}
