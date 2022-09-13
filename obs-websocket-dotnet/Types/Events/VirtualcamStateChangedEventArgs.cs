namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.VirtualcamStateChanged"/>
    /// </summary>
    public class VirtualcamStateChangedEventArgs
    {
        /// <summary>
        /// The specific state of the output
        /// </summary>
        public OutputStateChanged OutputState { get; }

        public VirtualcamStateChangedEventArgs(OutputStateChanged outputState)
        {
            OutputState = outputState;
        }
    }
}