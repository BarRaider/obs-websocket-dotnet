using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Instance of a connection with an obs-websocket server
    /// </summary>
    public partial class OBSWebsocket
    {
        #region Private Constants

        private const string REQUEST_FIELD_VOLUME_DB = "inputVolumeDb";
        private const string REQUEST_FIELD_VOLUME_MUL = "inputVolumeMul";

        private const string RESPONSE_FIELD_IMAGE_DATA = "imageData";


        #endregion

        /// <summary>
        /// Get basic OBS video information
        /// </summary>
        public ObsVideoSettings GetVideoSettings()
        {
            JObject response = SendRequest(nameof(GetVideoSettings));
            return JsonConvert.DeserializeObject<ObsVideoSettings>(response.ToString());
        }

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
        public string SaveSourceScreenshot(string sourceName, string imageFormat, string imageFilePath, int imageWidth = -1, int imageHeight = -1, int imageCompressionQuality = -1)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(imageFormat), imageFormat },
                { nameof(imageFilePath), imageFilePath }
            };

            if (imageWidth > -1)
            {
                request.Add(nameof(imageWidth), imageWidth);
            }
            if (imageHeight > -1)
            {
                request.Add(nameof(imageHeight), imageHeight);
            }
            if (imageCompressionQuality > -1)
            {
                request.Add(nameof(imageCompressionQuality), imageCompressionQuality);
            }

            var response = SendRequest(nameof(SaveSourceScreenshot), request);
            return (string)response["imageData"];
        }

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
        public string SaveSourceScreenshot(string sourceName, string imageFormat, string imageFilePath)
        {
            return SaveSourceScreenshot(sourceName, imageFormat, imageFilePath, -1, -1);
        }

        /// <summary>
        /// Executes hotkey routine, identified by hotkey unique name
        /// </summary>
        /// <param name="hotkeyName">Unique name of the hotkey, as defined when registering the hotkey (e.g. "ReplayBuffer.Save")</param>
        public void TriggerHotkeyByName(string hotkeyName)
        {
            var request = new JObject
            {
                { nameof(hotkeyName), hotkeyName }
            };

            SendRequest(nameof(TriggerHotkeyByName), request);
        }

        /// <summary>
        /// EExecutes hotkey routine, identified by bound combination of keys. A single key combination might trigger multiple hotkey routines depending on user settings
        /// </summary>
        /// <param name="keyId">Main key identifier (e.g. OBS_KEY_A for key "A"). Available identifiers are here: https://github.com/obsproject/obs-studio/blob/master/libobs/obs-hotkeys.h</param>
        /// <param name="keyModifier">Optional key modifiers object. You can combine multiple key operators. e.g. KeyModifier.Shift | KeyModifier.Control</param>
        public void TriggerHotkeyBySequence(OBSHotkey keyId, KeyModifier keyModifier = KeyModifier.None)
        {
            var request = new JObject
            {
                { nameof(keyId), keyId.ToString() },
                { "keyModifiers", new JObject{
                    { "shift", (keyModifier & KeyModifier.Shift) == KeyModifier.Shift },
                    { "alt", (keyModifier & KeyModifier.Alt) == KeyModifier.Alt },
                    { "control", (keyModifier & KeyModifier.Control) == KeyModifier.Control },
                    { "command", (keyModifier & KeyModifier.Command) == KeyModifier.Command } } 
                }
            };

            SendRequest(nameof(TriggerHotkeyBySequence), request);
        }

        /// <summary>
        /// Get the current scene info along with its items
        /// </summary>
        /// <returns>An <see cref="ObsScene"/> object describing the current scene</returns>
        public ObsScene GetCurrentProgramScene()
        {
            JObject response = SendRequest(nameof(GetCurrentProgramScene));
            return new ObsScene(response);
        }

        /// <summary>
        /// Set the current scene to the specified one
        /// </summary>
        /// <param name="sceneName">The desired scene name</param>
        public void SetCurrentProgramScene(string sceneName)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName }
            };

            SendRequest(nameof(SetCurrentProgramScene), request);
        }

        /// <summary>
        /// Get OBS stats (almost the same info as provided in OBS' stats window)
        /// </summary>
        public ObsStats GetStats()
        {
            JObject response = SendRequest(nameof(GetStats));
            return JsonConvert.DeserializeObject<ObsStats>(response.ToString());
        }

        /// <summary>
        /// List every available scene
        /// </summary>
        /// <returns>A <see cref="List{ObsScene}" /> of <see cref="ObsScene"/> objects describing each scene</returns>
        public List<SceneBasicInfo> ListScenes()
        {
            var response = GetSceneList();
            return response.Scenes;
        }

        /// <summary>
        /// Get a list of scenes in the currently active profile
        /// </summary>
        public GetSceneListInfo GetSceneList()
        {
            JObject response = SendRequest(nameof(GetSceneList));
            return JsonConvert.DeserializeObject<GetSceneListInfo>(response.ToString());
        }

        /// <summary>
        /// Get the specified scene's transition override info
        /// </summary>
        /// <param name="sceneName">Name of the scene to return the override info</param>
        /// <returns>TransitionOverrideInfo</returns>
        public TransitionOverrideInfo GetSceneSceneTransitionOverride(string sceneName)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName }
            };

            JObject response = SendRequest(nameof(GetSceneSceneTransitionOverride), request);
            return response.ToObject<TransitionOverrideInfo>();
        }

        /// <summary>
        /// Set specific transition override for a scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to set the transition override</param>
        /// <param name="transitionName">Name of the transition to use</param>
        /// <param name="transitionDuration">Duration in milliseconds of the transition if transition is not fixed. Defaults to the current duration specified in the UI if there is no current override and this value is not given</param>
        public void SetSceneSceneTransitionOverride(string sceneName, string transitionName, int transitionDuration = -1)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(transitionName), transitionName }
            };

            if (transitionDuration >= 0)
            {
                request.Add(nameof(transitionDuration), transitionDuration);
            }

            SendRequest(nameof(SetSceneSceneTransitionOverride), request);
        }

        /// <summary>
        /// If your code needs to perform multiple successive T-Bar moves (e.g. : in an animation, or in response to a user moving a T-Bar control in your User Interface), set release to false and call ReleaseTBar later once the animation/interaction is over.
        /// </summary>
        /// <param name="position">	T-Bar position. This value must be between 0.0 and 1.0.</param>
        /// <param name="release">Whether or not the T-Bar gets released automatically after setting its new position (like a user releasing their mouse button after moving the T-Bar). Call ReleaseTBar manually if you set release to false. Defaults to true.</param>
        public void SetTBarPosition(double position, bool release = true)
        {
            if (position < 0.0 || position > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            var request = new JObject
            {
                { nameof(position), position },
                { nameof(release), release}
            };

            SendRequest(nameof(SetTBarPosition), request);
        }

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">JObject with filter settings</param>
        /// <param name="overlay">Apply over existing settings?</param>
        public void SetSourceFilterSettings(string sourceName, string filterName, JObject filterSettings, bool overlay = false)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName },
                { nameof(filterSettings), filterSettings },
                { nameof(overlay), overlay }
            };

            SendRequest(nameof(SetSourceFilterSettings), request);
        }

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">Filter settings</param>
        /// <param name="overlay">Apply over existing settings?</param>
        public void SetSourceFilterSettings(string sourceName, string filterName, FilterSettings filterSettings, bool overlay = false)
        {
            SetSourceFilterSettings(sourceName, filterName, JObject.FromObject(filterSettings), overlay);
        }



        /// <summary>
        /// Modify the Source Filter's visibility
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Source filter name</param>
        /// <param name="filterEnabled">New filter state</param>
        public void SetSourceFilterEnabled(string sourceName, string filterName, bool filterEnabled)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName },
                { nameof(filterEnabled), filterEnabled }
            };

            SendRequest(nameof(SetSourceFilterEnabled), request);
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        public List<FilterSettings> GetSourceFilterList(string sourceName)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName }
            };

            JObject response = SendRequest(nameof(GetSourceFilterList), request);
            if (!response.HasValues)
            {
                return new List<FilterSettings>();
            }

            return JsonConvert.DeserializeObject<List<FilterSettings>>(response["filters"].ToString());
        }

        /// <summary>
        /// Return a list of settings for a specific filter
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Filter name</param>
        public FilterSettings GetSourceFilter(string sourceName, string filterName)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName }
            };

            JObject response = SendRequest(nameof(GetSourceFilter), request);
            return JsonConvert.DeserializeObject<FilterSettings>(response.ToString());
        }

        /// <summary>
        /// Remove the filter from a source
        /// </summary>
        /// <param name="sourceName">Name of the source the filter is on</param>
        /// <param name="filterName">Name of the filter to remove</param>
        public bool RemoveSourceFilter(string sourceName, string filterName)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName }
            };
            try
            {
                SendRequest(nameof(RemoveSourceFilter), request);
                return true;
            }
            catch (Exception e)
            {
                //TODO exception handling
                Console.WriteLine(e.Message);
            }
            return false;
        }

        /// <summary>
        /// Add a filter to a source
        /// </summary>
        /// <param name="sourceName">Name of the source for the filter</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterKind">Type of filter</param>
        /// <param name="filterSettings">JObject holding filter settings object</param>
        public void CreateSourceFilter(string sourceName, string filterName, string filterKind, JObject filterSettings)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName },
                { nameof(filterKind), filterKind },
                { nameof(filterSettings), filterSettings }
            };

            SendRequest(nameof(CreateSourceFilter), request);
        }

        /// <summary>
        /// Add a filter to a source
        /// </summary>
        /// <param name="sourceName">Name of the source for the filter</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterKind">Type of filter</param>
        /// <param name="filterSettings">Filter settings object</param>
        public void CreateSourceFilter(string sourceName, string filterName, string filterKind, FilterSettings filterSettings)
        {
            CreateSourceFilter(sourceName, filterName, filterKind, JObject.FromObject(filterSettings));
        }

        /// <summary>
        /// Toggles the status of the stream output.
        /// </summary>
        /// <returns>New state of the stream output</returns>
        public bool ToggleStream()
        {
            var response = SendRequest(nameof(ToggleStream));
            return (bool)response["outputActive"];
        }

        /// <summary>
        /// Toggles the status of the record output.
        /// </summary>
        public void ToggleRecord()
        {
            SendRequest(nameof(ToggleRecord));
        }

        /// <summary>
        /// Gets the status of the stream output
        /// </summary>
        /// <returns>An <see cref="OutputStatus"/> object describing the current outputs states</returns>
        public OutputStatus GetStreamStatus()
        {
            var response = SendRequest(nameof(GetStreamStatus));
            var outputStatus = new OutputStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// Get the current transition name and duration
        /// </summary>
        /// <returns>An <see cref="TransitionSettings"/> object with the current transition name and duration</returns>
        public TransitionSettings GetCurrentSceneTransition()
        {
            var response = SendRequest(nameof(GetCurrentSceneTransition));
            return new TransitionSettings(response);
        }

        /// <summary>
        /// Set the current transition to the specified one
        /// </summary>
        /// <param name="transitionName">Desired transition name</param>
        public void SetCurrentSceneTransition(string transitionName)
        {
            var request = new JObject
            {
                { nameof(transitionName), transitionName }
            };

            SendRequest(nameof(SetCurrentSceneTransition), request);
        }

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="transitionDuration">Desired transition duration (in milliseconds)</param>
        public void SetCurrentSceneTransitionDuration(int transitionDuration)
        {
            var request = new JObject
            {
                { nameof(transitionDuration), transitionDuration }
            };

            SendRequest(nameof(SetCurrentSceneTransitionDuration), request);
        }

        /// <summary>
        /// Change the current settings of a transition
        /// </summary>
        /// <param name="transitionSettings">Transition settings (they can be partial)</param>
        /// <param name="overlay">Whether to overlay over the current settins or replace them</param>
        /// <returns>Updated transition settings</returns>
        public void SetCurrentSceneTransitionSettings(JObject transitionSettings, bool overlay)
        {
            var requestFields = new JObject
            {
                { nameof(transitionSettings), JToken.FromObject(transitionSettings)},
                { nameof(overlay), overlay }
            };

            var response = SendRequest(nameof(SetCurrentSceneTransitionSettings), requestFields);
        }

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="inputName">Name of the source which volume will be changed</param>
        /// <param name="inputVolume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul (useDecibel is false), and under 0.0 for dB (useDecibel is true). Note: OBS will interpret dB values under -100.0 as Inf.</param>
        /// <param name="inputVolumeDb">Interperet `volume` data as decibels instead of amplitude/mul.</param>
        public void SetInputVolume(string inputName, float inputVolume, bool inputVolumeDb = false)
        {
            var requestFields = new JObject
            {
                { nameof(inputName), inputName }
            };

            if (inputVolumeDb)
            {
                requestFields.Add(REQUEST_FIELD_VOLUME_DB, inputVolume);
            }
            else
            {
                requestFields.Add(REQUEST_FIELD_VOLUME_MUL, inputVolume);
            }

            SendRequest(nameof(SetInputVolume), requestFields);
        }

        /// <summary>
        /// Get the volume of the specified source
        /// Volume is between `0.0` and `1.0` if using amplitude/mul (useDecibel is false), under `0.0` if using dB (useDecibel is true).
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <returns>An <see cref="VolumeInfo"/>Object containing the volume and mute state of the specified source.</returns>
        public VolumeInfo GetInputVolume(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            var response = SendRequest(nameof(GetInputVolume), request);
            return new VolumeInfo(response);
        }

        /// <summary>
        /// Gets the audio mute state of an input.
        /// </summary>
        /// <param name="inputName">Name of input to get the mute state of</param>
        /// <returns>Whether the input is muted</returns>
        public bool GetInputMute(string inputName)
        {
            var requestFields = new JObject
            {
                { nameof(inputName), inputName }
            };

            var response = SendRequest(nameof(GetInputMute), requestFields);
            return (bool)response["inputMuted"];
        }

        /// <summary>
        /// Set the mute state of the specified source
        /// </summary>
        /// <param name="inputName">Name of the source which mute state will be changed</param>
        /// <param name="inputMuted">Desired mute state</param>
        public void SetInputMute(string inputName, bool inputMuted)
        {
            var requestFields = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(inputMuted), inputMuted }
            };

            SendRequest(nameof(SetInputMute), requestFields);
        }

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="inputName">Name of the source which mute state will be toggled</param>
        public void ToggleInputMute(string inputName)
        {
            var requestFields = new JObject
            {
                { nameof(inputName), inputName }
            };

            SendRequest(nameof(ToggleInputMute), requestFields);
        }

        /// <summary>
        /// Sets the transform and crop info of a scene item
        /// </summary>
        /// <param name="sceneName">Name of the scene that has the SceneItem</param>
        /// <param name="sceneItemId">Id of the Scene Item</param>
        /// <param name="sceneItemTransform">JObject holding transform settings</param>
        public void SetSceneItemTransform(string sceneName, int sceneItemId, JObject sceneItemTransform)
        {
            var requestFields = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId },
                { nameof(sceneItemTransform), sceneItemTransform }
            };

            SendRequest(nameof(SetSceneItemTransform), requestFields);
        }

        /// <summary>
        /// Sets the transform and crop info of a scene item
        /// </summary>
        /// <param name="sceneName">Name of the scene that has the SceneItem</param>
        /// <param name="sceneItemId">Id of the Scene Item</param>
        /// <param name="sceneItemTransform">Transform settings</param>
        public void SetSceneItemTransform(string sceneName, int sceneItemId, SceneItemTransformInfo sceneItemTransform)
        {
            SetSceneItemTransform(sceneName, sceneItemId, JObject.FromObject(sceneItemTransform));
        }

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="sceneCollectionName">Desired scene collection name</param>
        public void SetCurrentSceneCollection(string sceneCollectionName)
        {
            var requestFields = new JObject
            {
                { nameof(sceneCollectionName), sceneCollectionName }
            };

            SendRequest(nameof(SetCurrentSceneCollection), requestFields);
        }

        /// <summary>
        /// Get the name of the current scene collection
        /// </summary>
        /// <returns>Name of the current scene collection</returns>
        public string GetCurrentSceneCollection()
        {
            var response = SendRequest(nameof(GetSceneCollectionList));
            var currentCollectionName = response["currentSceneCollectionName"];
            return (string)currentCollectionName;
        }

        /// <summary>
        /// List all scene collections
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all scene collections</returns>
        public List<string> GetSceneCollectionList()
        {
            var response = SendRequest(nameof(GetSceneCollectionList));
            return JsonConvert.DeserializeObject<List<string>>(response["sceneCollections"].ToString());
        }

        /// <summary>
        /// Set the current profile to the specified one
        /// </summary>
        /// <param name="profileName">Name of the desired profile</param>
        public void SetCurrentProfile(string profileName)
        {
            var requestFields = new JObject
            {
                { nameof(profileName), profileName }
            };

            SendRequest(nameof(SetCurrentProfile), requestFields);
        }

        /// <summary>
        /// List all profiles
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all profiles</returns>
        public GetProfileListInfo GetProfileList()
        {
            var response = SendRequest(nameof(GetProfileList));
            return JsonConvert.DeserializeObject<GetProfileListInfo>(response.ToString());
        }

        /// <summary>
        /// Start streaming. Will trigger an error if streaming is already active
        /// </summary>
        public void StartStream()
        {
            SendRequest(nameof(StartStream));
        }

        /// <summary>
        /// Stop streaming. Will trigger an error if streaming is not active.
        /// </summary>
        public void StopStream()
        {
            SendRequest(nameof(StopStream));
        }

        /// <summary>
        /// Start recording. Will trigger an error if recording is already active.
        /// </summary>
        public void StartRecord()
        {
            SendRequest(nameof(StartRecord));
        }

        /// <summary>
        /// Stop recording. Will trigger an error if recording is not active.
        /// </summary>
        public void StopRecord()
        {
            SendRequest(nameof(StopRecord));
        }

        /// <summary>
        /// Pause the current recording. Returns an error if recording is not active or already paused.
        /// </summary>
        public void PauseRecord()
        {
            SendRequest(nameof(PauseRecord));
        }

        /// <summary>
        /// Resume/unpause the current recording (if paused). Returns an error if recording is not active or not paused.
        /// </summary>
        public void ResumeRecord()
        {
            SendRequest(nameof(ResumeRecord));
        }

        /// <summary>
        /// Get the path of the current recording folder
        /// </summary>
        /// <returns>Current recording folder path</returns>
        public string GetRecordDirectory()
        {
            var response = SendRequest(nameof(GetRecordDirectory));
            return (string)response["recordDirectory"];
        }

        /// <summary>
        /// Get current recording status.
        /// </summary>
        /// <returns></returns>
        public RecordingStatus GetRecordStatus()
        {
            var response = SendRequest(nameof(GetRecordStatus));
            return JsonConvert.DeserializeObject<RecordingStatus>(response.ToString());
        }

        /// <summary>
        /// Get the status of the OBS replay buffer.
        /// </summary>
        /// <returns>Current recording status. true when active</returns>
        public bool GetReplayBufferStatus()
        {
            var response = SendRequest(nameof(GetReplayBufferStatus));
            return (bool)response["outputActive"];
        }

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <returns>Current transition duration (in milliseconds)</returns>
        public GetTransitionListInfo GetSceneTransitionList()
        {
            var response = SendRequest(nameof(GetSceneTransitionList));

            return JsonConvert.DeserializeObject<GetTransitionListInfo>(response.ToString());
        }

        /// <summary>
        /// Get status of Studio Mode
        /// </summary>
        /// <returns>Studio Mode status (on/off)</returns>
        public bool GetStudioModeEnabled()
        {
            var response = SendRequest(nameof(GetStudioModeEnabled));
            return (bool)response["studioModeEnabled"];
        }

        /// <summary>
        /// Enables or disables studio mode
        /// </summary>
        /// <param name="studioModeEnabled"></param>
        public void SetStudioModeEnabled(bool studioModeEnabled)
        {
            var requestFields = new JObject
            {
                { nameof(studioModeEnabled), studioModeEnabled }
            };

            SendRequest(nameof(SetStudioModeEnabled), requestFields);
        }

        /// <summary>
        /// Get the currently selected preview scene. Triggers an error
        /// if Studio Mode is disabled
        /// </summary>
        /// <returns>Preview scene object</returns>
        public ObsScene GetCurrentPreviewScene()
        {
            var response = SendRequest(nameof(GetCurrentPreviewScene));
            response.Add(GetSceneItemList((string)response["currentPreviewSceneName"]));
            return new ObsScene(response);
        }

        /// <summary>
        /// Change the currently active preview/studio scene to the one specified.
        /// Triggers an error if Studio Mode is disabled
        /// </summary>
        /// <param name="sceneName">Preview scene name</param>
        public void SetCurrentPreviewScene(string sceneName)
        {
            var requestFields = new JObject
            {
                { nameof(sceneName), sceneName }
            };

            SendRequest(nameof(SetCurrentPreviewScene), requestFields);
        }

        /// <summary>
        /// Change the currently active preview/studio scene to the one specified.
        /// Triggers an error if Studio Mode is disabled.
        /// </summary>
        /// <param name="previewScene">Preview scene object</param>
        public void SetCurrentPreviewScene(ObsScene previewScene)
        {
            SetCurrentPreviewScene(previewScene.Name);
        }

        /// <summary>
        /// Triggers the current scene transition. Same functionality as the `Transition` button in Studio Mode
        /// </summary>
        public void TriggerStudioModeTransition()
        {
            SendRequest(nameof(TriggerStudioModeTransition));
        }

        /// <summary>
        /// Toggles the state of the replay buffer output.
        /// </summary>
        public void ToggleReplayBuffer()
        {
            SendRequest(nameof(ToggleReplayBuffer));
        }

        /// <summary>
        /// Start recording into the Replay Buffer. Triggers an error
        /// if the Replay Buffer is already active, or if the "Save Replay Buffer"
        /// hotkey is not set in OBS' settings
        /// </summary>
        public void StartReplayBuffer()
        {
            SendRequest(nameof(StartReplayBuffer));
        }

        /// <summary>
        /// Stop recording into the Replay Buffer. Triggers an error if the
        /// Replay Buffer is not active.
        /// </summary>
        public void StopReplayBuffer()
        {
            SendRequest(nameof(StopReplayBuffer));
        }

        /// <summary>
        /// Save and flush the contents of the Replay Buffer to disk. Basically
        /// the same as triggering the "Save Replay Buffer" hotkey in OBS.
        /// Triggers an error if Replay Buffer is not active.
        /// </summary>
        public void SaveReplayBuffer()
        {
            SendRequest(nameof(SaveReplayBuffer));
        }

        /// <summary>
        /// Set the audio sync offset of the specified source
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <param name="inputAudioSyncOffset">Audio offset (in nanoseconds) for the specified source</param>
        public void SetInputAudioSyncOffset(string inputName, int inputAudioSyncOffset)
        {
            var requestFields = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(inputAudioSyncOffset), inputAudioSyncOffset }
            };

            SendRequest(nameof(SetInputAudioSyncOffset), requestFields);
        }

        /// <summary>
        /// Get the audio sync offset of the specified source
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <returns>Audio offset (in nanoseconds) of the specified source</returns>
        public int GetInputAudioSyncOffset(string inputName)
        {
            var requestFields = new JObject
            {
                { nameof(inputName), inputName }
            };
            var response = SendRequest(nameof(GetInputAudioSyncOffset), requestFields);
            return (int)response["inputAudioSyncOffset"];
        }

        /// <summary>
        /// Removes a scene item from a scene.
        /// Scenes only.
        /// </summary>
        /// <param name="sceneItemId">Scene item id</param>
        /// <param name="sceneName">Scene name from which to delete item</param>
        public void RemoveSceneItem(string sceneName, int sceneItemId)
        {
            var requestFields = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            SendRequest(nameof(RemoveSceneItem), requestFields);
        }

        /// <summary>
        /// Sends CEA-608 caption text over the stream output. As of OBS Studio 23.1, captions are not yet available on Linux.
        /// </summary>
        /// <param name="captionText">Captions text</param>
        public void SendStreamCaption(string captionText)
        {
            var requestFields = new JObject
            {
                { nameof(captionText), captionText }
            };

            SendRequest(nameof(SendStreamCaption), requestFields);
        }

        /// <summary>
        /// Duplicates a scene item
        /// </summary>
        /// <param name="sceneName">Name of the scene that has the SceneItem</param>
        /// <param name="sceneItemId">Id of the Scene Item</param>
        /// <param name="destinationSceneName">Name of scene to add the new duplicated Scene Item. If not specified will assume sceneName</param>
        public void DuplicateSceneItem(string sceneName, int sceneItemId, string destinationSceneName = null)
        {
            var requestFields = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            if (!String.IsNullOrEmpty(destinationSceneName))
            {
                requestFields.Add(nameof(destinationSceneName), destinationSceneName);
            }

            SendRequest(nameof(DuplicateSceneItem), requestFields);
        }

        /// <summary>
        /// Gets the names of all special inputs.
        /// </summary>
        /// <returns>Dictionary of special inputs.</returns>
        public Dictionary<string, string> GetSpecialInputs()
        {
            var response = SendRequest(nameof(GetSpecialInputs));
            var sources = new Dictionary<string, string>();
            foreach (KeyValuePair<string, JToken> kvp in response)
            {
                string key = kvp.Key;
                string value = (string)kvp.Value;
                if (key != "requestType")
                {
                    sources.Add(key, value);
                }
            }
            return sources;
        }

        /// <summary>
        /// Sets the current stream service settings (stream destination).
        /// Note: Simple RTMP settings can be set with type `rtmp_custom` and the settings fields `server` and `key`.
        /// </summary>
        /// <param name="service">Stream Service Type Name and Settings objects</param>
        public void SetStreamServiceSettings(StreamingService service)
        {
            var requestFields = new JObject
            {
                { "streamServiceType", service.Type },
                { "streamServiceSettings", JToken.FromObject(service.Settings) }
            };

            SendRequest(nameof(SetStreamServiceSettings), requestFields);
        }

        /// <summary>
        /// Gets the current stream service settings (stream destination).
        /// </summary>
        /// <returns>Stream service type and settings objects</returns>
        public StreamingService GetStreamServiceSettings()
        {
            var response = SendRequest(nameof(GetStreamServiceSettings));

            return JsonConvert.DeserializeObject<StreamingService>(response.ToString());
        }

        /// <summary>
        /// Gets the audio monitor type of an input.
        /// The available audio monitor types are:
        /// - `OBS_MONITORING_TYPE_NONE`
        /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
        /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
        /// </summary>
        /// <param name="inputName">Source name</param>
        /// <returns>The monitor type in use</returns>
        public string GetInputAudioMonitorType(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            var response = SendRequest(nameof(GetInputAudioMonitorType), request);
            return (string)response["monitorType"];
        }

        /// <summary>
        /// Sets the audio monitor type of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to set the audio monitor type of</param>
        /// <param name="monitorType">Audio monitor type. See `GetInputAudioMonitorType for possible types.</param>
        public void SetInputAudioMonitorType(string inputName, string monitorType)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(monitorType), monitorType }
            };

            SendRequest(nameof(SetInputAudioMonitorType), request);
        }

        /// <summary>
        /// Broadcasts a `CustomEvent` to all WebSocket clients. Receivers are clients which are identified and subscribed.
        /// </summary>
        /// <param name="eventData">Data payload to emit to all receivers</param>
        public void BroadcastCustomEvent(JObject eventData)
        {
            var request = new JObject
            {
                { nameof(eventData), eventData }
            };

            SendRequest(nameof(BroadcastCustomEvent), request);
        }

        /// <summary>
        /// Sets the cursor position of a media input.
        /// This request does not perform bounds checking of the cursor position.
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <param name="mediaCursor">New cursor position to set (milliseconds).</param>
        public void SetMediaInputCursor(string inputName, int mediaCursor)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(mediaCursor), mediaCursor }
            };

            SendRequest(nameof(SetMediaInputCursor), request);
        }

        /// <summary>
        /// Offsets the current cursor position of a media input by the specified value.
        /// This request does not perform bounds checking of the cursor position.
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <param name="mediaCursorOffset">Value to offset the current cursor position by (milliseconds +/-)</param>
        public void OffsetMediaInputCursor(string inputName, int mediaCursorOffset)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(mediaCursorOffset), mediaCursorOffset }
            };

            SendRequest(nameof(OffsetMediaInputCursor), request);
        }

        /// <summary>
        /// Creates a new input, adding it as a scene item to the specified scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to add the input to as a scene item</param>
        /// <param name="inputName">Name of the new input to created</param>
        /// <param name="inputKind">The kind of input to be created</param>
        /// <param name="inputSettings">Jobject holding the settings object to initialize the input with</param>
        /// <param name="sceneItemEnabled">Whether to set the created scene item to enabled or disabled</param>
        /// <returns>ID of the SceneItem in the scene.</returns>
        public int CreateInput(string sceneName, string inputName, string inputKind, JObject inputSettings, bool? sceneItemEnabled)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(inputName), inputName },
                { nameof(inputKind), inputKind }
            };

            if (inputSettings != null)
            {
                request.Add(nameof(inputSettings), inputSettings);
            }

            if (sceneItemEnabled.HasValue)
            {
                request.Add(nameof(sceneItemEnabled), sceneItemEnabled.Value);
            }

            var response = SendRequest(nameof(CreateInput), request);
            return (int)response["sceneItemId"];
        }

        /// <summary>
        /// Gets the default settings for an input kind.
        /// </summary>
        /// <param name="inputKind">Input kind to get the default settings for</param>
        /// <returns>Object of default settings for the input kind</returns>
        public JObject GetInputDefaultSettings(string inputKind)
        {
            var request = new JObject
            {
                { nameof(inputKind), inputKind }
            };

            var response = SendRequest(nameof(GetInputDefaultSettings), request);
            return (JObject)response["defaultInputSettings"];
        }

        /// <summary>
        /// Gets a list of all scene items in a scene.
        /// Scenes only
        /// </summary>
        /// <param name="sceneName">Name of the scene to get the items of</param>
        /// <returns>Array of scene items in the scene</returns>
        public List<SceneItemDetails> GetSceneItemList(string sceneName)
        {
            JObject request = null;
            if (!string.IsNullOrEmpty(sceneName))
            {
                request = new JObject
                {
                    { nameof(sceneName), sceneName }
                };
            }

            var response = SendRequest(nameof(GetSceneItemList), request);
            return response["sceneItems"].Select(m => new SceneItemDetails((JObject)m)).ToList();
        }

        /// <summary>
        /// Creates a new scene item using a source.
        /// Scenes only
        /// </summary>
        /// <param name="sceneName">Name of the scene to create the new item in</param>
        /// <param name="sourceName">Name of the source to add to the scene</param>
        /// <param name="sceneItemEnabled">Enable state to apply to the scene item on creation</param>
        /// <returns>Numeric ID of the scene item</returns>
        public int CreateSceneItem(string sceneName, string sourceName, bool sceneItemEnabled = true)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sourceName), sourceName },
                { nameof(sceneItemEnabled), sceneItemEnabled }
            };

            var response = SendRequest(nameof(CreateSceneItem), request);
            return (int)response["sceneItemId"];
        }

        /// <summary>
        /// Creates a new scene in OBS.
        /// </summary>
        /// <param name="sceneName">Name for the new scene</param>
        public void CreateScene(string sceneName)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName }
            };

            SendRequest(nameof(CreateScene), request);
        }

        /// <summary>
        /// Gets the enable state of all audio tracks of an input.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <returns>Object of audio tracks and associated enable states</returns>
        public SourceTracks GetInputAudioTracks(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            var response = SendRequest(nameof(GetInputAudioTracks), request);
            return new SourceTracks(response);
        }

        /// <summary>
        /// Sets the enable state of audio tracks of an input.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="inputAudioTracks">JObject holding track settings to apply</param>
        public void SetInputAudioTracks(string inputName, JObject inputAudioTracks)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(inputAudioTracks), inputAudioTracks }
            };

            SendRequest(nameof(SetInputAudioTracks), request);
        }

        /// <summary>
        /// Sets the enable state of audio tracks of an input.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="inputAudioTracks">Track settings to apply</param>
        public void SetInputAudioTracks(string inputName, SourceTracks inputAudioTracks)
        {
            SetInputAudioTracks(inputName, JObject.FromObject(inputAudioTracks));
        }       

        /// <summary>
        /// Gets the active and show state of a source.
        /// **Compatible with inputs and scenes.**
        /// </summary>
        /// <param name="sourceName">Name of the source to get the active state of</param>
        /// <returns>Whether the source is showing in Program</returns>
        public SourceActiveInfo GetSourceActive(string sourceName)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName }
            };

            var response = SendRequest(nameof(GetSourceActive), request);
            return new SourceActiveInfo(response);
        }

        /// <summary>
        /// Gets the status of the virtualcam output.
        /// </summary>
        /// <returns>An <see cref="VirtualCamStatus"/> object describing the current virtual camera state</returns>
        public VirtualCamStatus GetVirtualCamStatus()
        {
            JObject response = SendRequest(nameof(GetVirtualCamStatus));
            var outputStatus = new VirtualCamStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// Starts the virtualcam output.
        /// </summary>
        public void StartVirtualCam()
        {
            SendRequest(nameof(StartVirtualCam));
        }

        /// <summary>
        /// Stops the virtualcam output.
        /// </summary>
        public void StopVirtualCam()
        {
            SendRequest(nameof(StopVirtualCam));
        }

        /// <summary>
        /// Toggles the state of the virtualcam output.
        /// </summary>
        /// <returns>Whether the output is active</returns>
        public VirtualCamStatus ToggleVirtualCam()
        {
            JObject response = SendRequest(nameof(ToggleVirtualCam));
            var outputStatus = new VirtualCamStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// Gets the value of a \"slot\" from the selected persistent data realm.
        /// </summary>
        /// <param name="realm">The data realm to select. `OBS_WEBSOCKET_DATA_REALM_GLOBAL` or `OBS_WEBSOCKET_DATA_REALM_PROFILE`</param>
        /// <param name="slotName">The name of the slot to retrieve data from</param>
        /// <returns type="Any">Value associated with the slot. `null` if not set</returns>
        public JObject GetPersistentData(string realm, string slotName)
        {
            var request = new JObject
            {
                { nameof(realm), realm },
                { nameof(slotName), slotName }
            };

            return SendRequest(nameof(GetPersistentData), request);
        }

        /// <summary>
        /// Sets the value of a \"slot\" from the selected persistent data realm.
        /// </summary>
        /// <param name="realm">The data realm to select. `OBS_WEBSOCKET_DATA_REALM_GLOBAL` or `OBS_WEBSOCKET_DATA_REALM_PROFILE`</param>
        /// <param name="slotName">The name of the slot to retrieve data from</param>
        /// <param name="slotValue">The value to apply to the slot</param>
        public void SetPersistentData(string realm, string slotName, JObject slotValue)
        {
            var request = new JObject
            {
                { nameof(realm), realm },
                { nameof(slotName), slotName },
                { nameof(slotValue), slotValue }
            };

            SendRequest(nameof(SetPersistentData), request);
        }

        /// <summary>
        /// Creates a new scene collection, switching to it in the process.\n\nNote: This will block until the collection has finished changing.
        /// </summary>
        /// <param name="sceneCollectionName">Name for the new scene collection</param>
        public void CreateSceneCollection(string sceneCollectionName)
        {
            var request = new JObject
            {
                { nameof(sceneCollectionName), sceneCollectionName }
            };

            SendRequest(nameof(CreateSceneCollection), request);
        }

        /// <summary>
        /// Creates a new profile, switching to it in the process
        /// </summary>
        /// <param name="profileName">Name for the new profile</param>
        public void CreateProfile(string profileName)
        {
            var request = new JObject
            {
                { nameof(profileName), profileName }
            };

            SendRequest(nameof(CreateProfile), request);
        }

        /// <summary>
        /// Removes a profile. If the current profile is chosen, it will change to a different profile first.
        /// </summary>
        /// <param name="profileName">Name of the profile to remove</param>
        public void RemoveProfile(string profileName)
        {
            var request = new JObject
            {
                { nameof(profileName), profileName }
            };

            SendRequest(nameof(RemoveProfile), request);
        }

        /// <summary>
        /// Gets a parameter from the current profile's configuration.
        /// </summary>
        /// <param name="parameterCategory">Category of the parameter to get</param>
        /// <param name="parameterName">Name of the parameter to get</param>
        /// <returns></returns>
        public JObject GetProfileParameter(string parameterCategory, string parameterName)
        {
            var request = new JObject
            {
                { nameof(parameterCategory), parameterCategory },
                { nameof(parameterName), parameterName }
            };

            return SendRequest(nameof(GetProfileParameter), request);
        }

        /// <summary>
        /// Sets the value of a parameter in the current profile's configuration.
        /// </summary>
        /// <param name="parameterCategory">Category of the parameter to set</param>
        /// <param name="parameterName">Name of the parameter to set</param>
        /// <param name="parameterValue">Value of the parameter to set. Use `null` to delete</param>
        public void SetProfileParameter(string parameterCategory, string parameterName, string parameterValue)
        {
            var request = new JObject
            {
                { nameof(parameterCategory), parameterCategory },
                { nameof(parameterName), parameterName },
                { nameof(parameterValue), parameterValue }
            };

            SendRequest(nameof(SetProfileParameter), request);
        }

        /// <summary>
        /// Sets the current video settings.
        /// Note: Fields must be specified in pairs. For example, you cannot set only `baseWidth` without needing to specify `baseHeight`.
        /// </summary>
        /// <param name="obsVideoSettings">Object containing video settings</param>
        public void SetVideoSettings(ObsVideoSettings obsVideoSettings)
        {
            SendRequest(nameof(SetVideoSettings), JObject.FromObject(obsVideoSettings));
        }

        /// <summary>
        /// Gets the default settings for a filter kind.
        /// </summary>
        /// <param name="filterKind">Filter kind to get the default settings for</param>
        /// <returns>Object of default settings for the filter kind</returns>
        public JObject GetSourceFilterDefaultSettings(string filterKind)
        {
            var request = new JObject
            {
                { nameof(filterKind), filterKind }
            };

            return SendRequest(nameof(GetSourceFilterDefaultSettings), request);
        }

        /// <summary>
        /// Sets the name of a source filter (rename).
        /// </summary>
        /// <param name="sourceName">Name of the source the filter is on</param>
        /// <param name="filterName">Current name of the filter</param>
        /// <param name="newFilterName">New name for the filter</param>
        public void SetSourceFilterName(string sourceName, string filterName, string newFilterName)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName },
                { nameof(newFilterName), newFilterName }
            };

            SendRequest(nameof(SetSourceFilterName), request);
        }

        /// <summary>
        /// Sets the index position of a filter on a source.
        /// </summary>
        /// <param name="sourceName">Name of the source the filter is on</param>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="filterIndex">New index position of the filter</param>
        public void SetSourceFilterIndex(string sourceName, string filterName, int filterIndex)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(filterName), filterName },
                { nameof(filterIndex), filterIndex }
            };

            SendRequest(nameof(SetSourceFilterIndex), request);
        }

        /// <summary>
        /// Gets data about the current plugin and RPC version.
        /// </summary>
        /// <returns>Version info in an <see cref="ObsVersion"/> object</returns>
        public ObsVersion GetVersion()
        {
            JObject response = SendRequest(nameof(GetVersion));
            return new ObsVersion(response);
        }

        /// <summary>
        /// Call a request registered to a vendor.
        /// A vendor is a unique name registered by a third-party plugin or script, which allows for custom requests and events to be added to obs-websocket.
        /// If a plugin or script implements vendor requests or events, documentation is expected to be provided with them.
        /// </summary>
        /// <param name="vendorName">Name of the vendor to use</param>
        /// <param name="requestType">The request type to call</param>
        /// <param name="requestData">Object containing appropriate request data</param>
        /// <returns>Object containing appropriate response data. {} if request does not provide any response data</returns>
        public JObject CallVendorRequest(string vendorName, string requestType, JObject requestData = null)
        {
            var request = new JObject
            {
                { nameof(vendorName), vendorName },
                { nameof(requestType), requestType },
                { nameof(requestData), requestData }
            };

            return SendRequest(nameof(CallVendorRequest), request);
        }

        /// <summary>
        /// Gets an array of all hotkey names in OBS
        /// </summary>
        /// <returns>Array of hotkey names</returns>
        public List<string> GetHotkeyList()
        {
            var response = SendRequest(nameof(GetHotkeyList));
            return JsonConvert.DeserializeObject<List<string>>(response["hotkeys"].ToString());
        }

        /// <summary>
        /// Sleeps for a time duration or number of frames. Only available in request batches with types `SERIAL_REALTIME` or `SERIAL_FRAME`.
        /// </summary>
        /// <param name="sleepMillis">Number of milliseconds to sleep for (if `SERIAL_REALTIME` mode)</param>
        /// <param name="sleepFrames">Number of frames to sleep for (if `SERIAL_FRAME` mode)</param>
        public void Sleep(int sleepMillis, int sleepFrames)
        {
            var request = new JObject
            {
                { nameof(sleepMillis), sleepMillis },
                { nameof(sleepFrames), sleepFrames }
            };

            SendRequest(nameof(Sleep), request);
        }

        /// <summary>
        /// Gets an array of all inputs in OBS.
        /// </summary>
        /// <param name="inputKind">Restrict the array to only inputs of the specified kind</param>
        /// <returns>List of Inputs in OBS</returns>
        public List<Input> GetInputList(string inputKind = null)
        {
            var request = new JObject
            {
                { nameof(inputKind), inputKind }
            };

            var response = inputKind is null
                ? SendRequest(nameof(GetInputList))
                : SendRequest(nameof(GetInputList), request);

            var returnList = new List<Input>();
            foreach (var input in response["inputs"])
            {
                returnList.Add(new Input(input as JObject));
            }

            return returnList;
        }

        /// <summary>
        /// Gets an array of all available input kinds in OBS.
        /// </summary>
        /// <param name="unversioned">True == Return all kinds as unversioned, False == Return with version suffixes (if available)</param>
        /// <returns>Array of input kinds</returns>
        public List<string> GetInputKindList(bool unversioned = false)
        {
            var request = new JObject
            {
                { nameof(unversioned), unversioned }
            };

            var response = unversioned is false
                ? SendRequest(nameof(GetInputKindList))
                : SendRequest(nameof(GetInputKindList), request);

            return JsonConvert.DeserializeObject<List<string>>(response["inputKinds"].ToString());
        }

        /// <summary>
        /// Removes an existing input.
        /// Note: Will immediately remove all associated scene items.
        /// </summary>
        /// <param name="inputName">Name of the input to remove</param>
        public void RemoveInput(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            SendRequest(nameof(RemoveInput), request);
        }

        /// <summary>
        /// Sets the name of an input (rename).
        /// </summary>
        /// <param name="inputName">Current input name</param>
        /// <param name="newInputName">New name for the input</param>
        public void SetInputName(string inputName, string newInputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(newInputName), newInputName }
            };

            SendRequest(nameof(SetInputName), request);
        }

        /// <summary>
        /// Gets the settings of an input.
        /// Note: Does not include defaults. To create the entire settings object, overlay `inputSettings` over the `defaultInputSettings` provided by `GetInputDefaultSettings`.
        /// </summary>
        /// <param name="inputName">Name of the input to get the settings of</param>
        /// <returns>New populated InputSettings object</returns>
        public InputSettings GetInputSettings(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            var response = SendRequest(nameof(GetInputSettings), request);
            response.Merge(request);
            return new InputSettings(response);
        }

        /// <summary>
        /// Sets the settings of an input.
        /// </summary>
        /// <param name="inputSettings">Object of settings to apply</param>
        /// <param name="overlay">True == apply the settings on top of existing ones, False == reset the input to its defaults, then apply settings.</param>
        public void SetInputSettings(InputSettings inputSettings, bool overlay = true)
        {
            SetInputSettings(inputSettings.InputName, inputSettings.Settings, overlay);
        }

        /// <summary>
        /// Sets the settings of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to set the settings of</param>
        /// <param name="inputSettings">Object of settings to apply</param>
        /// <param name="overlay">True == apply the settings on top of existing ones, False == reset the input to its defaults, then apply settings.</param>
        public void SetInputSettings(string inputName, JObject inputSettings, bool overlay = true)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(inputSettings), inputSettings },
                { nameof(overlay), overlay }
            };

            SendRequest(nameof(SetInputSettings), request);
        }

        /// <summary>
        /// Gets the audio balance of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to get the audio balance of</param>
        /// <returns>Audio balance value from 0.0-1.0</returns>
        public double GetInputAudioBalance(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            var response = SendRequest(nameof(GetInputAudioBalance), request);
            return (double)response["inputAudioBalance"];
        }

        /// <summary>
        /// Sets the audio balance of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to set the audio balance of</param>
        /// <param name="inputAudioBalance">New audio balance value</param>
        public void SetInputAudioBalance(string inputName, double inputAudioBalance)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(inputAudioBalance), inputAudioBalance }
            };

            SendRequest(nameof(SetInputAudioBalance), request);
        }

        /// <summary>
        /// Gets the items of a list property from an input's properties.
        /// Note: Use this in cases where an input provides a dynamic, selectable list of items.
        /// For example, display capture, where it provides a list of available displays.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="propertyName">Name of the list property to get the items of</param>
        /// <returns>Array of items in the list property</returns>
        public List<JObject> GetInputPropertiesListPropertyItems(string inputName, string propertyName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(propertyName), propertyName }
            };

            var response = SendRequest(nameof(GetInputPropertiesListPropertyItems), request);
            return response["propertyItems"].Value<List<JObject>>();
        }

        /// <summary>
        /// Presses a button in the properties of an input.
        /// Note: Use this in cases where there is a button in the properties of an input that cannot be accessed in any other way.
        /// For example, browser sources, where there is a refresh button.
        /// </summary>
        /// <param name="inputName">Name of the input</param>
        /// <param name="propertyName">Name of the button property to press</param>
        public void PressInputPropertiesButton(string inputName, string propertyName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(propertyName), propertyName }
            };

            SendRequest(nameof(PressInputPropertiesButton), request);
        }

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
        public MediaInputStatus GetMediaInputStatus(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            return new MediaInputStatus(SendRequest(nameof(GetMediaInputStatus), request));
        }

        /// <summary>
        /// Triggers an action on a media input.
        /// </summary>
        /// <param name="inputName">Name of the media input</param>
        /// <param name="mediaAction">Identifier of the `ObsMediaInputAction` enum</param>
        public void TriggerMediaInputAction(string inputName, string mediaAction)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName },
                { nameof(mediaAction), mediaAction }
            };

            SendRequest(nameof(TriggerMediaInputAction), request);
        }

        /// <summary>
        /// Gets the filename of the last replay buffer save file.
        /// </summary>
        /// <returns>File path of last replay</returns>
        public string GetLastReplayBufferReplay()
        {
            var response = SendRequest(nameof(GetLastReplayBufferReplay));
            return (string)response["savedReplayPath"];
        }

        /// <summary>
        /// Toggles pause on the record output.
        /// </summary>
        public void ToggleRecordPause()
        {
            SendRequest(nameof(ToggleRecordPause));
        }

        /// <summary>
        /// Currently BROKEN in obs-websocket/obs-studio
        /// Basically GetSceneItemList, but for groups.
        /// Using groups at all in OBS is discouraged, as they are very broken under the hood.
        /// Groups only
        /// </summary>
        /// <param name="sceneName">Name of the group to get the items of</param>
        /// <returns>Array of scene items in the group</returns>
        public List<JObject> GetGroupSceneItemList(string sceneName)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName }
            };

            var response = SendRequest(nameof(GetGroupSceneItemList), request);
            return JsonConvert.DeserializeObject<List<JObject>>((string)response["sceneItems"]);
        }

        /// <summary>
        /// Searches a scene for a source, and returns its id.\n\nScenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene or group to search in</param>
        /// <param name="sourceName">Name of the source to find</param>
        /// <param name="searchOffset">Number of matches to skip during search. >= 0 means first forward. -1 means last (top) item</param>
        /// <returns>Numeric ID of the scene item</returns>
        public int GetSceneItemId(string sceneName, string sourceName, int searchOffset)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sourceName), sourceName },
                { nameof(searchOffset), searchOffset }
            };

            var response = SendRequest(nameof(GetSceneItemId), request);
            return (int)response["sceneItemId"];
        }

        /// <summary>
        /// Gets the transform and crop info of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Object containing scene item transform info</returns>
        public SceneItemTransformInfo GetSceneItemTransform(string sceneName, int sceneItemId)
        {
            var response = GetSceneItemTransformRaw(sceneName, sceneItemId);
            return JsonConvert.DeserializeObject<SceneItemTransformInfo>(response["sceneItemTransform"].ToString());
        }

        /// <summary>
        /// Gets the JObject of transform settings for a scene item. Use this one you don't want it populated with default values.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Object containing scene item transform info</returns>
        public JObject GetSceneItemTransformRaw(string sceneName, int sceneItemId)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            return SendRequest(nameof(GetSceneItemTransform), request);
        }

        /// <summary>
        /// Gets the enable state of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Whether the scene item is enabled. `true` for enabled, `false` for disabled</returns>
        public bool GetSceneItemEnabled(string sceneName, int sceneItemId)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            var response = SendRequest(nameof(GetSceneItemEnabled), request);
            return (bool)response["sceneItemEnabled"];
        }

        /// <summary>
        /// Gets the enable state of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <param name="sceneItemEnabled">New enable state of the scene item</param>
        public void SetSceneItemEnabled(string sceneName, int sceneItemId, bool sceneItemEnabled)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId },
                { nameof(sceneItemEnabled), sceneItemEnabled }
            };

            SendRequest(nameof(SetSceneItemEnabled), request);
        }

        /// <summary>
        /// Gets the lock state of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Whether the scene item is locked. `true` for locked, `false` for unlocked</returns>
        public bool GetSceneItemLocked(string sceneName, int sceneItemId)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            var response = SendRequest(nameof(GetSceneItemLocked), request);
            return (bool)response["sceneItemLocked"];
        }

        /// <summary>
        /// Sets the lock state of a scene item.
        /// Scenes and Group
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <param name="sceneItemLocked">New lock state of the scene item</param>
        public void SetSceneItemLocked(string sceneName, int sceneItemId, bool sceneItemLocked)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId },
                { nameof(sceneItemLocked), sceneItemLocked }
            };

            SendRequest(nameof(SetSceneItemLocked), request);
        }

        /// <summary>
        /// Gets the index position of a scene item in a scene.
        /// An index of 0 is at the bottom of the source list in the UI.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <returns>Index position of the scene item</returns>
        public int GetSceneItemIndex(string sceneName, int sceneItemId)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            var response = SendRequest(nameof(GetSceneItemIndex), request);
            return (int)response["sceneItemIndex"];
        }

        /// <summary>
        /// Sets the index position of a scene item in a scene.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName">Name of the scene the item is in</param>
        /// <param name="sceneItemId">Numeric ID of the scene item</param>
        /// <param name="sceneItemIndex">New index position of the scene item</param>
        public void SetSceneItemIndex(string sceneName, int sceneItemId, int sceneItemIndex)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId },
                { nameof(sceneItemIndex), sceneItemIndex }
            };

            SendRequest(nameof(SetSceneItemIndex), request);
        }

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
        public string GetSceneItemBlendMode(string sceneName, int sceneItemId)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId }
            };

            var response = SendRequest(nameof(GetSceneItemBlendMode), request);
            return (string)response["sceneItemBlendMode"];
        }

        /// <summary>
        /// Sets the blend mode of a scene item.
        /// Scenes and Groups
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="sceneItemId"></param>
        /// <param name="sceneItemBlendMode"></param>
        public void SetSceneItemBlendMode(string sceneName, int sceneItemId, string sceneItemBlendMode)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(sceneItemId), sceneItemId },
                { nameof(sceneItemBlendMode), sceneItemBlendMode }
            };

            SendRequest(nameof(SetSceneItemBlendMode), request);
        }

        /// <summary>
        /// Gets an array of all groups in OBS.
        /// Groups in OBS are actually scenes, but renamed and modified. In obs-websocket, we treat them as scenes where we can.
        /// </summary>
        /// <returns>Array of group names</returns>
        public List<string> GetGroupList()
        {
            var response = SendRequest(nameof(GetGroupList));
            return JsonConvert.DeserializeObject<List<string>>(response["groups"].ToString());
        }

        /// <summary>
        /// Removes a scene from OBS.
        /// </summary>
        /// <param name="sceneName">Name of the scene to remove</param>
        public void RemoveScene(string sceneName)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName }
            };

            SendRequest(nameof(RemoveScene), request);
        }

        /// <summary>
        /// Sets the name of a scene (rename).
        /// </summary>
        /// <param name="sceneName">Name of the scene to be renamed</param>
        /// <param name="newSceneName">New name for the scene</param>
        public void SetSceneName(string sceneName, string newSceneName)
        {
            var request = new JObject
            {
                { nameof(sceneName), sceneName },
                { nameof(newSceneName), newSceneName }
            };

            SendRequest(nameof(SetSceneName), request);
        }

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
        public string GetSourceScreenshot(string sourceName, string imageFormat, int imageWidth = -1, int imageHeight = -1, int imageCompressionQuality = -1)
        {
            var request = new JObject
            {
                { nameof(sourceName), sourceName },
                { nameof(imageFormat), imageFormat }
            };

            if (imageWidth > -1)
            {
                request.Add(nameof(imageWidth), imageWidth);
            }
            if (imageHeight > -1)
            {
                request.Add(nameof(imageHeight), imageHeight);
            }
            if (imageCompressionQuality > -1)
            {
                request.Add(nameof(imageCompressionQuality), imageCompressionQuality);
            }

            var response = SendRequest(nameof(GetSourceScreenshot), request);
            return (string)response["imageData"];
        }

        /// <summary>
        /// Gets an array of all available transition kinds.
        /// Similar to `GetInputKindList`
        /// </summary>
        /// <returns>Array of transition kinds</returns>
        public List<string> GetTransitionKindList()
        {
            var response = SendRequest(nameof(GetTransitionKindList));
            return JsonConvert.DeserializeObject<List<string>>(response["transitionKinds"].ToString());
        }

        /// <summary>
        /// Gets the cursor position of the current scene transition.
        /// Note: `transitionCursor` will return 1.0 when the transition is inactive.
        /// </summary>
        /// <returns>Cursor position, between 0.0 and 1.0</returns>
        public double GetCurrentSceneTransitionCursor()
        {
            var response = SendRequest(nameof(GetCurrentSceneTransitionCursor));
            return (double)response["transitionCursor"];
        }

        /// <summary>
        /// Opens the properties dialog of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to open the dialog of</param>
        public void OpenInputPropertiesDialog(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            SendRequest(nameof(OpenInputPropertiesDialog), request);
        }

        /// <summary>
        /// Opens the filters dialog of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to open the dialog of</param>
        public void OpenInputFiltersDialog(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            SendRequest(nameof(OpenInputFiltersDialog), request);
        }

        /// <summary>
        /// Opens the interact dialog of an input.
        /// </summary>
        /// <param name="inputName">Name of the input to open the dialog of</param>
        public void OpenInputInteractDialog(string inputName)
        {
            var request = new JObject
            {
                { nameof(inputName), inputName }
            };

            SendRequest(nameof(OpenInputInteractDialog), request);
        }

        /// <summary>
        /// Gets a list of connected monitors and information about them.
        /// </summary>
        /// <returns>a list of detected monitors with some information</returns>
        public List<Monitor> GetMonitorList()
        {
            var response = SendRequest(nameof(GetMonitorList));
            var monitors = new List<Monitor>();

            foreach(var monitor in response["monitors"])
            {
                monitors.Add(new Monitor((JObject)monitor));
            }

            return monitors;
        }
    }
}