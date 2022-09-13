using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneListChanged"/>
    /// </summary>
    public class SceneListChangedEventArgs
    {
        /// <summary>
        /// Updated array of scenes
        /// </summary>
        public List<JObject> Scenes { get; }

        public SceneListChangedEventArgs(List<JObject> scenes)
        {
            Scenes = scenes;
        }
    }
}