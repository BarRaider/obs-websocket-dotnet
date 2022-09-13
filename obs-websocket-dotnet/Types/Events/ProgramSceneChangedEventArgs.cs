namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProgramSceneChanged"/>
    /// </summary>
    public class ProgramSceneChangedEventArgs
    {
        public string SceneName { get; }

        public ProgramSceneChangedEventArgs(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}