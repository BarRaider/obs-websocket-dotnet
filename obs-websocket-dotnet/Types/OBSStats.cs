using System.Text.Json;using System.Text.Json.Serialization;

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
        [JsonPropertyName("activeFps")]
        public double FPS { set; get; }

        /// <summary>
        /// Number of frames rendered
        /// </summary>
        [JsonPropertyName("renderTotalFrames")]
        public long RenderTotalFrames { set; get; }

        /// <summary>
        /// Number of frames missed due to rendering lag
        /// </summary>
        [JsonPropertyName("renderSkippedFrames")]
        public long RenderMissedFrames { set; get; }

        /// <summary>
        /// Number of frames outputted
        /// </summary>
        [JsonPropertyName("outputTotalFrames")]
        public long OutputTotalFrames { set; get; }

        /// <summary>
        /// Number of frames skipped due to encoding lag
        /// </summary>
        [JsonPropertyName("outputSkippedFrames")]
        public long OutputSkippedFrames { set; get; }

        /// <summary>
        /// Average frame render time (in milliseconds)
        /// </summary>
        [JsonPropertyName("averageFrameRenderTime")]
        public double AverageFrameTime { set; get; }

        /// <summary>
        /// Current CPU usage (percentage)
        /// </summary>
        [JsonPropertyName("cpuUsage")]
        public double CpuUsage { set; get; }

        /// <summary>
        /// Current RAM usage (in megabytes)
        /// </summary>
        [JsonPropertyName("memoryUsage")]
        public double MemoryUsage { set; get; }

        /// <summary>
        /// Free recording disk space (in megabytes)
        /// </summary>
        [JsonPropertyName("availableDiskSpace")]
        public double FreeDiskSpace { set; get; }

        /// <summary>
        /// Total number of messages received by obs-websocket from the client
        /// </summary>
        [JsonPropertyName("webSocketSessionIncomingMessages")]
        public long SessionIncomingMessages { get; set; }

        /// <summary>
        /// Total number of messages sent by obs-websocket to the client
        /// </summary>
        [JsonPropertyName("webSocketSessionOutgoingMessages")]
        public long SessionOutgoingMessages { get; set; }
    }
}