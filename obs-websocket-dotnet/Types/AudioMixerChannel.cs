using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Audio Mixer Channel information
    /// </summary>
    public class AudioMixerChannel
    {
        /// <summary>
        /// Is channel enabled
        /// </summary>
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { set; get; }

        /// <summary>
        /// ID of the channel
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { set; get; }
    }
}