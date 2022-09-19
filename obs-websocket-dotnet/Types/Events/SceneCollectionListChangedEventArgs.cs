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
        public IReadOnlyCollection<string> SceneCollections { get; }

        public SceneCollectionListChangedEventArgs(IReadOnlyCollection<string> sceneCollections)
        {
            SceneCollections = sceneCollections;
        }
    }
}