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
            var scene = new OBSScene(response);
            return scene;
        }

        /// <summary>
        /// Set the current scene to the specified one
        /// </summary>
        /// <param name="sceneName">The desired scene name</param>
        public void SetCurrentScene(string sceneName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("scene-name", sceneName));

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
            requestFields.Add(new JProperty("source", itemName));
            requestFields.Add(new JProperty("render", visible));

            if (sceneName != null)
                requestFields.Add(new JProperty("scene-name", sceneName));

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
        /// <returns>An <see cref="OBSOutputStatus"/> object describing the current outputs states</returns>
        public OBSOutputStatus GetStreamingStatus()
        {
            JObject response = SendRequest("GetStreamingStatus");
            var outputStatus = new OBSOutputStatus(response);
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
        /// <returns>An <see cref="OBSCurrentTransitionInfo"/> object with the current transition name and duration</returns>
        public OBSCurrentTransitionInfo GetCurrentTransition()
        {
            JObject respBody = SendRequest("GetCurrentTransition");
            return new OBSCurrentTransitionInfo(respBody);
        }

        /// <summary>
        /// Set the current transition to the specified one
        /// </summary>
        /// <param name="transitionName">Desired transition name</param>
        public void SetCurrentTransition(string transitionName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("transition-name", transitionName));

            SendRequest("SetCurrentTransition", requestFields);
        }

        /// <summary>
        /// Change the transition's duration
        /// </summary>
        /// <param name="duration">Desired transition duration (in milliseconds)</param>
        public void SetTransitionDuration(int duration)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("duration", duration));

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
            requestFields.Add(new JProperty("source", sourceName));
            requestFields.Add(new JProperty("volume", volume));

            SendRequest("SetVolume", requestFields);
        }

        /// <summary>
        /// Get the volume of the specified source
        /// </summary>
        /// <param name="sourceName">Source name</param>
        /// <returns>An <see cref="OBSVolumeInfo"/> object containing the volume and mute state of the specified source</returns>
        public OBSVolumeInfo GetVolume(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));

            var response = SendRequest("GetVolume", requestFields);
            return new OBSVolumeInfo(response);
        }

        /// <summary>
        /// Set the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be changed</param>
        /// <param name="mute">Desired mute state</param>
        public void SetMute(string sourceName, bool mute)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));
            requestFields.Add(new JProperty("mute", mute));

            SendRequest("SetMute", requestFields);
        }

        /// <summary>
        /// Toggle the mute state of the specified source
        /// </summary>
        /// <param name="sourceName">Name of the source which mute state will be toggled</param>
        public void ToggleMute(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));

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
            requestFields.Add(new JProperty("item", itemName));
            requestFields.Add(new JProperty("x", x));
            requestFields.Add(new JProperty("y", y));

            if (sceneName != null)
                requestFields.Add(new JProperty("scene-name", sceneName));

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
            requestFields.Add(new JProperty("item", itemName));
            requestFields.Add(new JProperty("x-scale", xScale));
            requestFields.Add(new JProperty("y-scale", yScale));
            requestFields.Add(new JProperty("rotation", rotation));

            if (sceneName != null)
                requestFields.Add(new JProperty("scene-name", sceneName));

            SendRequest("SetSceneItemTransform", requestFields);
        }

        /// <summary>
        /// Set the current scene collection to the specified one
        /// </summary>
        /// <param name="scName">Desired scene collection name</param>
        public void SetCurrentSceneCollection(string scName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("sc-name", scName));

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
            requestFields.Add(new JProperty("profile-name", profileName));

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
    }
}