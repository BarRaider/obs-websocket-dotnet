using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Scene item point information
    /// </summary>
    public class SceneItemPointInfo
    {
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