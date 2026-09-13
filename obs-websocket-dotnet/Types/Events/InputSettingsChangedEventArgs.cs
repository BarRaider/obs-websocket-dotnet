using System;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputSettingsChanged"/>
    /// </summary>
    public class InputSettingsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }

        /// <summary>
        /// UUID of the input
        /// </summary>
        public string InputUuid { get; }

        /// <summary>
        /// New settings object of the input
        /// </summary>
        public JObject InputSettings { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="inputUuid">The input UUID</param>
        /// <param name="inputSettings">The new settings object of the input</param>
        public InputSettingsChangedEventArgs(string inputName, string inputUuid, JObject inputSettings)
        {
            InputName = inputName;
            InputUuid = inputUuid;
            InputSettings = inputSettings;
        }
    }
}
