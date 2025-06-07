using System;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.VendorEvent"/>
    /// </summary>
    public class VendorEventArgs : EventArgs
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
        public JsonObject eventData { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="vendorName">The vendor name</param>
        /// <param name="eventType">The event type</param>
        /// <param name="eventData">The event data as a Json Object</param>
        public VendorEventArgs(string vendorName, string eventType, JsonObject eventData)
        {
            VendorName = vendorName;
            EventType = eventType;
            this.eventData = eventData;
        }
    }
}