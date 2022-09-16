namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.StudioModeStateChanged"/>
    /// </summary>
    public class StudioModeStateChangedEventArgs
    {
        /// <summary>
        /// New Studio Mode status
        /// </summary>
        public bool StudioModeEnabled { get; }

        public StudioModeStateChangedEventArgs(bool studioModeEnabled)
        {
            StudioModeEnabled = studioModeEnabled;
        }
    }
}