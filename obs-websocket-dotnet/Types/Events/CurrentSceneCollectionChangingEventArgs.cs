using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneCollectionChanging"/>
    /// </summary>
    public class CurrentSceneCollectionChangingEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the current scene collection
        /// </summary>
        public string SceneCollectionName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneCollectionName">The scene collection name</param>
        public CurrentSceneCollectionChangingEventArgs(string sceneCollectionName)
        {
            SceneCollectionName = sceneCollectionName;
        }
    }
}