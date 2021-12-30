using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes a scene in OBS, along with its items
    /// </summary>
    public class OBSScene
    {
        // TODO: Test mapping of ProgramScene/PreviewScene with SetCurrentProgramScene / SetCurrentPreviewScene. Maybe a custom serialization mapper would be more useful

        /// <summary>
        /// OBS Scene name
        /// </summary>
        [JsonProperty(PropertyName = "sceneName")]
        public string Name;

        /// <summary>
        /// Scene name as used by GetCurrentProgramScene
        /// </summary>
        [JsonProperty(PropertyName = "currentProgramSceneName")]
        private string ProgramScene
        {
            set { Name = value; }
        }


        /// <summary>
        /// Scene name as used by GetCurrentPreviewScene
        /// </summary>
        [JsonProperty(PropertyName = "currentPreviewSceneName")]
        private string PreviewScene
        {
            set { Name = value; }
        }


        /// <summary>
        /// Builds the object from the JSON description
        /// </summary>
        /// <param name="data">JSON scene description as a <see cref="JObject" /></param>
        public OBSScene(JObject data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                NullValueHandling = NullValueHandling.Include
            };
            JsonConvert.PopulateObject(data.ToString(), this, settings);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OBSScene() { }
    }
}