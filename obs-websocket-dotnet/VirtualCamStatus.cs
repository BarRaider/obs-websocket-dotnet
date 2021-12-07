using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// VirtualCam Status
    /// </summary>
    public class VirtualCamStatus
    {
        /// <summary>
        /// The current virtual camera status
        /// </summary>
        [JsonProperty(PropertyName = "isVirtualCam")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Time elapsed since virtual camera started (only present if virtual cam currently active)
        /// </summary>
        [JsonProperty(PropertyName = "virtualCamTimecode")]
        public string Timecode { get; set; }

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
