using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Communication
{
    /// <summary>
    /// Close/Error codes sent by OBS Websocket when closing the connection
    /// </summary>
    public enum ObsCloseCodes
    {
        /// <summary>
        /// For internal use only to tell the request handler not to perform any close action.
        /// </summary>
        DontClose = 0,

        /// <summary>
        /// Unknown reason, should never be used.
        /// </summary>
        UnknownReason = 4000,

        /// <summary>
        /// The server was unable to decode the incoming websocket message.
        /// </summary>
        MessageDecodeError = 4002,

        /// <summary>
        /// A data field is required but missing from the payload.
        /// </summary>
        MissingDataField = 4003,

        /// <summary>
        /// A data field's value type is invalid.
        /// </summary>
        InvalidDataFieldType = 4004,

        /// <summary>
        /// A data field's value is invalid.
        /// </summary>
        InvalidDataFieldValue = 4005,

        /// <summary>
        /// The specified op was invalid or missing.
        /// </summary>
        UnknownOpCode = 4006,

        /// <summary>
        /// The client sent a websocket message without first sending Identify message.
        /// </summary>
        NotIdentified = 4007,

        /// <summary>
        /// The client sent an Identify message while already identified.
        /// </summary>
        AlreadyIdentified = 4008,

        /// <summary>
        /// The authentication attempt (via Identify) failed.
        /// </summary>
        AuthenticationFailed = 4009,

        /// <summary>
        /// The server detected the usage of an old version of the obs-websocket RPC protocol.
        /// </summary>
        UnsupportedRpcVersion = 4010,

        /// <summary>
        /// The websocket session has been invalidated by the obs-websocket server.
        /// </summary>
        SessionInvalidated = 4011,

        /// <summary>
        /// A requested feature is not supported due to hardware/software limitations.
        /// </summary>
        UnsupportedFeature = 4012
    }
}
