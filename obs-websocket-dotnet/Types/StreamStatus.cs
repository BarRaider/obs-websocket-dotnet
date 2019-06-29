using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Data of a stream status update
    /// </summary>
    public class StreamStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "streaming")]
        public bool Streaming { internal set; get; }

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public bool Recording { internal set; get; }

        /// <summary>
        /// Stream bitrate in bytes per second
        /// </summary>
        [JsonProperty(PropertyName = "bytes-per-sec")]
        public int BytesPerSec { internal set; get; }

        /// <summary>
        /// Stream bitrate in kilobits per second
        /// </summary>
        [JsonProperty(PropertyName = "kbits-per-sec")]
        public int KbitsPerSec { internal set; get; }

        /// <summary>
        /// RTMP output strain
        /// </summary>
        [JsonProperty(PropertyName = "strain")]
        public float Strain { internal set; get; }

        /// <summary>
        /// Total time since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-time")]
        public int TotalStreamTime { internal set; get; }

        /// <summary>
        /// Number of frames sent since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "num-total-frames")]
        public int TotalFrames { internal set; get; }

        /// <summary>
        /// Overall number of frames dropped since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "num-dropped-frames")]
        public int DroppedFrames { internal set; get; }

        /// <summary>
        /// Current framerate in Frames Per Second
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public float FPS { internal set; get; }

        /// <summary>
        /// Builds the object from the JSON event body
        /// </summary>
        /// <param name="data">JSON event body as a <see cref="JObject"/></param>
        public StreamStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}