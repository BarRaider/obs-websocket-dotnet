using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputActiveStateChanged"/>
    /// </summary>
    public class InputActiveStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Whether the input is active
        /// </summary>
        public bool VideoActive { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="videoActive">Is the video active</param>
        public InputActiveStateChangedEventArgs(string inputName, bool videoActive)
        {
            InputName = inputName;
            VideoActive = videoActive;
        }
    }
}