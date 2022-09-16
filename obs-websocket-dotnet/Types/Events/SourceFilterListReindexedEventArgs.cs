using System.Collections.Generic;
using OBSWebsocketDotNet;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterListReindexed"/>
    /// </summary>
    public class SourceFilterListReindexedEventArgs
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        public string SourceName { get; }
        
        /// <summary>
        /// Array of filter objects
        /// </summary>
        public List<FilterReorderItem> Filters { get; }

        public SourceFilterListReindexedEventArgs(string sourceName, List<FilterReorderItem> filters)
        {
            SourceName = sourceName;
            Filters = filters;
        }
    }
}