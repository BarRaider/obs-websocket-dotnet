using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes a scene item in an OBS scene
    /// </summary>
    public class SceneItem
    {
        /// <summary>
        /// Source name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string SourceName;

        /// <summary>
        /// Source type. Value is one of the following: "input", "filter", "transition", "scene" or "unknown"
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string InternalType;

        /// <summary>
        /// Source audio volume
        /// </summary>
        [JsonProperty(PropertyName = "volume")]
        public float AudioVolume;

        /// <summary>
        /// Scene item horizontal position/offset
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public float XPos;

        /// <summary>
        /// Scene item vertical position/offset
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public float YPos;

        /// <summary>
        /// Item source width, without scaling and transforms applied
        /// </summary>
        [JsonProperty(PropertyName = "source_cx")]
        public int SourceWidth;

        /// <summary>
        /// Item source height, without scaling and transforms applied
        /// </summary>
        [JsonProperty(PropertyName = "source_cy")]
        public int SourceHeight;

        /// <summary>
        /// Item width
        /// </summary>
        [JsonProperty(PropertyName = "cx")]
        public float Width;

        /// <summary>
        /// Item height
        /// </summary>
        [JsonProperty(PropertyName = "cy")]
        public float Height;

        /// <summary>
        /// Whether or not this Scene Item is locked and can't be moved around
        /// </summary>
        [JsonProperty(PropertyName = "locked")]
        public bool Locked { set; get; }

        /// <summary>
        /// Whether or not this Scene Item is set to "visible".
        /// </summary>
        [JsonProperty(PropertyName = "render")]
        public bool Render { set; get; }

        /// <summary>
        /// Scene item ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { set; get; }

        /// <summary>
        /// Name of the item's parent (if this item belongs to a group)
        /// </summary>
        [JsonProperty(PropertyName = "parentGroupName")]
        public string ParentGroupName { set; get; }

        /// <summary>
        /// Name of the item's parent (if this item belongs to a group)
        /// </summary>
        [JsonProperty(PropertyName = "groupChildren")]
        public List<SceneItem> GroupChildren { set; get; }

        /// <summary>
        /// Builds the object from the JSON scene description
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public SceneItem(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }

        /// <summary>
        /// Empty constructor for JSON deserialization
        /// </summary>
        public SceneItem()
        {
        }
    }
}