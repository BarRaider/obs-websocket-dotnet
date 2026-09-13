using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.RecordFileChanged"/>
    /// </summary>
    public class RecordFileChangedEventArgs : EventArgs
    {
        /// <summary>
        /// File name that the record output has begun writing to
        /// </summary>
        public string NewOutputPath { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="newOutputPath">The new output path</param>
        public RecordFileChangedEventArgs(string newOutputPath)
        {
            NewOutputPath = newOutputPath;
        }
    }
}
