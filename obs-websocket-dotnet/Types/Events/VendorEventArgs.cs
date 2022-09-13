using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.VendorEvent"/>
    /// </summary>
    public class VendorEventArgs
    {
        /// <summary>
        /// Name of the vendor emitting the event
        /// </summary>
        public string VendorName { get; }
        
        /// <summary>
        /// Vendor-provided event typedef
        /// </summary>
        public string EventType { get; } 
        
        /// <summary>
        /// Vendor-provided event data. {} if event does not provide any data
        /// </summary>
        public JObject eventData { get; }

        public VendorEventArgs(string vendorName, string eventType, JObject eventData)
        {
            VendorName = vendorName;
            EventType = eventType;
            this.eventData = eventData;
        }
    }
}