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
using obs_websocket_dotnet.Types;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<OBSVideoInfo> GetVideoInfo()
        {
            JObject response = await SendRequest("GetVideoInfo").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<OBSVideoInfo>(response.ToString());
        }

        public async Task<Output[]> ListOutputs()
        {
            JObject response = await SendRequest("ListOutputs").ConfigureAwait(false);
            JObject[] jOutputs = response["outputs"]?.Children<JObject>().ToArray();
            int outputCount = jOutputs?.Length ?? 0;
            if (outputCount == 0)
                return Array.Empty<Output>();
            Output[] outputs = new Output[outputCount];

            for (int i = 0; i < outputCount; i++)
            {
                try
                {
                    outputs[i] = Output.CreateOutput(jOutputs[i]);
                }
                catch (Exception ex)
                {
                    OBSLogger.Error(ex);
                }
            }
            return outputs;
        }

        public async Task<Output> GetOutput(string outputName)
        {
            var requestFields = new JObject();
            requestFields.Add("outputName", outputName);
            JObject response = await SendRequest("GetOutputInfo", requestFields).ConfigureAwait(false);

            return Output.CreateOutput(response["outputInfo"] as JObject);
        }

        public Task StartOutput(string outputName)
        {
            var requestFields = new JObject();
            requestFields.Add("outputName", outputName);

            return SendRequest("StartOutput", requestFields);
        }

        public Task StopOutput(string outputName, bool force = false)
        {
            var requestFields = new JObject();
            requestFields.Add("outputName", outputName);
            requestFields.Add("force", force);

            return SendRequest("StopOutput", requestFields);
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
        public async Task<SourceScreenshotResponse> TakeSourceScreenshot(string sourceName, string embedPictureFormat = null, string saveToFilePath = null, int width = -1, int height = -1)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
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

            var response = await SendRequest("TakeSourceScreenshot", requestFields).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<SourceScreenshotResponse>(response.ToString());
        }

        /// <summary>
        /// At least embedPictureFormat or saveToFilePath must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="embedPictureFormat">Format of the Data URI encoded picture. Can be "png", "jpg", "jpeg" or "bmp" (or any other value supported by Qt's Image module)</param>
        /// <param name="saveToFilePath">Full file path (file extension included) where the captured image is to be saved. Can be in a format different from pictureFormat. Can be a relative path.</param>
        public Task<SourceScreenshotResponse> TakeSourceScreenshot(string sourceName, string embedPictureFormat = null, string saveToFilePath = null)
        {
            return TakeSourceScreenshot(sourceName, embedPictureFormat, saveToFilePath, -1, -1);
        }

        /// <summary>
        /// At least embedPictureFormat or saveToFilePath must be specified.
        /// Clients can specify width and height parameters to receive scaled pictures. Aspect ratio is preserved if only one of these two parameters is specified.
        /// </summary>
        /// <param name="sourceName"></param>
        public Task<SourceScreenshotResponse> TakeSourceScreenshot(string sourceName)
        {
            return TakeSourceScreenshot(sourceName, null, null);
        }

        /// <summary>
        /// Get the current scene info along with its items
        /// </summary>
        /// <returns>An <see cref="OBSScene"/> object describing the current scene</returns>
        public async Task<OBSScene> GetCurrentScene()
        {
            JObject response = await SendRequest("GetCurrentScene").ConfigureAwait(false);
            return new OBSScene(response);
        }

        /// <summary>
        /// Set the current scene to the specified one
        /// </summary>
        /// <param name="sceneName">The desired scene name</param>
        public async Task SetCurrentScene(string sceneName)
        {
            var requestFields = new JObject();
            requestFields.Add("scene-name", sceneName);

            await SendRequest("SetCurrentScene", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the filename formatting string
        /// </summary>
        /// <returns>Current filename formatting string</returns>
        public async Task<string> GetFilenameFormatting()
        {
            JObject response = await SendRequest("GetFilenameFormatting").ConfigureAwait(false);
            return (string)response["filename-formatting"];
        }

        /// <summary>
        /// Get OBS stats (almost the same info as provided in OBS' stats window)
        /// </summary>
        public async Task<OBSStats> GetStats()
        {
            JObject response = await SendRequest("GetStats").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<OBSStats>(response["stats"].ToString());
        }

        /// <summary>
        /// List every available scene
        /// </summary>
        /// <returns>A <see cref="List{OBSScene}" /> of <see cref="OBSScene"/> objects describing each scene</returns>
        public async Task<List<OBSScene>> ListScenes()
        {
            var response = await GetSceneList().ConfigureAwait(false);
            return response.Scenes;
        }

        /// <summary>
        /// Get a list of scenes in the currently active profile
        /// </summary>
        public async Task<GetSceneListInfo> GetSceneList()
        {
            JObject response = await SendRequest("GetSceneList").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GetSceneListInfo>(response.ToString());
        }

        /// <summary>
        /// Changes the order of scene items in the requested scene
        /// </summary>
        /// <param name="sceneName">Name of the scene to reorder (defaults to current)</param>
        /// <param name="sceneItems">List of items to reorder, only ID or Name required</param>
        public async Task ReorderSceneItems(List<SceneItemStub> sceneItems, string sceneName = null)
        {
            var requestFields = new JObject();
            if (sceneName != null)
                requestFields.Add("scene", sceneName);

            var items = JObject.Parse(JsonConvert.SerializeObject(sceneItems));
            requestFields.Add("items", items);

            await SendRequest("ReorderSceneItems", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// List all sources available in the running OBS instance
        /// </summary>
        public async Task<List<SourceInfo>> GetSourcesList()
        {
            JObject response = await SendRequest("GetSourcesList").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<SourceInfo>>(response["sources"].ToString());
        }

        /// <summary>
        /// List all sources available in the running OBS instance
        /// </summary>
        public async Task<List<SourceType>> GetSourceTypesList()
        {
            JObject response = await SendRequest("GetSourceTypesList").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<SourceType>>(response["types"].ToString());
        }

        /// <summary>
        /// Change the visibility of the specified scene item
        /// </summary>
        /// <param name="itemName">Scene item which visiblity will be changed</param>
        /// <param name="visible">Desired visiblity</param>
        /// <param name="sceneName">Scene name of the specified item</param>
        public async Task SetSourceRender(string itemName, bool visible, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);
            requestFields.Add("visible", visible);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            await SendRequest("SetSceneItemProperties", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        public async Task<SceneItemProperties> GetSceneItemProperties(string itemName, string sceneName = null)
        {
            var propertiesJson = await GetSceneItemPropertiesJson(itemName, sceneName).ConfigureAwait(false);
            return propertiesJson.ToObject<SceneItemProperties>();
        }

        /// <summary>
        /// Gets the scene specific properties of the specified source item. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// Response is a JObject
        /// </summary>
        /// <param name="itemName">The name of the source</param>
        /// <param name="sceneName">The name of the scene that the source item belongs to. Defaults to the current scene.</param>
        public async Task<JObject> GetSceneItemPropertiesJson(string itemName, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            return await SendRequest("GetSceneItemProperties", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="sourceName">The name of the source</param>
        public async Task<TextGDIPlusProperties> GetTextGDIPlusProperties(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

            JObject response = await SendRequest("GetTextGDIPlusProperties", requestFields).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TextGDIPlusProperties>(response.ToString());
        }

        /// <summary>
        /// Set the current properties of a Text GDI Plus source.
        /// </summary>
        /// <param name="properties">properties for the source</param>
        public async Task SetTextGDIPlusProperties(TextGDIPlusProperties properties)
        {
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(properties));

            await SendRequest("SetTextGDIPlusProperties", requestFields).ConfigureAwait(false);

        }



        /// <summary>
        /// Move a filter in the chain (relative positioning)
        /// </summary>
        /// <param name="sourceName">Scene Name</param>
        /// <param name="filterName">Filter Name</param>
        /// <param name="movement">Direction to move</param>
        public async Task MoveSourceFilter(string sourceName, string filterName, FilterMovementType movement)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("movementType", movement.ToString().ToLower());

            await SendRequest("MoveSourceFilter", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Move a filter in the chain (absolute index positioning)
        /// </summary>
        /// <param name="sourceName">Scene Name</param>
        /// <param name="filterName">Filter Name</param>
        /// <param name="newIndex">Desired position of the filter in the chain</param>
        public async Task ReorderSourceFilter(string sourceName, string filterName, int newIndex)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("newIndex", newIndex);

            await SendRequest("ReorderSourceFilter", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Apply settings to a source filter
        /// </summary>
        /// <param name="sourceName">Source with filter</param>
        /// <param name="filterName">Filter name</param>
        /// <param name="filterSettings">Filter settings</param>
        public async Task SetSourceFilterSettings(string sourceName, string filterName, JObject filterSettings)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("filterSettings", filterSettings);

            await SendRequest("SetSourceFilterSettings", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify the Source Filter's visibility
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="filterName"></param>
        /// <param name="filterEnabled"></param>
        public async Task SetSourceFilterVisibility(string sourceName, string filterName, bool filterEnabled)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            requestFields.Add("filterEnabled", filterEnabled);

            await SendRequest("SetSourceFilterVisibility", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Return a list of all filters on a source
        /// </summary>
        /// <param name="sourceName"></param>
        public async Task<List<FilterSettings>> GetSourceFilters(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);

            JObject response = await SendRequest("GetSourceFilters", requestFields).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<FilterSettings>>(response["filters"].ToString());
        }

        /// <summary>
        /// Remove the filter from a source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="filterName"></param>
        public async Task<bool> RemoveFilterFromSource(string sourceName, string filterName)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterName", filterName);
            try
            {
                await SendRequest("RemoveFilterFromSource", requestFields).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                //TODO exception handling
                OBSLogger.Error(e.Message);
                OBSLogger.Debug(e);
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
        public async Task AddFilterToSource(string sourceName, string filterName, string filterType, JObject filterSettings)
        {
            var requestFields = new JObject();
            requestFields.Add("sourceName", sourceName);
            requestFields.Add("filterType", filterType);
            requestFields.Add("filterName", filterName);
            requestFields.Add("filterSettings", filterSettings);

            var result = await SendRequest("AddFilterToSource", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Start/Stop the streaming output
        /// </summary>
        public async Task ToggleStreaming()
        {
            await SendRequest("StartStopStreaming").ConfigureAwait(false);
        }

        /// <summary>
        /// Start/Stop the recording output
        /// </summary>
        public async Task ToggleRecording()
        {
            await SendRequest("StartStopRecording").ConfigureAwait(false);
        }

        /// <summary>
        /// Get the current status of the streaming and recording outputs
        /// </summary>
        /// <returns>An <see cref="OutputStatus"/> object describing the current outputs states</returns>
        public async Task<OutputStatus> GetStreamingStatus()
        {
            JObject response = await SendRequest("GetStreamingStatus").ConfigureAwait(false);
            var outputStatus = new OutputStatus(response);
            return outputStatus;
        }

        /// <summary>
        /// List all transitions
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all transition names</returns>
        public async Task<List<string>> ListTransitions()
        {
            var transitions = await GetTransitionList().ConfigureAwait(false);

            List<string> transitionNames = new List<string>();
            foreach (var item in transitions.Transitions)
                transitionNames.Add(item.Name);


            return transitionNames;
        }

        /// <summary>
        /// Get the current transition name and duration
        /// </summary>
        /// <returns>An <see cref="TransitionSettings"/> object with the current transition name and duration</returns>
        public async Task<TransitionSettings> GetCurrentTransition()
        {
            JObject respBody = await SendRequest("GetCurrentTransition").ConfigureAwait(false);
            return new TransitionSettings(respBody);
        }

        /// <summary>
        /// Set the current transition to the specified one
        /// </summary>
        /// <param name="transitionName">Desired transition name</param>
        public async Task SetCurrentTransition(string transitionName)
        {
            var requestFields = new JObject();
            requestFields.Add("transition-name", transitionName);

            await SendRequest("SetCurrentTransition", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="duration">Desired transition duration (in milliseconds)</param>
        public async Task SetTransitionDuration(int duration)
        {
            var requestFields = new JObject();
            requestFields.Add("duration", duration);

            await SendRequest("SetTransitionDuration", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which volume will be changed</param>
        /// <param name="volume">Desired volume. Must be between `0.0` and `1.0` for amplitude/mul (useDecibel is false), and under 0.0 for dB (useDecibel is true). Note: OBS will interpret dB values under -100.0 as Inf.</param>
        /// <param name="useDecibel">Interperet `volume` data as decibels instead of amplitude/mul.</param>
        public async Task SetVolume(string sourceName, float volume, bool useDecibel = false)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("volume", volume);
            requestFields.Add("useDecibel", useDecibel);

            await SendRequest("SetVolume", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the volume of the specified source
        /// Volume is between `0.0` and `1.0` if using amplitude/mul (useDecibel is false), under `0.0` if using dB (useDecibel is true).
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="useDecibel">Output volume in decibels of attenuation instead of amplitude/mul.</param>
        /// <returns>An <see cref="VolumeInfo"/>Object containing the volume and mute state of the specified source.</returns>
        public async Task<VolumeInfo> GetVolume(string sourceName, bool useDecibel = false)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("useDecibel", useDecibel);

            var response = await SendRequest("GetVolume", requestFields).ConfigureAwait(false);
            return new VolumeInfo(response);
        }

        /// <summary>
        /// Set the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be changed</param>
        /// <param name="mute">Desired mute state</param>
        public async Task SetMute(string sourceName, bool mute)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("mute", mute);

            await SendRequest("SetMute", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be toggled</param>
        public async Task ToggleMute(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

            await SendRequest("ToggleMute", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the position of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which position will be changed</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        public async Task SetSceneItemPosition(string itemName, float x, float y, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);
            requestFields.Add("x", x);
            requestFields.Add("y", y);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            await SendRequest("SetSceneItemPosition", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the scale and rotation of the specified scene item
        /// </summary>
        /// <param name="itemName">Name of the scene item which transform will be changed</param>
        /// <param name="rotation">Rotation in Degrees</param>
        /// <param name="xScale">Horizontal scale factor</param>
        /// <param name="yScale">Vertical scale factor</param>
        /// <param name="sceneName">(optional) name of the scene the item belongs to</param>
        public async Task SetSceneItemTransform(string itemName, float rotation = 0, float xScale = 1, float yScale = 1, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);
            requestFields.Add("x-scale", xScale);
            requestFields.Add("y-scale", yScale);
            requestFields.Add("rotation", rotation);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            await SendRequest("SetSceneItemTransform", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the scene specific properties of a source. Unspecified properties will remain unchanged. Coordinates are relative to the item's parent (the scene or group it belongs to).
        /// </summary>
        /// <param name="props">Object containing changes</param>
        /// <param name="sceneName">Option scene name</param>
        public async Task SetSceneItemProperties(SceneItemProperties props, string sceneName = null)
        {
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(props, DefaultSerializerSettings));

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            await SendRequest("SetSceneItemProperties", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sceneName"></param>
        public async Task SetSceneItemProperties(JObject obj, string sceneName = null)
        {
            // Serialize object to SceneItemProperties (needed before proper deserialization)
            var props = JsonConvert.DeserializeObject<SceneItemProperties>(obj.ToString(), DefaultSerializerSettings);

            // Deserialize object
            var requestFields = JObject.Parse(JsonConvert.SerializeObject(props, DefaultSerializerSettings));

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            await SendRequest("SetSceneItemProperties", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="scName">Desired scene collection name</param>
        public async Task SetCurrentSceneCollection(string scName)
        {
            var requestFields = new JObject();
            requestFields.Add("sc-name", scName);

            await SendRequest("SetCurrentSceneCollection", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the name of the current scene collection
        /// </summary>
        /// <returns>Name of the current scene collection</returns>
        public async Task<string> GetCurrentSceneCollection()
        {
            var response = await SendRequest("GetCurrentSceneCollection").ConfigureAwait(false);
            return (string)response["sc-name"];
        }

        /// <summary>
        /// List all scene collections
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all scene collections</returns>
        public async Task<List<string>> ListSceneCollections()
        {
            var response = await SendRequest("ListSceneCollections").ConfigureAwait(false);
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
        public async Task SetCurrentProfile(string profileName)
        {
            var requestFields = new JObject();
            requestFields.Add("profile-name", profileName);

            await SendRequest("SetCurrentProfile", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the name of the current profile
        /// </summary>
        /// <returns>Name of the current profile</returns>
        public async Task<string> GetCurrentProfile()
        {
            var response = await SendRequest("GetCurrentProfile").ConfigureAwait(false);
            return (string)response["profile-name"];
        }

        /// <summary>
        /// List all profiles
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of the names of all profiles</returns>
        public async Task<List<string>> ListProfiles()
        {
            var response = await SendRequest("ListProfiles").ConfigureAwait(false);
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
        public async Task StartStreaming()
        {
            await SendRequest("StartStreaming").ConfigureAwait(false);
        }

        /// <summary>
        /// Stop streaming. Will trigger an error if streaming is not active.
        /// </summary>
        public async Task StopStreaming()
        {
            await SendRequest("StopStreaming").ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle Streaming
        /// </summary>
        public async Task StartStopStreaming()
        {
            await SendRequest("StartStopStreaming").ConfigureAwait(false);
        }

        /// <summary>
        /// Start recording. Will trigger an error if recording is already active.
        /// </summary>
        public async Task StartRecording()
        {
            await SendRequest("StartRecording").ConfigureAwait(false);
        }

        /// <summary>
        /// Stop recording. Will trigger an error if recording is not active.
        /// </summary>
        public async Task StopRecording()
        {
            await SendRequest("StopRecording").ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle recording
        /// </summary>
        public async Task StartStopRecording()
        {
            await SendRequest("StartStopRecording").ConfigureAwait(false);
        }

        /// <summary>
        /// Change the current recording folder
        /// </summary>
        /// <param name="recFolder">Recording folder path</param>
        public async Task SetRecordingFolder(string recFolder)
        {
            var requestFields = new JObject();
            requestFields.Add("rec-folder", recFolder);
            await SendRequest("SetRecordingFolder", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the path of the current recording folder
        /// </summary>
        /// <returns>Current recording folder path</returns>
        public async Task<string> GetRecordingFolder()
        {
            var response = await SendRequest("GetRecordingFolder").ConfigureAwait(false);
            return (string)response["rec-folder"];
        }

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <returns>Current transition duration (in milliseconds)</returns>
        public async Task<int> GetTransitionDuration()
        {
            var response = await SendRequest("GetTransitionDuration").ConfigureAwait(false);
            return (int)response["transition-duration"];
        }

        /// <summary>
        /// Get duration of the currently selected transition (if supported)
        /// </summary>
        /// <returns>Current transition duration (in milliseconds)</returns>
        public async Task<GetTransitionListInfo> GetTransitionList()
        {
            var response = await SendRequest("GetTransitionList").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<GetTransitionListInfo>(response.ToString());
        }

        /// <summary>
        /// Get status of Studio Mode
        /// </summary>
        /// <returns>Studio Mode status (on/off)</returns>
        public async Task<bool> StudioModeEnabled()
        {
            var response = await SendRequest("GetStudioModeStatus").ConfigureAwait(false);
            return (bool)response["studio-mode"];
        }

        /// <summary>
        /// Disable Studio Mode
        /// </summary>
        public async Task DisableStudioMode()
        {
            await SendRequest("DisableStudioMode").ConfigureAwait(false);
        }

        /// <summary>
        /// Enable Studio Mode
        /// </summary>
        public async Task EnableStudioMode()
        {
            await SendRequest("EnableStudioMode").ConfigureAwait(false);
        }

        /// <summary>
        /// Enable Studio Mode
        /// </summary>
        public async Task<bool> GetStudioModeStatus()
        {
            var response = await SendRequest("GetStudioModeStatus").ConfigureAwait(false);
            return (bool)response["studio-mode"];
        }

        /// <summary>
        /// Enable/disable Studio Mode
        /// </summary>
        /// <param name="enable">Desired Studio Mode status</param>
        public async Task SetStudioMode(bool enable)
        {
            if (enable)
                await EnableStudioMode().ConfigureAwait(false);
            else
                await DisableStudioMode().ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle Studio Mode status (on to off or off to on)
        /// </summary>
        public async Task ToggleStudioMode()
        {
            await SendRequest("ToggleStudioMode").ConfigureAwait(false);
        }

        /// <summary>
        /// Get the currently selected preview scene. Triggers an error
        /// if Studio Mode is disabled
        /// </summary>
        /// <returns>Preview scene object</returns>
        public async Task<OBSScene> GetPreviewScene()
        {
            var response = await SendRequest("GetPreviewScene").ConfigureAwait(false);
            return new OBSScene(response);
        }

        /// <summary>
        /// Change the currently active preview scene to the one specified.
        /// Triggers an error if Studio Mode is disabled
        /// </summary>
        /// <param name="previewScene">Preview scene name</param>
        public async Task SetPreviewScene(string previewScene)
        {
            var requestFields = new JObject();
            requestFields.Add("scene-name", previewScene);
            await SendRequest("SetPreviewScene", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Change the currently active preview scene to the one specified.
        /// Triggers an error if Studio Mode is disabled.
        /// </summary>
        /// <param name="previewScene">Preview scene object</param>
        public async Task SetPreviewScene(OBSScene previewScene)
        {
            await SetPreviewScene(previewScene.Name).ConfigureAwait(false);
        }

        /// <summary>
        /// Triggers a Studio Mode transition (preview scene to program)
        /// </summary>
        /// <param name="transitionDuration">(optional) Transition duration</param>
        /// <param name="transitionName">(optional) Name of transition to use</param>
        public async Task TransitionToProgram(int transitionDuration = -1, string transitionName = null)
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

            await SendRequest("TransitionToProgram", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get if the specified source is muted
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>Source mute status (on/off)</returns>
        public async Task<bool> GetMute(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

            var response = await SendRequest("GetMute").ConfigureAwait(false);
            return (bool)response["muted"];
        }

        /// <summary>
        /// Toggle the Replay Buffer on/off
        /// </summary>
        public async Task ToggleReplayBuffer()
        {
            await SendRequest("StartStopReplayBuffer").ConfigureAwait(false);
        }

        /// <summary>
        /// Start recording into the Replay Buffer. Triggers an error
        /// if the Replay Buffer is already active, or if the "Save Replay Buffer"
        /// hotkey is not set in OBS' settings
        /// </summary>
        public async Task StartReplayBuffer()
        {
            await SendRequest("StartReplayBuffer").ConfigureAwait(false);
        }

        /// <summary>
        /// Stop recording into the Replay Buffer. Triggers an error if the
        /// Replay Buffer is not active.
        /// </summary>
        public async Task StopReplayBuffer()
        {
            await SendRequest("StopReplayBuffer").ConfigureAwait(false);
        }

        /// <summary>
        /// Toggle replay buffer
        /// </summary>
        public async Task StartStopReplayBuffer()
        {
            await SendRequest("StartStopReplayBuffer").ConfigureAwait(false);
        }

        /// <summary>
        /// Save and flush the contents of the Replay Buffer to disk. Basically
        /// the same as triggering the "Save Replay Buffer" hotkey in OBS.
        /// Triggers an error if Replay Buffer is not active.
        /// </summary>
        public async Task SaveReplayBuffer()
        {
            await SendRequest("SaveReplayBuffer").ConfigureAwait(false);
        }

        /// <summary>
        /// Set the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="syncOffset">Audio offset (in nanoseconds) for the specified source</param>
        public async Task SetSyncOffset(string sourceName, int syncOffset)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("offset", syncOffset);
            await SendRequest("SetSyncOffset", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the audio sync offset of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>Audio offset (in nanoseconds) of the specified source</returns>
        public async Task<int> GetSyncOffset(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            var response = await SendRequest("GetSyncOffset", requestFields).ConfigureAwait(false);
            return (int)response["offset"];
        }

        /// <summary>
        /// Deletes a scene item
        /// </summary>
        /// <param name="sceneItem">Scene item, requires name or id of item</param>
        /// /// <param name="sceneName">Scene name to delete item from (optional)</param>
        public async Task DeleteSceneItem(SceneItemStub sceneItem, string sceneName = null)
        {
            var requestFields = new JObject();

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            JObject minReqs = new JObject();
            if (sceneItem.SourceName != null)
                minReqs.Add("name", sceneItem.SourceName);

            minReqs.Add("id", sceneItem.ID);

            requestFields.Add("item", minReqs);

            await SendRequest("DeleteSceneItem", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a scene item
        /// </summary>
        /// <param name="sceneItemId">Scene item id</param>
        /// /// <param name="sceneName">Scene name to delete item from (optional)</param>
        public async Task DeleteSceneItem(int sceneItemId, string sceneName = null)
        {
            var requestFields = new JObject();

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            JObject minReqs = new JObject();

            minReqs.Add("id", sceneItemId);

            requestFields.Add("item", minReqs);

            await SendRequest("DeleteSceneItem", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItemName">Name of the scene item</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="sceneName">(optional) parent scene name of the specified source</param>
        public async Task SetSceneItemCrop(string sceneItemName,
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

            await SendRequest("SetSceneItemCrop", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="sceneItem">Scene item object</param>
        /// <param name="cropInfo">Crop coordinates</param>
        /// <param name="scene">Parent scene of scene item</param>
        public async Task SetSceneItemCrop(SceneItem sceneItem,
            SceneItemCropInfo cropInfo, OBSScene scene)
        {
            await SetSceneItemCrop(sceneItem.SourceName, cropInfo, scene.Name).ConfigureAwait(false);
        }

        /// <summary>
        /// Reset a scene item
        /// </summary>
        /// <param name="itemName">Name of the source item</param>
        /// <param name="sceneName">Name of the scene the source belongs to. Defaults to the current scene.</param>
        public async Task ResetSceneItem(string itemName, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add("item", itemName);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            await SendRequest("ResetSceneItem", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Send the provided text as embedded CEA-608 caption data. As of OBS Studio 23.1, captions are not yet available on Linux.
        /// </summary>
        /// <param name="text">Captions text</param>
        public async Task SendCaptions(string text)
        {
            var requestFields = new JObject();
            requestFields.Add("text", text);

            await SendRequest("SendCaptions", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the filename formatting string
        /// </summary>
        /// <param name="filenameFormatting">Filename formatting string to set</param>
        public async Task SetFilenameFormatting(string filenameFormatting)
        {
            var requestFields = new JObject();
            requestFields.Add("filename-formatting", filenameFormatting);

            await SendRequest("SetFilenameFormatting", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="fromSceneName">Source of the scene item</param>
        /// <param name="toSceneName">Destination for the scene item</param>
        /// <param name="sceneItem">Scene item, requires name or id</param>
        public async Task DuplicateSceneItem(string fromSceneName, string toSceneName, SceneItem sceneItem)
        {
            var requestFields = new JObject();

            requestFields.Add("fromScene", fromSceneName);
            requestFields.Add("toScene", toSceneName);

            JObject minReqs = new JObject();
            if (sceneItem.SourceName != null)
                minReqs.Add("name", sceneItem.SourceName);

            minReqs.Add("id", sceneItem.ID);

            requestFields.Add("item", minReqs);

            await SendRequest("DuplicateSceneItem", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Set the relative crop coordinates of the specified source item
        /// </summary>
        /// <param name="fromSceneName">Source of the scene item</param>
        /// <param name="toSceneName">Destination for the scene item</param>
        /// <param name="sceneItemID">Scene item id to duplicate</param>
        public async Task DuplicateSceneItem(string fromSceneName, string toSceneName, int sceneItemID)
        {
            var requestFields = new JObject();

            requestFields.Add("fromScene", fromSceneName);
            requestFields.Add("toScene", toSceneName);

            JObject minReqs = new JObject();
            minReqs.Add("id", sceneItemID);

            requestFields.Add("item", minReqs);

            await SendRequest("DuplicateSceneItem", requestFields).ConfigureAwait(false);
        }

        /// <summary>
        /// Get names of configured special sources (like Desktop Audio
        /// and Mic sources)
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetSpecialSources()
        {
            var response = await SendRequest("GetSpecialSources").ConfigureAwait(false);
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
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public async Task SetStreamingSettings(StreamingService service, bool save)
        {
            var jsonSettings = JsonConvert.SerializeObject(service.Settings);

            var requestFields = new JObject();
            requestFields.Add("type", service.Type);
            requestFields.Add("settings", jsonSettings);
            requestFields.Add("save", save);
            await SendRequest("SetStreamSettings", requestFields).ConfigureAwait(false);
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <summary>
        /// Get current streaming settings
        /// </summary>
        /// <returns></returns>
        public async Task<StreamingService> GetStreamSettings()
        {
            var response = await SendRequest("GetStreamSettings").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<StreamingService>(response.ToString());
        }

        /// <summary>
        /// Set current streaming settings
        /// </summary>
        /// <param name="service">Service settings</param>
        /// <param name="save">Save to disk</param>
        public Task SetStreamSettings(StreamingService service, bool save)
        {
            return SetStreamingSettings(service, save);
        }

        /// <summary>
        /// Save current Streaming settings to disk
        /// </summary>
        public Task SaveStreamSettings()
        {
            return SendRequest("SaveStreamSettings");
        }

        /// <summary>
        /// Get settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
        /// <returns>BrowserSource properties</returns>
        public async Task<BrowserSourceProperties> GetBrowserSourceProperties(string sourceName, string sceneName = null)
        {
            var request = new JObject();
            request.Add("source", sourceName);
            if (sceneName != null)
                request.Add("scene-name", sourceName);

            var response = await SendRequest("GetBrowserSourceProperties", request).ConfigureAwait(false);
            return new BrowserSourceProperties(response);
        }

        /// <summary>
        /// Set settings of the specified BrowserSource
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="props">BrowserSource properties</param>
        /// <param name="sceneName">Optional name of a scene where the specified source can be found</param>
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        [Obsolete("Deprecated in obs-websocket 4.8.0, use SetSourceSettings")]
        public async Task SetBrowserSourceProperties(string sourceName, BrowserSourceProperties props, string sceneName = null)
        {
            props.Source = sourceName;
            var request = JObject.FromObject(props);
            if (sceneName != null)
                request.Add("scene-name", sourceName);

            await SendRequest("SetBrowserSourceProperties", request).ConfigureAwait(false);
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <summary>
        /// Enable/disable the heartbeat event
        /// </summary>
        /// <param name="enable"></param>
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public async Task SetHeartbeat(bool enable)
        {
            var request = new JObject();
            request.Add("enable", enable);

            await SendRequest("SetHeartbeat", request).ConfigureAwait(false);
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <summary>
        /// Get the settings from a source item
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="sourceType">Type of the specified source. Useful for type-checking to avoid settings a set of settings incompatible with the actual source's type.</param>
        /// <returns>settings</returns>
        public async Task<SourceSettings> GetSourceSettings(string sourceName, string sourceType = null)
        {
            var request = new JObject();
            request.Add("sourceName", sourceName);
            if (sourceType != null)
                request.Add("sourceType", sourceType);

            JObject result = await SendRequest("GetSourceSettings", request).ConfigureAwait(false);
            SourceSettings settings = new SourceSettings(result);

            return settings;
        }

        /// <summary>
        /// Set settings of the specified source.
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <param name="settings">Settings for the source</param>
        /// <param name="sourceType">Type of the specified source. Useful for type-checking to avoid settings a set of settings incompatible with the actual source's type.</param>
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public async Task SetSourceSettings(string sourceName, JObject settings, string sourceType = null)
        {
            var request = new JObject();
            request.Add("sourceName", sourceName);
            request.Add("sourceSettings", settings);
            if (sourceType != null)
                request.Add("sourceType", sourceType);

            await SendRequest("SetSourceSettings", request).ConfigureAwait(false);
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <summary>
        /// Gets settings for a media source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public async Task<MediaSourceSettings> GetMediaSourceSettings(string sourceName)
        {
            var request = new JObject();
            request.Add("sourceName", sourceName);
            request.Add("sourceType", "ffmpeg_source");

            var response = await SendRequest("GetSourceSettings", request).ConfigureAwait(false);
            return response.ToObject<MediaSourceSettings>();
        }

        /// <summary>
        /// Sets settings of a media source
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="sourceSettings"></param>
        public async Task SetMediaSourceSettings(MediaSourceSettings sourceSettings)
        {
            if (sourceSettings.SourceType != "ffmpeg_source")
            {
                throw new System.Exception("Invalid SourceType");
            }
            await SendRequest("SetSourceSettings", JObject.FromObject(sourceSettings)).ConfigureAwait(false);
        }
    }
}
