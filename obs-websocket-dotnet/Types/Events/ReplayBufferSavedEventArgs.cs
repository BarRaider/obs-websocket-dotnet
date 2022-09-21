using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ReplayBufferSaved"/>
    /// </summary>
    public class ReplayBufferSavedEventArgs : EventArgs
    {
        /// <summary>
        /// Path of the saved replay file
        /// </summary>
        public string SavedReplayPath { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="savedReplayPath">The saved replay path</param>
        public ReplayBufferSavedEventArgs(string savedReplayPath)
        {
            SavedReplayPath = savedReplayPath;
        }
    }
}