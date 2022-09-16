namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.MediaInputPlaybackEnded"/>
    /// </summary>
    public class MediaInputPlaybackEndedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        public MediaInputPlaybackEndedEventArgs(string inputName)
        {
            InputName = inputName;
        }
    }
}