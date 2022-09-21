using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputVolumeChanged"/>
    /// </summary>
    public class InputVolumeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Current volume levels of source
        /// </summary>
        public InputVolume Volume { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="volume">The new input volume</param>
        public InputVolumeChangedEventArgs(InputVolume volume)
        {
            Volume = volume;
        }
    }
}