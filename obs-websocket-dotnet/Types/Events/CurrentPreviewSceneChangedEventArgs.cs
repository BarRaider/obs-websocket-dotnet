namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentPreviewSceneChanged"/>
    /// </summary>
    public class CurrentPreviewSceneChangedEventArgs
    {
        /// <summary>
        /// Name of the scene that was switched to
        /// </summary>
        public string SceneName { get; }

        public CurrentPreviewSceneChangedEventArgs(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}