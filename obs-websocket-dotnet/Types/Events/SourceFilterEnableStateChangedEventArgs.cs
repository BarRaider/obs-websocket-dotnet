using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterEnableStateChanged"/>
    /// </summary>
    public class SourceFilterEnableStateChangedEventArgs : EventArgs
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

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sourceName">The source name</param>
        /// <param name="filterName">The filter name</param>
        /// <param name="filterEnabled">If the filter is enabled or not</param>
        public SourceFilterEnableStateChangedEventArgs(string sourceName, string filterName, bool filterEnabled)
        {
            SourceName = sourceName;
            FilterName = filterName;
            FilterEnabled = filterEnabled;
        }
    }
}