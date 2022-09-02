namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes the state of an output (streaming or recording)
    /// </summary>
    public enum OutputState
    {
        /// <summary>
        /// The output is initializing and doesn't produce frames yet
        /// </summary>
        OBS_WEBSOCKET_OUTPUT_STARTING,

        /// <summary>
        /// The output is running and produces frames
        /// </summary>
        OBS_WEBSOCKET_OUTPUT_STARTED,

        /// <summary>
        /// The output is stopping and sends the last remaining frames in its buffer
        /// </summary>
        OBS_WEBSOCKET_OUTPUT_STOPPING,

        /// <summary>
        /// The output is completely stopped
        /// </summary>
        OBS_WEBSOCKET_OUTPUT_STOPPED,

        /// <summary>
        /// The output is paused (usually recording output)
        /// </summary>
        OBS_WEBSOCKET_OUTPUT_PAUSED,

        /// <summary>
        /// The output is resumed (i.e. no longer paused) - usually recording output
        /// </summary>
        OBS_WEBSOCKET_OUTPUT_RESUMED
    }
}