using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [JsonProperty(PropertyName = "outputActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public VirtualCamStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Constructor for jsonconverter
        /// </summary>
        public VirtualCamStatus()
        {
        }
    }
}
