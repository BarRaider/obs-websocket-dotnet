using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputAudioBalanceChanged"/>
    /// </summary>
    public class InputAudioBalanceChangedEventArgs : EventArgs 
    {   
        /// <summary>
        /// Name of the affected input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// New audio balance value of the input
        /// </summary>
        public double InputAudioBalance { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="inputAudioBalance">The input audio balance</param>
        public InputAudioBalanceChangedEventArgs(string inputName, double inputAudioBalance)
        {
            InputName = inputName;
            InputAudioBalance = inputAudioBalance;
        }
    }
}