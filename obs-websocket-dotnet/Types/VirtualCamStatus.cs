using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// VirtualCam Status
    /// </summary>
    public class VirtualCamStatus
    {
        /// <summary>
        /// Whether the output is active
        /// </summary>
        [JsonPropertyName("outputActive")]
        public bool IsActive { get; set; }        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JsonObject"/></param>
        public VirtualCamStatus(JsonObject data)
        {
            IsActive = data["outputActive"]?.GetValue<bool>() ?? false;
        }

        /// <summary>
        /// Constructor for jsonconverter
        /// </summary>
        public VirtualCamStatus()
        {
        }
    }
}
