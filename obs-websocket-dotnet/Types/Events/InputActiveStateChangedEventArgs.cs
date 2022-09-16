namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputActiveStateChanged"/>
    /// </summary>
    public class InputActiveStateChangedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Whether the input is active
        /// </summary>
        public bool VideoActive { get; }

        public InputActiveStateChangedEventArgs(string inputName, bool videoActive)
        {
            InputName = inputName;
            VideoActive = videoActive;
        }
    }
}