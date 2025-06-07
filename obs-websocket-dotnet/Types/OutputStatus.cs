using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Status of streaming output
    /// </summary>
    public class OutputStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        [JsonPropertyName("outputActive")]
        public readonly bool IsActive;

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
        public long TotalFrames { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OutputStatus(JsonObject data)
        {
            JsonSerializer2.PopulateObject(data.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OutputStatus() { }
    }
}
