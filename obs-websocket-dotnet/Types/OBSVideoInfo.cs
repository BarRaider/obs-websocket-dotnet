using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Basic OBS video information
    /// </summary>
    public class OBSVideoInfo
    {
        /// <summary>
        /// Base (canvas) width
        /// </summary>
        [JsonProperty(PropertyName = "baseWidth")]
        public int BaseWidth { internal set; get; }

        /// <summary>
        /// Base (canvas) height
        /// </summary>
        [JsonProperty(PropertyName = "baseHeight")]
        public int BaseHeight { internal set; get; }

        /// <summary>
        /// Output width
        /// </summary>
        [JsonProperty(PropertyName = "outputWidth")]
        public int OutputWidth { internal set; get; }

        /// <summary>
        /// Output height
        /// </summary>
        [JsonProperty(PropertyName = "outputHeight")]
        public int OutputHeight { internal set; get; }

        /// <summary>
        /// Scaling method used if output size differs from base size
        /// </summary>
        [JsonProperty(PropertyName = "scaleType")]
        public string ScaleType { internal set; get; }

        /// <summary>
        /// Frames rendered per second
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public double FPS { internal set; get; }

        /// <summary>
        /// Video color format
        /// </summary>
        [JsonProperty(PropertyName = "videoFormat")]
        public string VideoFormat { internal set; get; }

        /// <summary>
        /// Color space for YUV
        /// </summary>
        [JsonProperty(PropertyName = "colorSpace")]
        public string ColorSpace { internal set; get; }

        /// <summary>
        /// Color range (full or partial)
        /// </summary>
        [JsonProperty(PropertyName = "colorRange")]
        public string ColorRange { internal set; get; }
    }
}