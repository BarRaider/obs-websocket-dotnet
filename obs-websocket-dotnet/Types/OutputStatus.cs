using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [JsonProperty(PropertyName = "outputActive")]
        public readonly bool IsActive;

        /// <summary>
        /// Whether the output is currently reconnectins
        /// </summary>
        [JsonProperty(PropertyName = "outputReconnecting")]
        public bool IsReconnecting { get; set; }

        /// <summary>
        /// Current formatted timecode string for the output
        /// </summary>
        [JsonProperty(PropertyName = "outputTimecode")]
        public string TimeCode { get; set; }

        /// <summary>
        /// Current duration in milliseconds for the output
        /// </summary>
        [JsonProperty(PropertyName = "outputDuration")]
        public long Duration { get; set; }

        /// <summary>
        /// Congestion of the output
        /// </summary>
        [JsonProperty(PropertyName = "outputCongestion")]
        public double Congestion { get; set; }

        /// <summary>
        /// Nubmer of bytes sent by the output
        /// </summary>
        [JsonProperty(PropertyName = "outputBytes")]
        public long BytesSent { get; set; }

        /// <summary>
        /// Number of frames skipped by the output's process
        /// </summary>
        [JsonProperty(PropertyName = "outputSkippedFrames")]
        public long SkippedFrames { get; set; }

        /// <summary>
        /// Total number of frames delivered by the output's process
        /// </summary>
        [JsonProperty(PropertyName = "outputTotalFrames")]
        public long TotalFrames { get; set; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OutputStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OutputStatus() { }
    }
}
