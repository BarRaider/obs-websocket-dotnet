using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputVolumeMeters"/>
    /// </summary>
    public class InputVolumeMetersEventArgs
    {
        /// <summary>
        /// Array of active inputs with their associated volume levels
        /// </summary>
        public IReadOnlyCollection<JObject> inputs { get; }

        public InputVolumeMetersEventArgs(IReadOnlyCollection<JObject> inputs)
        {
            this.inputs = inputs;
        }
    }
}