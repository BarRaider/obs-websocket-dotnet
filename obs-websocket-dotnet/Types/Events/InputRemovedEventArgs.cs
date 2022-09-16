namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputRemoved"/>
    /// </summary>
    public class InputRemovedEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        public InputRemovedEventArgs(string inputName)
        {
            InputName = inputName;
        }
    }
}