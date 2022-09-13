namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputVolumeChanged"/>
    /// </summary>
    public class InputVolumeChangedEventArgs
    {
        /// <summary>
        /// Current volume levels of source
        /// </summary>
        public InputVolume Volume { get; }

        public InputVolumeChangedEventArgs(InputVolume volume)
        {
            Volume = volume;
        }
    }
}