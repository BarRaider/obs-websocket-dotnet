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
        #region Private Members

        private const string SOURCE_TYPE_JSON_FIELD = "sourceType";
        private const string SOURCE_TYPE_BROWSER_SOURCE = "browser_source";

        #endregion

        /// <summary>
        /// Get basic OBS video information
        /// </summary>
        public OBSVideoInfo GetVideoInfo()
        {
            JObject response = SendRequest("GetVideoInfo");
            return JsonConvert.DeserializeObject<OBSVideoInfo>(response.ToString());
        }

        /// <summary>
        /// At least embedPictureFormat or saveToFilePath must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="embedPictureFormat">Format of the Data URI encoded picture. Can be "png", "jpg", "jpeg" or "bmp" (or any other value supported by Qt's Image module)</param>
        /// <param name="saveToFilePath">Full file path (file extension included) where the captured image is to be saved. Can be in a format different from pictureFormat. Can be a relative path.</param>
        /// <param name="width">Screenshot width. Defaults to the source's base width.</param>
        /// <param name="height">Screenshot height. Defaults to the source's base height.</param>
        public SourceScreenshotResponse TakeSourceScreenshot(string sourceName, string embedPictureFormat = null, string saveToFilePath = null, int width = -1, int height = -1)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName }
            };

            if (embedPictureFormat != null)
            {
                requestFields.Add("embedPictureFormat", embedPictureFormat);
            }
            if (saveToFilePath != null)
            {
                requestFields.Add("saveToFilePath", saveToFilePath);
            }
            if (width > -1)
            {
                requestFields.Add("width", width);
            }
            if (height > -1)
            {
                requestFields.Add("height", height);
            }

            var response = SendRequest("TakeSourceScreenshot", requestFields);
            return JsonConvert.DeserializeObject<SourceScreenshotResponse>(response.ToString());
        }

        /// <summary>
        /// At least embedPictureFormat or saveToFilePath must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="embedPictureFormat">Format of the Data URI encoded picture. Can be "png", "jpg", "jpeg" or "bmp" (or any other value supported by Qt's Image module)</param>
        /// <param name="saveToFilePath">Full file path (file extension included) where the captured image is to be saved. Can be in a format different from pictureFormat. Can be a relative path.</param>
        public SourceScreenshotResponse TakeSourceScreenshot(string sourceName, string embedPictureFormat = null, string saveToFilePath = null)
        {
            return TakeSourceScreenshot(sourceName, embedPictureFormat, saveToFilePath, -1, -1);
        }

        /// <summary>
        /// Executes hotkey routine, identified by hotkey unique name
        /// </summary>
        /// <param name="hotkeyName">Unique name of the hotkey, as defined when registering the hotkey (e.g. "ReplayBuffer.Save")</param>
        public void TriggerHotkeyByName(string hotkeyName)
        {
            var requestFields = new JObject
            {
                { "hotkeyName", hotkeyName }
            };

            SendRequest("TriggerHotkeyByName", requestFields);
        }

        /// <summary>
        /// EExecutes hotkey routine, identified by bound combination of keys. A single key combination might trigger multiple hotkey routines depending on user settings
        /// </summary>
        /// <param name="key">Main key identifier (e.g. OBS_KEY_A for key "A"). Available identifiers are here: https://github.com/obsproject/obs-studio/blob/master/libobs/obs-hotkeys.h</param>
        /// <param name="keyModifier">Optional key modifiers object. You can combine multiple key operators. e.g. KeyModifier.Shift | KeyModifier.Control</param>
        public void TriggerHotkeyBySequence(OBSHotkey key, KeyModifier keyModifier = KeyModifier.None)
        {
            var requestFields = new JObject
            {
                { "keyId", key.ToString() },
                { "keyModifiers", new JObject{
                    { "shift", (keyModifier & KeyModifier.Shift) == KeyModifier.Shift },
                    { "alt", (keyModifier & KeyModifier.Alt) == KeyModifier.Alt },
                    { "control", (keyModifier & KeyModifier.Control) == KeyModifier.Control },
                    { "command", (keyModifier & KeyModifier.Command) == KeyModifier.Command } } 
                }
            };

            SendRequest("TriggerHotkeyBySequence", requestFields);
        }

        /// <summary>
        /// Get the current scene info along with its items
        /// </summary>
        /// <returns>An <see cref="OBSScene"/> object describing the current scene</returns>
        public OBSScene GetCurrentScene()
        {
            JObject response = SendRequest("GetCurrentScene");
            return new OBSScene(response);
        }

        /// <summary>
        /// Set the current scene to the specified one
        /// </summary>
        /// <param name="sceneName">The desired scene name</param>
        public void SetCurrentScene(string sceneName)
        {
            var requestFields = new JObject
            {
                { "scene-name", sceneName }
            };

            SendRequest("SetCurrentScene", requestFields);
        }

        /// <summary>
        /// Get the filename formatting string
        /// </summary>
        /// <returns>Current filename formatting string</returns>
        public string GetFilenameFormatting()
        {
            JObject response = SendRequest("GetFilenameFormatting");
            return (string)response["filename-formatting"];
        }

        /// <summary>
        /// Get OBS stats (almost the same info as provided in OBS' stats window)
        /// </summary>
        public OBSStats GetStats()
        {
            JObject response = SendRequest("GetStats");
            return JsonConvert.DeserializeObject<OBSStats>(response["stats"].ToString());
        }

        /// <summary>
        /// List every available scene
        /// </summary>
        /// <returns>A <see cref="List{OBSScene}" /> of <see cref="OBSScene"/> objects describing each scene</returns>
        public List<OBSScene> ListScenes()
        {
            var response = GetSceneList();
            return response.Scenes;
        }

        /// <summary>
        /// Get a list of scenes in the currently active profile
        /// </summary>
        public GetSceneListInfo GetSceneList()
        {
            JObject response = SendRequest("GetSceneList");
            return JsonConvert.DeserializeObject<GetSceneListInfo>(response.ToString());
        }

        /// <summary>
        /// Changes the order of scene items in the requested scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to reorder (defaults to current)</param>
        /// <param name="sceneItems">List of items to reorder, only ID or Name required</param>
        public void ReorderSceneItems(List<SceneItemStub> sceneItems, string sceneName = null)
        {
            var requestFields = new JObject();
            if (sceneName != null)
            {
                requestFields.Add("scene", sceneName);
            }

            var items = JObject.Parse(JsonConvert.SerializeObject(sceneItems));
            requestFields.Add("items", items);

            SendRequest("ReorderSceneItems", requestFields);
        }

        /// <summary>
        /// Get the specified scene's transition override info
        /// </summary>
        /// <param name="sceneName">Name of the scene to return the override info</param>
        /// <returns>TransitionOverrideInfo</returns>
        public TransitionOverrideInfo GetSceneTransitionOverride(string sceneName)
        {
            var requestFields = new JObject
            {
                { "sceneName", sceneName }
            };

            JObject response = SendRequest("GetSceneTransitionOverride", requestFields);
            return response.ToObject<TransitionOverrideInfo>();
        }

        /// <summary>
        /// Set specific transition override for a scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to set the transition override</param>
        /// <param name="transitionName">Name of the transition to use</param>
        /// <param name="transitionDuration">Duration in milliseconds of the transition if transition is not fixed. Defaults to the current duration specified in the UI if there is no current override and this value is not given</param>
        public void SetSceneTransitionOverride(string sceneName, string transitionName, int transitionDuration = -1)
        {
            var requestFields = new JObject
            {
                { "sceneName", sceneName },
                { "transitionName", transitionName }
            };

            if (transitionDuration >= 0)
            {
                requestFields.Add("transitionDuration", transitionDuration);
            }

            SendRequest("SetSceneTransitionOverride", requestFields);
        }

        /// <summary>
        /// Remove any transition override from a specific scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to remove the transition override</param>
        public void RemoveSceneTransitionOverride(string sceneName)
        {
            var requestFields = new JObject
            {
                { "sceneName", sceneName }
            };

            SendRequest("RemoveSceneTransitionOverride", requestFields);
        }

        /// <summary>
        /// List all sources available in the running OBS instance
        /// </summary>
        public List<SourceInfo> GetSourcesList()
        {
            JObject response = SendRequest("GetSourcesList");
            return JsonConvert.DeserializeObject<List<SourceInfo>>(response["sources"].ToString());
        }

        /// <summary>
        /// List all sources available in the running OBS instance
        /// </summary>
        public List<SourceType> GetSourceTypesList()
        {
            JObject response = SendRequest("GetSourceTypesList");
            return JsonConvert.DeserializeObject<List<SourceType>>(response["types"].ToString());
        }

        /// <summary>
        /// Change the visibility of the specified scene item
        /// </summary>
        /// <param name="itemName">Scene item which visiblity will be changed</param>
        /// <param name="visible">Desired visiblity</param>
        /// <param name="sceneName">Scene name of the specified item</param>
        public void SetSourceRender(string itemName, bool visible, string sceneName = null)
        {
            var requestFields = new JObject
            {
                { "item", itemName },
                { "visible", visible }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("SetSceneItemProperties", requestFields);
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        public SceneItemProperties GetSceneItemProperties(string itemName, string sceneName = null)
        {
            return JsonConvert.DeserializeObject<SceneItemProperties>(GetSceneItemPropertiesJson(itemName, sceneName).ToString());
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// Response is a JObject
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        public JObject GetSceneItemPropertiesJson(string itemName, string sceneName = null)
        {
            var requestFields = new JObject
            {
                { "item", itemName }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            return SendRequest("GetSceneItemProperties", requestFields);
        }

        /// <summary>
        /// Get the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="sourceName">The name of the source</param>
        public TextGDIPlusProperties GetTextGDIPlusProperties(string sourceName)
        {
            var requestFields = new JObject
            {
                { "source", sourceName }
            };

            JObject response = SendRequest("GetTextGDIPlusProperties", requestFields);
            return JsonConvert.DeserializeObject<TextGDIPlusProperties>(response.ToString());
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

            var requestFields = new JObject
            {
                { "position", position },
                { "release", release}
            };

            SendRequest("SetTBarPosition", requestFields);
        }
        /// <summary>
        /// Set the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="properties">properties for the source</param>
        public void SetTextGDIPlusProperties(TextGDIPlusProperties properties)
        {
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(properties));

            SendRequest("SetTextGDIPlusProperties", requestFields);

        }

        /// <summary>
        /// Move a filter in the chain (relative positioning)
        /// </summary>
        /// <param name="sourceName">Scene Name</param>
        /// <param name="filterName">Filter Name</param>
        /// <param name="movement">Direction to move</param>
        public void MoveSourceFilter(string sourceName, string filterName, FilterMovementType movement)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "movementType", movement.ToString().ToLowerInvariant() }
            };

            SendRequest("MoveSourceFilter", requestFields);
        }

        /// <summary>
        /// Move a filter in the chain (absolute index positioning)
        /// </summary>
        /// <param name="sourceName">Scene Name</param>
        /// <param name="filterName">Filter Name</param>
        /// <param name="newIndex">Desired position of the filter in the chain</param>
        public void ReorderSourceFilter(string sourceName, string filterName, int newIndex)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "newIndex", newIndex }
            };

            SendRequest("ReorderSourceFilter", requestFields);
        }

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">Filter settings</param>
        public void SetSourceFilterSettings(string sourceName, string filterName, JObject filterSettings)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "filterSettings", filterSettings }
            };

            SendRequest("SetSourceFilterSettings", requestFields);
        }

        /// <summary>
        /// Modify the Source Filter's visibility
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Source filter name</param>
        /// <param name="filterEnabled">New filter state</param>
        public void SetSourceFilterVisibility(string sourceName, string filterName, bool filterEnabled)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName },
                { "filterEnabled", filterEnabled }
            };

            SendRequest("SetSourceFilterVisibility", requestFields);
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        public List<FilterSettings> GetSourceFilters(string sourceName)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName }
            };

            JObject response = SendRequest("GetSourceFilters", requestFields);
            return JsonConvert.DeserializeObject<List<FilterSettings>>(response["filters"].ToString());
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="filterName">Filter name</param>
        public FilterSettings GetSourceFilterInfo(string sourceName, string filterName)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName }
            };

            JObject response = SendRequest("GetSourceFilterInfo", requestFields);
            return JsonConvert.DeserializeObject<FilterSettings>(response.ToString());
        }

        /// <summary>
        /// Release the T-Bar (like a user releasing their mouse button after moving it). YOU MUST CALL THIS if you called SetTBarPosition with the release parameter set to false.
        /// </summary>
        public void ReleaseTBar()
        {
            SendRequest("ReleaseTBar");
        }

        /// <summary>
        /// Remove the filter from a source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="filterName"></param>
        public bool RemoveFilterFromSource(string sourceName, string filterName)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterName", filterName }
            };
            try
            {
                SendRequest("RemoveFilterFromSource", requestFields);
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
        /// <param name="filterType">Type of filter</param>
        /// <param name="filterSettings">Filter settings object</param>
        public void AddFilterToSource(string sourceName, string filterName, string filterType, JObject filterSettings)
        {
            var requestFields = new JObject
            {
                { "sourceName", sourceName },
                { "filterType", filterType },
                { "filterName", filterName },
                { "filterSettings", filterSettings }
            };

            SendRequest("AddFilterToSource", requestFields);
        }

        /// <summary>
        /// Start/Stop the streaming output
        /// </summary>
        public void ToggleStreaming()
        {
            SendRequest("StartStopStreaming");
        }

        /// <summary>
        /// Start/Stop the recording output
        /// </summary>
        public void ToggleRecording()
        {
            SendRequest("StartStopRecording");
        }

        /// <summary>
        /// Get the current status of the streaming and recording outputs
        /// </summary>
        /// <returns>An <see cref="OutputStatus"/> object describing the current outputs states</returns>
        public OutputStatus GetStreamingStatus()
        {
            JObject response = SendRequest("GetStreamingStatus");
            var outputStatus = new OutputStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// List all transitions
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all transition names</returns>
        public List<string> ListTransitions()
        {
            var transitions = GetTransitionList();

            List<string> transitionNames = new List<string>();
            foreach (var item in transitions.Transitions)
                transitionNames.Add(item.Name);


            return transitionNames;
        }

        /// <summary>
        /// Get the current transition name and duration
        /// </summary>
        /// <returns>An <see cref="TransitionSettings"/> object with the current transition name and duration</returns>
        public TransitionSettings GetCurrentTransition()
        {
            JObject respBody = SendRequest("GetCurrentTransition");
            return new TransitionSettings(respBody);
        }

        /// <summary>
        /// Set the current transition to the specified one
        /// </summary>
        /// <param name="transitionName">Desired transition name</param>
        public void SetCurrentTransition(string transitionName)
        {
            var requestFields = new JObject
            {
                { "transition-name", transitionName }
            };

            SendRequest("SetCurrentTransition", requestFields);
        }

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="duration">Desired transition duration (in milliseconds)</param>
        public void SetTransitionDuration(int duration)
        {
            var requestFields = new JObject
            {
                { "duration", duration }
            };

            SendRequest("SetTransitionDuration", requestFields);
        }

        /// <summary>
        /// Change the current settings of a transition
        /// </summary>
        /// <param name="transitionName">Transition name</param>
        /// <param name="transitionSettings">Transition settings (they can be partial)</param>
        /// <returns>Updated transition settings</returns>
        public JObject SetTransitionSettings(string transitionName, JObject transitionSettings)
        {
            var requestFields = new JObject
            {
                { "transitionName", transitionName },
                { "transitionSettings", JToken.FromObject(transitionSettings)}
            };

            var response = SendRequest("SetTransitionSettings", requestFields);
            return (JObject) response.SelectToken("transitionSettings");
        }

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which volume will be changed</param>
        /// <param name="volume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul (useDecibel is false), and under 0.0 for dB (useDecibel is true). Note: OBS will interpret dB values under -100.0 as Inf.</param>
        /// <param name="useDecibel">Interperet `volume` data as decibels instead of amplitude/mul.</param>
        public void SetVolume(string sourceName, float volume, bool useDecibel = false)
        {
            var requestFields = new JObject
            {
                { "source", sourceName },
                { "volume", volume },
                { "useDecibel", useDecibel }
            };

            SendRequest("SetVolume", requestFields);
        }

        /// <summary>
        /// Get the volume of the specified source
        /// Volume is between `0.0` and `1.0` if using amplitude/mul (useDecibel is false), under `0.0` if using dB (useDecibel is true).
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="useDecibel">Output volume in decibels of attenuation instead of amplitude/mul.</param>
        /// <returns>An <see cref="VolumeInfo"/>Object containing the volume and mute state of the specified source.</returns>
        public VolumeInfo GetVolume(string sourceName, bool useDecibel = false)
        {
            var requestFields = new JObject
            {
                { "source", sourceName },
                { "useDecibel", useDecibel }
            };

            var response = SendRequest("GetVolume", requestFields);
            return new VolumeInfo(response);
        }

        /// <summary>
        /// Set the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be changed</param>
        /// <param name="mute">Desired mute state</param>
        public void SetMute(string sourceName, bool mute)
        {
            var requestFields = new JObject
            {
                { "source", sourceName },
                { "mute", mute }
            };

            SendRequest("SetMute", requestFields);
        }

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be toggled</param>
        public void ToggleMute(string sourceName)
        {
            var requestFields = new JObject
            {
                { "source", sourceName }
            };

            SendRequest("ToggleMute", requestFields);
        }

        /// <summary>
        /// Set the position of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which position will be changed</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        [Obsolete("Deprecated! Please use SetSceneItemProperties(). Will be removed in a future update")]
        public void SetSceneItemPosition(string itemName, float x, float y, string sceneName = null)
        {
            var requestFields = new JObject
            {
                { "item", itemName },
                { "x", x },
                { "y", y }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("SetSceneItemPosition", requestFields);
        }

        /// <summary>
        /// Set the scale and rotation of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which transform will be changed</param>
        /// <param name="rotation">Rotation in Degrees</param>
        /// <param name="xScale">Horizontal scale factor</param>
        /// <param name="yScale">Vertical scale factor</param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        [Obsolete("Deprecated! Please use SetSceneItemProperties(). Will be removed in a future update")]
        public void SetSceneItemTransform(string itemName, float rotation = 0, float xScale = 1, float yScale = 1, string sceneName = null)
        {
            var requestFields = new JObject
            {
                { "item", itemName },
                { "x-scale", xScale },
                { "y-scale", yScale },
                { "rotation", rotation }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("SetSceneItemTransform", requestFields);
        }

        /// <summary>
        /// Sets the scene specific properties of a source. Unspecified properties will remain unchanged. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="props">Object containing changes</param>
        /// <param name="sceneName">Option scene name</param>
        public void SetSceneItemProperties(SceneItemProperties props, string sceneName = null)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(props, settings));

            if (requestFields["item"] == null)
            {
                requestFields["item"] = props.ItemName;
            }

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("SetSceneItemProperties", requestFields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sceneName"></param>
        public void SetSceneItemProperties(JObject obj, string sceneName = null)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            // Serialize object to SceneItemProperties (needed before proper deserialization)
            var props = JsonConvert.DeserializeObject<SceneItemProperties>(obj.ToString(), settings);

            // Deserialize object
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(props, settings));

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("SetSceneItemProperties", requestFields);
        }

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="scName">Desired scene collection name</param>
        public void SetCurrentSceneCollection(string scName)
        {
            var requestFields = new JObject
            {
                { "sc-name", scName }
            };

            SendRequest("SetCurrentSceneCollection", requestFields);
        }

        /// <summary>
        /// Get the name of the current scene collection
        /// </summary>
        /// <returns>Name of the current scene collection</returns>
        public string GetCurrentSceneCollection()
        {
            var response = SendRequest("GetCurrentSceneCollection");
            return (string)response["sc-name"];
        }

        /// <summary>
        /// List all scene collections
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all scene collections</returns>
        public List<string> ListSceneCollections()
        {
            var response = SendRequest("ListSceneCollections");
            var items = (JArray)response["scene-collections"];

            List<string> sceneCollections = new List<string>();
            foreach (JObject item in items)
            {
                sceneCollections.Add((string)item["sc-name"]);
            }

            return sceneCollections;
        }

        /// <summary>
        /// Set the current profile to the specified one
        /// </summary>
        /// <param name="profileName">Name of the desired profile</param>
        public void SetCurrentProfile(string profileName)
        {
            var requestFields = new JObject
            {
                { "profile-name", profileName }
            };

            SendRequest("SetCurrentProfile", requestFields);
        }

        /// <summary>
        /// Get the name of the current profile
        /// </summary>
        /// <returns>Name of the current profile</returns>
        public string GetCurrentProfile()
        {
            var response = SendRequest("GetCurrentProfile");
            return (string)response["profile-name"];
        }

        /// <summary>
        /// List all profiles
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all profiles</returns>
        public List<string> ListProfiles()
        {
            var response = SendRequest("ListProfiles");
            var items = (JArray)response["profiles"];

            List<string> profiles = new List<string>();
            foreach (JObject item in items)
            {
                profiles.Add((string)item["profile-name"]);
            }

            return profiles;
        }

        // TODO: needs updating
        /// <summary>
        /// Start streaming. Will trigger an error if streaming is already active
        /// </summary>
        public void StartStreaming()
        {
            SendRequest("StartStreaming");
        }

        /// <summary>
        /// Stop streaming. Will trigger an error if streaming is not active.
        /// </summary>
        public void StopStreaming()
        {
            SendRequest("StopStreaming");
        }

        /// <summary>
        /// Toggle Streaming
        /// </summary>
        public void StartStopStreaming()
        {
            SendRequest("StartStopStreaming");
        }

        /// <summary>
        /// Start recording. Will trigger an error if recording is already active.
        /// </summary>
        public void StartRecording()
        {
            SendRequest("StartRecording");
        }

        /// <summary>
        /// Stop recording. Will trigger an error if recording is not active.
        /// </summary>
        public void StopRecording()
        {
            SendRequest("StopRecording");
        }

        /// <summary>
        /// Toggle recording
        /// </summary>
        public void StartStopRecording()
        {
            SendRequest("StartStopRecording");
        }

        /// <summary>
        /// Pause the current recording. Returns an error if recording is not active or already paused.
        /// </summary>
        public void PauseRecording()
        {
            SendRequest("PauseRecording");
        }

        /// <summary>
        /// Resume/unpause the current recording (if paused). Returns an error if recording is not active or not paused.
        /// </summary>
        public void ResumeRecording()
        {
            SendRequest("ResumeRecording");
        }

        /// <summary>
        /// Change the current recording folder
        /// </summary>
        /// <param name="recFolder">Recording folder path</param>
        public void SetRecordingFolder(string recFolder)
        {
            var requestFields = new JObject
            {
                { "rec-folder", recFolder }
            };

            SendRequest("SetRecordingFolder", requestFields);
        }

        /// <summary>
        /// Get the path of the current recording folder
        /// </summary>
        /// <returns>Current recording folder path</returns>
        public string GetRecordingFolder()
        {
            var response = SendRequest("GetRecordingFolder");
            return (string)response["rec-folder"];
        }

        /// <summary>
        /// Get current recording status.
        /// </summary>
        /// <returns></returns>
        public RecordingStatus GetRecordingStatus()
        {
            var response = SendRequest("GetRecordingStatus");
            return JsonConvert.DeserializeObject<RecordingStatus>(response.ToString());
        }

        /// <summary>
        /// Get the status of the OBS replay buffer.
        /// </summary>
        /// <returns>Current recording status. true when active</returns>
        public bool GetReplayBufferStatus()
        {
            var response = SendRequest("GetReplayBufferStatus");
            return (bool)response["isReplayBufferActive"];
        }

        /// <summary>
        /// Get the current settings of a transition
        /// </summary>
        /// <param name="transitionName">Transition name</param>
        /// <returns>Current transition settings</returns>
        public JObject GetTransitionSettings(string transitionName)
        {
            var requestFields = new JObject
            {
                { "transitionName", transitionName }
            };

            var response = SendRequest("GetTransitionSettings", requestFields);
            return (JObject)response.SelectToken("transitionSettings");
        }
        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <returns>Current transition duration (in milliseconds)</returns>
        public int GetTransitionDuration()
        {
            var response = SendRequest("GetTransitionDuration");
            return (int)response["transition-duration"];
        }

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <returns>Current transition duration (in milliseconds)</returns>
        public GetTransitionListInfo GetTransitionList()
        {
            var response = SendRequest("GetTransitionList");

            return JsonConvert.DeserializeObject<GetTransitionListInfo>(response.ToString());
        }

        /// <summary>
        /// Get the position of the current transition. Value will be between 0.0 and 1.0.
        /// Note: Returns 1.0 when not active.
        /// </summary>
        /// <returns></returns>
        public double GetTransitionPosition()
        {
            var response = SendRequest("GetTransitionPosition");

            return (double)response["position"];
        }

        /// <summary>
        /// Get status of Studio Mode
        /// </summary>
        /// <returns>Studio Mode status (on/off)</returns>
        public bool StudioModeEnabled()
        {
            var response = SendRequest("GetStudioModeStatus");
            return (bool)response["studio-mode"];
        }

        /// <summary>
        /// Disable Studio Mode
        /// </summary>
        public void DisableStudioMode()
        {
            SendRequest("DisableStudioMode");
        }

        /// <summary>
        /// Enable Studio Mode
        /// </summary>
        public void EnableStudioMode()
        {
            SendRequest("EnableStudioMode");
        }

        /// <summary>
        /// Enable Studio Mode
        /// </summary>
        public bool GetStudioModeStatus()
        {
            var response = SendRequest("GetStudioModeStatus");
            return (bool)response["studio-mode"];
        }

        /// <summary>
        /// Enable/disable Studio Mode
        /// </summary>
        /// <param name="enable">Desired Studio Mode status</param>
        public void SetStudioMode(bool enable)
        {
            if (enable)
                EnableStudioMode();
            else
                DisableStudioMode();
        }

        /// <summary>
        /// Toggle Studio Mode status (on to off or off to on)
        /// </summary>
        public void ToggleStudioMode()
        {
            SendRequest("ToggleStudioMode");
        }

        /// <summary>
        /// Get the currently selected preview scene. Triggers an error
        /// if Studio Mode is disabled
        /// </summary>
        /// <returns>Preview scene object</returns>
        public OBSScene GetPreviewScene()
        {
            var response = SendRequest("GetPreviewScene");
            return new OBSScene(response);
        }

        /// <summary>
        /// Change the currently active preview scene to the one specified.
        /// Triggers an error if Studio Mode is disabled
        /// </summary>
        /// <param name="previewScene">Preview scene name</param>
        public void SetPreviewScene(string previewScene)
        {
            var requestFields = new JObject
            {
                { "scene-name", previewScene }
            };

            SendRequest("SetPreviewScene", requestFields);
        }

        /// <summary>
        /// Change the currently active preview scene to the one specified.
        /// Triggers an error if Studio Mode is disabled.
        /// </summary>
        /// <param name="previewScene">Preview scene object</param>
        public void SetPreviewScene(OBSScene previewScene)
        {
            SetPreviewScene(previewScene.Name);
        }

        /// <summary>
        /// Triggers a Studio Mode transition (preview scene to program)
        /// </summary>
        /// <param name="transitionDuration">(optional) Transition duration</param>
        /// <param name="transitionName">(optional) Name of transition to use</param>
        public void TransitionToProgram(int transitionDuration = -1, string transitionName = null)
        {
            var requestFields = new JObject();

            if (transitionDuration > -1 || transitionName != null)
            {
                var withTransition = new JObject();

                if (transitionDuration > -1)
                {
                    withTransition.Add("duration", transitionDuration);
                }

                if (!String.IsNullOrEmpty(transitionName))
                {
                    withTransition.Add("name", transitionName);
                }

                requestFields.Add("with-transition", withTransition);
            }

            SendRequest("TransitionToProgram", requestFields);
        }

        /// <summary>
        /// Get if the specified source is muted
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>Source mute status (on/off)</returns>
        public bool GetMute(string sourceName)
        {
            var requestFields = new JObject
            {
                { "source", sourceName }
            };

            var response = SendRequest("GetMute", requestFields);
            return (bool)response["muted"];
        }

        /// <summary>
        /// Toggle the Replay Buffer on/off
        /// </summary>
        public void ToggleReplayBuffer()
        {
            SendRequest("StartStopReplayBuffer");
        }

        /// <summary>
        /// Start recording into the Replay Buffer. Triggers an error
        /// if the Replay Buffer is already active, or if the "Save Replay Buffer"
        /// hotkey is not set in OBS' settings
        /// </summary>
        public void StartReplayBuffer()
        {
            SendRequest("StartReplayBuffer");
        }

        /// <summary>
        /// Stop recording into the Replay Buffer. Triggers an error if the
        /// Replay Buffer is not active.
        /// </summary>
        public void StopReplayBuffer()
        {
            SendRequest("StopReplayBuffer");
        }

        /// <summary>
        /// Toggle replay buffer
        /// </summary>
        public void StartStopReplayBuffer()
        {
            SendRequest("StartStopReplayBuffer");
        }

        /// <summary>
        /// Save and flush the contents of the Replay Buffer to disk. Basically
        /// the same as triggering the "Save Replay Buffer" hotkey in OBS.
        /// Triggers an error if Replay Buffer is not active.
        /// </summary>
        public void SaveReplayBuffer()
        {
            SendRequest("SaveReplayBuffer");
        }

        /// <summary>
        /// Set the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="syncOffset">Audio offset (in nanoseconds) for the specified source</param>
        public void SetSyncOffset(string sourceName, int syncOffset)
        {
            var requestFields = new JObject
            {
                { "source", sourceName },
                { "offset", syncOffset }
            };

            SendRequest("SetSyncOffset", requestFields);
        }

        /// <summary>
        /// Get the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>Audio offset (in nanoseconds) of the specified source</returns>
        public int GetSyncOffset(string sourceName)
        {
            var requestFields = new JObject
            {
                { "source", sourceName }
            };
            var response = SendRequest("GetSyncOffset", requestFields);
            return (int)response["offset"];
        }

        /// <summary>
        /// Deletes a scene item
        /// </summary>
        /// <param name="sceneItem">Scene item, requires name or id of item</param>
        /// /// <param name="sceneName">Scene name to delete item from (optional)</param>
        public void DeleteSceneItem(SceneItemStub sceneItem, string sceneName = null)
        {
            var requestFields = new JObject();

            if (sceneName != null)
            {
                requestFields.Add("scene", sceneName);
            }

            JObject minReqs = new JObject
            {
                { "id", sceneItem.ID }
            };

            if (sceneItem.SourceName != null)
            {
                minReqs.Add("name", sceneItem.SourceName);
            }
            requestFields.Add("item", minReqs);

            SendRequest("DeleteSceneItem", requestFields);
        }

        /// <summary>
        /// Deletes a scene item
        /// </summary>
        /// <param name="sceneItemId">Scene item id</param>
        /// /// <param name="sceneName">Scene name to delete item from (optional)</param>
        public void DeleteSceneItem(int sceneItemId, string sceneName = null)
        {
            var requestFields = new JObject();

            if (sceneName != null)
            {
                requestFields.Add("scene", sceneName);
            }

            JObject minReqs = new JObject
            {
                { "id", sceneItemId }
            };
            requestFields.Add("item", minReqs);

            SendRequest("DeleteSceneItem", requestFields);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItemName">Name of the scene item</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="sceneName">(optional) parent scene name of the specified source</param>
        [Obsolete("Deprecated! Please use SetSceneItemProperties(). Will be removed in a future update")]
        public void SetSceneItemCrop(string sceneItemName, SceneItemCropInfo cropInfo, string sceneName = null)
        {
            var requestFields = new JObject
            {
                { "item", sceneItemName },
                { "top", cropInfo.Top },
                { "bottom", cropInfo.Bottom },
                { "left", cropInfo.Left },
                { "right", cropInfo.Right }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("SetSceneItemCrop", requestFields);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItem">Scene item object</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="scene">Parent scene of scene item</param>
        [Obsolete("Deprecated! Please use SetSceneItemProperties(). Will be removed in a future update")]
        public void SetSceneItemCrop(SceneItem sceneItem, SceneItemCropInfo cropInfo, OBSScene scene)
        {
            SetSceneItemCrop(sceneItem.SourceName, cropInfo, scene.Name);
        }

        /// <summary>
        /// Reset a scene item
        /// </summary>
        /// <param name="itemName">Name of the source item</param>
        /// <param name="sceneName">Name of the scene the source belongs to. Defaults to the current scene.</param>
        public void ResetSceneItem(string itemName, string sceneName = null)
        {
            var requestFields = new JObject
            {
                { "item", itemName }
            };

            if (sceneName != null)
            {
                requestFields.Add("scene-name", sceneName);
            }

            SendRequest("ResetSceneItem", requestFields);
        }

        /// <summary>
        /// Send the provided text as embedded CEA-608 caption data. As of OBS Studio 23.1, captions are not yet available on Linux.
        /// </summary>
        /// <param name="text">Captions text</param>
        public void SendCaptions(string text)
        {
            var requestFields = new JObject
            {
                { "text", text }
            };

            SendRequest("SendCaptions", requestFields);
        }

        /// <summary>
        /// Set the filename formatting string
        /// </summary>
        /// <param name="filenameFormatting">Filename formatting string to set</param>
        public void SetFilenameFormatting(string filenameFormatting)
        {
            var requestFields = new JObject
            {
                { "filename-formatting", filenameFormatting }
            };

            SendRequest("SetFilenameFormatting", requestFields);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="fromSceneName">Source of the scene item</param>
        /// <param name="toSceneName">Destination for the scene item</param>
        /// <param name="sceneItem">Scene item, requires name or id</param>
        public void DuplicateSceneItem(string fromSceneName, string toSceneName, SceneItem sceneItem)
        {
            var requestFields = new JObject
            {
                { "fromScene", fromSceneName },
                { "toScene", toSceneName }
            };

            JObject minReqs = new JObject();
            if (sceneItem.SourceName != null)
            {
                minReqs.Add("name", sceneItem.SourceName);
            }
            minReqs.Add("id", sceneItem.ID);
            requestFields.Add("item", minReqs);

            SendRequest("DuplicateSceneItem", requestFields);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="fromSceneName">Source of the scene item</param>
        /// <param name="toSceneName">Destination for the scene item</param>
        /// <param name="sceneItemID">Scene item id to duplicate</param>
        public void DuplicateSceneItem(string fromSceneName, string toSceneName, int sceneItemID)
        {
            var requestFields = new JObject
            {
                { "fromScene", fromSceneName },
                { "toScene", toSceneName }
            };

            JObject minReqs = new JObject
            {
                { "id", sceneItemID }
            };
            requestFields.Add("item", minReqs);

            SendRequest("DuplicateSceneItem", requestFields);
        }

        /// <summary>
        /// Get names of configured special sources (like Desktop Audio
        /// and Mic sources)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetSpecialSources()
        {
            var response = SendRequest("GetSpecialSources");
            var sources = new Dictionary<string, string>();
            foreach (KeyValuePair<string, JToken> kvp in response)
            {
                string key = kvp.Key;
                string value = (string)kvp.Value;
                if (key != "request-type" && key != "message-id" && key != "status")
                {
                    sources.Add(key, value);
                }
            }
            return sources;
        }

        /// <summary>
        /// Set current streaming settings
        /// </summary>
        /// <param name="service">Service settings</param>
        /// <param name="save">Save to disk</param>
        public void SetStreamingSettings(StreamingService service, bool save)
        {
            var requestFields = new JObject
            {
                { "type", service.Type },
                { "settings", JToken.FromObject(service.Settings) },
                { "save", save }
            };

            SendRequest("SetStreamSettings", requestFields);
        }

        /// <summary>
        /// Get current streaming settings
        /// </summary>
        /// <returns></returns>
        public StreamingService GetStreamSettings()
        {
            var response = SendRequest("GetStreamSettings");

            return JsonConvert.DeserializeObject<StreamingService>(response.ToString());
        }

        /// <summary>
        /// Set current streaming settings
        /// </summary>
        /// <param name="service">Service settings</param>
        /// <param name="save">Save to disk</param>
        public void SetStreamSettings(StreamingService service, bool save)
        {
            SetStreamingSettings(service, save);
        }

        /// <summary>
        /// Save current Streaming settings to disk
        /// </summary>
        public void SaveStreamSettings()
        {
            SendRequest("SaveStreamSettings");
        }

        /// <summary>
        /// Get settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
        /// <returns>BrowserSource properties</returns>
        [Obsolete("Deprecated! Please use GetSourceSettings(). Will be removed in v5.0")]
        public BrowserSourceProperties GetBrowserSourceProperties(string sourceName, string sceneName = null)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            if (sceneName != null)
            {
                request.Add("scene-name", sourceName);
            }
            var response = SendRequest("GetSourceSettings", request);
            if (response[SOURCE_TYPE_JSON_FIELD].ToString() != SOURCE_TYPE_BROWSER_SOURCE)
            {
                throw new Exception($"Invalid source_type. Expected: {SOURCE_TYPE_BROWSER_SOURCE} Received: {response[SOURCE_TYPE_JSON_FIELD]}");
            }

            return new BrowserSourceProperties(response);
        }

        /// <summary>
        /// Set settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="props">BrowserSource properties</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
        [Obsolete("Deprecated! Please use SetSourceSettings(). Will be removed in v5.0")]
        public void SetBrowserSourceProperties(string sourceName, BrowserSourceProperties props, string sceneName = null)
        {
            props.Source = sourceName;
            var request = JObject.FromObject(props);
            if (sceneName != null)
            {
                request.Add("scene-name", sourceName);
            }

            SetSourceSettings(sourceName, request, SOURCE_TYPE_BROWSER_SOURCE);
        }

        /// <summary>
        /// Enable/disable the heartbeat event
        /// </summary>
        /// <param name="enable"></param>
        [Obsolete("Deprecated! Please pool the appropriate data using individual requests. Will be removed in v5.0")]
        public void SetHeartbeat(bool enable)
        {
            var request = new JObject
            {
                { "enable", enable }
            };

            SendRequest("SetHeartbeat", request);
        }

        /// <summary>
        /// Get the settings from a source item
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="sourceType">Type of the specified source. Useful for type-checking to avoid settings a set of settings incompatible with the actual source's type.</param>
        /// <returns>settings</returns>
        public SourceSettings GetSourceSettings(string sourceName, string sourceType = null)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };
            if (sourceType != null)
            {
                request.Add("sourceType", sourceType);
            }

            JObject result = SendRequest("GetSourceSettings", request);
            SourceSettings settings = new SourceSettings(result);
            return settings;
        }

        /// <summary>
        /// Set settings of the specified source.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="settings">Settings for the source</param>
        /// <param name="sourceType">Type of the specified source. Useful for type-checking to avoid settings a set of settings incompatible with the actual source's type.</param>
        public void SetSourceSettings(string sourceName, JObject settings, string sourceType = null)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "sourceSettings", settings }
            };
            if (sourceType != null)
            {
                request.Add("sourceType", sourceType);
            }

            SendRequest("SetSourceSettings", request);
        }

        /// <summary>
        /// Gets settings for a media source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public MediaSourceSettings GetMediaSourceSettings(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "sourceType", "ffmpeg_source" }
            };

            var response = SendRequest("GetSourceSettings", request);
            return response.ToObject<MediaSourceSettings>();
        }

        /// <summary>
        /// Sets settings of a media source
        /// </summary>
        /// <param name="sourceSettings"></param>
        public void SetMediaSourceSettings(MediaSourceSettings sourceSettings)
        {
            if (sourceSettings.SourceType != "ffmpeg_source")
            {
                throw new System.Exception("Invalid SourceType");
            }
            SendRequest("SetSourceSettings", JObject.FromObject(sourceSettings));
        }

        /// <summary>
        /// Open a projector window or create a projector on a monitor. Requires OBS v24.0.4 or newer.
        /// </summary>
        /// <param name="projectorType">Type of projector: "Preview" (default), "Source", "Scene", "StudioProgram", or "Multiview" (case insensitive)</param>
        /// <param name="monitor">Monitor to open the projector on. If -1 or omitted, opens a window</param>
        /// <param name="geometry">Size and position of the projector window (only if monitor is -1). Encoded in Base64 using Qt's geometry encoding. Corresponds to OBS's saved projectors</param>
        /// <param name="name">Name of the source or scene to be displayed (ignored for other projector types)</param>
        public void OpenProjector(string projectorType = "preview", int monitor = -1, string geometry = null, string name = null)
        {
            var request = new JObject
            {
                { "type", projectorType },
                { "monitor", monitor }
            };

            if (geometry != null)
            {
                request.Add("geometry", geometry);
            }

            if (name != null)
            {
                request.Add("name", name);
            }

            SendRequest("OpenProjector", request);
        }

        /// <summary>
        /// Renames a source.
        /// Note: If the new name already exists as a source, obs-websocket will return an error.
        /// </summary>
        /// <param name="currentName">Current source name</param>
        /// <param name="newName">New source name</param>
        public void SetSourceName(string currentName, string newName)
        {
            var request = new JObject
            {
                { "sourceName", currentName },
                { "newName", newName }
            };

            SendRequest("SetSourceName", request);
        }

        /// <summary>
        /// List existing outputs
        /// </summary>
        /// <returns>Array of OutputInfo</returns>
        public List<OBSOutputInfo> ListOutputs()
        {
            var response = SendRequest("ListOutputs");
            return response["outputs"].ToObject<List<OBSOutputInfo>>();
        }

        /// <summary>
        /// Get the audio's active status of a specified source.
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <returns>Audio active status of the source.</returns>
        public bool GetAudioActive(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = SendRequest("GetAudioActive", request);
            return (bool)response["audioActive"];
        }

        /// <summary>
        /// Get the audio monitoring type of the specified source.
        /// Valid return values: none, monitorOnly, monitorAndOutput
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>The monitor type in use</returns>
        public string GetAudioMonitorType(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = SendRequest("GetAudioMonitorType", request);
            return (string)response["monitorType"];
        }

        /// <summary>
        /// Set the audio monitoring type of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="monitorType">The monitor type to use. Options: none, monitorOnly, monitorAndOutput</param>
        public void SetAudioMonitorType(string sourceName, string monitorType)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "monitorType", monitorType }
            };

            SendRequest("SetAudioMonitorType", request);
        }

        /// <summary>
        /// Broadcast custom message to all connected WebSocket clients
        /// </summary>
        /// <param name="realm">Identifier to be choosen by the client</param>
        /// <param name="data">User-defined data</param>
        public void BroadcastCustomMessage(string realm, JObject data)
        {
            var request = new JObject
            {
                { "realm", realm },
                { "data", data }
            };

            SendRequest("BroadcastCustomMessage", request);
        }

        /// <summary>
        /// Refreshes the specified browser source.
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        public void RefreshBrowserSource(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            SendRequest("RefreshBrowserSource", request);
        }

        /// <summary>
        /// Pause or play a media source. Supports ffmpeg and vlc media sources (as of OBS v25.0.8) Note :Leaving out playPause toggles the current pause state
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="playPause">(optional) Whether to pause or play the source. false for play, true for pause.</param>
        public void PlayPauseMedia(string sourceName, bool? playPause)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            if (playPause.HasValue)
            {
                request.Add("playPause", playPause.Value);
            }

            SendRequest("PlayPauseMedia", request);
        }

        /// <summary>
        /// Restart a media source. Supports ffmpeg and vlc media sources (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        public void RestartMedia(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            SendRequest("RestartMedia", request);
        }

        /// <summary>
        /// Stop a media source. Supports ffmpeg and vlc media sources (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        public void StopMedia(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            SendRequest("StopMedia", request);
        }

        /// <summary>
        /// Skip to the next media item in the playlist. Supports only vlc media source (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        public void NextMedia(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            SendRequest("NextMedia", request);
        }

        /// <summary>
        /// Go to the previous media item in the playlist. Supports only vlc media source (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        public void PreviousMedia(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            SendRequest("PreviousMedia", request);
        }

        /// <summary>
        /// Get the length of media in milliseconds. Supports ffmpeg and vlc media sources (as of OBS v25.0.8) Note: For some reason, for the first 5 or so seconds that the media is playing, the total duration can be off by upwards of 50ms.
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <returns>The total length of media in milliseconds.</returns>
        public int GetMediaDuration(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = SendRequest("GetMediaDuration", request);
            return (int)response["mediaDuration"];
        }

        /// <summary>
        /// Get the current timestamp of media in milliseconds. Supports ffmpeg and vlc media sources (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <returns>The time in milliseconds since the start of the media.</returns>
        public int GetMediaTime(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = SendRequest("GetMediaTime", request);
            return (int)response["timestamp"];
        }

        /// <summary>
        /// Set the timestamp of a media source. Supports ffmpeg and vlc media sources (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <param name="timestamp">Milliseconds to set the timestamp to.</param>
        public void SetMediaTime(string sourceName, int timestamp)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "timestamp", timestamp }
            };

            SendRequest("SetMediaTime", request);
        }

        /// <summary>
        /// Scrub media using a supplied offset. Supports ffmpeg and vlc media sources (as of OBS v25.0.8) Note: Due to processing/network delays, this request is not perfect. The processing rate of this request has also not been tested.
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <param name="timeOffset">Millisecond offset (positive or negative) to offset the current media position.</param>
        public void ScrubMedia(string sourceName, int timeOffset)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "timeOffset", timeOffset }
            };

            SendRequest("ScrubMedia", request);
        }

        /// <summary>
        /// Get the current playing state of a media source. Supports ffmpeg and vlc media sources (as of OBS v25.0.8)
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <returns>The media state of the provided source.</returns>
        public MediaState GetMediaState(string sourceName)
        {
           var request = new JObject
            {
                { "sourceName", sourceName }
            }; 

            var response = SendRequest("GetMediaState", request);
            return (MediaState)Enum.Parse(typeof(MediaState), (string)response["mediaState"]);
        }

        /// <summary>
        /// List the media state of all media sources (vlc and media source)
        /// </summary>
        /// <returns>Array of sources</returns>
        public IEnumerable<MediaSource> GetMediaSourcesList()
        {
            var result = new List<MediaSource>();

            var response = SendRequest("GetMediaSourcesList");
            return response["mediaSources"].Select(m => new MediaSource((JObject)m));
        }

        /// <summary>
        /// Create a source and add it as a sceneitem to a scene.
        /// </summary>
        /// <param name="sourceName">Source name.</param>
        /// <param name="sourceKind">Source kind, Eg. vlc_source</param>
        /// <param name="sceneName">Scene to add the new source to.</param>
        /// <param name="sourceSettings">Source settings data.</param>
        /// <param name="setVisible">Set the created SceneItem as visible or not. Defaults to true</param>
        /// <returns>ID of the SceneItem in the scene.</returns>
        public int CreateSource(string sourceName, string sourceKind, string sceneName, JObject sourceSettings, bool? setVisible)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "sourceKind", sourceKind },
                { "sceneName", sceneName }
            };

            if (sourceSettings != null)
            {
                request.Add("sourceSettings", sourceSettings);
            }

            if (setVisible.HasValue)
            {
                request.Add("setVisible", setVisible.Value);
            }

            var response = SendRequest("CreateSource", request);
            return (int)response["itemId"];
        }

        /// <summary>
        /// Get the default settings for a given source type.
        /// </summary>
        /// <param name="sourceKind">Source kind. Also called "source id" in libobs terminology.</param>
        /// <returns>Settings object for source.</returns>
        public JObject GetSourceDefaultSettings(string sourceKind)
        {
            var request = new JObject
            {
                { "sourceKind", sourceKind }
            };

            var response = SendRequest("GetSourceDefaultSettings", request);
            return (JObject)response["defaultSettings"];
        }

        /// <summary>
        /// Get a list of all scene items in a scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to get the list of scene items from. Defaults to the current scene if not specified.</param>
        public IEnumerable<SceneItemDetails> GetSceneItemList(string sceneName)
        {
            JObject request = null;
            if (!string.IsNullOrEmpty(sceneName))
            {
                request = new JObject
                {
                    { "sceneName", sceneName }
                };
            }

            var response = SendRequest("GetSceneItemList", request);
            return response["sceneItems"].Select(m => new SceneItemDetails((JObject)m));
        }

        /// <summary>
        /// Creates a scene item in a scene. In other words, this is how you add a source into a scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to create the scene item in</param>
        /// <param name="sourceName">Name of the source to be added</param>
        /// <param name="setVisible">Whether to make the scene item visible on creation or not. Default true</param>
        /// <returns>Numerical ID of the created scene item</returns>
        public int AddSceneItem(string sceneName, string sourceName, bool setVisible = true)
        {
            var request = new JObject
            {
                { "sceneName", sceneName },
                { "sourceName", sourceName },
                { "setVisible", setVisible }
            };

            var response = SendRequest("AddSceneItem", request);
            return (int)response["itemId"];
        }

        /// <summary>
        /// Create a new scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to create.</param>
        public void CreateScene(string sceneName)
        {
            var request = new JObject
            {
                { "sceneName", sceneName }
            };

            SendRequest("CreateScene", request);
        }

        /// <summary>
        /// Gets whether an audio track is active for a source.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>Indication for each track whther it's active or not</returns>
        public SourceTracks GetAudioTracks(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = SendRequest("GetAudioTracks", request);
            return new SourceTracks(response);
        }

        /// <summary>
        /// Sets whether an audio track is active for a source.
        /// </summary>
        /// <param name="sourceName">Source Name</param>
        /// <param name="trackNum">Audio tracks 1-6</param>
        /// <param name="isActive">Whether audio track is active or not</param>
        /// <returns></returns>
        public void SetAudioTrack(string sourceName, int trackNum, bool isActive)
        {
            var request = new JObject
            {
                { "sourceName", sourceName },
                { "track", trackNum },
                { "active", isActive },
            };

            SendRequest("SetAudioTracks", request);
        }

        /// <summary>
        /// Get the source's active status of a specified source (if it is showing in the final mix).
        /// </summary>
        /// <param name="sourceName">Source Name</param>
        /// <returns>Active status of the source</returns>
        public bool GetSourceActive(string sourceName)
        {
            var request = new JObject
            {
                { "sourceName", sourceName }
            };

            var response = SendRequest("GetSourceActive", request);
            return (bool)response["sourceActive"];
        }

        /// <summary>
        /// Get the current status of the virtual camera
        /// </summary>
        /// <returns>An <see cref="VirtualCamStatus"/> object describing the current virtual camera state</returns>
        public VirtualCamStatus GetVirtualCamStatus()
        {
            JObject response = SendRequest("GetVirtualCamStatus");
            var outputStatus = new VirtualCamStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// Start virtual camera. Will trigger an error if virtual camera is already started.
        /// </summary>
        public void StartVirtualCam()
        {
            SendRequest("StartVirtualCam");
        }

        /// <summary>
        /// Stop virtual camera. Will trigger an error if virtual camera is already stopped.
        /// </summary>
        public void StopVirtualCam()
        {
            SendRequest("StopVirtualCam");
        }

        /// <summary>
        /// Toggle virtual camera on or off (depending on the current virtual camera state).
        /// </summary>
        public void ToggleVirtualCam()
        {
            SendRequest("StartStopVirtualCam");
        }
    }
}