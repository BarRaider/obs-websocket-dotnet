namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemTransformChanged"/>
    /// </summary>
    public class SceneItemTransformEventArgs
    {
        /// <summary>
        /// Name of the scene item is in
        /// </summary>
        public string SceneName { get; } 
        
        /// <summary>
        /// Numeric ID of the scene item
        /// </summary>
        public string SceneItemId { get; } 
        
        /// <summary>
        /// Transform data
        /// </summary>
        public SceneItemTransformInfo Transform { get; }

        public SceneItemTransformEventArgs(string sceneName, string sceneItemId, SceneItemTransformInfo transform)
        {
            SceneName = sceneName;
            SceneItemId = sceneItemId;
            Transform = transform;
        }
    }
}