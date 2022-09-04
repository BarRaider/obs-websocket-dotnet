using System;
using System.Collections.Generic;
using System.Text;
using Websocket.Client;
namespace OBSWebsocketDotNet.Communication
{
    public class ObsDisconnectionInfo
    {
        public ObsCloseCodes ObsCloseCode { get; private set; }

        public string DisconnectReason { get; set; }
        public Websocket.Client.DisconnectionInfo WebsocketDisconnectionInfo { get; private set; }

        public ObsDisconnectionInfo(ObsCloseCodes obsCloseCode, string disconnectReason, DisconnectionInfo websocketDisconnectionInfo)
        {
            ObsCloseCode = obsCloseCode;
            DisconnectReason = disconnectReason;
            WebsocketDisconnectionInfo = websocketDisconnectionInfo;
        }
    }
}
