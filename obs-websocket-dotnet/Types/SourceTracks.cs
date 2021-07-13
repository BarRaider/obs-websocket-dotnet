using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Tracks status for a source item
    /// </summary>
    public class SourceTracks
    {
        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonProperty(PropertyName = "track1")]
        public bool IsTrack1Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonProperty(PropertyName = "track2")]
        public bool IsTrack2Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonProperty(PropertyName = "track3")]
        public bool IsTrack3Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonProperty(PropertyName = "track4")]
        public bool IsTrack4Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonProperty(PropertyName = "track5")]
        public bool IsTrack5Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonProperty(PropertyName = "track6")]
        public bool IsTrack6Active { set; get; }

        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public SourceTracks(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SourceTracks() { }
    }
}
