﻿namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes the state of an output (streaming or recording)
    /// </summary>
    public enum OutputState
    {
        /// <summary>
        /// OutputState could not be determined.
        /// </summary>
        Unknown,
        /// <summary>
        /// The output is initializing and doesn't produces frames yet
        /// </summary>
        Starting,

        /// <summary>
        /// The output is running and produces frames
        /// </summary>
        Started,

        /// <summary>
        /// The output is stopping and sends the last remaining frames in its buffer
        /// </summary>
        Stopping,

        /// <summary>
        /// The output is completely stopped
        /// </summary>
        Stopped,

        /// <summary>
        /// The output is paused. Only used for recording.
        /// </summary>
        Paused,

        /// <summary>
        /// The output is resumed from pause. Only used for recording.
        /// </summary>
        Resumed
    }
}