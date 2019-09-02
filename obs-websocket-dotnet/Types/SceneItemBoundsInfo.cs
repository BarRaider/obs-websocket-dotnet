using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Information on scene item bounds
    /// </summary>
    public class SceneItemBoundsInfo
    {
        /// <summary>
        /// Alignment of the bounding box
        /// </summary>
        [JsonProperty(PropertyName = "alignment")]
        public int Alignnment { set; get; }

        /// <summary>
        /// Type of bounding box
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SceneItemBoundsType Type { set; get; }

        /// <summary>
        /// Width of the bounding box
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public double Width { set; get; }

        /// <summary>
        /// Height of the bounding box
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public double Height { set; get; }
    }
}