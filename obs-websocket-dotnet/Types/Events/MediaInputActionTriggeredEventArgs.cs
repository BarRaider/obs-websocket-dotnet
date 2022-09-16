namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.MediaInputActionTriggered"/>
    /// </summary>
    public class MediaInputActionTriggeredEventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; } 
        
        /// <summary>
        /// Action performed on the input. See `ObsMediaInputAction` enum
        /// </summary>
        public string MediaAction { get; }

        public MediaInputActionTriggeredEventArgs(string inputName, string mediaAction)
        {
            InputName = inputName;
            MediaAction = mediaAction;
        }
    }
}