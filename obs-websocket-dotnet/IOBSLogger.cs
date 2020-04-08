using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet
{
    public interface IOBSLogger
    {
        void Log(string message, OBSLogLevel level);
        void Log(Exception ex, OBSLogLevel level);
    }

    public enum OBSLogLevel
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }
}
