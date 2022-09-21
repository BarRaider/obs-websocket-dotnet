using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types;
using OBSWebsocketDotNet.Types.Events;
using Websocket.Client;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// OBS Websocket Dotnet interface
    /// </summary>
    public interface IOBSWebsocket
    {
        #region Properties

        /// <summary>
        /// WebSocket request timeout, represented as a TimeSpan object
        /// </summary>
        TimeSpan WSTimeout { get; set; }

        /// <summary>
        /// Current connection state
        /// </summary>
        bool IsConnected { get; }

        #endregion

        #region Requests
        /// <summary>
        /// Get basic OBS video information
        /// </summary>
        ObsVideoSettings GetVideoSettings();

        /// <summary>
        /// Saves a screenshot of a source to the filesystem.
        /// The `imageWidth` and `imageHeight` parameters are treated as \"scale to inner\", meaning the smallest ratio will be used and the aspect ratio of the original resolution is kept.
        /// If `imageWidth` and `imageHeight` are not specified, the compressed image will use the full resolution of the source.
        /// **Compatible with inputs and scenes.**
        /// </summary>
        /// <param name="sourceName">Name of the source to take a screenshot of</param>
        /// <param name="imageFormat">Image compression format to use. Use `GetVersion` to get compatible image formats</param>
        /// <param name="imageFilePath">Path to save the screenshot file to. Eg. `C:\\Users\\user\\Desktop\\screenshot.png`</param>
        /// <param name="imageWidth">Width to scale the screenshot to</param>
        /// <param name="imageHeight">Height to scale the screenshot to</param>
        /// <param name="imageCompressionQuality">Compression quality to use. 0 for high compression, 100 for uncompressed. -1 to use \"default\" (whatever that means, idk)</param>
        /// <returns>Base64-encoded screenshot string</returns>
        string SaveSourceScreenshot(string sourceName, string imageFormat, string imageFilePath, int imageWidth = -1, int imageHeight = -1, int imageCompressionQuality = -1);

        /// <summary>
        /// Saves a screenshot of a source to the filesystem.
        /// The `imageWidth` and `imageHeight` parameters are treated as \"scale to inner\", meaning the smallest ratio will be used and the aspect ratio of the original resolution is kept.
        /// If `imageWidth` and `imageHeight` are not specified, the compressed image will use the full resolution of the source.
        /// **Compatible with inputs and scenes.**
        /// </summary>
        /// <param name="sourceName">Name of the source to take a screenshot of</param>
        /// <param name="imageFormat">Image compression format to use. Use `GetVersion` to get compatible image formats</param>
        /// <param name="imageFilePath">Path to save the screenshot file to. Eg. `C:\\Users\\user\\Desktop\\screenshot.png`</param>
        /// <returns>Base64-encoded screenshot string</returns>
        string SaveSourceScreenshot(string sourceName, string imageFormat, string imageFilePath);

        /// <summary>
        /// Executes hotkey routine, identified by hotkey unique name
        /// </summary>
        /// <param name="hotkeyName">Unique name of the hotkey, as defined when registering the hotkey (e.g. "ReplayBuffer.Save")</param>
        void TriggerHotkeyByName(string hotkeyName);

        /// <summary>
        /// Triggers a hotkey using a sequence of keys.
        /// </summary>
        /// <param name="keyId">Main key identifier (e.g. OBS_KEY_A for key "A"). Available identifiers are here: https://github.com/obsproject/obs-studio/blob/master/libobs/obs-hotkeys.h</param>
        /// <param name="keyModifier">Optional key modifiers object. You can combine multiple key operators. e.g. KeyModifier.Shift | KeyModifier.Control</param>
        void TriggerHotkeyByKeySequence(OBSHotkey keyId, KeyModifier keyModifier = KeyModifier.None);

        /// <summary>
        /// Get the name of the currently active scene. 
        /// </summary>
        /// <returns>Name of the current scene</returns>
        string GetCurrentProgramScene();

        /// <summary>
        /// Set the current scene to the specified one
        /// </summary>
        /// <param name="sceneName">The desired scene name</param>
        void SetCurrentProgramScene(string sceneName);

        /// <summary>
        /// Get OBS stats (almost the same info as provided in OBS' stats window)
        /// </summary>
        ObsStats GetStats();

        /// <summary>
        /// List every available scene
        /// </summary>
        /// <returns>A <see cref="List{SceneBasicInfo}" /> of <see cref="SceneBasicInfo"/> objects describing each scene</returns>
        List<SceneBasicInfo> ListScenes();

        /// <summary>
        /// Get a list of scenes in the currently active profile
        /// </summary>
        GetSceneListInfo GetSceneList();

        /// <summary>
        /// Get the specified scene's transition override info
        /// </summary>
        /// <param name="sceneName">Name of the scene to return the override info</param>
        /// <returns>TransitionOverrideInfo</returns>
        TransitionOverrideInfo GetSceneSceneTransitionOverride(string sceneName);

        /// <summary>
        /// Set specific transition override for a scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to set the transition override</param>
        /// <param name="transitionName">Name of the transition to use</param>
        /// <param name="transitionDuration">Duration in milliseconds of the transition if transition is not fixed. Defaults to the current duration specified in the UI if there is no current override and this value is not given</param>
        void SetSceneSceneTransitionOverride(string sceneName, string transitionName, int transitionDuration = -1);

        /// <summary>
        /// If your code needs to perform multiple successive T-Bar moves (e.g. : in an animation, or in response to a user moving a T-Bar control in your User Interface), set release to false and call ReleaseTBar later once the animation/interaction is over.
        /// </summary>
        /// <param name="position">	T-Bar position. This value must be between 0.0 and 1.0.</param>
        /// <param name="release">Whether or not the T-Bar gets released automatically after setting its new position (like a user releasing their mouse button after moving the T-Bar). Call ReleaseTBar manually if you set release to false. Defaults to true.</param>
        void SetTBarPosition(double position, bool release = true);

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">JObject with filter settings</param>
        /// <param name="overlay">Apply over existing settings?</param>
        void SetSourceFilterSettings(string sourceName, string filterName, JObject filterSettings, bool overlay = false);

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">Filter settings</param>
        /// <param name="overlay">Apply over existing settings?</param>
        void SetSourceFilterSettings(string sourceName, string filterName, FilterSettings filterSettings, bool overlay = false);

        /// <summary>
        /// Modify the Source Filter's visibility
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Source filter name</param>
        /// <param name="filterEnabled">New filter state</param>
        void SetSourceFilterEnabled(string sourceName, string filterName, bool filterEnabled);

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        List<FilterSettings> GetSourceFilterList(string sourceName);

        /// <summary>
        /// Return a list of settings for a specific filter
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Filter name</param>
        FilterSettings GetSourceFilter(string sourceName, string filterName);

        /// <summary>
        /// Remove the filter from a source
        /// </summary>
        /// <param name="sourceName">Name of the source the filter is on</param>
        /// <param name="filterName">Name of the filter to remove</param>
        bool RemoveSourceFilter(string sourceName, string filterName);

        /// <summary>
        /// Add a filter to a source
        /// </summary>
        /// <param name="sourceName">Name of the source for the filter</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterKind">Type of filter</param>
        /// <param name="filterSettings">JObject holding filter settings object</param>
        void CreateSourceFilter(string sourceName, string filterName, string filterKind, JObject filterSettings);

        /// <summary>
        /// Add a filter to a source
        /// </summary>
        /// <param name="sourceName">Name of the source for the filter</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterKind">Type of filter</param>
        /// <param name="filterSettings">Filter settings object</param>
        void CreateSourceFilter(string sourceName, string filterName, string filterKind, FilterSettings filterSettings);

        /// <summary>
        /// Toggles the status of the stream output.
        /// </summary>
        /// <returns>New state of the stream output</returns>
        bool ToggleStream();

        /// <summary>
        /// Toggles the status of the record output.
        /// </summary>
        void ToggleRecord();

        /// <summary>
        /// Gets the status of the stream output
        /// </summary>
        /// <returns>An <see cref="OutputStatus"/> object describing the current outputs states</returns>
        OutputStatus GetStreamStatus();

        /// <summary>
        /// Get the current transition name and duration
        /// </summary>
        /// <returns>An <see cref="TransitionSettings"/> object with the current transition name and duration</returns>
        TransitionSettings GetCurrentSceneTransition();

        /// <summary>
        /// Set the current transition to the specified one
        /// </summary>
        /// <param name="transitionName">Desired transition name</param>
        void SetCurrentSceneTransition(string transitionName);

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="transitionDuration">Desired transition duration (in milliseconds)</param>
        void SetCurrentSceneTransitionDuration(int transitionDuration);

        /// <summary>
        /// Change the current settings of a transition
        /// </summary>
        /// <param name="transitionSettings">Transition settings (they can be partial)</param>
        /// <param name="overlay">Whether to overlay over the current settins or replace them</param>
        /// <returns>Updated transition settings</returns>
        void SetCurrentSceneTransitionSettings(JObject transitionSettings, bool overlay);

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="inputName">Name of the source which volume will be changed</param>
        /// <param name="inputVolume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul (useDecibel is false), and under 0.0 for dB (useDecibel is true). Note: OBS will interpret dB values under -100.0 as Inf.</param>
        /// <param name="inputVolumeDb">Interperet `volume` data as decibels instead of amplitude/mul.</param>
        void SetInputVolume(string inputName, float inputVolume, bool inputVolumeDb = false);

        /// <summary>
        /// Get the volume of the specified source
        /// Volume is between `0.0` and `1.0` if using amplitude/mul (useDecibel is false), under `0.0` if using dB (useDecibel is true).
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <returns>An <see cref="VolumeInfo"/>Object containing the volume and mute state of the specified source.</returns>
        VolumeInfo GetInputVolume(string inputName);

        /// <summary>
        /// Gets the audio mute state of an input.
        /// </summary>
        /// <param name="inputName">Name of input to get the mute state of</param>
        /// <returns>Whether the input is muted</returns>
        bool GetInputMute(string inputName);

        /// <summary>
        /// Set the mute state of the specified source
        /// </summary>
        /// <param name="inputName">Name of the source which mute state will be changed</param>
        /// <param name="inputMuted">Desired mute state</param>
        void SetInputMute(string inputName, bool inputMuted);

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="inputName">Name of the source which mute state will be toggled</param>
        void ToggleInputMute(string inputName);

        /// <summary>
        /// Sets the transform and crop info of a scene item
        /// </summary>
        /// <param name="sceneName">Name of the scene that has the SceneItem</param>
        /// <param name="sceneItemId">Id of the Scene Item</param>
        /// <param name="sceneItemTransform">JObject holding transform settings</param>
        void SetSceneItemTransform(string sceneName, int sceneItemId, JObject sceneItemTransform);

        /// <summary>
        /// Sets the transform and crop info of a scene item
        /// </summary>
        /// <param name="sceneName">Name of the scene that has the SceneItem</param>
        /// <param name="sceneItemId">Id of the Scene Item</param>
        /// <param name="sceneItemTransform">Transform settings</param>
        void SetSceneItemTransform(string sceneName, int sceneItemId, SceneItemTransformInfo sceneItemTransform);

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="sceneCollectionName">Desired scene collection name</param>
        void SetCurrentSceneCollection(string sceneCollectionName);

        /// <summary>
        /// Get the name of the current scene collection
        /// </summary>
        /// <returns>Name of the current scene collection</returns>
        string GetCurrentSceneCollection();

        /// <summary>
        /// List all scene collections
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all scene collections</returns>
        List<string> GetSceneCollectionList();

        /// <summary>
        /// Set the current profile to the specified one
        /// </summary>
        /// <param name="profileName">Name of the desired profile</param>
        void SetCurrentProfile(string profileName);

        /// <summary>
        /// List all profiles
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all profiles</returns>
        GetProfileListInfo GetProfileList();

        /// <summary>
        /// Start streaming. Will trigger an error if streaming is already active
        /// </summary>
        void StartStream();

        /// <summary>
        /// Stop streaming. Will trigger an error if streaming is not active.
        /// </summary>
        void StopStream();

        /// <summary>
        /// Start recording. Will trigger an error if recording is already active.
        /// </summary>
        void StartRecord();

        /// <summary>
        /// Stop recording. Will trigger an error if recording is not active.
        /// <returns>File name for the saved recording</returns>
        /// </summary>
        string StopRecord();

        /// <summary>
        /// Pause the current recording. Returns an error if recording is not active or already paused.
        /// </summary>
        void PauseRecord();

        /// <summary>
        /// Resume/unpause the current recording (if paused). Returns an error if recording is not active or not paused.
        /// </summary>
        void ResumeRecord();

        /// <summary>
        /// Get the path of the current recording folder
        /// </summary>
        /// <returns>Current recording folder path</returns>
        string GetRecordDirectory();

        /// <summary>
        /// Get current recording status.
        /// </summary>
        /// <returns></returns>
        RecordingStatus GetRecordStatus();

        /// <summary>
        /// Get the status of the OBS replay buffer.
        /// </summary>
        /// <returns>Current recording status. true when active</returns>
        bool GetReplayBufferStatus();

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <returns>Current transition duration (in milliseconds)</returns>
        GetTransitionListInfo GetSceneTransitionList();

        /// <summary>
        /// Get status of Studio Mode
        /// </summary>
        /// <returns>Studio Mode status (on/off)</returns>
        bool GetStudioModeEnabled();

        /// <summary>
        /// Enables or disables studio mode
        /// </summary>
        /// <param name="studioModeEnabled"></param>
        void SetStudioModeEnabled(bool studioModeEnabled);

        /// <summary>
        /// Get the name of the currently selected preview scene. 
        /// Note: Triggers an error if Studio Mode is disabled
        /// </summary>
        /// <returns>Preview scene name</returns>
        string GetCurrentPreviewScene();

        /// <summary>
        /// Change the currently active preview/studio scene to the one specified.
        /// Triggers an error if Studio Mode is disabled
        /// </summary>
        /// <param name="sceneName">Preview scene name</param>
        void SetCurrentPreviewScene(string sceneName);

        /// <summary>
        /// Change the currently active preview/studio scene to the one specified.
        /// Triggers an error if Studio Mode is disabled.
        /// </summary>
        /// <param name="previewScene">Preview scene object</param>
        void SetCurrentPreviewScene(ObsScene previewScene);

        /// <summary>
        /// Triggers the current scene transition. Same functionality as the `Transition` button in Studio Mode
        /// </summary>
        void TriggerStudioModeTransition();

        /// <summary>
        /// Toggles the state of the replay buffer output.
        /// </summary>
        void ToggleReplayBuffer();

        /// <summary>
        /// Start recording into the Replay Buffer. Triggers an error
        /// if the Replay Buffer is already active, or if the "Save Replay Buffer"
        /// hotkey is not set in OBS' settings
        /// </summary>
        void StartReplayBuffer();

        /// <summary>
        /// Stop recording into the Replay Buffer. Triggers an error if the
        /// Replay Buffer is not active.
        /// </summary>
        void StopReplayBuffer();

        /// <summary>
        /// Save and flush the contents of the Replay Buffer to disk. Basically
        /// the same as triggering the "Save Replay Buffer" hotkey in OBS.
        /// Triggers an error if Replay Buffer is not active.
        /// </summary>
        void SaveReplayBuffer();

        /// <summary>
        /// Set the audio sync offset of the specified source
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <param name="inputAudioSyncOffset">Audio offset (in nanoseconds) for the specified source</param>
        void SetInputAudioSyncOffset(string inputName, int inputAudioSyncOffset);

        /// <summary>
        /// Get the audio sync offset of the specified source
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <returns>Audio offset (in nanoseconds) of the specified source</returns>
        int GetInputAudioSyncOffset(string inputName);

        /// <summary>
        /// Removes a scene item from a scene.
        /// Scenes only.
        /// </summary>
        /// <param name="sceneItemId">Scene item id</param>
        /// <param name="sceneName">Scene name from which to delete item</param>
        void RemoveSceneItem(string sceneName, int sceneItemId);

        /// <summary>
        /// Sends CEA-608 caption text over the stream output. As of OBS Studio 23.1, captions are not yet available on Linux.
        /// </summary>
        /// <param name="captionText">Captions text</param>
        void SendStreamCaption(string captionText);

        /// <summary>
        /// Duplicates a scene item
        /// </summary>
        /// <param name="sceneName">Name of the scene that has the SceneItem</param>
        /// <param name="sceneItemId">Id of the Scene Item</param>
        /// <param name="destinationSceneName">Name of scene to add the new duplicated Scene Item. If not specified will assume sceneName</param>
        void DuplicateSceneItem(string sceneName, int sceneItemId, string destinationSceneName = null);

        /// <summary>
        /// Gets the names of all special inputs.
        /// </summary>
        /// <returns>Dictionary of special inputs.</returns>
        Dictionary<string, string> GetSpecialInputs();

        /// <summary>
        /// Sets the current stream service settings (stream destination).
        /// Note: Simple RTMP settings can be set with type `rtmp_custom` and the settings fields `server` and `key`.
        /// </summary>
        /// <param name="service">Stream Service Type Name and Settings objects</param>
        void SetStreamServiceSettings(StreamingService service);

        /// <summary>
        /// Gets the current stream service settings (stream destination).
        /// </summary>
        /// <returns>Stream service type and settings objects</returns>
        StreamingService GetStreamServiceSettings();

        /// <summary>
        /// Gets the audio monitor type of an input.
        /// The available audio monitor types are:
        /// - `OBS_MONITORING_TYPE_NONE`
        /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
        /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <returns>The monitor type in use</returns>
        string GetInputAudioMonitorType(string inputName);

        /// <summary>
        /// Sets the audio monitor type of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to set the audio monitor type of</param>
        /// <param name="monitorType">Audio monitor type. See `GetInputAudioMonitorType for possible types.</param>
        void SetInputAudioMonitorType(string inputName, string monitorType);

        /// <summary>
        /// Broadcasts a `CustomEvent` to all WebSocket clients. Receivers are clients which are identified and subscribed.
        /// </summary>
        /// <param name="eventData">Data payload to emit to all receivers</param>
        void BroadcastCustomEvent(JObject eventData);

        /// <summary>
        /// Sets the cursor position of a media input.
        /// This request does not perform bounds checking of the cursor position.
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <param name="mediaCursor">New cursor position to set (milliseconds).</param>
        void SetMediaInputCursor(string inputName, int mediaCursor);

        /// <summary>
        /// Offsets the current cursor position of a media input by the specified value.
        /// This request does not perform bounds checking of the cursor position.
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <param name="mediaCursorOffset">Value to offset the current cursor position by (milliseconds +/-)</param>
        void OffsetMediaInputCursor(string inputName, int mediaCursorOffset);

        /// <summary>
        /// Creates a new input, adding it as a scene item to the specified scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to add the input to as a scene item</param>
        /// <param name="inputName">Name of the new input to created</param>
        /// <param name="inputKind">The kind of input to be created</param>
        /// <param name="inputSettings">Jobject holding the settings object to initialize the input with</param>
        /// <param name="sceneItemEnabled">Whether to set the created scene item to enabled or disabled</param>
        /// <returns>ID of the SceneItem in the scene.</returns>
        int CreateInput(string sceneName, string inputName, string inputKind, JObject inputSettings, bool? sceneItemEnabled);

        /// <summary>
        /// Gets the default settings for an input kind.
        /// </summary>
        /// <param name="inputKind">Input kind to get the default settings for</param>
        /// <returns>Object of default settings for the input kind</returns>
        JObject GetInputDefaultSettings(string inputKind);

        /// <summary>
        /// Gets a list of all scene items in a scene.
        /// Scenes only
        /// </summary>
        /// <param name="sceneName">Name of the scene to get the items of</param>
        /// <returns>Array of scene items in the scene</returns>
        List<SceneItemDetails> GetSceneItemList(string sceneName);

        /// <summary>
        /// Creates a new scene item using a source.
        /// Scenes only
        /// </summary>
        /// <param name="sceneName">Name of the scene to create the new item in</param>
        /// <param name="sourceName">Name of the source to add to the scene</param>
        /// <param name="sceneItemEnabled">Enable state to apply to the scene item on creation</param>
        /// <returns>Numeric ID of the scene item</returns>
        int CreateSceneItem(string sceneName, string sourceName, bool sceneItemEnabled = true);

        /// <summary>
        /// Creates a new scene in OBS.
        /// </summary>
        /// <param name="sceneName">Name for the new scene</param>
        void CreateScene(string sceneName);

        /// <summary>
        /// Gets the enable state of all audio tracks of an input.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <returns>Object of audio tracks and associated enable states</returns>
        SourceTracks GetInputAudioTracks(string inputName);

        /// <summary>
        /// Sets the enable state of audio tracks of an input.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="inputAudioTracks">JObject holding track settings to apply</param>
        void SetInputAudioTracks(string inputName, JObject inputAudioTracks);

        /// <summary>
        /// Sets the enable state of audio tracks of an input.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="inputAudioTracks">Track settings to apply</param>
        void SetInputAudioTracks(string inputName, SourceTracks inputAudioTracks);

        /// <summary>
        /// Gets the active and show state of a source.
        /// **Compatible with inputs and scenes.**
        /// </summary>
        /// <param name="sourceName">Name of the source to get the active state of</param>
        /// <returns>Whether the source is showing in Program</returns>
        SourceActiveInfo GetSourceActive(string sourceName);

        /// <summary>
        /// Gets the status of the virtualcam output.
        /// </summary>
        /// <returns>An <see cref="VirtualCamStatus"/> object describing the current virtual camera state</returns>
        VirtualCamStatus GetVirtualCamStatus();

        /// <summary>
        /// Starts the virtualcam output.
        /// </summary>
        void StartVirtualCam();

        /// <summary>
        /// Stops the virtualcam output.
        /// </summary>
        void StopVirtualCam();

        /// <summary>
        /// Toggles the state of the virtualcam output.
        /// </summary>
        /// <returns>Whether the output is active</returns>
        VirtualCamStatus ToggleVirtualCam();

        /// <summary>
        /// Gets the value of a \"slot\" from the selected persistent data realm.
        /// </summary>
        /// <param name="realm">The data realm to select. `OBS_WEBSOCKET_DATA_REALM_GLOBAL` or `OBS_WEBSOCKET_DATA_REALM_PROFILE`</param>
        /// <param name="slotName">The name of the slot to retrieve data from</param>
        /// <returns type="Any">Value associated with the slot. `null` if not set</returns>
        JObject GetPersistentData(string realm, string slotName);

        /// <summary>
        /// Sets the value of a \"slot\" from the selected persistent data realm.
        /// </summary>
        /// <param name="realm">The data realm to select. `OBS_WEBSOCKET_DATA_REALM_GLOBAL` or `OBS_WEBSOCKET_DATA_REALM_PROFILE`</param>
        /// <param name="slotName">The name of the slot to retrieve data from</param>
        /// <param name="slotValue">The value to apply to the slot</param>
        void SetPersistentData(string realm, string slotName, JObject slotValue);

        /// <summary>
        /// Creates a new scene collection, switching to it in the process.\n\nNote: This will block until the collection has finished changing.
        /// </summary>
        /// <param name="sceneCollectionName">Name for the new scene collection</param>
        void CreateSceneCollection(string sceneCollectionName);

        /// <summary>
        /// Creates a new profile, switching to it in the process
        /// </summary>
        /// <param name="profileName">Name for the new profile</param>
        void CreateProfile(string profileName);

        /// <summary>
        /// Removes a profile. If the current profile is chosen, it will change to a different profile first.
        /// </summary>
        /// <param name="profileName">Name of the profile to remove</param>
        void RemoveProfile(string profileName);

        /// <summary>
        /// Gets a parameter from the current profile's configuration.
        /// </summary>
        /// <param name="parameterCategory">Category of the parameter to get</param>
        /// <param name="parameterName">Name of the parameter to get</param>
        /// <returns></returns>
        JObject GetProfileParameter(string parameterCategory, string parameterName);

        /// <summary>
        /// Sets the value of a parameter in the current profile's configuration.
        /// </summary>
        /// <param name="parameterCategory">Category of the parameter to set</param>
        /// <param name="parameterName">Name of the parameter to set</param>
        /// <param name="parameterValue">Value of the parameter to set. Use `null` to delete</param>
        void SetProfileParameter(string parameterCategory, string parameterName, string parameterValue);

        /// <summary>
        /// Sets the current video settings.
        /// Note: Fields must be specified in pairs. For example, you cannot set only `baseWidth` without needing to specify `baseHeight`.
        /// </summary>
        /// <param name="obsVideoSettings">Object containing video settings</param>
        void SetVideoSettings(ObsVideoSettings obsVideoSettings);

        /// <summary>
        /// Gets the default settings for a filter kind.
        /// </summary>
        /// <param name="filterKind">Filter kind to get the default settings for</param>
        /// <returns>Object of default settings for the filter kind</returns>
        JObject GetSourceFilterDefaultSettings(string filterKind);

        /// <summary>
        /// Sets the name of a source filter (rename).
        /// </summary>
        /// <param name="sourceName">Name of the source the filter is on</param>
        /// <param name="filterName">Current name of the filter</param>
        /// <param name="newFilterName">New name for the filter</param>
        void SetSourceFilterName(string sourceName, string filterName, string newFilterName);

        /// <summary>
        /// Sets the index position of a filter on a source.
        /// </summary>
        /// <param name="sourceName">Name of the source the filter is on</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterIndex">New index position of the filter</param>
        void SetSourceFilterIndex(string sourceName, string filterName, int filterIndex);

        /// <summary>
        /// Gets data about the current plugin and RPC version.
        /// </summary>
        /// <returns>Version info in an <see cref="ObsVersion"/> object</returns>
        ObsVersion GetVersion();

        /// <summary>
        /// Call a request registered to a vendor.
        /// A vendor is a unique name registered by a third-party plugin or script, which allows for custom requests and events to be added to obs-websocket.
        /// If a plugin or script implements vendor requests or events, documentation is expected to be provided with them.
        /// </summary>
        /// <param name="vendorName">Name of the vendor to use</param>
        /// <param name="requestType">The request type to call</param>
        /// <param name="requestData">Object containing appropriate request data</param>
        /// <returns>Object containing appropriate response data. {} if request does not provide any response data</returns>
        JObject CallVendorRequest(string vendorName, string requestType, JObject requestData = null);

        /// <summary>
        /// Gets an array of all hotkey names in OBS
        /// </summary>
        /// <returns>Array of hotkey names</returns>
        List<string> GetHotkeyList();

        /// <summary>
        /// Sleeps for a time duration or number of frames. Only available in request batches with types `SERIAL_REALTIME` or `SERIAL_FRAME`.
        /// </summary>
        /// <param name="sleepMillis">Number of milliseconds to sleep for (if `SERIAL_REALTIME` mode)</param>
        /// <param name="sleepFrames">Number of frames to sleep for (if `SERIAL_FRAME` mode)</param>
        void Sleep(int sleepMillis, int sleepFrames);

        /// <summary>
        /// Gets an array of all inputs in OBS.
        /// </summary>
        /// <param name="inputKind">Restrict the array to only inputs of the specified kind</param>
        /// <returns>List of Inputs in OBS</returns>
        List<InputBasicInfo> GetInputList(string inputKind = null);

        /// <summary>
        /// Gets an array of all available input kinds in OBS.
        /// </summary>
        /// <param name="unversioned">True == Return all kinds as unversioned, False == Return with version suffixes (if available)</param>
        /// <returns>Array of input kinds</returns>
        List<string> GetInputKindList(bool unversioned = false);

        /// <summary>
        /// Removes an existing input.
        /// Note: Will immediately remove all associated scene items.
        /// </summary>
        /// <param name="inputName">Name of the input to remove</param>
        void RemoveInput(string inputName);

        /// <summary>
        /// Sets the name of an input (rename).
        /// </summary>
        /// <param name="inputName">Current input name</param>
        /// <param name="newInputName">New name for the input</param>
        void SetInputName(string inputName, string newInputName);

        /// <summary>
        /// Gets the settings of an input.
        /// Note: Does not include defaults. To create the entire settings object, overlay `inputSettings` over the `defaultInputSettings` provided by `GetInputDefaultSettings`.
        /// </summary>
        /// <param name="inputName">Name of the input to get the settings of</param>
        /// <returns>New populated InputSettings object</returns>
        InputSettings GetInputSettings(string inputName);

        /// <summary>
        /// Sets the settings of an input.
        /// </summary>
        /// <param name="inputSettings">Object of settings to apply</param>
        /// <param name="overlay">True == apply the settings on top of existing ones, False == reset the input to its defaults, then apply settings.</param>
        void SetInputSettings(InputSettings inputSettings, bool overlay = true);

        /// <summary>
        /// Sets the settings of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to set the settings of</param>
        /// <param name="inputSettings">Object of settings to apply</param>
        /// <param name="overlay">True == apply the settings on top of existing ones, False == reset the input to its defaults, then apply settings.</param>
        void SetInputSettings(string inputName, JObject inputSettings, bool overlay = true);

        /// <summary>
        /// Gets the audio balance of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to get the audio balance of</param>
        /// <returns>Audio balance value from 0.0-1.0</returns>
        double GetInputAudioBalance(string inputName);

        /// <summary>
        /// Sets the audio balance of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to set the audio balance of</param>
        /// <param name="inputAudioBalance">New audio balance value</param>
        void SetInputAudioBalance(string inputName, double inputAudioBalance);

        /// <summary>
        /// Gets the items of a list property from an input's properties.
        /// Note: Use this in cases where an input provides a dynamic, selectable list of items.
        /// For example, display capture, where it provides a list of available displays.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="propertyName">Name of the list property to get the items of</param>
        /// <returns>Array of items in the list property</returns>
        List<JObject> GetInputPropertiesListPropertyItems(string inputName, string propertyName);

        /// <summary>
        /// Presses a button in the properties of an input.
        /// Note: Use this in cases where there is a button in the properties of an input that cannot be accessed in any other way.
        /// For example, browser sources, where there is a refresh button.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="propertyName">Name of the button property to press</param>
        void PressInputPropertiesButton(string inputName, string propertyName);

        /// <summary>
        /// Gets the status of a media input.\n\nMedia States:
        /// - `OBS_MEDIA_STATE_NONE`
        /// - `OBS_MEDIA_STATE_PLAYING`
        /// - `OBS_MEDIA_STATE_OPENING`
        /// - `OBS_MEDIA_STATE_BUFFERING`
        /// - `OBS_MEDIA_STATE_PAUSED`
        /// - `OBS_MEDIA_STATE_STOPPED`
        /// - `OBS_MEDIA_STATE_ENDED`
        /// - `OBS_MEDIA_STATE_ERROR`
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <returns>Object containing string mediaState, int mediaDuration, int mediaCursor properties</returns>
        MediaInputStatus GetMediaInputStatus(string inputName);

        /// <summary>
        /// Triggers an action on a media input.
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <param name="mediaAction">Identifier of the `ObsMediaInputAction` enum</param>
        void TriggerMediaInputAction(string inputName, string mediaAction);

        /// <summary>
        /// Gets the filename of the last replay buffer save file.
        /// </summary>
        /// <returns>File path of last replay</returns>
        string GetLastReplayBufferReplay();

        /// <summary>
        /// Toggles pause on the record output.
        /// </summary>
        void ToggleRecordPause();

        /// <summary>
        /// Currently BROKEN in obs-websocket/obs-studio
        /// Basically GetSceneItemList, but for groups.
        /// Using groups at all in OBS is discouraged, as they are very broken under the hood.
        /// Groups only
        /// </summary>
        /// <param name="sceneName">Name of the group to get the items of</param>
        /// <returns>Array of scene items in the group</returns>
        List<JObject> GetGroupSceneItemList(string sceneName);

        /// <summary>
        /// Searches a scene for a source, and returns its id.\n\nScenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene or group to search in</param>
        /// <param name="sourceName">Name of the source to find</param>
        /// <param name="searchOffset">Number of matches to skip during search. >= 0 means first forward. -1 means last (top) item</param>
        /// <returns>Numeric ID of the scene item</returns>
        int GetSceneItemId(string sceneName, string sourceName, int searchOffset);

        /// <summary>
        /// Gets the transform and crop info of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Object containing scene item transform info</returns>
        SceneItemTransformInfo GetSceneItemTransform(string sceneName, int sceneItemId);

        /// <summary>
        /// Gets the JObject of transform settings for a scene item. Use this one you don't want it populated with default values.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Object containing scene item transform info</returns>
        JObject GetSceneItemTransformRaw(string sceneName, int sceneItemId);

        /// <summary>
        /// Gets the enable state of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Whether the scene item is enabled. `true` for enabled, `false` for disabled</returns>
        bool GetSceneItemEnabled(string sceneName, int sceneItemId);

        /// <summary>
        /// Gets the enable state of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <param name="sceneItemEnabled">New enable state of the scene item</param>
        void SetSceneItemEnabled(string sceneName, int sceneItemId, bool sceneItemEnabled);

        /// <summary>
        /// Gets the lock state of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Whether the scene item is locked. `true` for locked, `false` for unlocked</returns>
        bool GetSceneItemLocked(string sceneName, int sceneItemId);

        /// <summary>
        /// Sets the lock state of a scene item.
        /// Scenes and Group
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <param name="sceneItemLocked">New lock state of the scene item</param>
        void SetSceneItemLocked(string sceneName, int sceneItemId, bool sceneItemLocked);

        /// <summary>
        /// Gets the index position of a scene item in a scene.
        /// An index of 0 is at the bottom of the source list in the UI.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Index position of the scene item</returns>
        int GetSceneItemIndex(string sceneName, int sceneItemId);

        /// <summary>
        /// Sets the index position of a scene item in a scene.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <param name="sceneItemIndex">New index position of the scene item</param>
        void SetSceneItemIndex(string sceneName, int sceneItemId, int sceneItemIndex);

        /// <summary>
        /// Gets the blend mode of a scene item.
        /// Blend modes:
        /// - `OBS_BLEND_NORMAL`
        /// - `OBS_BLEND_ADDITIVE`
        /// - `OBS_BLEND_SUBTRACT`
        /// - `OBS_BLEND_SCREEN`
        /// - `OBS_BLEND_MULTIPLY`
        /// - `OBS_BLEND_LIGHTEN`
        /// - `OBS_BLEND_DARKEN`
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Current blend mode</returns>
        string GetSceneItemBlendMode(string sceneName, int sceneItemId);

        /// <summary>
        /// Sets the blend mode of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="sceneItemId"></param>
        /// <param name="sceneItemBlendMode"></param>
        void SetSceneItemBlendMode(string sceneName, int sceneItemId, string sceneItemBlendMode);

        /// <summary>
        /// Gets an array of all groups in OBS.
        /// Groups in OBS are actually scenes, but renamed and modified. In obs-websocket, we treat them as scenes where we can.
        /// </summary>
        /// <returns>Array of group names</returns>
        List<string> GetGroupList();

        /// <summary>
        /// Removes a scene from OBS.
        /// </summary>
        /// <param name="sceneName">Name of the scene to remove</param>
        void RemoveScene(string sceneName);

        /// <summary>
        /// Sets the name of a scene (rename).
        /// </summary>
        /// <param name="sceneName">Name of the scene to be renamed</param>
        /// <param name="newSceneName">New name for the scene</param>
        void SetSceneName(string sceneName, string newSceneName);

        /// <summary>
        /// Gets a Base64-encoded screenshot of a source.
        /// The `imageWidth` and `imageHeight` parameters are treated as \"scale to inner\", meaning the smallest ratio will be used and the aspect ratio of the original resolution is kept.
        /// If `imageWidth` and `imageHeight` are not specified, the compressed image will use the full resolution of the source.
        /// **Compatible with inputs and scenes.**
        /// </summary>
        /// <param name="sourceName">Name of the source to take a screenshot of</param>
        /// <param name="imageFormat">Image compression format to use. Use `GetVersion` to get compatible image formats</param>
        /// <param name="imageWidth">Width to scale the screenshot to</param>
        /// <param name="imageHeight">Height to scale the screenshot to</param>
        /// <param name="imageCompressionQuality">Compression quality to use. 0 for high compression, 100 for uncompressed. -1 to use \"default\" (whatever that means, idk)</param>
        /// <returns>Base64-encoded screenshot</returns>
        string GetSourceScreenshot(string sourceName, string imageFormat, int imageWidth = -1, int imageHeight = -1, int imageCompressionQuality = -1);

        /// <summary>
        /// Gets an array of all available transition kinds.
        /// Similar to `GetInputKindList`
        /// </summary>
        /// <returns>Array of transition kinds</returns>
        List<string> GetTransitionKindList();

        /// <summary>
        /// Gets the cursor position of the current scene transition.
        /// Note: `transitionCursor` will return 1.0 when the transition is inactive.
        /// </summary>
        /// <returns>Cursor position, between 0.0 and 1.0</returns>
        double GetCurrentSceneTransitionCursor();

        /// <summary>
        /// Opens the properties dialog of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to open the dialog of</param>
        void OpenInputPropertiesDialog(string inputName);

        /// <summary>
        /// Opens the filters dialog of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to open the dialog of</param>
        void OpenInputFiltersDialog(string inputName);

        /// <summary>
        /// Opens the interact dialog of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to open the dialog of</param>
        void OpenInputInteractDialog(string inputName);

        /// <summary>
        /// Gets a list of connected monitors and information about them.
        /// </summary>
        /// <returns>a list of detected monitors with some information</returns>
        List<Monitor> GetMonitorList();

        /// <summary>
        /// Connect this instance to the specified URL, and authenticate (if needed) with the specified password.
        /// NOTE: Please subscribe to the Connected/Disconnected events (or atlease check the IsConnected property) to determine when the connection is actually fully established
        /// </summary>
        /// <param name="url">Server URL in standard URL format.</param>
        /// <param name="password">Server password</param>
        [Obsolete("Please use ConnectAsync, this function will be removed in the next version")]
        void Connect(string url, string password);

        /// <summary>
        /// Connect this instance to the specified URL, and authenticate (if needed) with the specified password.
        /// NOTE: Please subscribe to the Connected/Disconnected events (or atleast check the IsConnected property) to determine when the connection is actually fully established
        /// </summary>
        /// <param name="url">Server URL in standard URL format.</param>
        /// <param name="password">Server password</param>
        void ConnectAsync(string url, string password);

        /// <summary>
        /// Disconnect this instance from the server
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sends a message to the websocket API with the specified request type and optional parameters
        /// </summary>
        /// <param name="requestType">obs-websocket request type, must be one specified in the protocol specification</param>
        /// <param name="additionalFields">additional JSON fields if required by the request type</param>
        /// <returns>The server's JSON response as a JObject</returns>
        JObject SendRequest(string requestType, JObject additionalFields = null);

        /// <summary>
        /// Request authentication data. You don't have to call this manually.
        /// </summary>
        /// <returns>Authentication data in an <see cref="OBSAuthInfo"/> object</returns>
        OBSAuthInfo GetAuthInfo();

        #endregion

        #region Events

        /// <summary>
        /// The current program scene has changed.
        /// </summary>
        event EventHandler<ProgramSceneChangedEventArgs> CurrentProgramSceneChanged;

        /// <summary>
        /// The list of scenes has changed.
        /// TODO: Make OBS fire this event when scenes are reordered.
        /// </summary>
        event EventHandler<SceneListChangedEventArgs> SceneListChanged;

        /// <summary>
        /// Triggered when the scene item list of the specified scene is reordered
        /// </summary>
        event EventHandler<SceneItemListReindexedEventArgs> SceneItemListReindexed;

        /// <summary>
        /// Triggered when a new item is added to the item list of the specified scene
        /// </summary>
        event EventHandler<SceneItemCreatedEventArgs> SceneItemCreated;

        /// <summary>
        /// Triggered when an item is removed from the item list of the specified scene
        /// </summary>
        event EventHandler<SceneItemRemovedEventArgs> SceneItemRemoved;

        /// <summary>
        /// Triggered when the visibility of a scene item changes
        /// </summary>
        event EventHandler<SceneItemEnableStateChangedEventArgs> SceneItemEnableStateChanged;

        /// <summary>
        /// Triggered when the lock status of a scene item changes
        /// </summary>
        event EventHandler<SceneItemLockStateChangedEventArgs> SceneItemLockStateChanged;

        /// <summary>
        /// Triggered when switching to another scene collection
        /// </summary>
        event EventHandler<CurrentSceneCollectionChangedEventArgs> CurrentSceneCollectionChanged;

        /// <summary>
        /// Triggered when a scene collection is created, deleted or renamed
        /// </summary>
        event EventHandler<SceneCollectionListChangedEventArgs> SceneCollectionListChanged;

        /// <summary>
        /// Triggered when switching to another transition
        /// </summary>
        event EventHandler<CurrentSceneTransitionChangedEventArgs> CurrentSceneTransitionChanged;

        /// <summary>
        /// Triggered when the current transition duration is changed
        /// </summary>
        event EventHandler<CurrentSceneTransitionDurationChangedEventArgs> CurrentSceneTransitionDurationChanged;

        /// <summary>
        /// Triggered when a transition between two scenes starts. Followed by <see cref="OBSWebsocket.CurrentProgramSceneChanged"/>
        /// </summary>
        event EventHandler<SceneTransitionStartedEventArgs> SceneTransitionStarted;

        /// <summary>
        /// Triggered when a transition (other than "cut") has ended. Please note that the from-scene field is not available in TransitionEnd
        /// </summary>
        event EventHandler<SceneTransitionEndedEventArgs> SceneTransitionEnded;

        /// <summary>
        /// Triggered when a stinger transition has finished playing its video
        /// </summary>
        event EventHandler<SceneTransitionVideoEndedEventArgs> SceneTransitionVideoEnded;

        /// <summary>
        /// Triggered when switching to another profile
        /// </summary>
        event EventHandler<CurrentProfileChangedEventArgs> CurrentProfileChanged;

        /// <summary>
        /// Triggered when a profile is created, imported, removed or renamed
        /// </summary>
        event EventHandler<ProfileListChangedEventArgs> ProfileListChanged;

        /// <summary>
        /// Triggered when the streaming output state changes
        /// </summary>
        event EventHandler<StreamStateChangedEventArgs> StreamStateChanged;

        /// <summary>
        /// Triggered when the recording output state changes
        /// </summary>
        event EventHandler<RecordStateChangedEventArgs> RecordStateChanged;

        /// <summary>
        /// Triggered when state of the replay buffer changes
        /// </summary>
        event EventHandler<ReplayBufferStateChangedEventArgs> ReplayBufferStateChanged;

        /// <summary>
        /// Triggered when the preview scene selection changes (Studio Mode only)
        /// </summary>
        event EventHandler<CurrentPreviewSceneChangedEventArgs> CurrentPreviewSceneChanged;

        /// <summary>
        /// Triggered when Studio Mode is turned on or off
        /// </summary>
        event EventHandler<StudioModeStateChangedEventArgs> StudioModeStateChanged;

        /// <summary>
        /// Triggered when OBS exits
        /// </summary>
        event EventHandler ExitStarted;

        /// <summary>
        /// Triggered when connected successfully to an obs-websocket server
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// Triggered when disconnected from an obs-websocket server
        /// </summary>
        event EventHandler<ObsDisconnectionInfo> Disconnected;

        /// <summary>
        /// A scene item is selected in the UI
        /// </summary>
        event EventHandler<SceneItemSelectedEventArgs> SceneItemSelected;

        /// <summary>
        /// A scene item transform has changed
        /// </summary>
        event EventHandler<SceneItemTransformEventArgs> SceneItemTransformChanged;

        /// <summary>
        /// The audio sync offset of an input has changed
        /// </summary>
        event EventHandler<InputAudioSyncOffsetChangedEventArgs> InputAudioSyncOffsetChanged;

        /// <summary>
        /// A filter was added to a source
        /// </summary>
        event EventHandler<SourceFilterCreatedEventArgs> SourceFilterCreated;

        /// <summary>
        /// A filter was removed from a source
        /// </summary>
        event EventHandler<SourceFilterRemovedEventArgs> SourceFilterRemoved;

        /// <summary>
        /// Filters in a source have been reordered
        /// </summary>
        event EventHandler<SourceFilterListReindexedEventArgs> SourceFilterListReindexed;

        /// <summary>
        /// Triggered when the visibility of a filter has changed
        /// </summary>
        event EventHandler<SourceFilterEnableStateChangedEventArgs> SourceFilterEnableStateChanged;

        /// <summary>
        /// A source has been muted or unmuted
        /// </summary>
        event EventHandler<InputMuteStateChangedEventArgs> InputMuteStateChanged;

        /// <summary>
        /// The volume of a source has changed
        /// </summary>
        event EventHandler<InputVolumeChangedEventArgs> InputVolumeChanged;

        /// <summary>
        /// A custom broadcast message was received
        /// </summary>
        event EventHandler<VendorEventArgs> VendorEvent;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file ends. The behavior depends on the type of media source being used.
        /// </summary>
        event EventHandler<MediaInputPlaybackEndedEventArgs> MediaInputPlaybackEnded;

        /// <summary>
        /// These events are emitted by the OBS sources themselves. For example when the media file starts playing. The behavior depends on the type of media source being used.
        /// </summary>
        event EventHandler<MediaInputPlaybackStartedEventArgs> MediaInputPlaybackStarted;

        /// <summary>
        /// This event is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
        /// </summary>
        event EventHandler<MediaInputActionTriggeredEventArgs> MediaInputActionTriggered;

        /// <summary>
        /// The virtual cam state has changed.
        /// </summary>
        event EventHandler<VirtualcamStateChangedEventArgs> VirtualcamStateChanged;

        /// <summary>
        /// The current scene collection has begun changing.
        /// </summary>
        event EventHandler<CurrentSceneCollectionChangingEventArgs> CurrentSceneCollectionChanging;

        /// <summary>
        /// The current profile has begun changing.
        /// </summary>
        event EventHandler<CurrentProfileChangingEventArgs> CurrentProfileChanging;

        /// <summary>
        /// The name of a source filter has changed.
        /// </summary>
        event EventHandler<SourceFilterNameChangedEventArgs> SourceFilterNameChanged;

        /// <summary>
        /// An input has been created.
        /// </summary>
        event EventHandler<InputCreatedEventArgs> InputCreated;

        /// <summary>
        /// An input has been removed.
        /// </summary>
        event EventHandler<InputRemovedEventArgs> InputRemoved;

        /// <summary>
        /// The name of an input has changed.
        /// </summary>
        event EventHandler<InputNameChangedEventArgs> InputNameChanged;

        /// <summary>
        /// An input's active state has changed.
        /// When an input is active, it means it's being shown by the program feed.
        /// </summary>
        event EventHandler<InputActiveStateChangedEventArgs> InputActiveStateChanged;

        /// <summary>
        /// An input's show state has changed.
        /// When an input is showing, it means it's being shown by the preview or a dialog.
        /// </summary>
        event EventHandler<InputShowStateChangedEventArgs> InputShowStateChanged;

        /// <summary>
        /// The audio balance value of an input has changed.
        /// </summary>
        event EventHandler<InputAudioBalanceChangedEventArgs> InputAudioBalanceChanged;

        /// <summary>
        /// The audio tracks of an input have changed.
        /// </summary>
        event EventHandler<InputAudioTracksChangedEventArgs> InputAudioTracksChanged;

        /// <summary>
        /// The monitor type of an input has changed.
        /// Available types are:
        /// - `OBS_MONITORING_TYPE_NONE`
        /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
        /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
        /// </summary>
        event EventHandler<InputAudioMonitorTypeChangedEventArgs> InputAudioMonitorTypeChanged;

        /// <summary>
        /// A high-volume event providing volume levels of all active inputs every 50 milliseconds.
        /// </summary>
        event EventHandler<InputVolumeMetersEventArgs> InputVolumeMeters;

        /// <summary>
        /// The replay buffer has been saved.
        /// </summary>
        event EventHandler<ReplayBufferSavedEventArgs> ReplayBufferSaved;

        /// <summary>
        /// A new scene has been created.
        /// </summary>
        event EventHandler<SceneCreatedEventArgs> SceneCreated;

        /// <summary>
        /// A scene has been removed.
        /// </summary>
        event EventHandler<SceneRemovedEventArgs> SceneRemoved;

        /// <summary>
        /// The name of a scene has changed.
        /// </summary>
        event EventHandler<SceneNameChangedEventArgs> SceneNameChanged;

        #endregion
    }
}