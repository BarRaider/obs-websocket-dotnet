using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneCollectionListChanged"/>
    /// </summary>
    public class SceneCollectionListChangedEventArgs
    {
        /// <summary>
        /// Updated list of scene collections
        /// </summary>
        public List<string> SceneCollections { get; }

        public SceneCollectionListChangedEventArgs(List<string> sceneCollections)
        {
            SceneCollections = sceneCollections;
        }
    }
}