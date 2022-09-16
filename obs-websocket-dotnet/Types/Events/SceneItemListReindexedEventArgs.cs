using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemListReindexed"/>
    /// </summary>
    public class SceneItemListReindexedEventArgs 
    {
        /// <summary>
        /// Name of the scene where items where reordered
        /// </summary>
        public string SceneName { get; } 

        /// <summary>
        /// List of all scene items as JObject
        /// </summary>
        public List<JObject> SceneItems { get; }

        public SceneItemListReindexedEventArgs(string sceneName, List<JObject> sceneItems)
        {
            SceneName = sceneName;
            SceneItems = sceneItems;
        }
    }
}