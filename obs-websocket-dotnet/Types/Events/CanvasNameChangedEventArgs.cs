using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CanvasNameChanged"/>
    /// </summary>
    public class CanvasNameChangedEventArgs : EventArgs
    {
        /// <summary>
        /// UUID of the canvas
        /// </summary>
        public string CanvasUuid { get; }

        /// <summary>
        /// Old name of the canvas
        /// </summary>
        public string OldCanvasName { get; }

        /// <summary>
        /// New name of the canvas
        /// </summary>
        public string CanvasName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="canvasUuid">The canvas UUID</param>
        /// <param name="oldCanvasName">The old canvas name</param>
        /// <param name="canvasName">The new canvas name</param>
        public CanvasNameChangedEventArgs(string canvasUuid, string oldCanvasName, string canvasName)
        {
            CanvasUuid = canvasUuid;
            OldCanvasName = oldCanvasName;
            CanvasName = canvasName;
        }
    }
}
