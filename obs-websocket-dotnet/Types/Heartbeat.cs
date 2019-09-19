using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Heartbeat response
    /// </summary>
    public class Heartbeat
    {
        /// <summary>
        /// Create a heartbeat
        /// </summary>
        /// <param name="body"></param>
        public Heartbeat(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Toggles between every JSON message as an "I am alive" indicator.
        /// </summary>
        [JsonProperty(PropertyName = "pulse")]
        public bool Pulse { set; get; }

        /// <summary>
        /// Current active profile.
        /// </summary>
        [JsonProperty(PropertyName = "current-profile")]
        public string CurrentProfile { set; get; }

        /// <summary>
        /// Current active scene.
        /// </summary>
        [JsonProperty(PropertyName = "current-scene")]
        public string CurrentScene { set; get; }

        /// <summary>
        /// Current streaming state.
        /// </summary>
        [JsonProperty(PropertyName = "streaming")]
        public bool Streaming { set; get; }

        /// <summary>
        /// Total time (in seconds) since the stream started.
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-time")]
        public int totalStreamTime { set; get; }

        /// <summary>
        /// Total bytes sent since the stream started.
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-bytes")]
        public ulong TotalStreamBytes { set; get; }

        /// <summary>
        /// Total frames streamed since the stream started.
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-frames")]
        public ulong TotalStreamFrames { set; get; }

        /// <summary>
        /// Current recording state.
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public bool Recording { set; get; }

        /// <summary>
        /// Total time (in seconds) since recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-time")]
        public int TotalRecordTime { set; get; }

        /// <summary>
        /// Total bytes recorded since the recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-bytes")]
        public int TotalTecordBytes { set; get; }

        /// <summary>
        /// Total frames recorded since the recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-frames")]
        public int TotalRecordFrames { set; get; }

        /// <summary>
        /// OBS Stats
        /// </summary>
        [JsonProperty(PropertyName = "stats")]
        public OBSStats Stats { set; get; }
    }
}