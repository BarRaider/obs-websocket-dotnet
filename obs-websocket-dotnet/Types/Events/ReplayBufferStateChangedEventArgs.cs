using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ReplayBufferStateChanged"/>
    /// </summary>
    public class ReplayBufferStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public OutputStateChanged OutputState { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="outputState">Specific state of the output</param>
        public ReplayBufferStateChangedEventArgs(OutputStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}