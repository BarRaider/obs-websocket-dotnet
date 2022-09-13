namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.RecordStateChanged"/>
    /// </summary>
    public class RecordStateChangedEventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public RecordStateChanged OutputState { get; }

        public RecordStateChangedEventArgs(RecordStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}