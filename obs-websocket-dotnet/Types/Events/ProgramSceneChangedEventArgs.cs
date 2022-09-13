namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProgramSceneChanged"/>
    /// </summary>
    public class ProgramSceneChangedEventArgs
    {
        /// <summary>
        /// The new scene name
        /// </summary>
        public string SceneName { get; }

        public ProgramSceneChangedEventArgs(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}