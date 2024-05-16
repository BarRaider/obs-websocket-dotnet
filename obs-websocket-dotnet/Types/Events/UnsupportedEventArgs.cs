using System;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for unsupported events
    /// </summary>
    public class UnsupportedEventArgs : EventArgs
    {
        /// <summary>
        /// The type of the event
        /// </summary>
        public string EventType { get; }
        /// <summary>
        /// The body of the event
        /// </summary>
        public JObject Body { get; }

        /// <summary>
        /// Event args for unsupported events
        /// </summary>
        public UnsupportedEventArgs(string eventType, JObject body)
        {
            EventType = eventType;
            Body = body;
        }
    }
}
