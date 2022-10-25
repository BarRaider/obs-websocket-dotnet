using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// GetRecordingStatus response
    /// </summary>
    public class RecordingStatus
    {
        /// <summary>
        /// Current recording status
        /// </summary>
        [JsonProperty(PropertyName = "outputActive")]
        public bool IsRecording { set; get; }

        /// <summary>
        /// Whether the recording is paused or not
        /// </summary>
        [JsonProperty(PropertyName = "outputPaused")]
        public bool IsRecordingPaused { set; get; }

        /// <summary>
        /// Current formatted timecode string for the output
        /// </summary>
        [JsonProperty(PropertyName = "outputTimecode")]
        public string RecordTimecode { set; get; }

        /// <summary>
        /// Current duration in milliseconds for the output
        /// </summary>
        [JsonProperty(PropertyName = "outputDuration")]
        public long RecordingDuration { set; get; }

        /// <summary>
        /// Number of bytes sent by the output
        /// </summary>
        [JsonProperty(PropertyName = "outputBytes")]
        public long RecordingBytes { set; get; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public RecordingStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public RecordingStatus() { }
    }
}
