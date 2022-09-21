using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneCollectionChanged"/> 
    /// </summary>
    public class CurrentSceneCollectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the new scene collection
        /// </summary>
        public string SceneCollectionName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneCollectionName">The scene collection name</param>
        public CurrentSceneCollectionChangedEventArgs(string sceneCollectionName)
        {
            SceneCollectionName = sceneCollectionName;
        }
    }
}