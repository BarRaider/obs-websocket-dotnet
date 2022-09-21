using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputAudioSyncOffsetChanged"/>
    /// </summary>
    public class InputAudioSyncOffsetChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// New sync offset in milliseconds
        /// </summary>
        public int InputAudioSyncOffset { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="inputAudioSyncOffset">The input audio sync offset</param>
        public InputAudioSyncOffsetChangedEventArgs(string inputName, int inputAudioSyncOffset)
        {
            InputName = inputName;
            InputAudioSyncOffset = inputAudioSyncOffset;
        }
    }
}