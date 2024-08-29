using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.MediaInputPlaybackStarted"/>
    /// </summary>
    public class MediaInputPlaybackStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        public MediaInputPlaybackStartedEventArgs(string inputName)
        {
            InputName = inputName;
        }
    }
}