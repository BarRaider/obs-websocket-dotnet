using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemLockStateChanged"/>
    /// </summary>
    public class SceneItemLockStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the scene the item is in
        /// </summary>
        public string SceneName { get; } 
        
        /// <summary>
        /// Numeric ID of the scene item
        /// </summary>
        public int SceneItemId { get; }
        
        /// <summary>
        /// Whether the scene item is locked (visible)
        /// </summary>
        public bool SceneItemLocked { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneName">The scene name</param>
        /// <param name="sceneItemId">The scene item id</param>
        /// <param name="sceneItemLocked">is the scene item locked</param>
        public SceneItemLockStateChangedEventArgs(string sceneName, int sceneItemId, bool sceneItemLocked)
        {
            SceneName = sceneName;
            SceneItemId = sceneItemId;
            SceneItemLocked = sceneItemLocked;
        }
    }
}