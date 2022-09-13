namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputShowStateChanged"/>
    /// </summary>
    public class InputShowStateChangedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Whether the input is showing
        /// </summary>
        public bool VideoShowing { get; }

        public InputShowStateChangedEventArgs(string inputName, bool videoShowing)
        {
            InputName = inputName;
            VideoShowing = videoShowing;
        }
    }
}