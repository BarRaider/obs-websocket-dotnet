using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.MediaInputActionTriggered"/>
    /// </summary>
    public class MediaInputActionTriggeredEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; } 
        
        /// <summary>
        /// Action performed on the input. See `ObsMediaInputAction` enum
        /// </summary>
        public string MediaAction { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="mediaAction">The media action data</param>
        public MediaInputActionTriggeredEventArgs(string inputName, string mediaAction)
        {
            InputName = inputName;
            MediaAction = mediaAction;
        }
    }
}