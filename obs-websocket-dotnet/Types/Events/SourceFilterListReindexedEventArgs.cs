using System.Collections.Generic;

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
        public IReadOnlyCollection<FilterReorderItem> Filters { get; }

        public SourceFilterListReindexedEventArgs(string sourceName, IReadOnlyCollection<FilterReorderItem> filters)
        {
            SourceName = sourceName;
            Filters = filters;
        }
    }
}