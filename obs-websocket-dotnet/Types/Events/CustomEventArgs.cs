using System;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CustomEvent"/>
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        /// <summary>
        /// Custom event data, as passed to BroadcastCustomEvent by the sender
        /// </summary>
        public JObject EventData { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="eventData">The custom event data</param>
        public CustomEventArgs(JObject eventData)
        {
            EventData = eventData;
        }
    }
}
