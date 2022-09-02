using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Item transformation information
    /// </summary>
    public class SceneItemTransformInfo
    {
        /// <summary>
        /// Alignment of the item
        /// </summary>
        [JsonProperty(PropertyName = "alignment")]
        public int Alignnment { set; get; }

        /// <summary>
        /// The point on the scene item that the item is manipulated from
        /// </summary>
        [JsonProperty(PropertyName = "boundsAlignment")]
        public int BoundsAlignnment { set; get; }

        /// <summary>
        /// Height of the bounding box
        /// </summary>
        [JsonProperty(PropertyName = "boundsHeight")]
        public double BoundsHeight { set; get; }

        /// <summary>
        /// Width of the bounding box
        /// </summary>
        [JsonProperty(PropertyName = "boundsWidth")]
        public double BoundsWidth { set; get; }

        /// <summary>
        /// Type of bounding box
        /// </summary>
        [JsonProperty(PropertyName = "boundsType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SceneItemBoundsType BoundsType { set; get; }

        /// <summary>
        /// Bottom crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "cropBottom")]
        public int CropBottom;

        /// <summary>
        /// Left crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "cropLeft")]
        public int CropLeft;

        /// <summary>
        /// Right crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "cropRight")]
        public int CropRight;

        /// <summary>
        /// Top crop (in pixels)
        /// </summary>
        [JsonProperty(PropertyName = "cropTop")]
        public int CropTop;

        /// <summary>
        /// The clockwise rotation of the scene item in degrees around the point of alignment.
        /// </summary>
        [JsonProperty(PropertyName = "rotation")]
        public double Rotation { set; get; }

        /// <summary>
        /// The x-scale factor of the scene item
        /// </summary>
        [JsonProperty(PropertyName = "scaleX")]
        public double ScaleX { get; set; }

        /// <summary>
        /// The y-scale factor of the scene item
        /// </summary>
        [JsonProperty(PropertyName = "scaleY")]
        public double ScaleY { get; set; }

        /// <summary>
        /// Base height (without scaling) of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceHeight")]
        public double SourceHeight { set; get; }

        /// <summary>
        /// Base width (without scaling) of the source
        /// </summary>
        [JsonProperty(PropertyName = "sourceWidth")]
        public double SourceWidth { set; get; }

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
        /// The x position of the scene item from the left
        /// </summary>
        [JsonProperty(PropertyName = "positionX")]
        public double X { set; get; }

        /// <summary>
        /// The y position of the scene item from the top
        /// </summary>
        [JsonProperty(PropertyName = "positionY")]
        public double Y { set; get; }

        /// <summary>
        /// Initialize the scene item transform
        /// </summary>
        /// <param name="body"></param>
        public SceneItemTransformInfo(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SceneItemTransformInfo() { }
    }
}