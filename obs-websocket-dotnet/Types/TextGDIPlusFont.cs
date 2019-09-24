using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Font information for a Text GDI+ source
    /// </summary>
    public class TextGDIPlusFont
    {
        /// <summary>
        /// Font face.
        /// </summary>
        [JsonProperty(PropertyName = "face")]
        public string Face { set; get; }

        /// <summary>
        /// Font text styling flag. Bold=1, Italic=2, Bold Italic=3, Underline=5, Strikeout=8
        /// </summary>
        [JsonProperty(PropertyName = "flags")]
        public int Flags { set; get; }

        /// <summary>
        /// Font text size.
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public int Size { set; get; }

        /// <summary>
        /// Font Style (unknown function).
        /// </summary>
        [JsonProperty(PropertyName = "style")]
        public string Style { set; get; }
    }
}