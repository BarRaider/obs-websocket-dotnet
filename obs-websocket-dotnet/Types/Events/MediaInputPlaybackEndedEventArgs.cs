using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.MediaInputPlaybackEnded"/>
    /// </summary>
    public class MediaInputPlaybackEndedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        public MediaInputPlaybackEndedEventArgs(string inputName)
        {
            InputName = inputName;
        }
    }
}