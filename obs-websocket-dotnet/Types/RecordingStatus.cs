using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Status of streaming output and recording output
    /// </summary>
    public class RecordingStatus
    {
        /// <summary>
        /// Current recording status
        /// </summary>
        [JsonProperty(PropertyName = "isRecording")]
        public readonly bool IsRecording;

        /// <summary>
        /// Whether the recording is paused or not.
        /// </summary>
        [JsonProperty(PropertyName = "isRecordingPaused")]
        public readonly bool IsRecordingPaused;

        /// <summary>
        /// Time elapsed since recording started (only present if currently recording).
        /// </summary>
        [JsonProperty(PropertyName = "recordTimecode")]
        public readonly string RecordingTimecode;

        /// <summary>
        /// Absolute path to the recording file (only present if currently recording).
        /// </summary>
        [JsonProperty(PropertyName = "recordingFilename")]
        public readonly string RecordingFilename;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public RecordingStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}
