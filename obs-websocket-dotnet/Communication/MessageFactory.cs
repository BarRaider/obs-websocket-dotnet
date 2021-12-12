using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Communication
{
    internal static class MessageFactory
    {
        internal static JObject BuildMessage(MessageTypes opCode, string messageType, JObject additionalFields, out string messageId)
        {
            messageId = Guid.NewGuid().ToString();
            JObject payload = new JObject()
            {
                { "op", (int)opCode }
            };

            JObject data = new JObject();
            
            switch (opCode)
            {
                case MessageTypes.Request:
                    data.Add("requestType", messageType);
                    data.Add("requestId", messageId);
                    data.Add("requestData", additionalFields);
                    additionalFields = null;
                    break;
                case MessageTypes.RequestBatch:
                    data.Add("requestId", messageId);
                    break;

            }

            if (additionalFields != null)
            {
                data.Merge(additionalFields);
            }
            payload.Add("d", data);
            return payload;            
        }
    }
}
