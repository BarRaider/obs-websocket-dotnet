namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ReplayBufferStateChanged"/>
    /// </summary>
    public class ReplayBufferStateChangedEventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public OutputStateChanged OutputState { get; }

        public ReplayBufferStateChangedEventArgs(OutputStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}