using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Properties for a GDI+ Text source
    /// </summary>
    public class TextGDIPlusProperties
    {
        /// <summary>
        /// Source name.
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public string SourceName { set; get; }

        /// <summary>
        /// Text Alignment ("left", "center", "right").
        /// </summary>
        [JsonProperty(PropertyName = "align")]
        public string Alignment { set; get; }

        /// <summary>
        /// Background color.
        /// </summary>
        [JsonProperty(PropertyName = "bk-color")]
        public ulong BackgroundColor { set; get; }

        /// <summary>
        /// Background opacity (0-100).
        /// </summary>
        [JsonProperty(PropertyName = "bk-opacity")]
        public int BackgroundOpacity { set; get; }

        /// <summary>
        /// Chat log.
        /// </summary>
        [JsonProperty(PropertyName = "chatlog")]
        public bool IsChatLog { set; get; }

        /// <summary>
        /// Chat log lines.
        /// </summary>
        [JsonProperty(PropertyName = "chatlog_lines")]
        public int ChatlogLines { set; get; }

        /// <summary>
        /// Text color.
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public ulong TextColor { set; get; }

        /// <summary>
        /// Extents wrap.
        /// </summary>
        [JsonProperty(PropertyName = "extents")]
        public bool HasExtents { set; get; }

        /// <summary>
        /// Extents cx.
        /// </summary>
        [JsonProperty(PropertyName = "extents_cx")]
        public int Height { set; get; }

        /// <summary>
        /// Extents cy.
        /// </summary>
        [JsonProperty(PropertyName = "extents_cy")]
        public int Width { set; get; }

        /// <summary>
        /// File path name.
        /// </summary>
        [JsonProperty(PropertyName = "file")]
        public string Filename { set; get; }

        /// <summary>
        /// Read text from the specified file.
        /// </summary>
        [JsonProperty(PropertyName = "read_from_file")]
        public bool IsReadFromFile { set; get; }

        /// <summary>
        /// Holds data for the font. Ex: "font": { "face": "Arial", "flags": 0, "size": 150, "style": "" }
        /// </summary>
        [JsonProperty(PropertyName = "font")]
        public TextGDIPlusFont Font { set; get; }

        /// <summary>
        /// Gradient enabled.
        /// </summary>
        [JsonProperty(PropertyName = "gradient")]
        public bool HasGradient { set; get; }

        /// <summary>
        /// Gradient color.
        /// </summary>
        [JsonProperty(PropertyName = "gradient_color")]
        public ulong GradientColor { set; get; }

        /// <summary>
        /// Gradient direction.
        /// </summary>
        [JsonProperty(PropertyName = "gradient_dir")]
        public float GradientDirection { set; get; }

        /// <summary>
        /// Gradient opacity (0-100).
        /// </summary>
        [JsonProperty(PropertyName = "gradient_opacity")]
        public int GradientOpacity { set; get; }

        /// <summary>
        /// Outline.
        /// </summary>
        [JsonProperty(PropertyName = "outline")]
        public bool HasOutline { set; get; }

        /// <summary>
        /// Outline color.
        /// </summary>
        [JsonProperty(PropertyName = "outline_color")]
        public ulong OutlineColor { set; get; }

        /// <summary>
        /// Outline size.
        /// </summary>
        [JsonProperty(PropertyName = "outline_size")]
        public int OutlineSize { set; get; }

        /// <summary>
        /// Outline opacity (0-100).
        /// </summary>
        [JsonProperty(PropertyName = "outline_opacity")]
        public int OutlineOpacity { set; get; }

        /// <summary>
        /// Text content to be displayed.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { set; get; }

        /// <summary>
        /// Text vertical alignment ("top", "center", "bottom").
        /// </summary>
        [JsonProperty(PropertyName = "valign")]
        public string VeritcalAslignment { set; get; }

        /// <summary>
        /// Vertical text enabled.
        /// </summary>
        [JsonProperty(PropertyName = "vertical")]
        public bool IsVertical { set; get; }
    }
}