using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet
{
    public interface IOBSLogger
    {
        OBSLoggerSettings LoggerSettings { get; }
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

    [Flags]
    public enum OBSLoggerSettings
    {
        /// <summary>
        /// Default option.
        /// </summary>
        None = 0,
        /// <summary>
        /// When <see cref="JsonEventArgs"/> are deserialized, log when additional data is available that was not deserialized. 
        /// </summary>
        LogExtraEventData = 1 << 0
    }
}
