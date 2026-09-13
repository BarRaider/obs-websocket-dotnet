using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CanvasRemoved"/>
    /// </summary>
    public class CanvasRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the removed canvas
        /// </summary>
        public string CanvasName { get; }

        /// <summary>
        /// UUID of the removed canvas
        /// </summary>
        public string CanvasUuid { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="canvasName">The canvas name</param>
        /// <param name="canvasUuid">The canvas UUID</param>
        public CanvasRemovedEventArgs(string canvasName, string canvasUuid)
        {
            CanvasName = canvasName;
            CanvasUuid = canvasUuid;
        }
    }
}
