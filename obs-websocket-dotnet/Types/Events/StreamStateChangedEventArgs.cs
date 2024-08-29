using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.StreamStateChanged"/>
    /// </summary>
    public class StreamStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public OutputStateChanged OutputState { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="outputState">The output state data</param>
        public StreamStateChangedEventArgs(OutputStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}