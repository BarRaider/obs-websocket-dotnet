using System;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneCollectionListChanged"/>
    /// </summary>
    public class SceneCollectionListChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Updated list of scene collections
        /// </summary>
        public List<string> SceneCollections { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneCollections">Collection of scene collection names as string</param>
        public SceneCollectionListChangedEventArgs(List<string> sceneCollections)
        {
            SceneCollections = sceneCollections;
        }
    }
}