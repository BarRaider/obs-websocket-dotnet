using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using OBSWebsocketDotNet.Types.Events;

namespace OBSWebsocketDotNet
{
    public partial class OBSWebsocket
    {
        #region Events

        /// <summary>
        /// The current program scene has changed.
        /// </summary>
        public event EventHandler<ProgramSceneChangedEventArgs> CurrentProgramSceneChanged;

        /// <summary>
        /// The list of scenes has changed.
        /// TODO: Make OBS fire this event when scenes are reordered.
        /// </summary>
        public event EventHandler<SceneListChangedEventArgs> SceneListChanged;

        /// <summary>
        /// Triggered when the scene item list of the specified scene is reordered
        /// </summary>
        public event EventHandler<SceneItemListReindexedEventArgs> SceneItemListReindexed;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        public event EventHandler<SceneItemCreatedEventArgs> SceneItemCreated;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        public event EventHandler<SceneItemRemovedEventArgs> SceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        public event EventHandler<SceneItemEnableStateChangedEventArgs> SceneItemEnableStateChanged;

        /// <summary>
        /// Triggered when the lock status of a scene item changes
        /// </summary>
        public event EventHandler<SceneItemLockStateChangedEventArgs> SceneItemLockStateChanged;

        /// <summary>
        /// Triggered when switching to another scene collection
        /// </summary>
        public event EventHandler<CurrentSceneCollectionChangedEventArgs> CurrentSceneCollectionChanged;

        /// <summary>
        /// Triggered when a scene collection is created, deleted or renamed
        /// </summary>
        public event EventHandler<SceneCollectionListChangedEventArgs> SceneCollectionListChanged;

        /// <summary>
        /// Triggered when switching to another transition
        /// </summary>
        public event EventHandler<CurrentSceneTransitionChangedEventArgs> CurrentSceneTransitionChanged;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        public event EventHandler<CurrentSceneTransitionDurationChangedEventArgs> CurrentSceneTransitionDurationChanged;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="CurrentProgramSceneChanged"/>
        /// </summary>
        public event EventHandler<SceneTransitionStartedEventArgs> SceneTransitionStarted;

        /// <summary>
        /// Triggered when a transition (other than "cut") has ended. Please note that the from-scene field is not available in TransitionEnd
        /// </summary>
        public event EventHandler<SceneTransitionEndedEventArgs> SceneTransitionEnded;

        /// <summary>
        /// Triggered when a stinger transition has finished playing its video
        /// </summary>
        public event EventHandler<SceneTransitionVideoEndedEventArgs> SceneTransitionVideoEnded;

        /// <summary>
        /// Triggered when switching to another profile
        /// </summary>
        public event EventHandler<CurrentProfileChangedEventArgs> CurrentProfileChanged;

        /// <summary>
        /// Triggered when a profile is created, imported, removed or renamed
        /// </summary>
        public event EventHandler<ProfileListChangedEventArgs> ProfileListChanged;

        /// <summary>
        /// Triggered when the streaming output state changes
        /// </summary>
        public event EventHandler<StreamStateChangedEventArgs> StreamStateChanged;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        public event EventHandler<RecordStateChangedEventArgs> RecordStateChanged;

        /// <summary>
        /// Triggered when state of the replay buffer changes
        /// </summary>
        public event EventHandler<ReplayBufferStateChangedEventArgs> ReplayBufferStateChanged;

        /// <summary>
        /// Triggered when the preview scene selection changes (Studio Mode only)
        /// </summary>
        public event EventHandler<CurrentPreviewSceneChangedEventArgs> CurrentPreviewSceneChanged;

        /// <summary>
        /// Triggered when Studio Mode is turned on or off
        /// </summary>
        public event EventHandler<StudioModeStateChangedEventArgs> StudioModeStateChanged;

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
        public event EventHandler<ObsDisconnectionInfo> Disconnected;

        /// <summary>
        /// A scene item is selected in the UI
        /// </summary>
        public event EventHandler<SceneItemSelectedEventArgs> SceneItemSelected;

        /// <summary>
        /// A scene item transform has changed
        /// </summary>
        public event EventHandler<SceneItemTransformEventArgs> SceneItemTransformChanged;

        /// <summary>
        /// The audio sync offset of an input has changed
        /// </summary>
        public event EventHandler<InputAudioSyncOffsetChangedEventArgs> InputAudioSyncOffsetChanged;

        /// <summary>
        /// A filter was added to a source
        /// </summary>
        public event EventHandler<SourceFilterCreatedEventArgs> SourceFilterCreated;

        /// <summary>
        /// A filter was removed from a source
        /// </summary>
        public event EventHandler<SourceFilterRemovedEventArgs> SourceFilterRemoved;

        /// <summary>
        /// Filters in a source have been reordered
        /// </summary>
        public event EventHandler<SourceFilterListReindexedEventArgs> SourceFilterListReindexed;

        /// <summary>
        /// Triggered when the visibility of a filter has changed
        /// </summary>
        public event EventHandler<SourceFilterEnableStateChangedEventArgs> SourceFilterEnableStateChanged;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        public event EventHandler<InputMuteStateChangedEventArgs> InputMuteStateChanged;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        public event EventHandler<InputVolumeChangedEventArgs> InputVolumeChanged;

        /// <summary>
        /// A custom broadcast message was received
        /// </summary>
        public event EventHandler<VendorEventArgs> VendorEvent;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file ends. The behavior depends on the type of media source being used.
        /// </summary>
        public event EventHandler<MediaInputPlaybackEndedEventArgs> MediaInputPlaybackEnded;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file starts playing. The behavior depends on the type of media source being used.
        /// </summary>
        public event EventHandler<MediaInputPlaybackStartedEventArgs> MediaInputPlaybackStarted;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        public event EventHandler<MediaInputActionTriggeredEventArgs> MediaInputActionTriggered;

        /// <summary>
        /// The virtual cam state has changed.
        /// </summary>
        public event EventHandler<VirtualcamStateChangedEventArgs> VirtualcamStateChanged;

        /// <summary>
        /// The current scene collection has begun changing.
        /// </summary>
        public event EventHandler<CurrentSceneCollectionChangingEventArgs> CurrentSceneCollectionChanging;

        /// <summary>
        /// The current profile has begun changing.
        /// </summary>
        public event EventHandler<CurrentProfileChangingEventArgs> CurrentProfileChanging;

        /// <summary>
        /// The name of a source filter has changed.
        /// </summary>
        public event EventHandler<SourceFilterNameChangedEventArgs> SourceFilterNameChanged;

        /// <summary>
        /// An input has been created.
        /// </summary>
        public event EventHandler<InputCreatedEventArgs> InputCreated;

        /// <summary>
        /// An input has been removed.
        /// </summary>
        public event EventHandler<InputRemovedEventArgs> InputRemoved;

        /// <summary>
        /// The name of an input has changed.
        /// </summary>
        public event EventHandler<InputNameChangedEventArgs> InputNameChanged;

        /// <summary>
        /// An input's active state has changed.
        /// When an input is active, it means it's being shown by the program feed.
        /// </summary>
        public event EventHandler<InputActiveStateChangedEventArgs> InputActiveStateChanged;

        /// <summary>
        /// An input's show state has changed.
        /// When an input is showing, it means it's being shown by the preview or a dialog.
        /// </summary>
        public event EventHandler<InputShowStateChangedEventArgs> InputShowStateChanged;

        /// <summary>
        /// The audio balance value of an input has changed.
        /// </summary>
        public event EventHandler<InputAudioBalanceChangedEventArgs> InputAudioBalanceChanged;

        /// <summary>
        /// The audio tracks of an input have changed.
        /// </summary>
        public event EventHandler<InputAudioTracksChangedEventArgs> InputAudioTracksChanged;

        /// <summary>
        /// The monitor type of an input has changed.
        /// Available types are:
        /// - `OBS_MONITORING_TYPE_NONE`
        /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
        /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
        /// </summary>
        public event EventHandler<InputAudioMonitorTypeChangedEventArgs> InputAudioMonitorTypeChanged;

        /// <summary>
        /// A high-volume event providing volume levels of all active inputs every 50 milliseconds.
        /// </summary>
        public event EventHandler<InputVolumeMetersEventArgs> InputVolumeMeters;

        /// <summary>
        /// The replay buffer has been saved.
        /// </summary>
        public event EventHandler<ReplayBufferSavedEventArgs> ReplayBufferSaved;

        /// <summary>
        /// A new scene has been created.
        /// </summary>
        public event EventHandler<SceneCreatedEventArgs> SceneCreated;

        /// <summary>
        /// A scene has been removed.
        /// </summary>
        public event EventHandler<SceneRemovedEventArgs> SceneRemoved;

        /// <summary>
        /// The name of a scene has changed.
        /// </summary>
        public event EventHandler<SceneNameChangedEventArgs> SceneNameChanged;

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
                    CurrentProgramSceneChanged?.Invoke(this, new ProgramSceneChangedEventArgs((string)body["sceneName"]));
                    break;

                case nameof(SceneListChanged):
                    SceneListChanged?.Invoke(this, new SceneListChangedEventArgs(JsonConvert.DeserializeObject<List<JObject>>((string)body["scenes"])));
                    break;

                case nameof(SceneItemListReindexed):
                    SceneItemListReindexed?.Invoke(this, new SceneItemListReindexedEventArgs((string)body["sceneName"], JsonConvert.DeserializeObject<List<JObject>>((string)body["sceneItems"])));
                    break;

                case nameof(SceneItemCreated):
                    SceneItemCreated?.Invoke(this, new SceneItemCreatedEventArgs((string)body["sceneName"], (string)body["sourceName"], (int)body["sceneItemId"], (int)body["sceneItemIndex"]));
                    break;

                case nameof(SceneItemRemoved):
                    SceneItemRemoved?.Invoke(this, new SceneItemRemovedEventArgs((string)body["sceneName"], (string)body["sourceName"], (int)body["sceneItemId"]));
                    break;

                case nameof(SceneItemEnableStateChanged):
                    SceneItemEnableStateChanged?.Invoke(this, new SceneItemEnableStateChangedEventArgs((string)body["sceneName"], (int)body["sceneItemId"], (bool)body["sceneItemEnabled"]));
                    break;

                case nameof(SceneItemLockStateChanged):
                    SceneItemLockStateChanged?.Invoke(this, new SceneItemLockStateChangedEventArgs((string)body["sceneName"], (int)body["sceneItemId"], (bool)body["sceneItemLocked"]));
                    break;

                case nameof(CurrentSceneCollectionChanged):
                    CurrentSceneCollectionChanged?.Invoke(this, new CurrentSceneCollectionChangedEventArgs((string)body["sceneCollectionName"]));
                    break;

                case nameof(SceneCollectionListChanged):
                    SceneCollectionListChanged?.Invoke(this, new SceneCollectionListChangedEventArgs(JsonConvert.DeserializeObject<List<string>>((string)body["sceneCollections"])));
                    break;

                case nameof(CurrentSceneTransitionChanged):
                    CurrentSceneTransitionChanged?.Invoke(this, new CurrentSceneTransitionChangedEventArgs((string)body["transitionName"]));
                    break;

                case nameof(CurrentSceneTransitionDurationChanged):
                    CurrentSceneTransitionDurationChanged?.Invoke(this, new CurrentSceneTransitionDurationChangedEventArgs((int)body["transitionDuration"]));
                    break;

                case nameof(SceneTransitionStarted):
                    SceneTransitionStarted?.Invoke(this, new SceneTransitionStartedEventArgs((string)body["transitionName"]));
                    break;

                case nameof(SceneTransitionEnded):
                    SceneTransitionEnded?.Invoke(this, new SceneTransitionEndedEventArgs((string)body["transitionName"]));
                    break;

                case nameof(SceneTransitionVideoEnded):
                    SceneTransitionVideoEnded?.Invoke(this, new SceneTransitionVideoEndedEventArgs((string)body["transitionName"]));
                    break;

                case nameof(CurrentProfileChanged):
                    CurrentProfileChanged?.Invoke(this, new CurrentProfileChangedEventArgs((string)body["profileName"]));
                    break;

                case nameof(ProfileListChanged):
                    ProfileListChanged?.Invoke(this, new ProfileListChangedEventArgs(JsonConvert.DeserializeObject<List<string>>((string)body["profiles"])));
                    break;

                case nameof(StreamStateChanged):
                    StreamStateChanged?.Invoke(this, new StreamStateChangedEventArgs(new OutputStateChanged(body)));
                    break;

                case nameof(RecordStateChanged):
                    RecordStateChanged?.Invoke(this, new RecordStateChangedEventArgs(new RecordStateChanged(body)));
                    break;

                case nameof(CurrentPreviewSceneChanged):
                    CurrentPreviewSceneChanged?.Invoke(this, new CurrentPreviewSceneChangedEventArgs((string)body["sceneName"]));
                    break;

                case nameof(StudioModeStateChanged):
                    StudioModeStateChanged?.Invoke(this, new StudioModeStateChangedEventArgs((bool)body["studioModeEnabled"]));
                    break;

                case nameof(ReplayBufferStateChanged):
                    ReplayBufferStateChanged?.Invoke(this, new ReplayBufferStateChangedEventArgs(new OutputStateChanged(body)));
                    break;

                case nameof(ExitStarted):
                    ExitStarted?.Invoke(this, EventArgs.Empty);
                    break;

                case nameof(SceneItemSelected):
                    SceneItemSelected?.Invoke(this, new SceneItemSelectedEventArgs((string)body["sceneName"], (string)body["sceneItemId"]));
                    break;

                case nameof(SceneItemTransformChanged):
                    SceneItemTransformChanged?.Invoke(this, new SceneItemTransformEventArgs((string)body["sceneName"], (string)body["sceneItemId"], new SceneItemTransformInfo((JObject)body["sceneItemTransform"])));
                    break;

                case nameof(InputAudioSyncOffsetChanged):
                    InputAudioSyncOffsetChanged?.Invoke(this, new InputAudioSyncOffsetChangedEventArgs((string)body["inputName"], (int)body["inputAudioSyncOffset"]));
                    break;

                case nameof(InputMuteStateChanged):
                    InputMuteStateChanged?.Invoke(this, new InputMuteStateChangedEventArgs((string)body["inputName"], (bool)body["inputMuted"]));
                    break;

                case nameof(InputVolumeChanged):
                    InputVolumeChanged?.Invoke(this, new InputVolumeChangedEventArgs(new InputVolume(body)));
                    break;

                case nameof(SourceFilterCreated):
                    SourceFilterCreated?.Invoke(this, new SourceFilterCreatedEventArgs((string)body["sourceName"], (string)body["filterName"], (string)body["filterKind"], (int)body["filterIndex"], (JObject)body["filterSettings"], (JObject)body["defaultFilterSettings"]));
                    break;

                case nameof(SourceFilterRemoved):
                    SourceFilterRemoved?.Invoke(this, new SourceFilterRemovedEventArgs((string)body["sourceName"], (string)body["filterName"]));
                    break;

                case nameof(SourceFilterListReindexed):
                    if (SourceFilterListReindexed != null)
                    {
                        List<FilterReorderItem> filters = new List<FilterReorderItem>();
                        JsonConvert.PopulateObject(body["filters"].ToString(), filters);

                        SourceFilterListReindexed?.Invoke(this, new SourceFilterListReindexedEventArgs((string)body["sourceName"], filters));
                    }
                    break;

                case nameof(SourceFilterEnableStateChanged):
                    SourceFilterEnableStateChanged?.Invoke(this, new SourceFilterEnableStateChangedEventArgs((string)body["sourceName"], (string)body["filterName"], (bool)body["filterEnabled"]));
                    break;

                case nameof(VendorEvent):
                    VendorEvent?.Invoke(this, new VendorEventArgs((string)body["vendorName"], (string)body["eventType"], body));
                    break;

                case nameof(MediaInputPlaybackEnded):
                    MediaInputPlaybackEnded?.Invoke(this, new MediaInputPlaybackEndedEventArgs((string)body["inputName"]));
                    break;

                case nameof(MediaInputPlaybackStarted):
                    MediaInputPlaybackStarted?.Invoke(this, new MediaInputPlaybackStartedEventArgs((string)body["sourceName"]));
                    break;

                case nameof(MediaInputActionTriggered):
                    MediaInputActionTriggered?.Invoke(this, new MediaInputActionTriggeredEventArgs((string)body["inputName"], (string)body["mediaAction"]));
                    break;

                case nameof(VirtualcamStateChanged):
                    VirtualcamStateChanged?.Invoke(this, new VirtualcamStateChangedEventArgs(new OutputStateChanged(body)));
                    break;

                case nameof(CurrentSceneCollectionChanging):
                    CurrentSceneCollectionChanging?.Invoke(this, new CurrentSceneCollectionChangingEventArgs((string)body["sceneCollectionName"]));
                    break;

                case nameof(CurrentProfileChanging):
                    CurrentProfileChanging?.Invoke(this, new CurrentProfileChangingEventArgs((string)body["profileName"]));
                    break;

                case nameof(SourceFilterNameChanged):
                    SourceFilterNameChanged?.Invoke(this, new SourceFilterNameChangedEventArgs((string)body["sourceName"], (string)body["oldFilterName"], (string)body["filterName"]));
                    break;

                case nameof(InputCreated):
                    InputCreated?.Invoke(this, new InputCreatedEventArgs((string)body["inputName"], (string)body["inputKind"], (string)body["unversionedInputKind"], (JObject)body["inputSettings"], (JObject)body["defaultInputSettings"]));
                    break;

                case nameof(InputRemoved):
                    InputRemoved?.Invoke(this, new InputRemovedEventArgs((string)body["inputName"]));
                    break;

                case nameof(InputNameChanged):
                    InputNameChanged?.Invoke(this, new InputNameChangedEventArgs((string)body["oldInputName"], (string)body["inputName"]));
                    break;

                case nameof(InputActiveStateChanged):
                    InputActiveStateChanged?.Invoke(this, new InputActiveStateChangedEventArgs((string)body["inputName"], (bool)body["videoActive"]));
                    break;

                case nameof(InputShowStateChanged):
                    InputShowStateChanged?.Invoke(this, new InputShowStateChangedEventArgs((string)body["inputName"], (bool)body["videoShowing"]));
                    break;

                case nameof(InputAudioBalanceChanged):
                    InputAudioBalanceChanged?.Invoke(this, new InputAudioBalanceChangedEventArgs((string)body["inputName"], (double)body["inputAudioBalance"]));
                    break;

                case nameof(InputAudioTracksChanged):
                    InputAudioTracksChanged?.Invoke(this, new InputAudioTracksChangedEventArgs((string)body["inputName"], (JObject)body["inputAudioTracks"]));
                    break;

                case nameof(InputAudioMonitorTypeChanged):
                    InputAudioMonitorTypeChanged?.Invoke(this, new InputAudioMonitorTypeChangedEventArgs((string)body["inputName"], (string)body["monitorType"]));
                    break;

                case nameof(InputVolumeMeters):
                    InputVolumeMeters?.Invoke(this, new InputVolumeMetersEventArgs(JsonConvert.DeserializeObject<List<JObject>>((string)body["inputs"])));
                    break;

                case nameof(ReplayBufferSaved):
                    ReplayBufferSaved?.Invoke(this, new ReplayBufferSavedEventArgs((string)body["savedReplayPath"]));
                    break;

                case nameof(SceneCreated):
                    SceneCreated?.Invoke(this, new SceneCreatedEventArgs((string)body["sceneName"], (bool)body["isGroup"]));
                    break;

                case nameof(SceneRemoved):
                    SceneRemoved?.Invoke(this, new SceneRemovedEventArgs((string)body["sceneName"], (bool)body["isGroup"]));
                    break;

                case nameof(SceneNameChanged):
                    SceneNameChanged?.Invoke(this, new SceneNameChangedEventArgs((string)body["oldSceneName"], (string)body["sceneName"]));
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