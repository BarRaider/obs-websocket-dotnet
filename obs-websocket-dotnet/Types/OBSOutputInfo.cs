using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// OBS Output information
    /// </summary>
    public class OBSOutputInfo
    {
        /// <summary>
        /// Output Name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name;

        /// <summary>
        /// Output type/kind
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type;

        /// <summary>
        /// Video output height
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public int Height;

        /// <summary>
        /// Video output width
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int Width;

        /// <summary>
        /// Settings
        /// </summary>
        [JsonProperty(PropertyName = "settings")]
        public JObject Settings;

        /// <summary>
        /// Output status (active or not)
        /// </summary>
        [JsonProperty(PropertyName = "active")]
        public bool IsActive;

        /// <summary>
        /// Output reconnection status (reconnecting or not)
        /// </summary>
        [JsonProperty(PropertyName = "reconnecting")]
        public bool IsReconnecting;

        /// <summary>
        /// Output congestion
        /// </summary>
        [JsonProperty(PropertyName = "congestion")]
        public double Congestion;

        /// <summary>
        /// Number of frames sent
        /// </summary>
        [JsonProperty(PropertyName = "totalFrames")]
        public int TotalFrames;

        /// <summary>
        /// Number of frames dropped
        /// </summary>
        [JsonProperty(PropertyName = "droppedFrames")]
        public int DroppedFrames;

        /// <summary>
        /// Total bytes sent
        /// </summary>
        [JsonProperty(PropertyName = "totalBytes")]
        public int TotalBytes;

        /// <summary>
        /// Output flags
        /// </summary>
        [JsonProperty(PropertyName = "flags")]
        public OBSOutputFlags Flags;
    }
}
