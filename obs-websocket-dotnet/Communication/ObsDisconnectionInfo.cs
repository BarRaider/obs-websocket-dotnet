using System;
using System.Collections.Generic;
using System.Text;
using Websocket.Client;
namespace OBSWebsocketDotNet.Communication
{
    /// <summary>
    /// Disconnection information received from the OBS Websocket server
    /// </summary>
    public class ObsDisconnectionInfo
    {
        /// <summary>
        /// Close/Error codes sent by OBS Websocket when closing the connection
        /// </summary>
        public ObsCloseCodes ObsCloseCode { get; private set; }

        /// <summary>
        /// String reason of disconnect
        /// </summary>
        public string DisconnectReason { get; set; }

        /// <summary>
        /// Websocket Client internal information
        /// </summary>
        public Websocket.Client.DisconnectionInfo WebsocketDisconnectionInfo { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obsCloseCode">Close/Error codes sent by OBS Websocket when closing the connection</param>
        /// <param name="disconnectReason">String reason of disconnect</param>
        /// <param name="websocketDisconnectionInfo">Websocket Client internal information</param>
        public ObsDisconnectionInfo(ObsCloseCodes obsCloseCode, string disconnectReason, DisconnectionInfo websocketDisconnectionInfo)
        {
            ObsCloseCode = obsCloseCode;
            DisconnectReason = disconnectReason;
            WebsocketDisconnectionInfo = websocketDisconnectionInfo;
        }
    }
}
