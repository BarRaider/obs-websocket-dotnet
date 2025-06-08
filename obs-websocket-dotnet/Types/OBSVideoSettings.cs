using System.Text.Json;using System.Text.Json.Serialization;

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
        [JsonPropertyName("fpsNumerator")]
        public double FpsNumerator { set; get; }

        /// <summary>
        /// Denominator of the fractional FPS value
        /// </summary>
        [JsonPropertyName("fpsDenominator")]
        public double FpsDenominator { set; get; }

        /// <summary>
        /// Base (canvas) width
        /// </summary>
        [JsonPropertyName("baseWidth")]
        public int BaseWidth { set; get; }

        /// <summary>
        /// Base (canvas) height
        /// </summary>
        [JsonPropertyName("baseHeight")]
        public int BaseHeight { set; get; }

        /// <summary>
        /// Width of the output resolution in pixels
        /// </summary>
        [JsonPropertyName("outputWidth")]
        public int OutputWidth { set; get; }

        /// <summary>
        /// Height of the output resolution in pixels
        /// </summary>
        [JsonPropertyName("outputHeight")]
        public int OutputHeight { set; get; }
    }
}