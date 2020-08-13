using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    ///
    /// </summary>
    public class SceneItemProperties
    {
        /// <summary>
        /// Initialize the scene item transform
        /// </summary>
        /// <param name="body"></param>
        public SceneItemProperties(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Constructor used for json converter
        /// </summary>
        public SceneItemProperties()
        {
        }

        /// <summary>
        /// Crop Information
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "crop")]
        public SceneItemCropInfo Crop { set; get; } = null!;

        /// <summary>
        /// Bounds Information
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "bounds")]
        public SceneItemBoundsInfo Bounds { set; get; } = null!;

        /// <summary>
        /// Scale Information
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "scale")]
        public SceneItemPointInfo Scale { set; get; } = null!;

        /// <summary>
        /// Position of the item
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "position")]
        public SceneItemPositionInfo Position { set; get; } = null!;

        /// <summary>
        /// Scene item name, <i>populated from GetSceneItemProperites only</i>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string? ItemName { set; get; }

        /// <summary>
        /// Scene item name, <i>populated from GetSceneItemProperites only</i>
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public string? Item { set; get; }

        /// <summary>
        /// Scene item height (base source height multiplied by the vertical scaling factor)
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public double Height { set; get; }

        /// <summary>
        /// Scene item width (base source width multiplied by the horizontal scaling factor)
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public double Width { set; get; }

        /// <summary>
        /// If the scene item is locked in position
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "locked")]
        public bool Locked { set; get; }

        /// <summary>
        /// If the scene item is visible
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "visible")]
        public bool Visible { set; get; }

        /// <summary>
        /// Base height (without scaling) of the source
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceHeight")]
        public int SourceHeight { set; get; }

        /// <summary>
        /// Base width (without scaling) of the source
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceWidth")]
        public int SourceWidth { set; get; }

        /// <summary>
        /// The clockwise rotation of the scene item in degrees around the point of alignment.
        /// </summary>
        [JsonProperty(PropertyName = "rotation")]
        public double Rotation { set; get; }
    }
}