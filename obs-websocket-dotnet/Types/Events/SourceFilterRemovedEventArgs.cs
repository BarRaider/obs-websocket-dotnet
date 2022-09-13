namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterRemoved"/>
    /// </summary>
    public class SourceFilterRemovedEventArgs
    {
        /// <summary>
        /// Name of the source the filter was on
        /// </summary>
        public string SourceName { get; }
        
        /// <summary>
        /// Name of the filter
        /// </summary>
        public string FilterName { get; }

        public SourceFilterRemovedEventArgs(string sourceName, string filterName)
        {
            SourceName = sourceName;
            FilterName = filterName;
        }
    }
}