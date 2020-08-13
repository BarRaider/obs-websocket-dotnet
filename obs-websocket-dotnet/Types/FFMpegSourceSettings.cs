using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class FFMpegSourceSettings
    {
        /// <summary>
        /// Clear window when media ends
        /// </summary>
        [JsonProperty(PropertyName = "clear_on_media_end")]
        public bool ClearOnMediaEnd { get; set; }

        /// <summary>
        /// Close when inactive
        /// </summary>
        [JsonProperty(PropertyName = "close_when_inactive")]
        public bool CloseWhenInactive { get; set; }

        /// <summary>
        /// HW Decoder
        /// </summary>
        [JsonProperty(PropertyName = "color_range")]
        public int ColorRange { get; set; }

        /// <summary>
        /// HW Decoder
        /// </summary>
        [JsonProperty(PropertyName = "hw_decode")]
        public bool HWDecode { get; set; }

        /// <summary>
        /// Looping
        /// </summary>
        [JsonProperty(PropertyName = "looping")]
        public bool Looping { get; set; }

        /// <summary>
        /// Restart when activated
        /// </summary>
        [JsonProperty(PropertyName = "restart_on_activate")]
        public bool RestartOnActivate { get; set; }

        /// <summary>
        /// Is Local file
        /// </summary>
        [JsonProperty(PropertyName = "is_local_file")]
        public bool IsLocalFile { get; set; }

        /// <summary>
        /// Local filename
        /// </summary>
        [JsonProperty(PropertyName = "local_file")]
        public string? LocalFile { get; set; }

        /// <summary>
        /// Speed percentage
        /// </summary>
        [JsonProperty(PropertyName = "speed_percent")]
        public int SpeedPercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FFMpegSourceSettings()
        {
            SpeedPercent = 100;
            RestartOnActivate = true;
            HWDecode = true;
            ClearOnMediaEnd = true;
            IsLocalFile = true;
        }
    }
}
