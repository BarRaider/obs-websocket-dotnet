using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Scene item position information
    /// </summary>
    public class SceneItemPositionInfo
    {
        /// <summary>
        /// The point on the scene item that the item is manipulated from
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "alignment")]
        public int Alignment { set; get; }

        /// <summary>
        /// The x position of the scene item from the left
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "x")]
        public double X { set; get; }

        /// <summary>
        /// The y position of the scene item from the top
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "y")]
        public double Y { set; get; }
    }
}