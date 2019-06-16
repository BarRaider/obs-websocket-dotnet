using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Data of a stream status update
    /// </summary>
    public class StreamStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "streaming")]
        public readonly bool Streaming;

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public readonly bool Recording;

        /// <summary>
        /// Stream bitrate in bytes per second
        /// </summary>
        [JsonProperty(PropertyName = "bytes-per-sec")]
        public readonly int BytesPerSec;

        /// <summary>
        /// Stream bitrate in kilobits per second
        /// </summary>
        [JsonProperty(PropertyName = "kbits-per-sec")]
        public readonly int KbitsPerSec;

        /// <summary>
        /// RTMP output strain
        /// </summary>
        [JsonProperty(PropertyName = "strain")]
        public readonly float Strain;

        /// <summary>
        /// Total time since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-time")]
        public readonly int TotalStreamTime;

        /// <summary>
        /// Number of frames sent since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "num-total-frames")]
        public readonly int TotalFrames;

        /// <summary>
        /// Overall number of frames dropped since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "num-dropped-frames")]
        public readonly int DroppedFrames;

        /// <summary>
        /// Current framerate in Frames Per Second
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public readonly float FPS;

        /// <summary>
        /// Builds the object from the JSON event body
        /// </summary>
        /// <param name="data">JSON event body as a <see cref="JObject"/></param>
        public StreamStatus(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}
