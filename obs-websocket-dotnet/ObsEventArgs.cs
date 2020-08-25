using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OBSWebsocketDotNet
{
    public class JsonEventArgs : EventArgs
    {
        [JsonProperty("update-type")]
        protected string UpdateType = null!;

        [JsonExtensionData(ReadData = true)]
        public Dictionary<string, JToken>? ExtensionData;

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (ExtensionData != null)
            {
                KeyValuePair<string, JToken>[] missedData = ExtensionData.Where(p => p.Key != "preview-only").ToArray();
                if (OBSLogger.LoggerSettings.HasFlag(OBSLoggerSettings.LogExtraEventData) && missedData.Length > 0)
                {
                    OBSLogger.Debug($"Data not taken in '{UpdateType}': {string.Join("\n", missedData.Select(p => $"\"{p.Key}\" : \"{p.Value}\""))}");
                }
            }
        }
    }

    public enum ChangeType
    {
        None = 0,
        Added = 1,
        Removed = 2,
        Updated = 3
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SourceOrderChanged"/>
    /// </summary>
    public class SourceOrderChangedEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        [JsonRequired]
        [JsonProperty("scene-items")]
        public SourceOrderChangedSceneItem[] SceneItems = null!;
    }
    public class SourceOrderChangedSceneItem
    {
        [JsonRequired]
        [JsonProperty("source-name")]
        public string ItemSourceName = null!;
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
    }
    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemAdded"/>
    /// or <see cref="OBSWebsocket.SceneItemRemoved"/>
    /// </summary>
    public class SceneItemUpdatedEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
        [JsonIgnore]
        public ChangeType ChangeType;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneChanged"/>
    /// </summary>
    public class SceneChangeEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string NewSceneName = null!;
        [JsonProperty("sources")]
        public SceneItem[]? SceneItems;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemVisibilityChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="itemName">Name of the concerned item</param>
    /// <param name="isVisible">Visibility of the item</param>
    public class SceneItemVisibilityChangedEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
        [JsonRequired]
        [JsonProperty("item-visible")]
        public bool IsVisible;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newTransitionName">Name of the new selected transition</param>
    public class TransitionChangeEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("transition-name")]
        public string NewTransitionName = null!;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionDurationChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newDuration">Name of the new transition duration (in milliseconds)</param>
    public class TransitionDurationChangeEventArgs : JsonEventArgs //OBSWebsocket sender, int newDuration);
    {
        [JsonRequired]
        [JsonProperty("new-duration")]
        public int NewDuration;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StreamingStateChanged"/>, <see cref="OBSWebsocket.RecordingStateChanged"/>
    /// or <see cref="OBSWebsocket.ReplayBufferStateChanged"/> 
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="type">New output state</param>
    public class OutputStateChangedEventArgs : JsonEventArgs //OBSWebsocket sender, OutputState type);
    {
        [JsonIgnore]
        public OutputState OutputState;
        [JsonProperty("rec-timecode")]
        public TimeSpan RecordingTime = TimeSpan.Zero;
        [JsonProperty("stream-timecode")]
        public TimeSpan StreamingTime = TimeSpan.Zero;
        [JsonRequired]
        [JsonProperty("update-type")]
        protected string EventType
        {
            set
            {
                OutputState = value switch
                {
                    "StreamStarting" =>     OutputState.Starting,
                    "StreamStarted" =>      OutputState.Started,
                    "StreamStopping" =>     OutputState.Stopping,
                    "StreamStopped" =>      OutputState.Stopped,
                    "RecordingStarting" =>  OutputState.Starting,
                    "RecordingStarted" =>   OutputState.Started,
                    "RecordingStopping" =>  OutputState.Stopping,
                    "RecordingStopped" =>   OutputState.Stopped,
                    "RecordingPaused" =>    OutputState.Paused,
                    "RecordingResumed" =>   OutputState.Resumed,
                    "ReplayStarting" =>     OutputState.Starting,
                    "ReplayStarted" =>      OutputState.Started,
                    "ReplayStopping" =>     OutputState.Stopping,
                    "ReplayStopped" =>      OutputState.Stopped,
                    _ => OutputState.Unknown
                };
                if (OutputState == OutputState.Unknown)
                    OBSLogger.Warning($"OutputState could not be determined from '{value}'.");
            }
        }

    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StreamStatus"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="status">Stream status data</param>
    public class StreamStatusEventArgs : JsonEventArgs //OBSWebsocket sender, StreamStatus status);
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "streaming")]
        public bool Streaming { internal set; get; }

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public bool Recording { internal set; get; }

        /// <summary>
        /// True if the replay buffer is active
        /// </summary>
        [JsonProperty(PropertyName = "replay-buffer-active")]
        public bool ReplayBufferActive { internal set; get; }

        /// <summary>
        /// Stream bitrate in bytes per second
        /// </summary>
        [JsonProperty(PropertyName = "bytes-per-sec")]
        public int BytesPerSec { internal set; get; }

        /// <summary>
        /// Stream bitrate in kilobits per second
        /// </summary>
        [JsonProperty(PropertyName = "kbits-per-sec")]
        public int KbitsPerSec { internal set; get; }

        /// <summary>
        /// Percentage of dropped frames
        /// </summary>
        [JsonProperty(PropertyName = "strain")]
        public float Strain { internal set; get; }

        /// <summary>
        /// Total time since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-time")]
        public int TotalStreamTime { internal set; get; }

        /// <summary>
        /// Number of frames sent since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "num-total-frames")]
        public int TotalFrames { internal set; get; }

        /// <summary>
        /// Overall number of frames dropped since streaming start
        /// </summary>
        [JsonProperty(PropertyName = "num-dropped-frames")]
        public int DroppedFrames { internal set; get; }

        /// <summary>
        /// Current framerate in Frames Per Second
        /// </summary>
        [JsonProperty(PropertyName = "fps")]
        public float FPS { internal set; get; }
        /// <summary>
        /// Number of frames rendered
        /// </summary>
        [JsonProperty(PropertyName = "render-total-frames")]
        public int RenderTotalFrames { internal set; get; }

        /// <summary>
        /// Number of frames missed due to rendering lag
        /// </summary>
        [JsonProperty(PropertyName = "render-missed-frames")]
        public int RenderMissedFrames { internal set; get; }

        /// <summary>
        /// Number of frames outputted
        /// </summary>
        [JsonProperty(PropertyName = "output-total-frames")]
        public int OutputTotalFrames { internal set; get; }

        /// <summary>
        /// Total number of skipped frames
        /// </summary>
        [JsonProperty(PropertyName = "output-skipped-frames")]
        public int SkippedFrames { internal set; get; }

        /// <summary>
        /// Average frame time (in milliseconds)
        /// </summary>
        [JsonProperty(PropertyName = "average-frame-time")]
        public double AverageFrameTime { internal set; get; }

        /// <summary>
        /// Current CPU usage (percentage)
        /// </summary>
        [JsonProperty(PropertyName = "cpu-usage")]
        public double CPU { internal set; get; }

        /// <summary>
        /// Current RAM usage (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "memory-usage")]
        public double MemoryUsage { internal set; get; }


        /// <summary>
        /// Free recording disk space (in megabytes)
        /// </summary>
        [JsonProperty(PropertyName = "free-disk-space")]
        public double FreeDiskSpace { internal set; get; }

        /// <summary>
        /// Overall stream time
        /// </summary>
        [JsonProperty(PropertyName = "stream-timecode")]
        public TimeSpan StreamTime { internal set; get; } = TimeSpan.Zero;

        /// <summary>
        /// Length of the current recording.
        /// </summary>
        [JsonProperty(PropertyName = "rec-timecode")]
        public TimeSpan? RecordingTime { internal set; get; } = TimeSpan.Zero;

        /// <summary>
        /// True if recording is paused, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "recording-paused")]
        public bool RecordingPaused { internal set; get; }
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StudioModeSwitched"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="enabled">New Studio Mode status</param>
    public class StudioModeChangeEventArgs : JsonEventArgs //OBSWebsocket sender, bool enabled);
    {
        [JsonRequired]
        [JsonProperty("new-state")]
        public bool StudioModeEnabled;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.Heartbeat"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="heatbeat">heartbeat data</param>
    public class HeartBeatEventArgs : JsonEventArgs //OBSWebsocket sender, Heartbeat heatbeat);
    {
        /// <summary>
        /// Toggles between every JSON message as an "I am alive" indicator.
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "pulse")]
        public bool Pulse { set; get; }

        /// <summary>
        /// Current active profile.
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "current-profile")]
        public string CurrentProfile { set; get; } = null!;

        /// <summary>
        /// Current active scene.
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "current-scene")]
        public string CurrentScene { set; get; } = null!;

        /// <summary>
        /// Current streaming state.
        /// </summary>
        [JsonProperty(PropertyName = "streaming")]
        public bool Streaming { set; get; }

        /// <summary>
        /// Total time (in seconds) since the stream started.
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-time")]
        public int TotalStreamTime { set; get; }

        [JsonProperty("stream-timecode")]
        public TimeSpan StreamTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Total bytes sent since the stream started.
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-bytes")]
        public ulong TotalStreamBytes { set; get; }

        /// <summary>
        /// Total frames streamed since the stream started.
        /// </summary>
        [JsonProperty(PropertyName = "total-stream-frames")]
        public ulong TotalStreamFrames { set; get; }

        /// <summary>
        /// Current recording state.
        /// </summary>
        [JsonProperty(PropertyName = "recording")]
        public bool Recording { set; get; }

        [JsonProperty(PropertyName = "recording-paused")]
        public bool RecordingPaused { set; get; }

        /// <summary>
        /// Total time (in seconds) since recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-time")]
        public int TotalRecordTime { set; get; }

        [JsonProperty("rec-timecode")]
        public TimeSpan RecordTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Total bytes recorded since the recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-bytes")]
        public int TotalRecordBytes { set; get; }

        /// <summary>
        /// Total frames recorded since the recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-frames")]
        public int TotalRecordFrames { set; get; }

        /// <summary>
        /// OBS Stats
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "stats")]
        public OBSStats Stats { set; get; } = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemDeselected"/> and <see cref="OBSWebsocket.SceneItemSelected"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene item was in</param>
    /// <param name="itemName">Name of the item deselected</param>
    /// <param name="itemId">Id of the item deselected</param>
    public class SceneItemSelectionEventArgs : JsonEventArgs //OBSWebsocket sender, string sceneName, string itemName, string itemId);
    {
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemTransformChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transform">Transform data</param>
    public class SceneItemTransformEventArgs : JsonEventArgs //OBSWebsocket sender, SceneItemTransformInfo transform);
    {
        /// <summary>
        /// Name of the scene
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "scene-name")]
        public string SceneName { internal set; get; } = null!;

        /// <summary>
        /// Name of the item in the scene
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "item-name")]
        public string ItemName { internal set; get; } = null!;

        /// <summary>
        /// Scene Item ID
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "item-id")]
        public string ItemID { internal set; get; } = null!;

        /// <summary>
        /// Scene item transform properties
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "transform")]
        public SceneItemProperties Transform { internal set; get; } = null!;

        public SceneItemTransformEventArgs()
        { }

        /// <summary>
        /// Initialize the scene item transform
        /// </summary>
        /// <param name="body"></param>
        public SceneItemTransformEventArgs(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }
    }


    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceAudioMixersChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="mixerInfo">Mixer information that was updated</param>
    public class SourceAudioMixersChangedEventArgs : JsonEventArgs //OBSWebsocket sender, AudioMixersChangedInfo mixerInfo);
    {
        /// <summary>
        /// Mixer source name
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; } = null!;

        /// <summary>
        /// Routing status of the source for each audio mixer (array of 6 values)
        /// </summary>
        [JsonProperty(PropertyName = "mixers")]
        public List<AudioMixerChannel> Mixers { get; set; } = new List<AudioMixerChannel>();

        /// <summary>
        /// Raw mixer flags (little-endian, one bit per mixer) as an hexadecimal value
        /// </summary>
        [JsonProperty(PropertyName = "hexMixersValue")]
        public string? HexMixersValue { set; get; }

        public SourceAudioMixersChangedEventArgs() { }
        /// <summary>
        /// Create mixer response
        /// </summary>
        /// <param name="body"></param>
        public SourceAudioMixersChangedEventArgs(JObject body)
        {
            if (body == null) return;
            JsonConvert.PopulateObject(body.ToString(), this);
        }
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceAudioSyncOffsetChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of the source for the offset change</param>
    /// <param name="syncOffset">Sync offset value</param>
    public class SourceAudioSyncOffsetEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, int syncOffset);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonProperty("syncOffset")]
        public int SyncOffset;
    }


    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceCreated"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="settings">Newly created source settings</param>
    public class SourceCreatedEventArgs : JsonEventArgs //OBSWebsocket sender, SourceSettings settings);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("sourceType")]
        public string SourceType = null!;
        [JsonRequired]
        [JsonProperty("sourceKind")]
        public string SourceKind = null!;
        [JsonProperty("sourceSettings", NullValueHandling = NullValueHandling.Ignore, IsReference = true)]
        public SourceSettings? Settings = null;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceDestroyed"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Newly destroyed source information</param>
    /// <param name="sourceKind">Kind of source destroyed</param>
    /// <param name="sourceType">Type of source destroyed</param>
    public class SourceDestroyedEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, string sourceType, string sourceKind);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("sourceType")]
        public string SourceType = null!;
        [JsonRequired]
        [JsonProperty("sourceKind")]
        public string SourceKind = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceRenamed"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newName">New name of source</param>
    /// <param name="previousName">Previous name of source</param>
    public class SourceRenamedEventArgs : JsonEventArgs //OBSWebsocket sender, string newName, string previousName);
    {
        [JsonRequired]
        [JsonProperty("newName")]
        public string NewName = null!;
        [JsonRequired]
        [JsonProperty("previousName")]
        public string PreviousName = null!;
        [JsonRequired]
        [JsonProperty("sourceType")]
        public string sourceType = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceMuteStateChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="muted">Current mute state of source</param>
    public class SourceMuteStateChangedEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, bool muted);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("muted")]
        public bool Muted;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceVolumeChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="volume">Current volume level of source</param>
    public class SourceVolumeChangedEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, float volume);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("volume")]
        public float Volume;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterRemoved"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filterName">Name of removed filter</param>
    public class SourceFilterRemovedEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, string filterName);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("filterName")]
        public string FilterName = null!;
        [JsonRequired]
        [JsonProperty("filterType")]
        public string FilterType = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterAdded"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filterName">Name of filter</param>
    /// <param name="filterType">Type of filter</param>
    /// <param name="filterSettings">Settings for filter</param>
    public class SourceFilterAddedEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, string filterName, string filterType, JObject? filterSettings);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("filterName")]
        public string FilterName = null!;
        [JsonRequired]
        [JsonProperty("filterType")]
        public string FilterType = null!;
        [JsonRequired]
        [JsonProperty("filterSettings")]
        public JObject? FilterSettings;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFiltersReordered"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filters">Current order of filters for source</param>
    public class SourceFiltersReorderedEventArgs : JsonEventArgs //OBSWebsocket sender, string sourceName, List<FilterReorderItem> filters);
    {
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        [JsonRequired]
        [JsonProperty("filters")]
        public FilterReorderItem[] Filters = null!;
    }

    public class TransitionBeginEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("name")]
        public string TransitionName = null!;
        [JsonRequired]
        [JsonProperty("type")]
        public string TransitionType = null!;
        [JsonRequired]
        [JsonProperty("duration")]
        public int Duration;
        [JsonRequired]
        [JsonProperty("from-scene")]
        public string FromScene = null!;
        [JsonRequired]
        [JsonProperty("to-scene")]
        public string ToScene = null!;
    }
    public class TransitionEndEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("to-scene")]
        public string ToScene = null!;
        [JsonRequired]
        [JsonProperty("name")]
        public string TransitionName = null!;
        [JsonRequired]
        [JsonProperty("type")]
        public string TransitionType = null!;
        [JsonRequired]
        [JsonProperty("duration")]
        public int Duration;
    }

    public class TransitionVideoEndEventArgs : JsonEventArgs
    {
        [JsonRequired]
        [JsonProperty("name")]
        public string TransitionName = null!;
        [JsonRequired]
        [JsonProperty("type")]
        public string TransitionType = null!;
        [JsonRequired]
        [JsonProperty("duration")]
        public int Duration;
        [JsonRequired]
        [JsonProperty("from-scene")]
        public string FromScene = null!;
        [JsonRequired]
        [JsonProperty("to-scene")]
        public string ToScene = null!;
    }
}
