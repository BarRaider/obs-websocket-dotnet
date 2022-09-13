namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneCreated"/>
    /// </summary>
    public class SceneCreatedEventArgs
    {
        /// <summary>
        /// Name of the new scene
        /// </summary>
        public string SceneName { get; }
        
        /// <summary>
        /// Whether the new scene is a group
        /// </summary>
        public bool IsGroup { get; }

        public SceneCreatedEventArgs(string sceneName, bool isGroup)
        {
            SceneName = sceneName;
            IsGroup = isGroup;
        }
    }
}