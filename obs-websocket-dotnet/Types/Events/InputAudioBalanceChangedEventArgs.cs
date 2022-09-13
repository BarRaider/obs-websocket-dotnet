namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputAudioBalanceChanged"/>
    /// </summary>
    public class InputAudioBalanceChangedEventArgs
    {   
        /// <summary>
        /// Name of the affected input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// New audio balance value of the input
        /// </summary>
        public double InputAudioBalance { get; }

        public InputAudioBalanceChangedEventArgs(string inputName, double inputAudioBalance)
        {
            InputName = inputName;
            InputAudioBalance = inputAudioBalance;
        }
    }
}