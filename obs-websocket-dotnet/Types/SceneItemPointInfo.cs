using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Scene item point information
    /// </summary>
    public class SceneItemPointInfo
    {
        /// <summary>
        /// The scale filter of the source
        /// </summary>
        [JsonProperty(PropertyName = "filter")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SceneItemScaleFilterType Filter { get; set; }

        /// <summary>
        /// The x-scale factor of the scene item
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public double X { get; set; }

        /// <summary>
        /// The y-scale factor of the scene item
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public double Y { get; set; }
    }
}