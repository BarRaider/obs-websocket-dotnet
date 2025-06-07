using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("1")]
        public bool IsTrack1Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonPropertyName("2")]
        public bool IsTrack2Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonPropertyName("3")]
        public bool IsTrack3Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonPropertyName("4")]
        public bool IsTrack4Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonPropertyName("5")]
        public bool IsTrack5Active { set; get; }

        /// <summary>
        /// Is the track active
        /// </summary>
        [JsonPropertyName("6")]
        public bool IsTrack6Active { set; get; }

        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public SourceTracks(JsonObject data)
        {
            JsonSerializer2.PopulateObject(data.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SourceTracks() { }
    }
}
