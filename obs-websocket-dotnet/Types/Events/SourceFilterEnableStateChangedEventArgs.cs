namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterEnableStateChanged"/>
    /// </summary>
    public class SourceFilterEnableStateChangedEventArgs
    {
        /// <summary>
        /// Name of the source the filter is on
        /// </summary>
        public string SourceName { get; }
        
        /// <summary>
        /// Name of the filter
        /// </summary>
        public string FilterName { get; } 
        
        /// <summary>
        /// Whether the filter is enabled
        /// </summary>
        public bool FilterEnabled { get; }

        public SourceFilterEnableStateChangedEventArgs(string sourceName, string filterName, bool filterEnabled)
        {
            SourceName = sourceName;
            FilterName = filterName;
            FilterEnabled = filterEnabled;
        }
    }
}