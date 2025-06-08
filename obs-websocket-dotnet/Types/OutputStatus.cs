using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Status of streaming output
    /// </summary>
    public class OutputStatus
    {        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        [JsonPropertyName("outputActive")]
        public bool IsActive { get; private set; }

        /// <summary>
        /// Whether the output is currently reconnectins
        /// </summary>
        [JsonPropertyName("outputReconnecting")]
        public bool IsReconnecting { get; set; }

        /// <summary>
        /// Current formatted timecode string for the output
        /// </summary>
        [JsonPropertyName("outputTimecode")]
        public string TimeCode { get; set; }

        /// <summary>
        /// Current duration in milliseconds for the output
        /// </summary>
        [JsonPropertyName("outputDuration")]
        public long Duration { get; set; }

        /// <summary>
        /// Congestion of the output
        /// </summary>
        [JsonPropertyName("outputCongestion")]
        public double Congestion { get; set; }

        /// <summary>
        /// Nubmer of bytes sent by the output
        /// </summary>
        [JsonPropertyName("outputBytes")]
        public long BytesSent { get; set; }

        /// <summary>
        /// Number of frames skipped by the output's process
        /// </summary>
        [JsonPropertyName("outputSkippedFrames")]
        public long SkippedFrames { get; set; }

        /// <summary>
        /// Total number of frames delivered by the output's process
        /// </summary>
        [JsonPropertyName("outputTotalFrames")]
        public long TotalFrames { get; set; }        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JsonObject"/></param>
        public OutputStatus(JsonObject data)
        {
            IsActive = data["outputActive"]?.GetValue<bool>() ?? false;
            IsReconnecting = data["outputReconnecting"]?.GetValue<bool>() ?? false;
            TimeCode = data["outputTimecode"]?.GetValue<string>() ?? string.Empty;
            Duration = data["outputDuration"]?.GetValue<long>() ?? 0L;
            Congestion = data["outputCongestion"]?.GetValue<double>() ?? 0.0;
            BytesSent = data["outputBytes"]?.GetValue<long>() ?? 0L;
            SkippedFrames = data["outputSkippedFrames"]?.GetValue<long>() ?? 0L;
            TotalFrames = data["outputTotalFrames"]?.GetValue<long>() ?? 0L;
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OutputStatus() { }
    }
}
