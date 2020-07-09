using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace obs_websocket_dotnet.Types
{
    /// <summary>
    /// Describes an Output in OBS.
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Creates a new <see cref="Output"/> from a JSON object.
        /// If the response is recognized as a Stream or File output, a <see cref="StreamOutput"/> or <see cref="FileOutput"/> will be returned with the Output settings.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Output CreateOutput(JObject response)
        {
            string outputName = response["name"]?.Value<string>().ToLower();
            if (string.IsNullOrEmpty(outputName))
            {
                OBSLogger.Warning($"Invalid output name from response: {response.ToString()}");
            }
            else if (outputName.Contains("stream"))
            {
                return new StreamOutput(response);
            }
            else if (outputName.Contains("file"))
            {
                return new FileOutput(response);
            }
            else
            {
                OBSLogger.Warning($"Received an unrecognized output name: {outputName}");
            }
            return new Output(response);
        }

        protected Output(JObject response)
        {
            Name = response["name"].Value<string>();
            Type = response["type"].Value<string>();
            Width = response["width"].Value<int>();
            Height = response["height"].Value<int>();
            Flags = (OutputFlags)response["flags"]["rawValue"].Value<int>();
            Active = response["active"].Value<bool>();
            Reconnecting = response["reconnecting"].Value<bool>();
            Congestion = response["congestion"].Value<double>();
            TotalFrames = response["totalFrames"].Value<int>();
            DroppedFrames = response["droppedFrames"].Value<int>();
            TotalBytes = response["totalBytes"].Value<int>();
        }
        public readonly string Name;
        public readonly string Type;
        public readonly int Width;
        public readonly int Height;
        public readonly OutputFlags Flags;
        public readonly bool Active;
        public readonly bool Reconnecting;
        public readonly double Congestion;
        public readonly int TotalFrames;
        public readonly int DroppedFrames;
        public readonly int TotalBytes;
    }

    public class StreamOutput : Output
    {
        public StreamOutput(JObject response)
            : base(response)
        {

            Settings = new StreamOutputSettings(response["settings"] as JObject);
        }

        public readonly StreamOutputSettings Settings;
    }
    public class FileOutput : Output
    {
        public FileOutput(JObject response)
            : base(response)
        {

            Settings = new FileOutputSettings(response["settings"] as JObject);
        }

        public readonly FileOutputSettings Settings;
    }

    public struct FileOutputSettings
    {
        public FileOutputSettings(JObject jObject)
        {
            MuxerSettings = jObject["muxer_settings"]?.Value<string>();
            Path = jObject["path"]?.Value<string>();
        }
        public string MuxerSettings;
        public string Path;
    }

    public struct StreamOutputSettings
    {
        public StreamOutputSettings(JObject jObject)
        {
            BindIP = jObject["bind_ip"]?.Value<string>();
            DynamicBitrate = jObject["dyn_bitrate"]?.Value<bool>() ?? false;
            LowLatencyMode = jObject["low_latency_mode_enabled"]?.Value<bool>() ?? false;
            NewSocketLoopEnabled = jObject["new_socket_loop_enabled"]?.Value<bool>() ?? false;
        }
        public string BindIP;
        public bool DynamicBitrate;
        public bool LowLatencyMode;
        public bool NewSocketLoopEnabled;
    }

    [Flags]
    public enum OutputFlags
    {
        None = 0,
        Video = 1 << 0,
        Audio = 1 << 1,
        AV = Video | Audio,
        Encoded = 1 << 2,
        UsesService = 1 << 3,
        Multitrack = 1 << 4,
        CanPause = 1 << 5
    }
}
