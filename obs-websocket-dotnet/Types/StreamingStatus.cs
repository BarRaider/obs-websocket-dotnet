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
    public class StreamingStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "streaming")]
        public readonly bool IsStreaming;

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public readonly bool IsRecording;

        /// <summary>
        /// Time elapsed since recording started (only present if currently recording)
        /// </summary>
        [JsonProperty(PropertyName = "stream-timecode")]
        public readonly string StreamingTimecode;

        /// <summary>
        /// Time elapsed since recording started (only present if currently recording)
        /// </summary>
        [JsonProperty(PropertyName = "rec-timecode")]
        public readonly string RecordingTimecode;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public StreamingStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}
