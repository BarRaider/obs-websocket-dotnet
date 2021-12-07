using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System.Collections.Generic;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newSceneName">Name of the new current scene</param>
    public delegate void SceneChangeCallback(OBSWebsocket sender, string newSceneName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SourceOrderChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where items where reordered</param>
    public delegate void SourceOrderChangeCallback(OBSWebsocket sender, string sceneName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemAdded"/>
    /// or <see cref="OBSWebsocket.SceneItemRemoved"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="itemName">Name of the concerned item</param>
    public delegate void SceneItemUpdateCallback(OBSWebsocket sender, string sceneName, string itemName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemVisibilityChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="itemName">Name of the concerned item</param>
    /// <param name="isVisible">Visibility of the item</param>
    public delegate void SceneItemVisibilityChangedCallback(OBSWebsocket sender, string sceneName, string itemName, bool isVisible);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneItemLockChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="itemName">Name of the concerned item</param>
    /// <param name="itemId">Id of the concerned item</param>
    /// <param name="isLocked">Lock status of the item</param>
    public delegate void SceneItemLockChangedCallback(OBSWebsocket sender, string sceneName, string itemName, int itemId, bool isLocked);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newTransitionName">Name of the new selected transition</param>
    public delegate void TransitionChangeCallback(OBSWebsocket sender, string newTransitionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionDurationChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newDuration">Name of the new transition duration (in milliseconds)</param>
    public delegate void TransitionDurationChangeCallback(OBSWebsocket sender, int newDuration);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionBegin"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Transition name</param>
    /// <param name="transitionType">Transition type</param>
    /// <param name="duration">Transition duration (in milliseconds). Will be -1 for any transition with a fixed duration, such as a Stinger, due to limitations of the OBS API</param>
    /// <param name="fromScene">Source scene of the transition</param>
    /// <param name="toScene">Destination scene of the transition</param>
    public delegate void TransitionBeginCallback(OBSWebsocket sender, string transitionName, string transitionType, int duration, string fromScene, string toScene);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionEnd"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Transition name</param>
    /// <param name="transitionType">Transition type</param>
    /// <param name="duration">Transition duration (in milliseconds).</param>
    /// <param name="toScene">Destination scene of the transition</param>
    public delegate void TransitionEndCallback(OBSWebsocket sender, string transitionName, string transitionType, int duration, string toScene);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.TransitionVideoEnd"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transitionName">Transition name</param>
    /// <param name="transitionType">Transition type</param>
    /// <param name="duration">Transition duration (in milliseconds).</param>
    /// <param name="fromScene">Source scene of the transition</param>
    /// <param name="toScene">Destination scene of the transition</param>
    public delegate void TransitionVideoEndCallback(OBSWebsocket sender, string transitionName, string transitionType, int duration, string fromScene, string toScene);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StreamingStateChanged"/>, <see cref="OBSWebsocket.RecordingStateChanged"/>
    /// or <see cref="OBSWebsocket.ReplayBufferStateChanged"/> 
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="type">New output state</param>
    public delegate void OutputStateCallback(OBSWebsocket sender, OutputState type);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StreamStatus"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="status">Stream status data</param>
    public delegate void StreamStatusCallback(OBSWebsocket sender, StreamStatus status);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.StudioModeSwitched"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="enabled">New Studio Mode status</param>
    public delegate void StudioModeChangeCallback(OBSWebsocket sender, bool enabled);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.Heartbeat"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="heatbeat">heartbeat data</param>
    public delegate void HeartBeatCallback(OBSWebsocket sender, Heartbeat heatbeat);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemDeselected"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene item was in</param>
    /// <param name="itemName">Name of the item deselected</param>
    /// <param name="itemId">Id of the item deselected</param>
    public delegate void SceneItemDeselectedCallback(OBSWebsocket sender, string sceneName, string itemName, string itemId);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemSelected"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene item was in</param>
    /// <param name="itemName">Name of the item seletected</param>
    /// <param name="itemId">Id of the item selected</param>
    public delegate void SceneItemSelectedCallback(OBSWebsocket sender, string sceneName, string itemName, string itemId);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SceneItemTransformChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="transform">Transform data</param>
    public delegate void SceneItemTransformCallback(OBSWebsocket sender, SceneItemTransformInfo transform);


    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceAudioMixersChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="mixerInfo">Mixer information that was updated</param>
    public delegate void SourceAudioMixersChangedCallback(OBSWebsocket sender, AudioMixersChangedInfo mixerInfo);



    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceAudioSyncOffsetChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of the source for the offset change</param>
    /// <param name="syncOffset">Sync offset value</param>
    public delegate void SourceAudioSyncOffsetCallback(OBSWebsocket sender, string sourceName, int syncOffset);


    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceCreated"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="settings">Newly created source settings</param>
    public delegate void SourceCreatedCallback(OBSWebsocket sender, SourceSettings settings);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceDestroyed"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Newly destroyed source information</param>
    /// <param name="sourceKind">Kind of source destroyed</param>
    /// <param name="sourceType">Type of source destroyed</param>
    public delegate void SourceDestroyedCallback(OBSWebsocket sender, string sourceName, string sourceType, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceRenamed"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newName">New name of source</param>
    /// <param name="previousName">Previous name of source</param>
    public delegate void SourceRenamedCallback(OBSWebsocket sender, string newName, string previousName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceMuteStateChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="muted">Current mute state of source</param>
    public delegate void SourceMuteStateChangedCallback(OBSWebsocket sender, string sourceName, bool muted);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceAudioDeactivated"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    public delegate void SourceAudioDeactivatedCallback(OBSWebsocket sender, string sourceName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceAudioActivated"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    public delegate void SourceAudioActivatedCallback(OBSWebsocket sender, string sourceName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceVolumeChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="volume">Current volume levels of source</param>
    public delegate void SourceVolumeChangedCallback(OBSWebsocket sender, SourceVolume volume);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterRemoved"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filterName">Name of removed filter</param>
    public delegate void SourceFilterRemovedCallback(OBSWebsocket sender, string sourceName, string filterName);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterAdded"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filterName">Name of filter</param>
    /// <param name="filterType">Type of filter</param>
    /// <param name="filterSettings">Settings for filter</param>
    public delegate void SourceFilterAddedCallback(OBSWebsocket sender, string sourceName, string filterName, string filterType, JObject filterSettings);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFiltersReordered"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filters">Current order of filters for source</param>
    public delegate void SourceFiltersReorderedCallback(OBSWebsocket sender, string sourceName, List<FilterReorderItem> filters);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.SourceFilterVisibilityChanged"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="filterName">Name of filter</param>
    /// <param name="filterEnabled">New filter state</param>
    public delegate void SourceFilterVisibilityChangedCallback(OBSWebsocket sender, string sourceName, string filterName, bool filterEnabled);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.BroadcastCustomMessageReceived"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="realm">Identifier provided by the sender</param>
    /// <param name="data">User-defined data</param>
    public delegate void BroadcastCustomMessageCallback(OBSWebsocket sender, string realm, JObject data);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaEnded"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaEndedCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaStarted"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaStartedCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaPrevious"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaPreviousCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaNext"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaNextCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaStopped"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaStoppedCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaRestarted"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaRestartedCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaPaused"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaPausedCallback(OBSWebsocket sender, string sourceName, string sourceKind);

    /// <summary>
    /// Callback by <see cref="OBSWebsocket.MediaPlaying"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sourceName">Name of source</param>
    /// <param name="sourceKind">Kind of source</param>
    public delegate void MediaPlayingCallback(OBSWebsocket sender, string sourceName, string sourceKind);
}