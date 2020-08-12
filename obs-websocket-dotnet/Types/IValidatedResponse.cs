using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    public interface IValidatedResponse
    {
        public bool ResponseValid { get; }
    }
}
