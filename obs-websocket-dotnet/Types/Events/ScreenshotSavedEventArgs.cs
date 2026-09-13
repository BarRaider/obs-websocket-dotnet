using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ScreenshotSaved"/>
    /// </summary>
    public class ScreenshotSavedEventArgs : EventArgs
    {
        /// <summary>
        /// Path of the saved image file
        /// </summary>
        public string SavedScreenshotPath { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="savedScreenshotPath">The saved screenshot path</param>
        public ScreenshotSavedEventArgs(string savedScreenshotPath)
        {
            SavedScreenshotPath = savedScreenshotPath;
        }
    }
}
