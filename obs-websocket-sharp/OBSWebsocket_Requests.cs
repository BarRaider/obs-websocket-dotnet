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

namespace OBSWebsocketSharp
{
    public partial class OBSWebsocket
    {
        public OBSScene GetCurrentScene()
        {
            JObject response = SendRequest("GetCurrentScene");
            var scene = new OBSScene(response);
            return scene;
        }

        public void SetCurrentScene(string sceneName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("scene-name", sceneName));

            SendRequest("SetCurrentScene", requestFields);
        }

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

        public void SetSourceRender(string itemName, bool visible, string sceneName = null)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", itemName));
            requestFields.Add(new JProperty("render", visible));

            if (sceneName != null)
                requestFields.Add(new JProperty("scene-name", sceneName));

            SendRequest("SetSourceRender", requestFields);
        }

        public void ToggleStreaming()
        {
            SendRequest("StartStopStreaming");
        }

        public void ToggleRecording()
        {
            SendRequest("StartStopRecording");
        }

        public OBSOutputStatus GetStreamingStatus()
        {
            JObject response = SendRequest("GetStreamingStatus");
            var outputStatus = new OBSOutputStatus(response);
            return outputStatus;
        }

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

        public OBSCurrentTransitionInfo GetCurrentTransition()
        {
            JObject respBody = SendRequest("GetCurrentTransition");
            return new OBSCurrentTransitionInfo(respBody);
        }

        public void SetCurrentTransition(string transitionName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("transition-name", transitionName));

            SendRequest("SetCurrentTransition", requestFields);
        }

        public void SetTransitionDuration(int duration)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("duration", duration));

            SendRequest("SetTransitionDuration", requestFields);
        }

        public void SetVolume(string sourceName, float volume)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));
            requestFields.Add(new JProperty("volume", volume));

            SendRequest("SetVolume", requestFields);
        }

        public OBSVolumeInfo GetVolume(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));

            var response = SendRequest("GetVolume", requestFields);
            return new OBSVolumeInfo(response);
        }

        public void SetMute(string sourceName, bool mute)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));
            requestFields.Add(new JProperty("mute", mute));

            SendRequest("SetMute", requestFields);
        }

        public void ToggleMute(string sourceName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("source", sourceName));

            SendRequest("ToggleMute", requestFields);
        }

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

        public void SetCurrentSceneCollection(string scName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("sc-name", scName));

            SendRequest("SetCurrentSceneCollection", requestFields);
        }

        public string GetCurrentSceneCollection()
        {
            var response = SendRequest("GetCurrentSceneCollection");
            return (string)response["sc-name"];
        }

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

        public void SetCurrentProfile(string profileName)
        {
            var requestFields = new JObject();
            requestFields.Add(new JProperty("profile-name", profileName));

            SendRequest("SetCurrentProfile", requestFields);
        }

        public string GetCurrentProfile()
        {
            var response = SendRequest("GetCurrentProfile");
            return (string)response["profile-name"];
        }

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
