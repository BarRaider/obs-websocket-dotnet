using Newtonsoft.Json.Linq;
using System;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Describes an Output in OBS.
    /// </summary>
    public class OBSOutputInfo
    {
        /// <summary>
        /// Creates a new <see cref="OBSOutputInfo"/> from a JSON object.
        /// If the response is recognized as a Stream or File output, a <see cref="StreamOutput"/> or <see cref="FileOutput"/> will be returned with the Output settings.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static OBSOutputInfo CreateOutput(JObject response)
        {
            string outputName = response["name"]?.Value<string>().ToLower() ?? throw new ErrorResponseException("Output response did not contain 'name' for the Output", response);
            if (outputName.Length == 0)
            {
                OBSLogger.Warning($"Invalid output name from response: {response.ToString(Newtonsoft.Json.Formatting.None)}");
                // TODO: Should this throw an exception or return null?
            }
            else if (outputName.Contains("stream"))
            {
                return new StreamOutput(response);
            }
            else if (outputName.Contains("file"))
            {
                return new FileOutput(response);
            }
            else if (outputName.Contains("replay"))
            {
                return new ReplayOutput(response);
            }
            else
            {
                OBSLogger.Warning($"Received an unrecognized output name: {outputName}");
            }
            return new OBSOutputInfo(response);
        }

        protected OBSOutputInfo(JObject response)
        {
            Name = response["name"]?.Value<string>() ?? throw new ErrorResponseException("Output response did not contain 'name'", response);
            Type = response["type"]?.Value<string>() ?? throw new ErrorResponseException("Output response did not contain 'type'", response);
            Width = response["width"]?.Value<int>() ?? -1;
            Height = response["height"]?.Value<int>() ?? -1;
            Flags = (OutputFlags)(response["flags"]?["rawValue"]?.Value<int>() ?? 0);
            Active = response["active"]?.Value<bool>() ?? false;
            Reconnecting = response["reconnecting"]?.Value<bool>() ?? false;
            Congestion = response["congestion"]?.Value<double>() ?? 0;
            TotalFrames = response["totalFrames"]?.Value<int>() ?? 0;
            DroppedFrames = response["droppedFrames"]?.Value<int>() ?? 0;
            TotalBytes = response["totalBytes"]?.Value<int>() ?? 0;
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

    public class StreamOutput : OBSOutputInfo
    {
        public StreamOutput(JObject response)
            : base(response)
        {
            if (response["settings"] is JObject settings)
                Settings = new StreamOutputSettings(settings);
        }

        public readonly StreamOutputSettings Settings;
    }
    public class FileOutput : OBSOutputInfo
    {
        public FileOutput(JObject response)
            : base(response)
        {

            if (response["settings"] is JObject settings)
                Settings = new FileOutputSettings(settings);
        }

        public readonly FileOutputSettings Settings;
    }

    public class ReplayOutput : OBSOutputInfo
    {
        public readonly ReplayOutputSettings Settings;
        public ReplayOutput(JObject response)
            : base(response)
        {
            if (response["settings"] is JObject settings)
                Settings = new ReplayOutputSettings(settings);
        }
    }

#pragma warning disable CA1815 // Override equals and operator equals on value types
    public struct FileOutputSettings
    {
        public FileOutputSettings(JObject jObject)
        {
            MuxerSettings = jObject["muxer_settings"]?.Value<string>();
            Path = jObject["path"]?.Value<string>();
        }
        public readonly string? MuxerSettings;
        public readonly string? Path;
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
        public readonly string? BindIP;
        public readonly bool DynamicBitrate;
        public readonly bool LowLatencyMode;
        public readonly bool NewSocketLoopEnabled;
    }

    public struct ReplayOutputSettings
    {
        public ReplayOutputSettings(JObject jObject)
        {
            AllowSpaces = jObject["allow_spaces"]?.Value<bool>() ?? false;
            Directory = jObject["directory"]?.Value<string>();
            Extension = jObject["extension"]?.Value<string>();
            Format = jObject["format"]?.Value<string>();
            MaxSizeMB = jObject["max_size_mb"]?.Value<int>() ?? 0;
            MaxTimeSecond = jObject["max_time_sec"]?.Value<int>() ?? 0;
            MuxerSettings = jObject["muxer_settings"]?.Value<string>();
            Path = jObject["path"]?.Value<string>();
        }

        public readonly bool AllowSpaces;
        public readonly string? Directory;
        public readonly string? Extension;
        public readonly string? Format;
        public readonly int MaxSizeMB;
        public readonly int MaxTimeSecond;
        public readonly string? MuxerSettings;
        public readonly string? Path;
    }
#pragma warning restore CA1815 // Override equals and operator equals on value types

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
