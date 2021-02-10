using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#if DISABLED
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
        [JsonRequired]
        [JsonProperty(PropertyName = "streaming")]
        public bool Streaming { internal set; get; }

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public bool Recording { internal set; get; }

        /// <summary>
        /// True if the replay buffer is active
        /// </summary>
        [JsonProperty(PropertyName = "replay-buffer-active")]
        public bool ReplayBufferActive { internal set; get; }

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
        /// Percentage of dropped frames
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
        /// Number of frames rendered
        /// </summary>
        [JsonProperty(PropertyName = "render-total-frames")]
        public int RenderTotalFrames { internal set; get; }

        /// <summary>
        /// Number of frames missed due to rendering lag
        /// </summary>
        [JsonProperty(PropertyName = "render-missed-frames")]
        public int RenderMissedFrames { internal set; get; }

        /// <summary>
        /// Number of frames outputted
        /// </summary>
        [JsonProperty(PropertyName = "output-total-frames")]
        public int OutputTotalFrames { internal set; get; }

        /// <summary>
        /// Total number of skipped frames
        /// </summary>
        [JsonProperty(PropertyName = "output-skipped-frames")]
        public int SkippedFrames { internal set; get; }

        /// <summary>
        /// Average frame time (in milliseconds)
        /// </summary>
        [JsonProperty(PropertyName = "average-frame-time")]
        public double AverageFrameTime { internal set; get; }

        /// <summary>
        /// Current CPU usage (percentage)
        /// </summary>
        [JsonProperty(PropertyName = "cpu-usage")]
        public double CPU { internal set; get; }

        /// <summary>
        /// Current RAM usage (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "memory-usage")]
        public double MemoryUsage { internal set; get; }


        /// <summary>
        /// Free recording disk space (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "free-disk-space")]
        public double FreeDiskSpace { internal set; get; }

        /// <summary>
        /// Overall stream time
        /// </summary>
        [JsonProperty(PropertyName = "stream-timecode")]
        public string StreamTime { internal set; get; } = null!;

        /// <summary>
        /// True if recording is paused, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording-paused")]
        public bool RecordingPaused { internal set; get; }
    }
}
#endif