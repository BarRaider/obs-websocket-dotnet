using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Response from <see cref="OBSWebsocket.GetTransitionKindListInfo"/>
    /// </summary>
    public class GetTransitionKindListInfo
    {
        /// <summary>
        /// Array of transition kinds
        /// </summary>
        [JsonProperty(PropertyName = "transitionKinds")]
        public List<string> Kinds { set; get; }

    }
}
