using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputVolumeMeters"/>
    /// </summary>
    public class InputVolumeMetersEventArgs : EventArgs
    {
        /// <summary>
        /// Array of active inputs with their associated volume levels
        /// </summary>
        public List<JObject> inputs { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputs">Collection inputs as JObjects</param>
        public InputVolumeMetersEventArgs(List<JObject> inputs)
        {
            this.inputs = inputs;
        }
    }
}