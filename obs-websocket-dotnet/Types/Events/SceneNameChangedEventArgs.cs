namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneNameChanged"/>
    /// </summary>
    public class SceneNameChangedEventArgs
    {
        /// <summary>
        /// Old name of the scene
        /// </summary>
        public string OldSceneName { get; }
        
        /// <summary>
        /// New name of the scene
        /// </summary>
        public string SceneName { get; }

        public SceneNameChangedEventArgs(string oldSceneName, string sceneName)
        {
            OldSceneName = oldSceneName;
            SceneName = sceneName;
        }
    }
}