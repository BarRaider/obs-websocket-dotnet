using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// BrowserSource source properties
    /// </summary>
    public class BrowserSourceProperties
    {
        /// <summary>
        /// Source name for the browser properties
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public string Source;

        /// <summary>
        /// URL to load in the embedded browser
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string URL;

        /// <summary>
        /// true if the URL points to a local file, false otherwise.
        /// </summary>
        [JsonProperty(PropertyName = "is_local_file")]
        public bool IsLocalFile;

        /// <summary>
        /// Additional CSS to apply to the page
        /// </summary>
        [JsonProperty(PropertyName = "css")]
        public string CustomCSS;

        /// <summary>
        /// Embedded browser render (viewport) width
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int Width;

        /// <summary>
        /// Embedded browser render (viewport) height
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public int Height;

        /// <summary>
        /// Embedded browser render frames per second
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public int FPS;

        /// <summary>
        /// true if source should be disabled (inactive) when not visible, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "shutdown")]
        public bool ShutdownWhenNotVisible;

        /// <summary>
        /// true if source should be visible, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "render")]
        public bool Visible;

        /// <summary>
        /// Construct the object from JSON response data
        /// </summary>
        /// <param name="props"></param>
        public BrowserSourceProperties(JObject props)
        {
            JsonConvert.PopulateObject(props.ToString(), this);
        }
    }
}