using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// GetSceneItemSource response
    /// </summary>
    public class SceneItemSource
    {
        /// <summary>
        /// Name of the source associated with the scene item
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; }

        /// <summary>
        /// UUID of the source associated with the scene item
        /// </summary>
        [JsonProperty(PropertyName = "sourceUuid")]
        public string SourceUuid { set; get; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public SceneItemSource(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SceneItemSource() { }
    }
}
