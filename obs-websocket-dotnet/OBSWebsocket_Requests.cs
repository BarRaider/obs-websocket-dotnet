/*
    The MIT License (MIT)

    Copyright (c) 2017 Stéphane Lepin

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Instance of a connection with an obs-websocket server
    /// </summary>
    public partial class OBSWebsocket
    {
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
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            if (embedPictureFormat != null)
            requestFields.Add("embedPictureFormat", embedPictureFormat);
            if (saveToFilePath != null)
                requestFields.Add("saveToFilePath", saveToFilePath);
            if (width > -1)
            requestFields.Add("height", width);
            if (height > -1)
                requestFields.Add("height", height);

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
            return TakeSourceScreenshot(sourceName, embedPictureFormat, saveToFilePath);
        }

        /// <summary>
        /// At least embedPictureFormat or saveToFilePath must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        public SourceScreenshotResponse TakeSourceScreenshot(string sourceName)
        {
            return TakeSourceScreenshot(sourceName);
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
            var requestFields = new JObject();
            requestFields.Add("scene-name", sceneName);

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
                requestFields.Add("scene", sceneName);

            var items = JObject.Parse(JsonConvert.SerializeObject(sceneItems));
            requestFields.Add("items", items);

            SendRequest("ReorderSceneItems", requestFields);
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
            var requestFields = new JObject();
            requestFields.Add("item", itemName);
            requestFields.Add("visible", visible);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            SendRequest("SetSceneItemProperties", requestFields);
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        public SceneItemProperties GetSceneItemProperties(string itemName, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            JObject response = SendRequest("GetSceneItemProperties", requestFields);
            return JsonConvert.DeserializeObject<SceneItemProperties>(response.ToString());
        }

        /// <summary>
        /// Get the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="sourceName">The name of the source</param>
        public TextGDIPlusProperties GetTextGDIPlusProperties(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

            JObject response = SendRequest("GetTextGDIPlusProperties", requestFields);
            return JsonConvert.DeserializeObject<TextGDIPlusProperties>(response.ToString());
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
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("movementType", movement.ToString().ToLower());

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
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("newIndex", newIndex);

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
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("filterSettings", filterSettings);

            SendRequest("SetSourceFilterSettings", requestFields);
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName"></param>
        public List<FilterSettings> GetSourceFilters(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);

            JObject response = SendRequest("GetSourceFilters", requestFields);

            return JsonConvert.DeserializeObject<List<FilterSettings>>(response["filters"].ToString());
        }

        /// <summary>
        /// Remove the filter from a source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="filterName"></param>
        public bool RemoveFilterFromSource(string sourceName, string filterName)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
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
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterType", filterType);
            requestFields.Add("filterName", filterName);
            requestFields.Add("filterSettings", filterSettings);

            var result = SendRequest("AddFilterToSource", requestFields);
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
            var requestFields = new JObject();
            requestFields.Add("transition-name", transitionName);

            SendRequest("SetCurrentTransition", requestFields);
        }

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="duration">Desired transition duration (in milliseconds)</param>
        public void SetTransitionDuration(int duration)
        {
            var requestFields = new JObject();
            requestFields.Add("duration", duration);

            SendRequest("SetTransitionDuration", requestFields);
        }

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which volume will be changed</param>
        /// <param name="volume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul (useDecibel is false), and under 0.0 for dB (useDecibel is true). Note: OBS will interpret dB values under -100.0 as Inf.</param>
        /// <param name="useDecibel">Interperet `volume` data as decibels instead of amplitude/mul.</param>
        public void SetVolume(string sourceName, float volume, bool useDecibel = false)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("volume", volume);
            requestFields.Add("useDecibel", useDecibel);

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
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("useDecibel", useDecibel);

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
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("mute", mute);

            SendRequest("SetMute", requestFields);
        }

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be toggled</param>
        public void ToggleMute(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

            SendRequest("ToggleMute", requestFields);
        }

        /// <summary>
        /// Set the position of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which position will be changed</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        public void SetSceneItemPosition(string itemName, float x, float y, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);
            requestFields.Add("x", x);
            requestFields.Add("y", y);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

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
        public void SetSceneItemTransform(string itemName, float rotation = 0, float xScale = 1, float yScale = 1, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);
            requestFields.Add("x-scale", xScale);
            requestFields.Add("y-scale", yScale);
            requestFields.Add("rotation", rotation);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            SendRequest("SetSceneItemTransform", requestFields);
        }

        /// <summary>
        /// Sets the scene specific properties of a source. Unspecified properties will remain unchanged. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="props">Object containing changes</param>
        /// <param name="sceneName">Option scene name</param>
        public void SetSceneItemProperties(SceneItemProperties props, string sceneName = null)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(props, settings));

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            SendRequest("SetSceneItemProperties", requestFields);
        }

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="scName">Desired scene collection name</param>
        public void SetCurrentSceneCollection(string scName)
        {
            var requestFields = new JObject();
            requestFields.Add("sc-name", scName);

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
            var requestFields = new JObject();
            requestFields.Add("profile-name", profileName);

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
        /// Change the current recording folder
        /// </summary>
        /// <param name="recFolder">Recording folder path</param>
        public void SetRecordingFolder(string recFolder)
        {
            var requestFields = new JObject();
            requestFields.Add("rec-folder", recFolder);
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
            var requestFields = new JObject();
            requestFields.Add("scene-name", previewScene);
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
                    withTransition.Add("duration");

                if (transitionName != null)
                    withTransition.Add("name", transitionName);

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
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

            var response = SendRequest("GetMute");
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
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("offset", syncOffset);
            SendRequest("SetSyncOffset", requestFields);
        }

        /// <summary>
        /// Get the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>Audio offset (in nanoseconds) of the specified source</returns>
        public int GetSyncOffset(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
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
                requestFields.Add("scene-name", sceneName);

            JObject minReqs = new JObject();
            if (sceneItem.SourceName != null)
                minReqs.Add("name", sceneItem.SourceName);

            minReqs.Add("id", sceneItem.ID);

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
                requestFields.Add("scene-name", sceneName);

            JObject minReqs = new JObject();

            minReqs.Add("id", sceneItemId);

            requestFields.Add("item", minReqs);

            SendRequest("DeleteSceneItem", requestFields);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItemName">Name of the scene item</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="sceneName">(optional) parent scene name of the specified source</param>
        public void SetSceneItemCrop(string sceneItemName,
            SceneItemCropInfo cropInfo, string sceneName = null)
        {
            var requestFields = new JObject();

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            requestFields.Add("item", sceneItemName);
            requestFields.Add("top", cropInfo.Top);
            requestFields.Add("bottom", cropInfo.Bottom);
            requestFields.Add("left", cropInfo.Left);
            requestFields.Add("right", cropInfo.Right);

            SendRequest("SetSceneItemCrop", requestFields);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItem">Scene item object</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="scene">Parent scene of scene item</param>
        public void SetSceneItemCrop(SceneItem sceneItem,
            SceneItemCropInfo cropInfo, OBSScene scene)
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
            var requestFields = new JObject();
            requestFields.Add("item", itemName);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            SendRequest("ResetSceneItem", requestFields);
        }

        /// <summary>
        /// Send the provided text as embedded CEA-608 caption data. As of OBS Studio 23.1, captions are not yet available on Linux.
        /// </summary>
        /// <param name="text">Captions text</param>
        public void SendCaptions(string text)
        {
            var requestFields = new JObject();
            requestFields.Add("text", text);

            SendRequest("SendCaptions", requestFields);
        }

        /// <summary>
        /// Set the filename formatting string
        /// </summary>
        /// <param name="filenameFormatting">Filename formatting string to set</param>
        public void SetFilenameFormatting(string filenameFormatting)
        {
            var requestFields = new JObject();
            requestFields.Add("filename-formatting", filenameFormatting);

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
            var requestFields = new JObject();

            requestFields.Add("fromScene", fromSceneName);
            requestFields.Add("toScene", toSceneName);

            JObject minReqs = new JObject();
            if (sceneItem.SourceName != null)
                minReqs.Add("name", sceneItem.SourceName);

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
            var requestFields = new JObject();

            requestFields.Add("fromScene", fromSceneName);
            requestFields.Add("toScene", toSceneName);

            JObject minReqs = new JObject();
            minReqs.Add("id", sceneItemID);

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
            foreach (KeyValuePair<string, JToken> x in response)
            {
                string key = x.Key;
                string value = (string)x.Value;
                if (key != "request-type" && key != "message-id")
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
            var jsonSettings = JsonConvert.SerializeObject(service.Settings);

            var requestFields = new JObject();
            requestFields.Add("type", service.Type);
            requestFields.Add("settings", jsonSettings);
            requestFields.Add("save", save);
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
        public BrowserSourceProperties GetBrowserSourceProperties(string sourceName, string sceneName = null)
        {
            var request = new JObject();
            request.Add("source", sourceName);
            if (sceneName != null)
                request.Add("scene-name", sourceName);

            var response = SendRequest("GetBrowserSourceProperties", request);
            return new BrowserSourceProperties(response);
        }

        /// <summary>
        /// Set settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="props">BrowserSource properties</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
        public void SetBrowserSourceProperties(string sourceName, BrowserSourceProperties props, string sceneName = null)
        {
            //override sourcename in props with the name passed
            props.Source = sourceName;
            var request = new JObject();
            var jsonString = JsonConvert.SerializeObject(request);
            JsonConvert.PopulateObject(jsonString, request);
            SendRequest("SetBrowserSourceProperties", request);
        }

        /// <summary>
        /// Enable/disable the heartbeat event
        /// </summary>
        /// <param name="enable"></param>
        public void SetHeartbeat(bool enable)
        {
            var request = new JObject();
            request.Add("enable", enable);

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
            var request = new JObject();
            request.Add("sourceName", sourceName);
            if (sourceType != null)
                request.Add("sourceType", sourceType);

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
            var request = new JObject();
            request.Add("sourceName", sourceName);
            request.Add("sourceSettings", settings);
            if (sourceType != null)
                request.Add("sourceType", sourceType);

            SendRequest("SetSourceSettings", request);
        }
    }
}
