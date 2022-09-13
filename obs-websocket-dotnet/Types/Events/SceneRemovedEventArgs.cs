namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneRemoved"/>
    /// </summary>
    public class SceneRemovedEventArgs
    {
        /// <summary>
        /// Name of the removed scene
        /// </summary>
        public string SceneName { get; }
        
        /// <summary>
        /// Whether the removed scene was a group
        /// </summary>
        public bool IsGroup { get; }

        public SceneRemovedEventArgs(string sceneName, bool isGroup)
        {
            SceneName = sceneName;
            IsGroup = isGroup;
        }
    }
}