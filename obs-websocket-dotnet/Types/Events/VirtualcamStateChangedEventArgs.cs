using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.VirtualcamStateChanged"/>
    /// </summary>
    public class VirtualcamStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public OutputStateChanged OutputState { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="outputState">The output state data</param>
        public VirtualcamStateChangedEventArgs(OutputStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}