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
    /// <summary>
    /// Base class for OBSWebsocket <see cref="EventArgs"/>.
    /// </summary>
    public abstract class JsonEventArgs : EventArgs
    {
        /// <summary>
        /// Event type.
        /// </summary>
        [JsonProperty("update-type")]
        protected string UpdateType = null!;

        /// <summary>
        /// Additional data not deserialized into the object.
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public Dictionary<string, JToken>? ExtensionData;

        /// <summary>
        /// Logs the unhandled properties if enabled by the <see cref="IOBSLogger"/>.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (ExtensionData != null && ExtensionData.Count > 0)
            {
                KeyValuePair<string, JToken>[] missedData = ExtensionData.Where(p => p.Key != "preview-only").ToArray();
                if (OBSLogger.LoggerSettings.HasFlag(OBSLoggerSettings.LogExtraEventData) && missedData.Length > 0)
                {
                    OBSLogger.Debug($"Data not taken in '{UpdateType}': {string.Join("\n", missedData.Select(p => $"\"{p.Key}\" : \"{p.Value}\""))}");
                }
            }
        }
    }

    /// <summary>
    /// Type of change made to a SceneItem. Used in <see cref="SceneItemUpdatedEventArgs"/>
    /// </summary>
    public enum SceneItemChangeType
    {
        /// <summary>
        /// No change or change type could not be determined.
        /// </summary>
        None = 0,
        /// <summary>
        /// Scene item was added.
        /// </summary>
        Added = 1,
        /// <summary>
        /// Scene item was removed.
        /// </summary>
        Removed = 2,
        /// <summary>
        /// Scene item was updated.
        /// </summary>
        Updated = 3
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SourceOrderChanged"/>
    /// </summary>
    public class SourceOrderChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the scene where items have been reordered.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        /// <summary>
        /// Array of scene items in their new order.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-items")]
        public SourceOrderChangedSceneItem[] SceneItems = null!;
    }

    /// <summary>
    /// Used in <see cref="SourceOrderChangedEventArgs"/> to describe a scene item.
    /// </summary>
    public class SourceOrderChangedSceneItem
    {
        /// <summary>
        /// Name of the source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("source-name")]
        public string SourceName = null!;
        /// <summary>
        /// Source's ID.
        /// </summary>
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
        /// <summary>
        /// Name of the scene affected.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        /// <summary>
        /// Name of the item.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        /// <summary>
        /// ID of the item.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
        /// <summary>
        /// Type of change applied to the item.
        /// </summary>
        [JsonIgnore]
        public SceneItemChangeType ChangeType;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneChanged"/>
    /// </summary>
    public class SceneChangeEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the scene that was switched to.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string NewSceneName = null!;
        /// <summary>
        /// Array of sources in the scene.
        /// </summary>
        [JsonProperty("sources")]
        public SceneItem[]? SceneItems;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemVisibilityChanged"/>
    /// </summary>
    public class SceneItemVisibilityChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the scene where the item is.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        /// <summary>
        /// Name of the concerned item.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        /// <summary>
        /// ID of the concerned item.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
        /// <summary>
        /// True if the item is visible.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-visible")]
        public bool IsVisible;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemLockChanged"/>
    /// </summary>
    public class SceneItemLockChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the scene where the item is.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        /// <summary>
        /// Name of the concerned item.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        /// <summary>
        /// ID of the concerned item.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
        /// <summary>
        /// True if the item is locked.
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-locked")]
        public bool IsLocked;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionChanged"/>
    /// </summary>
    public class TransitionChangeEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the new selected transition
        /// </summary>
        [JsonRequired]
        [JsonProperty("transition-name")]
        public string NewTransitionName = null!;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionDurationChanged"/>
    /// </summary>
    public class TransitionDurationChangeEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Value of the new transition duration (in milliseconds)
        /// </summary>
        [JsonRequired]
        [JsonProperty("new-duration")]
        public int NewDuration;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StreamingStateChanged"/>, <see cref="OBSWebsocket.RecordingStateChanged"/>
    /// or <see cref="OBSWebsocket.ReplayBufferStateChanged"/> 
    /// </summary>
    public class OutputStateChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// New output state
        /// </summary>
        [JsonIgnore]
        public OutputState OutputState;
        /// <summary>
        /// Length of the current recording, <see cref="TimeSpan.Zero"/> if not recording.
        /// </summary>
        [JsonProperty("rec-timecode")]
        public TimeSpan RecordingTime = TimeSpan.Zero;

        /// <summary>
        /// Length of the current stream, <see cref="TimeSpan.Zero"/> if not streaming.
        /// </summary>
        [JsonProperty("stream-timecode")]
        public TimeSpan StreamingTime = TimeSpan.Zero;

        /// <summary>
        /// Setter for the <see cref="OutputState"/>.
        /// </summary>
        [JsonRequired]
        [JsonProperty("update-type")]
        protected string EventType
        {
            set
            {
                OutputState = value switch
                {
                    "StreamStarting" => OutputState.Starting,
                    "StreamStarted" => OutputState.Started,
                    "StreamStopping" => OutputState.Stopping,
                    "StreamStopped" => OutputState.Stopped,
                    "RecordingStarting" => OutputState.Starting,
                    "RecordingStarted" => OutputState.Started,
                    "RecordingStopping" => OutputState.Stopping,
                    "RecordingStopped" => OutputState.Stopped,
                    "RecordingPaused" => OutputState.Paused,
                    "RecordingResumed" => OutputState.Resumed,
                    "ReplayStarting" => OutputState.Starting,
                    "ReplayStarted" => OutputState.Started,
                    "ReplayStopping" => OutputState.Stopping,
                    "ReplayStopped" => OutputState.Stopped,
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
    public class StreamStatusEventArgs : JsonEventArgs
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
        /// Length of the current stream, <see cref="TimeSpan.Zero"/> if not streaming.
        /// </summary>
        [JsonProperty(PropertyName = "stream-timecode")]
        public TimeSpan StreamTime { internal set; get; } = TimeSpan.Zero;

        /// <summary>
        /// Length of the current recording, <see cref="TimeSpan.Zero"/> if not recording.
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
    public class StudioModeChangeEventArgs : JsonEventArgs
    {
        /// <summary>
        /// New Studio Mode status
        /// </summary>
        [JsonRequired]
        [JsonProperty("new-state")]
        public bool StudioModeEnabled;
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.Heartbeat"/>
    /// </summary>
    public class HeartBeatEventArgs : JsonEventArgs
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

        /// <summary>
        /// Length of the current stream, <see cref="TimeSpan.Zero"/> if not streaming.
        /// </summary>
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

        /// <summary>
        /// True if recording is paused.
        /// </summary>
        [JsonProperty(PropertyName = "recording-paused")]
        public bool RecordingPaused { set; get; }

        /// <summary>
        /// Total time (in seconds) since recording started.
        /// </summary>
        [JsonProperty(PropertyName = "total-record-time")]
        public int TotalRecordTime { set; get; }

        /// <summary>
        /// Length of the current recording, <see cref="TimeSpan.Zero"/> if not recording.
        /// </summary>
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
    public class SceneItemSelectionEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the scene the item is in.
        /// </summary>
        [JsonRequired]
        [JsonProperty("scene-name")]
        public string SceneName = null!;
        /// <summary>
        /// Name of the item selected
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-name")]
        public string ItemName = null!;
        /// <summary>
        /// Id of the item selected
        /// </summary>
        [JsonRequired]
        [JsonProperty("item-id")]
        public int ItemId;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemTransformChanged"/>
    /// </summary>
    public class SceneItemTransformEventArgs : JsonEventArgs
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

        /// <summary>
        /// Creates an empty <see cref="SceneItemTransformEventArgs"/>.
        /// </summary>
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
    public class SourceAudioMixersChangedEventArgs : JsonEventArgs
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

        /// <summary>
        /// Creates an empty <see cref="SourceAudioMixersChangedEventArgs"/>.
        /// </summary>
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
    public class SourceAudioSyncOffsetEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the source for the offset change.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Sync offset value.
        /// </summary>
        [JsonProperty("syncOffset")]
        public int SyncOffset;
    }


    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceCreated"/>
    /// </summary>
    public class SourceCreatedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the created source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Source type. Can be "input", "scene", "transition" or "filter".
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceType")]
        public string SourceType = null!;
        /// <summary>
        /// Source kind.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceKind")]
        public string SourceKind = null!;
        /// <summary>
        /// Settings for the source.
        /// </summary>
        [JsonProperty("sourceSettings", NullValueHandling = NullValueHandling.Ignore, IsReference = true)]
        public SourceSettings? Settings = null;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceDestroyed"/>
    /// </summary>
    public class SourceDestroyedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the destroyed source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Source type. Can be "input", "scene", "transition" or "filter".
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceType")]
        public string SourceType = null!;
        /// <summary>
        /// Source kind.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceKind")]
        public string SourceKind = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceRenamed"/>
    /// </summary>
    public class SourceRenamedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// New name of source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("newName")]
        public string NewName = null!;
        /// <summary>
        /// Previous name of source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("previousName")]
        public string PreviousName = null!;
        /// <summary>
        /// Source type. Can be "input", "scene", "transition" or "filter".
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceType")]
        public string sourceType = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceMuteStateChanged"/>
    /// </summary>
    public class SourceMuteStateChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Current mute state of source
        /// </summary>
        [JsonRequired]
        [JsonProperty("muted")]
        public bool Muted;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceVolumeChanged"/>
    /// </summary>
    public class SourceVolumeChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Current volume level of source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("volume")]
        public float Volume;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterRemoved"/>
    /// </summary>
    public class SourceFilterRemovedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Name of the removed filter
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterName")]
        public string FilterName = null!;
        /// <summary>
        /// Type of the removed filter.
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterType")]
        public string FilterType = null!;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterAdded"/>
    /// </summary>
    public class SourceFilterAddedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Name of the added filter
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterName")]
        public string FilterName = null!;
        /// <summary>
        /// Type of the added filter.
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterType")]
        public string FilterType = null!;
        /// <summary>
        /// Settings for the filter, may be null.
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterSettings")]
        public JObject? FilterSettings;
    }

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFiltersReordered"/>
    /// </summary>
    public class SourceFiltersReorderedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the source.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;
        /// <summary>
        /// Array of Filters in their new order.
        /// </summary>
        [JsonRequired]
        [JsonProperty("filters")]
        public FilterReorderItem[] Filters = null!;
    }

    /// <summary>
    /// The visibility/enabled state of a filter changed.
    /// Used by <see cref="OBSWebsocket.SourceFilterVisibilityChanged"/>.
    /// </summary>
    public class SourceFilterVisibilityChangedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the source the filter is in.
        /// </summary>
        [JsonRequired]
        [JsonProperty("sourceName")]
        public string SourceName = null!;

        /// <summary>
        /// Name of the filter.
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterName")]
        public string FilterName = null!;

        /// <summary>
        /// True if the filter is enabled (visible).
        /// </summary>
        [JsonRequired]
        [JsonProperty("filterEnabled")]
        public bool FilterEnabled;
    }

    /// <summary>
    /// Base class for transition events.
    /// </summary>
    public abstract class TransitionEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Name of the transition.
        /// </summary>
        [JsonRequired]
        [JsonProperty("name")]
        public string TransitionName = null!;
        /// <summary>
        /// Type of the transition.
        /// </summary>
        [JsonRequired]
        [JsonProperty("type")]
        public string TransitionType = null!;
        /// <summary>
        /// Duration of the transition.
        /// </summary>
        [JsonRequired]
        [JsonProperty("duration")]
        public int Duration;
        /// <summary>
        /// Scene that was transitioned into.
        /// </summary>
        [JsonRequired]
        [JsonProperty("to-scene")]
        public string ToScene = null!;
    }

    /// <summary>
    /// Used by <see cref="OBSWebsocket.TransitionBegin"/>
    /// </summary>
    public class TransitionBeginEventArgs : TransitionEventArgs
    {
        /// <summary>
        /// Scene being transitioned out of.
        /// </summary>
        [JsonRequired]
        [JsonProperty("from-scene")]
        public string FromScene = null!;
    }
    /// <summary>
    /// Used by <see cref="OBSWebsocket.TransitionEnd"/>.
    /// </summary>
    public class TransitionEndEventArgs : TransitionEventArgs
    {
    }

    /// <summary>
    /// Used by <see cref="OBSWebsocket.TransitionVideoEnd"/>.
    /// </summary>
    public class TransitionVideoEndEventArgs : TransitionEventArgs
    {
        /// <summary>
        /// Scene being transitioned out of.
        /// </summary>
        [JsonRequired]
        [JsonProperty("from-scene")]
        public string FromScene = null!;
    }

    /// <summary>
    /// Used by <see cref="OBSWebsocket.BroadcastCustomMessageReceived"/>.
    /// </summary>
    public class BroadcastCustomMessageReceivedEventArgs : JsonEventArgs
    {
        /// <summary>
        /// Identifier chosen by the client sending the broadcast. 
        /// </summary>
        [JsonRequired]
        [JsonProperty("realm")]
        public string Realm = null!;

        /// <summary>
        /// Data defined by the client sending the broadcast.
        /// </summary>
        [JsonProperty("data")]
        public JObject? Data;

    }
}
