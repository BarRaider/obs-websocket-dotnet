namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemEnableStateChanged"/>
    /// </summary>
    public class SceneItemEnableStateChangedEventArgs
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
        /// Whether the scene item is enabled (visible)
        /// </summary>
        public bool SceneItemEnabled { get; }

        public SceneItemEnableStateChangedEventArgs(string sceneName, int sceneItemId, bool sceneItemEnabled)
        {
            SceneName = sceneName;
            SceneItemId = sceneItemId;
            SceneItemEnabled = sceneItemEnabled;
        }
    }
}