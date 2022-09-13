namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemLockStateChanged"/>
    /// </summary>
    public class SceneItemLockStateChangedEventArgs
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

        public SceneItemLockStateChangedEventArgs(string sceneName, int sceneItemId, bool sceneItemLocked)
        {
            SceneName = sceneName;
            SceneItemId = sceneItemId;
            SceneItemLocked = sceneItemLocked;
        }
    }
}