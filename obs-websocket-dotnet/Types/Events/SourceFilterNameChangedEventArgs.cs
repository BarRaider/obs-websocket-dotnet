namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterNameChanged"/>
    /// </summary>
    public class SourceFilterNameChangedEventArgs
    {
        /// <summary>
        /// The source the filter is on
        /// </summary>
        public string SourceName { get; } 
        
        /// <summary>
        /// Old name of the filter
        /// </summary>
        public string OldFilterName { get; } 
        
        /// <summary>
        /// New name of the filter
        /// </summary>
        public string FilterName { get; }

        public SourceFilterNameChangedEventArgs(string sourceName, string oldFilterName, string filterName)
        {
            SourceName = sourceName;
            OldFilterName = oldFilterName;
            FilterName = filterName;
        }
    }
}