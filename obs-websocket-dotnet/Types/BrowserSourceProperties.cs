using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// BrowserSource source properties
    /// </summary>
    public class BrowserSourceProperties : IValidatedResponse
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public bool ResponseValid => Source != null && URL != null;
        /// <summary>
        /// Source name for the browser properties
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceName")]
        public string Source = null!;

        /// <summary>
        /// URL to load in the embedded browser
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "url")]
        public string URL = null!;

        /// <summary>
        /// true if the URL points to a local file, false otherwise.
        /// </summary>
        [JsonProperty(PropertyName = "is_local_file")]
        public bool IsLocalFile;

        /// <summary>
        /// File to load in embedded browser 
        /// </summary>
        [JsonProperty(PropertyName = "local_file")]
        public string? LocalFile;

        /// <summary>
        /// Additional CSS to apply to the page
        /// </summary>
        [JsonProperty(PropertyName = "css")]
        public string? CustomCSS;

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
        /// true if custom FPS is set
        /// </summary>
        [JsonProperty(PropertyName = "fps_custom")]
        public bool CustomFPS;

        /// <summary>
        /// true if source should be disabled (inactive) when not visible, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "shutdown")]
        public bool ShutdownWhenNotVisible;

        /// <summary>
        /// true if source should restart the video when visible
        /// </summary>
        [JsonProperty(PropertyName = "restart_when_active")]
        public bool RestartWhenActive;

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
            string? settingsStr = props["sourceSettings"]?.ToString();
            string? sourceString = props["sourceName"]?.ToString();
            if (settingsStr != null)
            {
                JsonConvert.PopulateObject(settingsStr, this);
                Source = sourceString ?? string.Empty;
            }
        }
    }
}