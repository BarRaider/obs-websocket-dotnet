using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Scene transformation information from an event
    /// </summary>
    public class SceneItemTransformInfo
    {
        /// <summary>
        /// Name of the scene
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "scene-name")]
        public string SceneName { internal set; get; } = null!;

        /// <summary>
        /// Name of the item in the scene
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "item-name")]
        public string ItemName { internal set; get; } = null!;

        /// <summary>
        /// Scene Item ID
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "item-id")]
        public string ItemID { internal set; get; } = null!;

        /// <summary>
        /// Scene item transform properties
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "transform")]
        public SceneItemProperties Transform { internal set; get; } = null!;

        /// <summary>
        /// Initialize the scene item transform
        /// </summary>
        /// <param name="body"></param>
        public SceneItemTransformInfo(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }
    }
}