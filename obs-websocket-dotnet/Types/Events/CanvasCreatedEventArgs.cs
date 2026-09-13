using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CanvasCreated"/>
    /// </summary>
    public class CanvasCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the new canvas
        /// </summary>
        public string CanvasName { get; }

        /// <summary>
        /// UUID of the new canvas
        /// </summary>
        public string CanvasUuid { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="canvasName">The canvas name</param>
        /// <param name="canvasUuid">The canvas UUID</param>
        public CanvasCreatedEventArgs(string canvasName, string canvasUuid)
        {
            CanvasName = canvasName;
            CanvasUuid = canvasUuid;
        }
    }
}
