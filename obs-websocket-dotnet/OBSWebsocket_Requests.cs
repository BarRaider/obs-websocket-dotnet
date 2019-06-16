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

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Instance of a connection with an obs-websocket server
    /// </summary>
    public partial class OBSWebsocket
    {
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
        /// List every available scene
        /// </summary>
        /// <returns>A <see cref="List{OBSScene}" /> of <see cref="OBSScene"/> objects describing each scene</returns>
        public List<OBSScene> ListScenes()
        {
            JObject response = SendRequest("GetSceneList");
            JArray items = (JArray)response["scenes"];

            var scenes = new List<OBSScene>();
            foreach (JObject sceneData in items)
            {
                OBSScene scene = new OBSScene(sceneData);
                scenes.Add(scene);
            }

            return scenes;
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
            requestFields.Add("source", itemName);
            requestFields.Add("render", visible);

            if (sceneName != null)
                requestFields.Add("scene-name", sceneName);

            SendRequest("SetSourceRender", requestFields);
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
            JObject response = SendRequest("GetTransitionList");
            JArray items = (JArray)response["transitions"];

            List<string> transitionNames = new List<string>();
            foreach (JObject item in items)
            {
                transitionNames.Add((string)item["name"]);
            }

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
        /// <param name="volume">Desired volume in linear scale (0.0 to 1.0)</param>
        public void SetVolume(string sourceName, float volume)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);
            requestFields.Add("volume", volume);

            SendRequest("SetVolume", requestFields);
        }

        /// <summary>
        /// Get the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>An <see cref="VolumeInfo"/> object containing the volume and mute state of the specified source</returns>
        public VolumeInfo GetVolume(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add("source", sourceName);

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
            foreach(JObject item in items)
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
        /// Get status of Studio Mode
        /// </summary>
        /// <returns>Studio Mode status (on/off)</returns>
        public bool StudioModeEnabled()
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
                SendRequest("EnableStudioMode");
            else
                SendRequest("DisableStudioMode");
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

            if(transitionDuration > -1 || transitionName != null)
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
                requestFields.Add("scene-name");

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
        /// Get names of configured special sources (like Desktop Audio
        /// and Mic sources)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetSpecialSources()
        {
            var response = SendRequest("GetSpecialSources");
            var sources = new Dictionary<string, string>();
            foreach(KeyValuePair<string, JToken> x in response)
            {
                string key = x.Key;
                string value = (string)x.Value;
                if(key != "request-type" && key != "message-id")
                {
                    sources.Add(key, value);
                }
            }
            return sources;
        }

        /// <summary>
        /// Set current streaming settings
        /// </summary>
        /// <param name="service"></param>
        /// <param name="save"></param>
        public void SetStreamingSettings(StreamingService service, bool save)
        {
            var requestFields = new JObject();
            requestFields.Add("type", service.Type);
            requestFields.Add("settings", service.Settings);
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

            var service = new StreamingService();
            service.Type = (string)response["type"];
            service.Settings = (JObject)response["settings"];

            return service;
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
            var request = new JObject();
            request.Add("source", sourceName);
            if (sceneName != null)
                request.Add("scene-name", sourceName);

            request.Merge(props.ToJSON(), new JsonMergeSettings()
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            SendRequest("SetBrowserSourceProperties", request);
        }
    }
}
