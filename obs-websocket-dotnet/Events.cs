using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Websocket.Client;

namespace OBSWebsocketDotNet
{
    #region EventDelegates
    /// <summary>
    /// Called by <see cref="OBSWebsocket.CurrentProgramSceneChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newSceneName">Name of the new current scene</param>
    public delegate void CurrentProgramSceneChangeCallback(OBSWebsocket sender, string newSceneName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneListChanged"/>
    /// The list of scenes has changed.
    /// TODO: Make OBS fire this event when scenes are reordered.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="scenes">Updated array of scenes</param>
    public delegate void SceneListChangedCallback(OBSWebsocket sender, List<JObject> scenes);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemListReindexed"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where items where reordered</param>
    /// /// <param name="sceneItems">List of all scene items as JObject</param>
    public delegate void SceneItemListReindexedCallback(OBSWebsocket sender, string sceneName, List<JObject> sceneItems);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemCreated"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="sourceName">Name of the concerned item</param>
    /// <param name="sceneItemId">Numeric ID of the scene item</param>
    /// <param name="sceneItemIndex">Index position of the item</param>
    public delegate void SceneItemCreatedCallback(OBSWebsocket sender, string sceneName, string sourceName, int sceneItemId, int sceneItemIndex);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemRemoved"/>
    /// A scene item has been removed.
    /// This event is not emitted when the scene the item is in is removed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene the item was removed from</param>
    /// <param name="sourceName">Name of the underlying source (input/scene)</param>
    /// <param name="sceneItemId">Numeric ID of the scene item</param>
    public delegate void SceneItemRemovedCallback(OBSWebsocket sender, string sceneName, string sourceName, int sceneItemId);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemEnableStateChanged"/>
    /// A scene item's enable state has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene the item is in</param>
    /// <param name="sceneItemId">Numeric ID of the scene item</param>
    /// <param name="sceneItemEnabled">Whether the scene item is enabled (visible)</param>
    public delegate void SceneItemEnableStateChangedCallback(OBSWebsocket sender, string sceneName, int sceneItemId, bool sceneItemEnabled);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemLockStateChanged"/>
    /// A scene item's lock state has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene the item is in</param>
    /// <param name="sceneItemId">Numeric ID of the scene item</param>
    /// <param name="sceneItemLocked">Whether the scene item is locked</param>
    public delegate void SceneItemLockStateChangedCallback(OBSWebsocket sender, string sceneName, int sceneItemId, bool sceneItemLocked);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.CurrentSceneCollectionChanged"/>
    /// The current scene collection has changed.
    /// Note: If polling has been paused during `CurrentSceneCollectionChanging`, this is the que to restart polling.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneCollectionName">Name of the new scene collection</param>
    public delegate void CurrentSceneCollectionChangedCallback(OBSWebsocket sender, string sceneCollectionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneCollectionListChanged"/>
    /// The scene collection list has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneCollections">Updated list of scene collections</param>
    public delegate void SceneCollectionListChangedCallback(OBSWebsocket sender, List<string> sceneCollections);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.CurrentSceneTransitionChanged"/>
    /// The current scene transition has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Name of the new transition</param>
    public delegate void CurrentSceneTransitionChangedCallback(OBSWebsocket sender, string transitionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.CurrentSceneTransitionDurationChanged"/>
    /// The current scene transition duration has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionDuration">Transition duration in milliseconds</param>
    public delegate void CurrentSceneTransitionDurationChangedCallback(OBSWebsocket sender, int transitionDuration);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneTransitionStarted"/>
    /// A scene transition has started.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Transition name</param>
    public delegate void SceneTransitionStartedCallback(OBSWebsocket sender, string transitionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneTransitionEnded"/>
    /// A scene transition has completed fully.
    /// Note: Does not appear to trigger when the transition is interrupted by the user.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Scene transition name</param>
    public delegate void SceneTransitionEndedCallback(OBSWebsocket sender, string transitionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneTransitionVideoEnded"/>
    /// A scene transition's video has completed fully.
    /// Useful for stinger transitions to tell when the video *actually* ends.
    /// `SceneTransitionEnded` only signifies the cut point, not the completion of transition playback.
    /// Note: Appears to be called by every transition, regardless of relevance.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Scene transition name</param>
    public delegate void SceneTransitionVideoEndedCallback(OBSWebsocket sender, string transitionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.CurrentProfileChanged"/>
    /// The current profile has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="profileName">Name of the new profile</param>
    public delegate void CurrentProfileChangedCallback(OBSWebsocket sender, string profileName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.ProfileListChanged"/>
    /// The profile list has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="profiles">Updated list of profiles</param>
    public delegate void ProfileListChangedCallback(OBSWebsocket sender, List<string> profiles);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StreamStateChanged"/>
    /// The state of the stream output has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="outputActive">Whether the output is active</param>
    /// <param name="outputState">The specific state of the output</param>
    public delegate void StreamStateChangedCallback(OBSWebsocket sender, bool outputActive, string outputState);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.RecordStateChanged"/>
    /// The state of the record output has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="outputActive">Whether the output is active</param>
    /// <param name="outputState">The specific state of the output</param>
    public delegate void RecordStateChangedCallback(OBSWebsocket sender, bool outputActive, string outputState);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.CurrentPreviewSceneChanged"/>
    /// The current preview scene has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene that was switched to</param>
    public delegate void CurrentPreviewSceneChangedCallback(OBSWebsocket sender, string sceneName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StudioModeStateChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="studioModeEnabled">New Studio Mode status</param>
    public delegate void StudioModeStateChangedCallback(OBSWebsocket sender, bool studioModeEnabled);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StudioModeStateChanged"/>
    /// The state of the replay buffer output has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="outputActive">Whether the output is active</param>
    /// <param name="outputState">The specific state of the output</param>
    public delegate void ReplayBufferStateChangedCallback(OBSWebsocket sender, bool outputActive, string outputState);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemSelected"/>
    /// A scene item has been selected in the Ui.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene item is in</param>
    /// <param name="sceneItemId">Numeric ID of the scene item</param>
    public delegate void SceneItemSelectedCallback(OBSWebsocket sender, string sceneName, string sceneItemId);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemTransformChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transform">Transform data</param>
    public delegate void SceneItemTransformCallback(OBSWebsocket sender, string sceneName, string sceneItemId, SceneItemTransformInfo transform);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputAudioSyncOffsetChanged"/>
    /// The sync offset of an input has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="inputAudioSyncOffset">New sync offset in milliseconds</param>
    public delegate void InputAudioSyncOffsetChangedCallback(OBSWebsocket sender, string inputName, int inputAudioSyncOffset);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputMuteStateChanged"/>
    /// An input's mute state has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="inputMuted">Whether the input is muted</param>
    public delegate void InputMuteStateChangedCallback(OBSWebsocket sender, string inputName, bool inputMuted);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputVolumeChanged"/>
    /// An input's volume level has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="volume">Current volume levels of source</param>
    public delegate void InputVolumeChangedCallback(OBSWebsocket sender, InputVolume volume);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterRemoved"/>
    /// A filter has been removed from a source.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of the source the filter was on</param>
    /// <param name="filterName">Name of the filter</param>
    public delegate void SourceFilterRemovedCallback(OBSWebsocket sender, string sourceName, string filterName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterCreated"/>
    /// A filter has been added to a source.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of the source the filter was added to</param>
    /// <param name="filterName">Name of the filter</param>
    /// <param name="filterKind">The kind of the filter</param>
    /// <param name="filterIndex">Index position of the filter</param>
    /// <param name="filterSettings">The settings configured to the filter when it was created</param>
    /// <param name="defaultFilterSettings">The default settings for the filter</param>
    public delegate void SourceFilterCreatedCallback(OBSWebsocket sender, string sourceName, string filterName, string filterKind, int filterIndex, JObject filterSettings, JObject defaultFilterSettings);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterListReindexed"/>
    /// A source's filter list has been reindexed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of the source</param>
    /// <param name="filters">Array of filter objects</param>
    public delegate void SourceFilterListReindexedCallback(OBSWebsocket sender, string sourceName, List<FilterReorderItem> filters);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterEnableStateChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of the source the filter is on</param>
    /// <param name="filterName">Name of the filter</param>
    /// <param name="filterEnabled">Whether the filter is enabled</param>
    public delegate void SourceFilterEnableStateChangedCallback(OBSWebsocket sender, string sourceName, string filterName, bool filterEnabled);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.VendorEvent"/>
    /// An event has been emitted from a vendor.
    /// A vendor is a unique name registered by a third-party plugin or script, which allows for custom requests and events to be added to obs-websocket.
    /// If a plugin or script implements vendor requests or events, documentation is expected to be provided with them.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="vendorName">Name of the vendor emitting the event</param>
    /// <param name="eventType">Vendor-provided event typedef</param>
    /// <param name="eventData">Vendor-provided event data. {} if event does not provide any data</param>
    public delegate void VendorEventCallback(OBSWebsocket sender, string vendorName, string eventType, JObject eventData);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaInputPlaybackEnded"/>
    /// A media input has finished playing.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    public delegate void MediaInputPlaybackEndedCallback(OBSWebsocket sender, string inputName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaInputPlaybackStarted"/>
    /// A media input has started playing.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    public delegate void MediaInputPlaybackStartedCallback(OBSWebsocket sender, string inputName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaInputActionTriggered"/>
    /// An action has been performed on an input.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="mediaAction">Action performed on the input. See `ObsMediaInputAction` enum</param>
    public delegate void MediaInputActionTriggeredCallback(OBSWebsocket sender, string inputName, string mediaAction);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.VirtualcamStateChanged"/>
    /// The state of the virtualcam output has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="outputActive">Whether the output is active</param>
    /// <param name="outputState">The specific state of the output</param>
    public delegate void VirtualcamStateChangedCallback(OBSWebsocket sender, bool outputActive, string outputState);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.VirtualcamStateChanged"/>
    /// The current scene collection has begun changing.
    /// Note: We recommend using this event to trigger a pause of all polling requests, as performing any requests during a
    /// scene collection change is considered undefined behavior and can cause crashes!
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneCollectionName">Name of the current scene collection</param>
    public delegate void CurrentSceneCollectionChangingCallback(OBSWebsocket sender, string sceneCollectionName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.CurrentProfileChanging"/>
    /// The current profile has begun changing.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="profileName">Name of the current profile</param>
    public delegate void CurrentProfileChangingCallback(OBSWebsocket sender, string profileName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterNameChanged"/>
    /// The name of a source filter has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">The source the filter is on</param>
    /// <param name="oldFilterName">Old name of the filter</param>
    /// <param name="filterName">New name of the filter</param>
    public delegate void SourceFilterNameChangedCallback(OBSWebsocket sender, string sourceName, string oldFilterName, string filterName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputCreated"/>
    /// An input has been created.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">ame of the input</param>
    /// <param name="inputKind">The kind of the input</param>
    /// <param name="unversionedInputKind">The unversioned kind of input (aka no `_v2` stuff)</param>
    /// <param name="inputSettings">The settings configured to the input when it was created</param>
    /// <param name="defaultInputSettings">The default settings for the input</param>
    public delegate void InputCreatedCallback(OBSWebsocket sender, string inputName, string inputKind, string unversionedInputKind, JObject inputSettings, JObject defaultInputSettings);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputRemoved"/>
    /// An input has been removed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    public delegate void InputRemovedCallback(OBSWebsocket sender, string inputName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputNameChanged"/>
    /// The name of an input has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="oldInputName">Old name of the input</param>
    /// <param name="inputName">New name of the input</param>
    public delegate void InputNameChangedCallback(OBSWebsocket sender, string oldInputName, string inputName);

    /// <summary>
    /// An input's active state has changed.
    /// When an input is active, it means it's being shown by the program feed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="videoActive">Whether the input is active</param>
    public delegate void InputActiveStateChangedCallback(OBSWebsocket sender, string inputName, bool videoActive);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputShowStateChanged"/>
    /// An input's show state has changed.
    /// When an input is showing, it means it's being shown by the preview or a dialog.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="videoShowing">Whether the input is showing</param>
    public delegate void InputShowStateChangedCallback(OBSWebsocket sender, string inputName, bool videoShowing);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputAudioBalanceChanged"/>
    /// The audio balance value of an input has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the affected input</param>
    /// <param name="inputAudioBalance">New audio balance value of the input</param>
    public delegate void InputAudioBalanceChangedCallback(OBSWebsocket sender, string inputName, double inputAudioBalance);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputAudioTracksChanged"/>
    /// The audio tracks of an input have changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="inputAudioTracks">Object of audio tracks along with their associated enable states</param>
    public delegate void InputAudioTracksChangedCallback(OBSWebsocket sender, string inputName, JObject inputAudioTracks);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputAudioMonitorTypeChanged"/>
    /// The monitor type of an input has changed.
    /// Available types are:
    /// - `OBS_MONITORING_TYPE_NONE`
    /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
    /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputName">Name of the input</param>
    /// <param name="monitorType">New monitor type of the input</param>
    public delegate void InputAudioMonitorTypeChangedCallback(OBSWebsocket sender, string inputName, string monitorType);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.InputVolumeMeters"/>
    /// A high-volume event providing volume levels of all active inputs every 50 milliseconds.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="inputs">Array of active inputs with their associated volume levels</param>
    public delegate void InputVolumeMetersCallback(OBSWebsocket sender, List<JObject> inputs);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.ReplayBufferSaved"/>
    /// The replay buffer has been saved.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="savedReplayPath">Path of the saved replay file</param>
    public delegate void ReplayBufferSavedCallback(OBSWebsocket sender, string savedReplayPath);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneCreated"/>
    /// A new scene has been created.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the new scene</param>
    /// <param name="isGroup">Whether the new scene is a group</param>
    public delegate void SceneCreatedCallback(OBSWebsocket sender, string sceneName, bool isGroup);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneRemoved"/>
    /// A scene has been removed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the removed scene</param>
    /// <param name="isGroup">Whether the scene was a group</param>
    public delegate void SceneRemovedCallback(OBSWebsocket sender, string sceneName, bool isGroup);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneNameChanged"/>
    /// The name of a scene has changed.
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="oldSceneName">Old name of the scene</param>
    /// <param name="sceneName">New name of the scene</param>
    public delegate void SceneNameChangedCallback(OBSWebsocket sender, string oldSceneName, string sceneName);

    #endregion

    public partial class OBSWebsocket
    {
        #region Events

        /// <summary>
        /// The current program scene has changed.
        /// </summary>
        public event CurrentProgramSceneChangeCallback CurrentProgramSceneChanged;

        /// <summary>
        /// The list of scenes has changed.
        /// TODO: Make OBS fire this event when scenes are reordered.
        /// </summary>
        public event SceneListChangedCallback SceneListChanged;

        /// <summary>
        /// Triggered when the scene item list of the specified scene is reordered
        /// </summary>
        public event SceneItemListReindexedCallback SceneItemListReindexed;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        public event SceneItemCreatedCallback SceneItemCreated;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        public event SceneItemRemovedCallback SceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        public event SceneItemEnableStateChangedCallback SceneItemEnableStateChanged;

        /// <summary>
        /// Triggered when the lock status of a scene item changes
        /// </summary>
        public event SceneItemLockStateChangedCallback SceneItemLockStateChanged;

        /// <summary>
        /// Triggered when switching to another scene collection
        /// </summary>
        public event CurrentSceneCollectionChangedCallback CurrentSceneCollectionChanged;

        /// <summary>
        /// Triggered when a scene collection is created, deleted or renamed
        /// </summary>
        public event SceneCollectionListChangedCallback SceneCollectionListChanged;

        /// <summary>
        /// Triggered when switching to another transition
        /// </summary>
        public event CurrentSceneTransitionChangedCallback CurrentSceneTransitionChanged;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        public event CurrentSceneTransitionDurationChangedCallback CurrentSceneTransitionDurationChanged;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="CurrentProgramSceneChanged"/>
        /// </summary>
        public event SceneTransitionStartedCallback SceneTransitionStarted;

        /// <summary>
        /// Triggered when a transition (other than "cut") has ended. Please note that the from-scene field is not available in TransitionEnd
        /// </summary>
        public event SceneTransitionEndedCallback SceneTransitionEnded;

        /// <summary>
        /// Triggered when a stinger transition has finished playing its video
        /// </summary>
        public event SceneTransitionVideoEndedCallback SceneTransitionVideoEnded;

        /// <summary>
        /// Triggered when switching to another profile
        /// </summary>
        public event CurrentProfileChangedCallback CurrentProfileChanged;

        /// <summary>
        /// Triggered when a profile is created, imported, removed or renamed
        /// </summary>
        public event ProfileListChangedCallback ProfileListChanged;

        /// <summary>
        /// Triggered when the streaming output state changes
        /// </summary>
        public event StreamStateChangedCallback StreamStateChanged;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        public event RecordStateChangedCallback RecordStateChanged;

        /// <summary>
        /// Triggered when state of the replay buffer changes
        /// </summary>
        public event ReplayBufferStateChangedCallback ReplayBufferStateChanged;

        /// <summary>
        /// Triggered when the preview scene selection changes (Studio Mode only)
        /// </summary>
        public event CurrentPreviewSceneChangedCallback CurrentPreviewSceneChanged;

        /// <summary>
        /// Triggered when Studio Mode is turned on or off
        /// </summary>
        public event StudioModeStateChangedCallback StudioModeStateChanged;

        /// <summary>
        /// Triggered when OBS exits
        /// </summary>
        public event EventHandler ExitStarted;

        /// <summary>
        /// Triggered when connected successfully to an obs-websocket server
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Triggered when disconnected from an obs-websocket server
        /// </summary>
        public event EventHandler<DisconnectionInfo> Disconnected;

        /// <summary>
        /// A scene item is selected in the UI
        /// </summary>
        public event SceneItemSelectedCallback SceneItemSelected;

        /// <summary>
        /// A scene item transform has changed
        /// </summary>
        public event SceneItemTransformCallback SceneItemTransformChanged;

        /// <summary>
        /// The audio sync offset of an input has changed
        /// </summary>
        public event InputAudioSyncOffsetChangedCallback InputAudioSyncOffsetChanged;

        /// <summary>
        /// A filter was added to a source
        /// </summary>
        public event SourceFilterCreatedCallback SourceFilterCreated;

        /// <summary>
        /// A filter was removed from a source
        /// </summary>
        public event SourceFilterRemovedCallback SourceFilterRemoved;

        /// <summary>
        /// Filters in a source have been reordered
        /// </summary>
        public event SourceFilterListReindexedCallback SourceFilterListReindexed;

        /// <summary>
        /// Triggered when the visibility of a filter has changed
        /// </summary>
        public event SourceFilterEnableStateChangedCallback SourceFilterEnableStateChanged;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event InputMuteStateChangedCallback InputMuteStateChanged;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        public event InputVolumeChangedCallback InputVolumeChanged;

        /// <summary>
        /// A custom broadcast message was received
        /// </summary>
        public event VendorEventCallback VendorEvent;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file ends. The behavior depends on the type of media source being used.
        /// </summary>
        public event MediaInputPlaybackEndedCallback MediaInputPlaybackEnded;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file starts playing. The behavior depends on the type of media source being used.
        /// </summary>
        public event MediaInputPlaybackStartedCallback MediaInputPlaybackStarted;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event MediaInputActionTriggeredCallback MediaInputActionTriggered;

        /// <summary>
        /// The virtual cam state has changed.
        /// </summary>
        public event VirtualcamStateChangedCallback VirtualcamStateChanged;

        /// <summary>
        /// The current scene collection has begun changing.
        /// </summary>
        public event CurrentSceneCollectionChangingCallback CurrentSceneCollectionChanging;

        /// <summary>
        /// The current profile has begun changing.
        /// </summary>
        public event CurrentProfileChangingCallback CurrentProfileChanging;

        /// <summary>
        /// The name of a source filter has changed.
        /// </summary>
        public event SourceFilterNameChangedCallback SourceFilterNameChanged;

        /// <summary>
        /// An input has been created.
        /// </summary>
        public event InputCreatedCallback InputCreated;

        /// <summary>
        /// An input has been removed.
        /// </summary>
        public event InputRemovedCallback InputRemoved;

        /// <summary>
        /// The name of an input has changed.
        /// </summary>
        public event InputNameChangedCallback InputNameChanged;

        /// <summary>
        /// An input's active state has changed.
        /// When an input is active, it means it's being shown by the program feed.
        /// </summary>
        public event InputActiveStateChangedCallback InputActiveStateChanged;

        /// <summary>
        /// An input's show state has changed.
        /// When an input is showing, it means it's being shown by the preview or a dialog.
        /// </summary>
        public event InputShowStateChangedCallback InputShowStateChanged;

        /// <summary>
        /// The audio balance value of an input has changed.
        /// </summary>
        public event InputAudioBalanceChangedCallback InputAudioBalanceChanged;

        /// <summary>
        /// The audio tracks of an input have changed.
        /// </summary>
        public event InputAudioTracksChangedCallback InputAudioTracksChanged;

        /// <summary>
        /// The monitor type of an input has changed.
        /// Available types are:
        /// - `OBS_MONITORING_TYPE_NONE`
        /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
        /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
        /// </summary>
        public event InputAudioMonitorTypeChangedCallback InputAudioMonitorTypeChanged;

        /// <summary>
        /// A high-volume event providing volume levels of all active inputs every 50 milliseconds.
        /// </summary>
        public event InputVolumeMetersCallback InputVolumeMeters;

        /// <summary>
        /// The replay buffer has been saved.
        /// </summary>
        public event ReplayBufferSavedCallback ReplayBufferSaved;

        /// <summary>
        /// A new scene has been created.
        /// </summary>
        public event SceneCreatedCallback SceneCreated;

        /// <summary>
        /// A scene has been removed.
        /// </summary>
        public event SceneRemovedCallback SceneRemoved;

        /// <summary>
        /// The name of a scene has changed.
        /// </summary>
        public event SceneNameChangedCallback SceneNameChanged;

        #endregion

        #region EventProcessing

        /// <summary>
        /// Update message handler
        /// </summary>
        /// <param name="eventType">Value of "event-type" in the JSON body</param>
        /// <param name="body">full JSON message body</param>
        protected void ProcessEventType(string eventType, JObject body)
        {
            body = (JObject)body["eventData"];

            switch (eventType)
            {
                case nameof(CurrentProgramSceneChanged):
                    CurrentProgramSceneChanged?.Invoke(this, (string)body["sceneName"]);
                    break;

                case nameof(SceneListChanged):
                    SceneListChanged?.Invoke(this, JsonConvert.DeserializeObject<List<JObject>>((string)body["scenes"]));
                    break;

                case nameof(SceneItemListReindexed):
                    SceneItemListReindexed?.Invoke(this, (string)body["sceneName"], JsonConvert.DeserializeObject<List<JObject>>((string)body["sceneItems"]));
                    break;

                case nameof(SceneItemCreated):
                    SceneItemCreated?.Invoke(this, (string)body["sceneName"], (string)body["sourceName"], (int)body["sceneItemId"], (int)body["sceneItemIndex"]);
                    break;

                case nameof(SceneItemRemoved):
                    SceneItemRemoved?.Invoke(this, (string)body["sceneName"], (string)body["sourceName"], (int)body["sceneItemId"]);
                    break;

                case nameof(SceneItemEnableStateChanged):
                    SceneItemEnableStateChanged?.Invoke(this, (string)body["sceneName"], (int)body["sceneItemId"], (bool)body["sceneItemEnabled"]);
                    break;

                case nameof(SceneItemLockStateChanged):
                    SceneItemLockStateChanged?.Invoke(this, (string)body["sceneName"], (int)body["sceneItemId"], (bool)body["sceneItemLocked"]);
                    break;

                case nameof(CurrentSceneCollectionChanged):
                    CurrentSceneCollectionChanged?.Invoke(this, (string)body["sceneCollectionName"]);
                    break;

                case nameof(SceneCollectionListChanged):
                    SceneCollectionListChanged?.Invoke(this, JsonConvert.DeserializeObject<List<string>>((string)body["sceneCollections"]));
                    break;

                case nameof(CurrentSceneTransitionChanged):
                    CurrentSceneTransitionChanged?.Invoke(this, (string)body["transitionName"]);
                    break;

                case nameof(CurrentSceneTransitionDurationChanged):
                    CurrentSceneTransitionDurationChanged?.Invoke(this, (int)body["transitionDuration"]);
                    break;

                case nameof(SceneTransitionStarted):
                    SceneTransitionStarted?.Invoke(this, (string)body["transitionName"]);
                    break;

                case nameof(SceneTransitionEnded):
                    SceneTransitionEnded?.Invoke(this, (string)body["transitionName"]);
                    break;

                case nameof(SceneTransitionVideoEnded):
                    SceneTransitionVideoEnded?.Invoke(this, (string)body["transitionName"]);
                    break;

                case nameof(CurrentProfileChanged):
                    CurrentProfileChanged?.Invoke(this, (string)body["profileName"]);
                    break;

                case nameof(ProfileListChanged):
                    ProfileListChanged?.Invoke(this, JsonConvert.DeserializeObject<List<string>>((string)body["profiles"]));
                    break;

                case nameof(StreamStateChanged):
                    StreamStateChanged?.Invoke(this, (bool)body["outputActive"], (string)body["outputState"]);
                    break;

                case nameof(RecordStateChanged):
                    RecordStateChanged?.Invoke(this, (bool)body["outputActive"], (string)body["outputState"]);
                    break;

                case nameof(CurrentPreviewSceneChanged):
                    CurrentPreviewSceneChanged?.Invoke(this, (string)body["sceneName"]);
                    break;

                case nameof(StudioModeStateChanged):
                    StudioModeStateChanged?.Invoke(this, (bool)body["studioModeEnabled"]);
                    break;

                case nameof(ReplayBufferStateChanged):
                    ReplayBufferStateChanged?.Invoke(this, (bool)body["outputActive"], (string)body["outputState"]);
                    break;

                case nameof(ExitStarted):
                    ExitStarted?.Invoke(this, EventArgs.Empty);
                    break;

                case nameof(SceneItemSelected):
                    SceneItemSelected?.Invoke(this, (string)body["sceneName"], (string)body["sceneItemId"]);
                    break;

                case nameof(SceneItemTransformChanged):
                    SceneItemTransformChanged?.Invoke(this, (string)body["sceneName"], (string)body["sceneItemId"], new SceneItemTransformInfo((JObject)body["sceneItemTransform"]));
                    break;

                case nameof(InputAudioSyncOffsetChanged):
                    InputAudioSyncOffsetChanged?.Invoke(this, (string)body["inputName"], (int)body["inputAudioSyncOffset"]);
                    break;

                case nameof(InputMuteStateChanged):
                    InputMuteStateChanged?.Invoke(this, (string)body["inputName"], (bool)body["inputMuted"]);
                    break;

                case nameof(InputVolumeChanged):
                    InputVolumeChanged?.Invoke(this, new InputVolume(body));
                    break;

                case nameof(SourceFilterCreated):
                    SourceFilterCreated?.Invoke(this, (string)body["sourceName"], (string)body["filterName"], (string)body["filterKind"], (int)body["filterIndex"], (JObject)body["filterSettings"], (JObject)body["defaultFilterSettings"]);
                    break;

                case nameof(SourceFilterRemoved):
                    SourceFilterRemoved?.Invoke(this, (string)body["sourceName"], (string)body["filterName"]);
                    break;

                case nameof(SourceFilterListReindexed):
                    if (SourceFilterListReindexed != null)
                    {
                        List<FilterReorderItem> filters = new List<FilterReorderItem>();
                        JsonConvert.PopulateObject(body["filters"].ToString(), filters);

                        SourceFilterListReindexed?.Invoke(this, (string)body["sourceName"], filters);
                    }
                    break;

                case nameof(SourceFilterEnableStateChanged):
                    SourceFilterEnableStateChanged?.Invoke(this, (string)body["sourceName"], (string)body["filterName"], (bool)body["filterEnabled"]);
                    break;

                case nameof(VendorEvent):
                    VendorEvent?.Invoke(this, (string)body["vendorName"], (string)body["eventType"], body);
                    break;

                case nameof(MediaInputPlaybackEnded):
                    MediaInputPlaybackEnded?.Invoke(this, (string)body["inputName"]);
                    break;

                case nameof(MediaInputPlaybackStarted):
                    MediaInputPlaybackStarted?.Invoke(this, (string)body["sourceName"]);
                    break;

                case nameof(MediaInputActionTriggered):
                    MediaInputActionTriggered?.Invoke(this, (string)body["inputName"], (string)body["mediaAction"]);
                    break;

                case nameof(VirtualcamStateChanged):
                    VirtualcamStateChanged?.Invoke(this, (bool)body["outputActive"], (string)body["outputState"]);
                    break;

                case nameof(CurrentSceneCollectionChanging):
                    CurrentSceneCollectionChanging?.Invoke(this, (string)body["sceneCollectionName"]);
                    break;

                case nameof(CurrentProfileChanging):
                    CurrentProfileChanging?.Invoke(this, (string)body["profileName"]);
                    break;

                case nameof(SourceFilterNameChanged):
                    SourceFilterNameChanged?.Invoke(this, (string)body["sourceName"], (string)body["oldFilterName"], (string)body["filterName"]);
                    break;

                case nameof(InputCreated):
                    InputCreated?.Invoke(this, (string)body["inputName"], (string)body["inputKind"], (string)body["unversionedInputKind"], (JObject)body["inputSettings"], (JObject)body["defaultInputSettings"]);
                    break;

                case nameof(InputRemoved):
                    InputRemoved?.Invoke(this, (string)body["inputName"]);
                    break;

                case nameof(InputNameChanged):
                    InputNameChanged?.Invoke(this, (string)body["oldInputName"], (string)body["inputName"]);
                    break;

                case nameof(InputActiveStateChanged):
                    InputActiveStateChanged?.Invoke(this, (string)body["inputName"], (bool)body["videoActive"]);
                    break;

                case nameof(InputShowStateChanged):
                    InputShowStateChanged?.Invoke(this, (string)body["inputName"], (bool)body["videoShowing"]);
                    break;

                case nameof(InputAudioBalanceChanged):
                    InputAudioBalanceChanged?.Invoke(this, (string)body["inputName"], (double)body["inputAudioBalance"]);
                    break;

                case nameof(InputAudioTracksChanged):
                    InputAudioTracksChanged?.Invoke(this, (string)body["inputName"], (JObject)body["inputAudioTracks"]);
                    break;

                case nameof(InputAudioMonitorTypeChanged):
                    InputAudioMonitorTypeChanged?.Invoke(this, (string)body["inputName"], (string)body["monitorType"]);
                    break;

                case nameof(InputVolumeMeters):
                    InputVolumeMeters?.Invoke(this, JsonConvert.DeserializeObject<List<JObject>>((string)body["inputs"]));
                    break;

                case nameof(ReplayBufferSaved):
                    ReplayBufferSaved?.Invoke(this, (string)body["savedReplayPath"]);
                    break;

                case nameof(SceneCreated):
                    SceneCreated?.Invoke(this, (string)body["sceneName"], (bool)body["isGroup"]);
                    break;

                case nameof(SceneRemoved):
                    SceneRemoved?.Invoke(this, (string)body["sceneName"], (bool)body["isGroup"]);
                    break;

                case nameof(SceneNameChanged):
                    SceneNameChanged?.Invoke(this, (string)body["oldSceneName"], (string)body["sceneName"]);
                    break;

                default:
                    var message = $"Unsupported Event: {eventType}\n{body}";
                    Console.WriteLine(message);
                    Debug.WriteLine(message);
                    break;
            }
        }

        #endregion
    }
}