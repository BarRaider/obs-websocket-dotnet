using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Crop coordinates for a scene item
    /// </summary>
    public class SceneItemCropInfo
    {
        /// <summary>
        /// Top crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "top")]
        public int Top;

        /// <summary>
        /// Bottom crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "bottom")]
        public int Bottom;

        /// <summary>
        /// Left crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "left")]
        public int Left;

        /// <summary>
        /// Right crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "right")]
        public int Right;
    }
}