using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// OBS Stats
    /// </summary>
    public class ObsStats
    {
        /// <summary>
        /// Current framerate.
        /// </summary>
        [JsonProperty(PropertyName = "activeFps")]
        public double FPS { set; get; }

        /// <summary>
        /// Number of frames rendered
        /// </summary>
        [JsonProperty(PropertyName = "renderTotalFrames")]
        public long RenderTotalFrames { set; get; }

        /// <summary>
        /// Number of frames missed due to rendering lag
        /// </summary>
        [JsonProperty(PropertyName = "renderSkippedFrames")]
        public long RenderMissedFrames { set; get; }

        /// <summary>
        /// Number of frames outputted
        /// </summary>
        [JsonProperty(PropertyName = "outputTotalFrames")]
        public long OutputTotalFrames { set; get; }

        /// <summary>
        /// Number of frames skipped due to encoding lag
        /// </summary>
        [JsonProperty(PropertyName = "outputSkippedFrames")]
        public long OutputSkippedFrames { set; get; }

        /// <summary>
        /// Average frame render time (in milliseconds)
        /// </summary>
        [JsonProperty(PropertyName = "averageFrameRenderTime")]
        public double AverageFrameTime { set; get; }

        /// <summary>
        /// Current CPU usage (percentage)
        /// </summary>
        [JsonProperty(PropertyName = "cpuUsage")]
        public double CpuUsage { set; get; }

        /// <summary>
        /// Current RAM usage (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "memoryUsage")]
        public double MemoryUsage { set; get; }

        /// <summary>
        /// Free recording disk space (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "availableDiskSpace")]
        public double FreeDiskSpace { set; get; }

        /// <summary>
        /// Total number of messages received by obs-websocket from the client
        /// </summary>
        [JsonProperty(PropertyName = "webSocketSessionIncomingMessages")]
        public long SessionIncomingMessages { get; set; }

        /// <summary>
        /// Total number of messages sent by obs-websocket to the client
        /// </summary>
        [JsonProperty(PropertyName = "webSocketSessionOutgoingMessages")]
        public long SessionOutgoingMessages { get; set; }
    }
}