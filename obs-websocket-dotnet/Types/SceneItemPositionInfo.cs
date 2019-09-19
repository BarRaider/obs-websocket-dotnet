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
        [JsonProperty(PropertyName = "alignment")]
        public int Alingnment { set; get; }

        /// <summary>
        /// The x position of the scene item from the left
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public double X { set; get; }

        /// <summary>
        /// The y position of the scene item from the top
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public double Y { set; get; }
    }
}