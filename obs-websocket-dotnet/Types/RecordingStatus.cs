using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public int RecordingDuration { set; get; }

        /// <summary>
        /// Number of bytes sent by the output
        /// </summary>
        [JsonProperty(PropertyName = "outputBytes")]
        public int RecordingBytes { set; get; }
    }
}
