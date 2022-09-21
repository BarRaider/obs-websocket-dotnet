using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterRemoved"/>
    /// </summary>
    public class SourceFilterRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the source the filter was on
        /// </summary>
        public string SourceName { get; }
        
        /// <summary>
        /// Name of the filter
        /// </summary>
        public string FilterName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sourceName">The source name</param>
        /// <param name="filterName">The filter name that's been removed</param>
        public SourceFilterRemovedEventArgs(string sourceName, string filterName)
        {
            SourceName = sourceName;
            FilterName = filterName;
        }
    }
}