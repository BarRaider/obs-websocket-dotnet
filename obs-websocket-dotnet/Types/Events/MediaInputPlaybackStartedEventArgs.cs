namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.MediaInputPlaybackStarted"/>
    /// </summary>
    public class MediaInputPlaybackStartedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        public MediaInputPlaybackStartedEventArgs(string inputName)
        {
            InputName = inputName;
        }
    }
}