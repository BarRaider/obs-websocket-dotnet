using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputMuteStateChanged"/>
    /// </summary>
    public class InputMuteStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Whether the input is muted
        /// </summary>
        public bool InputMuted { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="inputMuted">Is the input muted</param>
        public InputMuteStateChangedEventArgs(string inputName, bool inputMuted)
        {
            InputName = inputName;
            InputMuted = inputMuted;
        }
    }
}