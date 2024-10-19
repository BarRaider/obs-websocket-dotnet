using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Basic OBS video information
    /// </summary>
    public class ObsVideoSettings
    {
        /// <summary>
        /// Numerator of the fractional FPS value
        /// </summary>
        [JsonProperty(PropertyName = "fpsNumerator")]
        public double FpsNumerator { internal set; get; }

        /// <summary>
        /// Denominator of the fractional FPS value
        /// </summary>
        [JsonProperty(PropertyName = "fpsDenominator")]
        public double FpsDenominator { internal set; get; }

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
        /// Width of the output resolution in pixels
        /// </summary>
        [JsonProperty(PropertyName = "outputWidth")]
        public int OutputWidth { internal set; get; }

        /// <summary>
        /// Height of the output resolution in pixels
        /// </summary>
        [JsonProperty(PropertyName = "outputHeight")]
        public int OutputHeight { internal set; get; }
    }
}