using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
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
        [JsonPropertyName("fps_custom")]
        public bool CustomFPS { get; set; } = false;

        /// <summary>
        /// Frames Per Second
        /// </summary>
        [JsonPropertyName("fps")]
        public int FPS { get; set; } = 30;

        /// <summary>
        /// Control audio via OBS
        /// </summary>
        [JsonPropertyName("reroute_audio")]
        public bool RerouteAudio { get; set; } = false;

        /// <summary>
        /// Height
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; } = 600;

        /// <summary>
        /// Width
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; } = 800;

        /// <summary>
        /// Custom CSS
        /// </summary>
        [JsonPropertyName("css")]
        public string CSS { get; set; } = CSS_DEFAULT_VALUE;

        /// <summary>
        /// Is Local file
        /// </summary>
        [JsonPropertyName("is_local_file")]
        public bool IsLocalFile { get; set; } = false;

        /// <summary>
        /// Local filename (when IsLocalFile is true)
        /// </summary>
        [JsonPropertyName("local_file")]
        public string LocalFile { get; set; }

        /// <summary>
        /// URL (when IsLocalFile is false)
        /// </summary>
        [JsonPropertyName("url")]
        public string URL { get; set; }

        /// <summary>
        /// Refresh browser when scene becomes active
        /// </summary>
        [JsonPropertyName("restart_when_active")]
        public bool RestartWhenActive { get; set; } = false;

        /// <summary>
        /// Shutdown source when not visible
        /// </summary>
        [JsonPropertyName("shutdown")]
        public bool ShutdownWhenNotVisible { get; set; } = false;
              
        /// <summary>
        /// Page Permissions
        /// </summary>
        [JsonPropertyName("webpage_control_level")]
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
        private InputBrowserSourceSettings(InputSettings settings) : base(JsonSerializer.SerializeToNode(settings, AppJsonSerializerContext.Default.InputSettings)?.AsObject())
        {
            if (settings.InputKind != SUPPORTED_INPUT_KIND)
            {
                throw new InvalidCastException();
            }
            CustomFPS = settings.Settings["fps_custom"]?.GetValue<bool>() ?? CustomFPS;
            FPS = settings.Settings["fps"]?.GetValue<int>() ?? FPS;
            RerouteAudio = settings.Settings["reroute_audio"]?.GetValue<bool>() ?? RerouteAudio;
            Height = settings.Settings["height"]?.GetValue<int>() ?? Height;
            Width = settings.Settings["width"]?.GetValue<int>() ?? Width;
            CSS = settings.Settings["css"]?.GetValue<string>() ?? CSS;
            IsLocalFile = settings.Settings["is_local_file"]?.GetValue<bool>() ?? IsLocalFile;
            LocalFile = settings.Settings["local_file"]?.GetValue<string>() ?? LocalFile;
            URL = settings.Settings["url"]?.GetValue<string>() ?? URL;
            RestartWhenActive = settings.Settings["restart_when_active"]?.GetValue<bool>() ?? RestartWhenActive;
            ShutdownWhenNotVisible = settings.Settings["shutdown"]?.GetValue<bool>() ?? ShutdownWhenNotVisible;
            ControlLevel = settings.Settings["webpage_control_level"]?.GetValue<int>() ?? ControlLevel;

        }
    }
}
