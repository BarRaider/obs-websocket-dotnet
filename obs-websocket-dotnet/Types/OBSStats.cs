using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// OBS Stats
    /// </summary>
    public class OBSStats
    {
        /// <summary>
        /// Current framerate.
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public double FPS { set; get; }

        /// <summary>
        /// Number of frames rendered
        /// </summary>
        [JsonProperty(PropertyName = "render-total-frames")]
        public int RenderTotalFrames { set; get; }

        /// <summary>
        /// Number of frames missed due to rendering lag
        /// </summary>
        [JsonProperty(PropertyName = "render-missed-frames")]
        public int RenderMissedFrames { set; get; }

        /// <summary>
        /// Number of frames outputted
        /// </summary>
        [JsonProperty(PropertyName = "output-total-frames")]
        public int OutputTotalFrames { set; get; }

        /// <summary>
        /// Number of frames skipped due to encoding lag
        /// </summary>
        [JsonProperty(PropertyName = "output-skipped-frames")]
        public int OutputSkippedFrames { set; get; }

        /// <summary>
        /// Average frame render time (in milliseconds)
        /// </summary>
        [JsonProperty(PropertyName = "average-frame-time")]
        public double AverageFrameTime { set; get; }

        /// <summary>
        /// Current CPU usage (percentage)
        /// </summary>
        [JsonProperty(PropertyName = "cpu-usage")]
        public double CpuUsage { set; get; }

        /// <summary>
        /// Current RAM usage (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "memory-usage")]
        public double MemoryUsage { set; get; }

        /// <summary>
        /// Free recording disk space (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "free-disk-space")]
        public double FreeDiskSpace { set; get; }
    }
}