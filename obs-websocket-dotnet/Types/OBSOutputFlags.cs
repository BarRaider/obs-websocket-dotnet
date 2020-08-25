using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// OBS Output flags
    /// </summary>
    public class OBSOutputFlags
    {
        /// <summary>
        /// Raw flags value
        /// </summary>
        [JsonProperty(PropertyName = "rawValue")]
        public int RawValue;

        /// <summary>
        /// Output uses audio
        /// </summary>
        [JsonProperty(PropertyName = "audio")]
        public bool IsAudio;

        /// <summary>
        /// Output uses video
        /// </summary>
        [JsonProperty(PropertyName = "video")]
        public bool IsVideo;

        /// <summary>
        /// Output is encoded
        /// </summary>
        [JsonProperty(PropertyName = "encoded")]
        public bool IsEncoded;

        /// <summary>
        /// Output uses several tracks
        /// </summary>
        [JsonProperty(PropertyName = "multiTrack")]
        public bool IsMultiTrack;

        /// <summary>
        /// Output users a service
        /// </summary>
        [JsonProperty(PropertyName = "service")]
        public bool IsService;
    }
}
