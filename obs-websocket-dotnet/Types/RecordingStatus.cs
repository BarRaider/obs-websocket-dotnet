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
        [JsonProperty(PropertyName = "isRecording")]
        public bool IsRecording { set; get; }

        /// <summary>
        /// Whether the recording is paused or not
        /// </summary>
        [JsonProperty(PropertyName = "isRecordingPaused")]
        public bool IsRecordingPaused { set; get; }

        /// <summary>
        /// Time elapsed since recording started (only present if currently recording)
        /// </summary>
        [JsonProperty(PropertyName = "recordTimecode")]
        public string RecordTimeCode { set; get; }

        /// <summary>
        /// Filename for the recording (only present if currently recording)
        /// </summary>
        [JsonProperty(PropertyName = "recordingFilename")]
        public string RecordingFilename { set; get; }
    }
}
