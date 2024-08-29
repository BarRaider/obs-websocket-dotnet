using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputNameChanged"/>
    /// </summary>
    public class InputNameChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Old name of the input
        /// </summary>
        public string OldInputName { get; }
        
        /// <summary>
        /// New name of the input
        /// </summary>
        public string InputName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="oldInputName">The old input name</param>
        /// <param name="inputName">The new input name</param>
        public InputNameChangedEventArgs(string oldInputName, string inputName)
        {
            OldInputName = oldInputName;
            InputName = inputName;
        }
    }
}