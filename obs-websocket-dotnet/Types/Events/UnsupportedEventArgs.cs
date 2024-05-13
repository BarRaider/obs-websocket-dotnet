using System;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for unsupported events
    /// </summary>
    public class UnsupportedEventArgs : EventArgs
    {
        public string EventType { get; }
        public JObject Body { get; }

        public UnsupportedEventArgs(string eventType, JObject body)
        {
            EventType = eventType;
            Body = body;
        }
    }
}
