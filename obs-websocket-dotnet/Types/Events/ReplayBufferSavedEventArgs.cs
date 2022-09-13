namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ReplayBufferSaved"/>
    /// </summary>
    public class ReplayBufferSavedEventArgs
    {
        /// <summary>
        /// Path of the saved replay file
        /// </summary>
        public string SavedReplayPath { get; }

        public ReplayBufferSavedEventArgs(string savedReplayPath)
        {
            SavedReplayPath = savedReplayPath;
        }
    }
}