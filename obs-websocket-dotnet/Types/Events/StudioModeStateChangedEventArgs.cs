using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.StudioModeStateChanged"/>
    /// </summary>
    public class StudioModeStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// New Studio Mode status
        /// </summary>
        public bool StudioModeEnabled { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="studioModeEnabled">Is studio mode enabled</param>
        public StudioModeStateChangedEventArgs(bool studioModeEnabled)
        {
            StudioModeEnabled = studioModeEnabled;
        }
    }
}