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
        public IReadOnlyCollection<JObject> Scenes { get; }

        public SceneListChangedEventArgs(IReadOnlyCollection<JObject> scenes)
        {
            Scenes = scenes;
        }
    }
}