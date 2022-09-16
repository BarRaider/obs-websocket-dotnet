namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.StreamStateChanged"/>
    /// </summary>
    public class StreamStateChangedEventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public OutputStateChanged OutputState { get; }

        public StreamStateChangedEventArgs(OutputStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}