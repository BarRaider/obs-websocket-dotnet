using System;
using System.Reflection;

namespace WebSocketSharp
{
    public static class Logging
    {
        public static void Disable(this Logger logger)
        {
            var field = logger.GetType().GetField("_output", BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(logger, new Action<LogData, string>((d, s) => { }));
        }
    }
}
