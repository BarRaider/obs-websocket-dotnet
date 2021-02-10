using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Interface for a logger that can be used by obs-websocket-dotnet.
    /// </summary>
    public interface IOBSLogger
    {
        /// <summary>
        /// Settings for the logger.
        /// </summary>
        OBSLoggerSettings LoggerSettings { get; }
        /// <summary>
        /// Logs a message with the given <see cref="OBSLogLevel"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        void Log(string message, OBSLogLevel level);
        /// <summary>
        /// Logs an exception with the given <see cref="OBSLogLevel"/>.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        void Log(Exception ex, OBSLogLevel level);
    }

    /// <summary>
    /// Log level associated with a log message.
    /// </summary>
    public enum OBSLogLevel
    {
        /// <summary>
        /// Used for debug messages
        /// </summary>
        Debug = 0,
        /// <summary>
        /// Used for info
        /// </summary>
        Info = 1,
        /// <summary>
        /// Used for warnings
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Used for errors
        /// </summary>
        Error = 3
    }

    /// <summary>
    /// Logger settings.
    /// </summary>
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
