using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Information on a connected Monitor
    /// </summary>
    public class Monitor
    {
        /// <summary>
        /// Monitor height (px)
        /// </summary>
        [JsonProperty(PropertyName = "monitorHeight")]
        public int Height { get; set; }

        /// <summary>
        /// Monitor width (px)
        /// </summary>
        [JsonProperty(PropertyName = "monitorWidth")]
        public int Width { get; set; }

        /// <summary>
        /// Monitor Name
        /// </summary>
        [JsonProperty(PropertyName = "monitorName")]
        public string Name { get; set; }

        /// <summary>
        /// Monitor Index
        /// </summary>
        [JsonProperty(PropertyName = "monitorIndex")]
        public int Index { get; set; }

        /// <summary>
        /// Monitor Position X
        /// </summary>
        [JsonProperty(PropertyName = "monitorPositionX")]
        public int PositionX { get; set; }

        /// <summary>
        /// Monitor Position Y
        /// </summary>
        [JsonProperty(PropertyName = "monitorPositionY")]
        public int PositionY { get; set; }

        /// <summary>
        /// Constructor to auto populate
        /// </summary>
        /// <param name="data"></param>
        public Monitor (JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Monitor() { }
    }
}
