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
        public List<SceneItemDetails> Items;        /// <summary>
        /// Builds the object from the JSON description
        /// </summary>
        /// <param name="data">JSON scene description as a <see cref="JsonObject" /></param>
        public ObsScene(JsonObject data)
        {
            if (data.ContainsKey("currentProgramSceneName"))
            {
                Name = data["currentProgramSceneName"]?.GetValue<string>() ?? string.Empty;
            }
            else
            {
                Name = data["sceneName"]?.GetValue<string>() ?? string.Empty;
            }
            
            IsGroup = data["isGroup"]?.GetValue<bool>() ?? false;
            
            // Handle sources list
            var sourcesArray = data["sources"]?.AsArray();
            Items = new List<SceneItemDetails>();
            if (sourcesArray != null)
            {
                foreach (var item in sourcesArray)
                {
                    if (item?.AsObject() != null)
                    {
                        Items.Add(new SceneItemDetails(item.AsObject()));
                    }
                }
            }
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public ObsScene() { }
    }
}