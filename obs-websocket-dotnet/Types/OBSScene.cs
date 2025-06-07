using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes a scene in OBS, along with its items
    /// </summary>
    public class ObsScene
    {
        /// <summary>
        /// OBS Scene name
        /// </summary>
        [JsonPropertyName("sceneName")]
        public string Name;

        /// <summary>
        /// Is group
        /// </summary>
        [JsonPropertyName("isGroup")]
        public bool IsGroup;

        /// <summary>
        /// Scene item list
        /// </summary>
        [JsonPropertyName("sources")]
        public List<SceneItemDetails> Items;

        /// <summary>
        /// Builds the object from the JSON description
        /// </summary>
        /// <param name="data">JSON scene description as a <see cref="JObject" /></param>
        public ObsScene(JsonObject data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                NullValueHandling = NullValueHandling.Include
            };
            if (data.ContainsKey("currentProgramSceneName"))
            {
                var newToken = JToken.FromObject(data["currentProgramSceneName"]);
                data.Add("sceneName", newToken);
            }
            JsonConvert.PopulateObject(data.ToString(), this, settings);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public ObsScene() { }
    }
}