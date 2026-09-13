using System;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SourceFilterSettingsChanged"/>
    /// </summary>
    public class SourceFilterSettingsChangedEventArgs : EventArgs
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
        /// New settings object of the filter
        /// </summary>
        public JObject FilterSettings { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sourceName">The source name</param>
        /// <param name="filterName">The filter name</param>
        /// <param name="filterSettings">The new settings object of the filter</param>
        public SourceFilterSettingsChangedEventArgs(string sourceName, string filterName, JObject filterSettings)
        {
            SourceName = sourceName;
            FilterName = filterName;
            FilterSettings = filterSettings;
        }
    }
}
