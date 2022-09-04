using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Communication
{
    public enum ObsCloseCodes
    {
        DontClose = 0,
        UnknownReason = 4000,
        MessageDecodeError = 4002,
        MissingDataField = 4003,
        InvalidDataFieldType = 4004,
        InvalidDataFieldValue = 4005,
        UnknownOpCode = 4006,
        NotIdentified = 4007,
        AlreadyIdentified = 4008,
        AuthenticationFailed = 4009,
        UnsupportedRpcVersion = 4010,
        SessionInvalidated = 4011
    }
}
