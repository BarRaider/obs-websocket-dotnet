namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputAudioSyncOffsetChanged"/>
    /// </summary>
    public class InputAudioSyncOffsetChangedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// New sync offset in milliseconds
        /// </summary>
        public int InputAudioSyncOffset { get; }

        public InputAudioSyncOffsetChangedEventArgs(string inputName, int inputAudioSyncOffset)
        {
            InputName = inputName;
            InputAudioSyncOffset = inputAudioSyncOffset;
        }
    }
}