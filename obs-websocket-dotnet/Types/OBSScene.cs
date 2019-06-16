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
        /// <summary>
        /// OBS Scene name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name;

        /// <summary>
        /// Scene item list
        /// </summary>
        [JsonProperty(PropertyName = "sources")]
        public List<SceneItem> Items;

        /// <summary>
        /// Builds the object from the JSON description
        /// </summary>
        /// <param name="data">JSON scene description as a <see cref="JObject" /></param>
        public OBSScene(JObject data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Auto;
            settings.NullValueHandling = NullValueHandling.Include;
            JsonConvert.PopulateObject(data.ToString(), this, settings);
        }

        /// <summary>
        /// Constructor used for jsonconverter
        /// </summary>
        public OBSScene()
        {
        }
    }
}