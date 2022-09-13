namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemSelected"/>
    /// </summary>
    public class SceneItemSelectedEventArgs
    {
        /// <summary>
        /// Name of the scene item is in
        /// </summary>
        public string SceneName { get; }
        
        /// <summary>
        /// Numeric ID of the scene item
        /// </summary>
        public string SceneItemId { get; }

        public SceneItemSelectedEventArgs(string sceneName, string sceneItemId)
        {
            SceneName = sceneName;
            SceneItemId = sceneItemId;
        }
    }
}