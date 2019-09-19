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
        [JsonProperty(PropertyName = "crop")]
        public SceneItemCropInfo Crop { set; get; }

        /// <summary>
        /// Bounds Information
        /// </summary>
        [JsonProperty(PropertyName = "bounds")]
        public SceneItemBoundsInfo Bounds { set; get; }

        /// <summary>
        /// Scale Information
        /// </summary>
        [JsonProperty(PropertyName = "scale")]
        public SceneItemPointInfo Scale { set; get; }

        /// <summary>
        /// Position of the item
        /// </summary>
        [JsonProperty(PropertyName = "position")]
        public SceneItemPositionInfo Position { set; get; }

        /// <summary>
        /// Scene item name, <i>populated from GetSceneItemProperites only</i>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string ItemName { set; get; }

        /// <summary>
        /// Scene item name, <i>populated from GetSceneItemProperites only</i>
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public string Item { set; get; }

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
        [JsonProperty(PropertyName = "locked")]
        public bool Locked { set; get; }

        /// <summary>
        /// If the scene item is visible
        /// </summary>
        [JsonProperty(PropertyName = "visible")]
        public bool Visible { set; get; }

        /// <summary>
        /// Base height (without scaling) of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceHeight")]
        public int SourceHeight { set; get; }

        /// <summary>
        /// Base width (without scaling) of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceWidth")]
        public int SourceWidth { set; get; }

        /// <summary>
        /// The clockwise rotation of the scene item in degrees around the point of alignment.
        /// </summary>
        [JsonProperty(PropertyName = "rotation")]
        public double Rotation { set; get; }
    }
}