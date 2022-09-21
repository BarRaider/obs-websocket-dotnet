using System;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterListReindexed"/>
    /// </summary>
    public class SourceFilterListReindexedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        public string SourceName { get; }
        
        /// <summary>
        /// Array of filter objects
        /// </summary>
        public List<FilterReorderItem> Filters { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sourceName">The source name</param>
        /// <param name="filters">Collection of filters</param>
        public SourceFilterListReindexedEventArgs(string sourceName, List<FilterReorderItem> filters)
        {
            SourceName = sourceName;
            Filters = filters;
        }
    }
}