using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

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
        [JsonPropertyName("alignment")]
        public int Alignnment { set; get; }

        /// <summary>
        /// The point on the scene item that the item is manipulated from
        /// </summary>
        [JsonPropertyName("boundsAlignment")]
        public int BoundsAlignnment { set; get; }

        /// <summary>
        /// Height of the bounding box
        /// </summary>
        [JsonPropertyName("boundsHeight")]
        public double BoundsHeight { set; get; }

        /// <summary>
        /// Width of the bounding box
        /// </summary>
        [JsonPropertyName("boundsWidth")]
        public double BoundsWidth { set; get; }        /// <summary>
        /// Type of bounding box
        /// </summary>
        [JsonPropertyName("boundsType")]
        [JsonConverter(typeof(JsonStringEnumConverter<SceneItemBoundsType>))]
        public SceneItemBoundsType BoundsType { set; get; }

        /// <summary>
        /// Bottom crop (in pixels)
        /// </summary>
        [JsonPropertyName("cropBottom")]
        public int CropBottom;

        /// <summary>
        /// Left crop (in pixels)
        /// </summary>
        [JsonPropertyName("cropLeft")]
        public int CropLeft;

        /// <summary>
        /// Right crop (in pixels)
        /// </summary>
        [JsonPropertyName("cropRight")]
        public int CropRight;

        /// <summary>
        /// Top crop (in pixels)
        /// </summary>
        [JsonPropertyName("cropTop")]
        public int CropTop;

        /// <summary>
        /// The clockwise rotation of the scene item in degrees around the point of alignment.
        /// </summary>
        [JsonPropertyName("rotation")]
        public double Rotation { set; get; }

        /// <summary>
        /// The x-scale factor of the scene item
        /// </summary>
        [JsonPropertyName("scaleX")]
        public double ScaleX { get; set; }

        /// <summary>
        /// The y-scale factor of the scene item
        /// </summary>
        [JsonPropertyName("scaleY")]
        public double ScaleY { get; set; }

        /// <summary>
        /// Base height (without scaling) of the source
        /// </summary>
        [JsonPropertyName("sourceHeight")]
        public double SourceHeight { set; get; }

        /// <summary>
        /// Base width (without scaling) of the source
        /// </summary>
        [JsonPropertyName("sourceWidth")]
        public double SourceWidth { set; get; }

        /// <summary>
        /// Scene item height (base source height multiplied by the vertical scaling factor)
        /// </summary>
        [JsonPropertyName("height")]
        public double Height { set; get; }

        /// <summary>
        /// Scene item width (base source width multiplied by the horizontal scaling factor)
        /// </summary>
        [JsonPropertyName("width")]
        public double Width { set; get; }

        /// <summary>
        /// The x position of the scene item from the left
        /// </summary>
        [JsonPropertyName("positionX")]
        public double X { set; get; }

        /// <summary>
        /// The y position of the scene item from the top
        /// </summary>
        [JsonPropertyName("positionY")]
        public double Y { set; get; }

        /// <summary>
        /// Initialize the scene item transform
        /// </summary>        /// <param name="body"></param>
        public SceneItemTransformInfo(JsonObject body)
        {
            var transformInfo = JsonSerializer.Deserialize<SceneItemTransformInfo>(body.ToString(), AppJsonSerializerContext.Default.SceneItemTransformInfo);
            if (transformInfo != null)
            {
                // Copy properties from the deserialized object
                this.Alignnment = transformInfo.Alignnment;
                this.BoundsAlignnment = transformInfo.BoundsAlignnment;
                this.BoundsHeight = transformInfo.BoundsHeight;
                this.BoundsWidth = transformInfo.BoundsWidth;
                this.BoundsType = transformInfo.BoundsType;
                this.CropBottom = transformInfo.CropBottom;
                this.CropLeft = transformInfo.CropLeft;
                this.CropRight = transformInfo.CropRight;
                this.CropTop = transformInfo.CropTop;
                this.Height = transformInfo.Height;
                this.PositionX = transformInfo.PositionX;
                this.PositionY = transformInfo.PositionY;
                this.Rotation = transformInfo.Rotation;
                this.ScaleX = transformInfo.ScaleX;
                this.ScaleY = transformInfo.ScaleY;
                this.SourceHeight = transformInfo.SourceHeight;
                this.SourceWidth = transformInfo.SourceWidth;
                this.Width = transformInfo.Width;
            }
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SceneItemTransformInfo() { }
    }
}