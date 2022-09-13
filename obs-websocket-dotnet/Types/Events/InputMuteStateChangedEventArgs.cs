namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputMuteStateChanged"/>
    /// </summary>
    public class InputMuteStateChangedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Whether the input is muted
        /// </summary>
        public bool InputMuted { get; }

        public InputMuteStateChangedEventArgs(string inputName, bool inputMuted)
        {
            InputName = inputName;
            InputMuted = inputMuted;
        }
    }
}