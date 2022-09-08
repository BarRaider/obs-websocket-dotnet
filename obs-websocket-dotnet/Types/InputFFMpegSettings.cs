using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Input class dedicated for the ffmpeg_source input kind.
    /// Usage: InputFFMpegSettings.FromInputSettings(InputSettings)
    /// </summary>
    public class InputFFMpegSettings : Input
    {
        private const string SUPPORTED_INPUT_KIND = "ffmpeg_source";

        /// <summary>
        /// Buffering MB
        /// </summary>
        [JsonProperty(PropertyName = "buffering_mb")]
        public int BufferingMB { get; set; } = 2;

        /// <summary>
        /// Clear window when media ends
        /// </summary>
        [JsonProperty(PropertyName = "clear_on_media_end")]
        public bool ClearOnMediaEnd { get; set; } = true;

        /// <summary>
        /// Close when inactive
        /// </summary>
        [JsonProperty(PropertyName = "close_when_inactive")]
        public bool CloseWhenInactive { get; set; } = false;

        /// <summary>
        /// Color Range
        /// </summary>
        [JsonProperty(PropertyName = "color_range")]
        public int ColorRange { get; set; } = 0;

        /// <summary>
        /// HW Decoder
        /// </summary>
        [JsonProperty(PropertyName = "hw_decode")]
        public bool HWDecode { get; set; } = false;

        /// <summary>
        /// Is Local file
        /// </summary>
        [JsonProperty(PropertyName = "is_local_file")]
        public bool IsLocalFile { get; set; } = true;

        /// <summary>
        /// Local filename
        /// </summary>
        [JsonProperty(PropertyName = "local_file")]
        public string LocalFile { get; set; }

        /// <summary>
        /// Looping
        /// </summary>
        [JsonProperty(PropertyName = "looping")]
        public bool Looping { get; set; } = false;

        /// <summary>
        /// Apply alpha in linear space
        /// </summary>
        [JsonProperty(PropertyName = "linear_alpha")]
        public bool LinearAlpha { get; set; } = false;

        /// <summary>
        /// Restart when activated
        /// </summary>
        [JsonProperty(PropertyName = "restart_on_activate")]
        public bool RestartOnActivate { get; set; } = true;

        /// <summary>
        /// ffmpeg options
        /// </summary>
        [JsonProperty(PropertyName = "ffmpeg_options")]
        public string Options { get; set; }

        /// <summary>
        /// Speed percentage
        /// </summary>
        [JsonProperty(PropertyName = "speed_percent")]
        public int SpeedPercent { get; set; } = 100;

        /// <summary>
        /// Static constructor to instanciate a InputFFMpegSettings object
        /// Requires an InputSettings class with InputKind of ffmpeg_source to create
        /// </summary>
        /// <param name="settings">Setings object</param>
        /// <returns></returns>
        public static InputFFMpegSettings FromInputSettings(InputSettings settings)
        {
            return new InputFFMpegSettings(settings);
        }
        // Private Constrctor
        private InputFFMpegSettings(InputSettings settings) : base(JObject.FromObject(settings))
        {
            if (settings.InputKind != SUPPORTED_INPUT_KIND)
            {
                throw new InvalidCastException();
            }
            JsonConvert.PopulateObject(settings.Settings.ToString(), this);
        }
    }
}
