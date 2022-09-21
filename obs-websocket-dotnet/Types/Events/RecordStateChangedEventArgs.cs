using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.RecordStateChanged"/>
    /// </summary>
    public class RecordStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public RecordStateChanged OutputState { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="outputState">The record state change data</param>
        public RecordStateChangedEventArgs(RecordStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}