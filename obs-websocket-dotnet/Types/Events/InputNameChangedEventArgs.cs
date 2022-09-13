namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputNameChanged"/>
    /// </summary>
    public class InputNameChangedEventArgs
    {
        /// <summary>
        /// Old name of the input
        /// </summary>
        public string OldInputName { get; }
        
        /// <summary>
        /// New name of the input
        /// </summary>
        public string InputName { get; }

        public InputNameChangedEventArgs(string oldInputName, string inputName)
        {
            OldInputName = oldInputName;
            InputName = inputName;
        }
    }
}