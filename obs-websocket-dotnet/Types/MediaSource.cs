using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Meta Data on the selected media source
    /// </summary>
    public class MediaSource
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; }

        /// <summary>
        /// Kind of source (a.k.a ffmpeg_source or vlc_source)
        /// </summary>
        [JsonProperty(PropertyName = "sourceKind")]
        public string SourceKind { set; get; }

        /// <summary>
        /// Type of the specified source. Useful for type-checking if you expect a specific settings schema.
        /// </summary>
        [JsonProperty(PropertyName = "mediaState")]
        public MediaState MediaState { set; get; }

        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public MediaSource(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public MediaSource() { }
    }
}
