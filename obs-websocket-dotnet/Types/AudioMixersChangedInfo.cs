using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Response from audio mixer change event
    /// </summary>
    public class AudioMixersChangedInfo
    {
        /// <summary>
        /// Mixer source name
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; }

        /// <summary>
        /// Routing status of the source for each audio mixer (array of 6 values)
        /// </summary>
        [JsonProperty(PropertyName = "mixers")]
        public List<AudioMixerChannel> Mixers { get; set; }

        /// <summary>
        /// Raw mixer flags (little-endian, one bit per mixer) as an hexadecimal value
        /// </summary>
        [JsonProperty(PropertyName = "hexMixersValue")]
        public string HexMixersValue { set; get; }

        /// <summary>
        /// Create mixer response
        /// </summary>
        /// <param name="body"></param>
        public AudioMixersChangedInfo(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }
    }
}