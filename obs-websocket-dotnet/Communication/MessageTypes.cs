using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Communication
{
    internal enum MessageTypes
    {
        Hello = 0,
        Identify = 1,
        Identified = 2,
        ReIdentify = 3,
        Event = 5,
        Request = 6,
        RequestResponse = 7,
        RequestBatch = 8,
        RequestBatchResponse = 9
    }
}
