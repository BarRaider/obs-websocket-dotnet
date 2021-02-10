using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Contains an optional <see cref="IOBSLogger"/> instance used by <see cref="OBSWebsocketDotNet"/>.
    /// </summary>
    public static class OBSLogger
    {
        internal static IOBSLogger? logger;

        /// <summary>
        /// Settings used by the logger.
        /// </summary>
        public static OBSLoggerSettings LoggerSettings => logger?.LoggerSettings ?? OBSLoggerSettings.None;

        /// <summary>
        /// Sets the <see cref="IOBSLogger"/> that will receive all log messages from <see cref="OBSWebsocketDotNet"/>.
        /// </summary>
        /// <param name="obsLogger"></param>
        public static void SetLogger(IOBSLogger obsLogger)
        {
            logger = obsLogger;
        }

        internal static void Debug(string message) => logger?.Log(message, OBSLogLevel.Debug);
        internal static void Debug(Exception ex) => logger?.Log(ex, OBSLogLevel.Debug);
        internal static void Info(string message) => logger?.Log(message, OBSLogLevel.Info);
        internal static void Info(Exception ex) => logger?.Log(ex, OBSLogLevel.Info);
        internal static void Warning(string message) => logger?.Log(message, OBSLogLevel.Warning);
        internal static void Warning(Exception ex) => logger?.Log(ex, OBSLogLevel.Warning);
        internal static void Error(string message) => logger?.Log(message, OBSLogLevel.Error);
        internal static void Error(Exception ex) => logger?.Log(ex, OBSLogLevel.Error);

    }
}
