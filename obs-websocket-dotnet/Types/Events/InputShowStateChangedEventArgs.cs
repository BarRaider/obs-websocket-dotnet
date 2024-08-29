using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputShowStateChanged"/>
    /// </summary>
    public class InputShowStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Whether the input is showing
        /// </summary>
        public bool VideoShowing { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="videoShowing">Is the video showing</param>
        public InputShowStateChangedEventArgs(string inputName, bool videoShowing)
        {
            InputName = inputName;
            VideoShowing = videoShowing;
        }
    }
}