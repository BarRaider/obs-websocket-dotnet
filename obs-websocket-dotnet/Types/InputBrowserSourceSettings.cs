using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Input class dedicated for the ffmpeg_source input kind.
    /// Usage: InputBrowserSourceSettings.FromInputSettings(InputSettings)
    /// </summary>
    public class InputBrowserSourceSettings : Input
    {
        private const string SUPPORTED_INPUT_KIND = "browser_source";
        private const string CSS_DEFAULT_VALUE = "body { background-color: rgba(0, 0, 0, 0); margin: 0px auto; overflow: hidden; }";

        /// <summary>
        /// Set a custom FPS (using the FPS property)
        /// </summary>
        [JsonProperty(PropertyName = "fps_custom")]
        public bool CustomFPS { get; set; } = false;

        /// <summary>
        /// Frames Per Second
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public int FPS { get; set; } = 30;

        /// <summary>
        /// Control audio via OBS
        /// </summary>
        [JsonProperty(PropertyName = "reroute_audio")]
        public bool RerouteAudio { get; set; } = false;

        /// <summary>
        /// Height
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; } = 600;

        /// <summary>
        /// Width
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; } = 800;

        /// <summary>
        /// Custom CSS
        /// </summary>
        [JsonProperty(PropertyName = "css")]
        public string CSS { get; set; } = CSS_DEFAULT_VALUE;

        /// <summary>
        /// Is Local file
        /// </summary>
        [JsonProperty(PropertyName = "is_local_file")]
        public bool IsLocalFile { get; set; } = false;

        /// <summary>
        /// Local filename (when IsLocalFile is true)
        /// </summary>
        [JsonProperty(PropertyName = "local_file")]
        public string LocalFile { get; set; }

        /// <summary>
        /// URL (when IsLocalFile is false)
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string URL { get; set; }

        /// <summary>
        /// Refresh browser when scene becomes active
        /// </summary>
        [JsonProperty(PropertyName = "restart_when_active")]
        public bool RestartWhenActive { get; set; } = false;

        /// <summary>
        /// Shutdown source when not visible
        /// </summary>
        [JsonProperty(PropertyName = "shutdown")]
        public bool ShutdownWhenNotVisible { get; set; } = false;
              
        /// <summary>
        /// Page Permissions
        /// </summary>
        [JsonProperty(PropertyName = "webpage_control_level")]
        public int ControlLevel { get; set; } = 1;

        /// <summary>
        /// Static constructor to instanciate a InputBrowserSourceSettings object
        /// Requires an InputSettings class with InputKind of browser_source to create
        /// </summary>
        /// <param name="settings">Settings object</param>
        /// <returns></returns>
        public static InputBrowserSourceSettings FromInputSettings(InputSettings settings)
        {
            return new InputBrowserSourceSettings(settings);
        }
        // Private Constrctor
        private InputBrowserSourceSettings(InputSettings settings) : base(JObject.FromObject(settings))
        {
            if (settings.InputKind != SUPPORTED_INPUT_KIND)
            {
                throw new InvalidCastException();
            }
            JsonConvert.PopulateObject(settings.Settings.ToString(), this);
        }
    }
}
