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
using System;
using System.Collections.Generic;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Describes the state of an output (streaming or recording)
    /// </summary>
    public enum OutputState
    {
        /// <summary>
        /// The output is initializing and doesn't produces frames yet
        /// </summary>
        Starting,

        /// <summary>
        /// The output is running and produces frames
        /// </summary>
        Started,

        /// <summary>
        /// The output is stopping and sends the last remaining frames in its buffer
        /// </summary>
        Stopping,

        /// <summary>
        /// The output is completely stopped
        /// </summary>
        Stopped
    }

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnSceneChange"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newSceneName">Name of the new current scene</param>
    public delegate void SceneChangeCallback(OBSWebsocket sender, string newSceneName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnSourceOrderChange"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where items where reordered</param>
    public delegate void SourceOrderChangeCallback(OBSWebsocket sender, string sceneName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnSceneItemVisibilityChange"/>, <see cref="OBSWebsocket.OnSceneItemAdded"/> or <see cref="OBSWebsocket.OnSceneItemRemoved"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="sceneName">Name of the scene where the item is</param>
    /// <param name="itemName">Name of the concerned item</param>
    public delegate void SceneItemUpdateCallback(OBSWebsocket sender, string sceneName, string itemName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnTransitionChange"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newTransitionName">Name of the new selected transition</param>
    public delegate void TransitionChangeCallback(OBSWebsocket sender, string newTransitionName);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnTransitionDurationChange"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="newDuration">Name of the new transition duration (in milliseconds)</param>
    public delegate void TransitionDurationChangeCallback(OBSWebsocket sender, int newDuration);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnStreamingStateChange"/> or <see cref="OBSWebsocket.OnRecordingStateChange"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="type">New output state</param>
    public delegate void OutputStateCallback(OBSWebsocket sender, OutputState type);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnStreamStatus"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="status">Stream status data</param>
    public delegate void StreamStatusCallback(OBSWebsocket sender, OBSStreamStatus status);

    /// <summary>
    /// Called by <see cref="OBSWebsocket.OnStudioModeSwitched"/>
    /// </summary>
    /// <param name="sender"><see cref="OBSWebsocket"/> instance</param>
    /// <param name="enabled">New Studio Mode status</param>
    public delegate void StudioModeChangeCallback(OBSWebsocket sender, bool enabled);

    /// <summary>
    /// Describes a scene in OBS, along with its items
    /// </summary>
    public struct OBSScene
    {
        /// <summary>
        /// OBS Scene name
        /// </summary>
        public string Name;

        /// <summary>
        /// Scene item list
        /// </summary>
        public List<OBSSceneItem> Items;

        /// <summary>
        /// Builds the object from the JSON description
        /// </summary>
        /// <param name="data">JSON scene description as a <see cref="JObject" /></param>
        public OBSScene(JObject data)
        {
            Name = (string)data["name"];
            Items = new List<OBSSceneItem>();

            var sceneItems = (JArray)data["sources"];
            foreach (JObject item in sceneItems)
            {
                Items.Add(new OBSSceneItem(item));
            }
        }
    }

    /// <summary>
    /// Describes a scene item in an OBS scene
    /// </summary>
    public struct OBSSceneItem
    {
        /// <summary>
        /// Source name
        /// </summary>
        public string SourceName;

        /// <summary>
        /// Source internal type
        /// </summary>
        public string InternalType;

        /// <summary>
        /// Source audio volume
        /// </summary>
        public float AudioVolume;

        /// <summary>
        /// Scene item horizontal position/offset
        /// </summary>
        public float XPos;

        /// <summary>
        /// Scene item vertical position/offset
        /// </summary>
        public float YPos;

        /// <summary>
        /// Item source width, without scaling and transforms applied
        /// </summary>
        public int SourceWidth;

        /// <summary>
        /// Item source height, without scaling and transforms applied
        /// </summary>
        public int SourceHeight;

        /// <summary>
        /// Item width
        /// </summary>
        public float Width;

        /// <summary>
        /// Item height
        /// </summary>
        public float Height;

        /// <summary>
        /// Builds the object from the JSON scene description
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public OBSSceneItem(JObject data)
        {
            SourceName = (string)data["name"];
            InternalType = (string)data["type"];

            AudioVolume = (float)data["volume"];
            XPos = (float)data["x"];
            YPos = (float)data["y"];
            SourceWidth = (int)data["source_cx"];
            SourceHeight = (int)data["source_cy"];
            Width = (float)data["cx"];
            Height = (float)data["cy"];
        }
    }

    /// <summary>
    /// Data required by authentication
    /// </summary>
    public struct OBSAuthInfo
    {
        /// <summary>
        /// True if authentication is required, false otherwise
        /// </summary>
        public readonly bool AuthRequired;

        /// <summary>
        /// Authentication challenge
        /// </summary>
        public readonly string Challenge;

        /// <summary>
        /// Password salt
        /// </summary>
        public readonly string PasswordSalt;

        /// <summary>
        /// Builds the object from JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSAuthInfo(JObject data)
        {
            AuthRequired = (bool)data["authRequired"];
            Challenge = (string)data["challenge"];
            PasswordSalt = (string)data["salt"];
        }
    }

    /// <summary>
    /// Version info of the plugin, the API and OBS Studio
    /// </summary>
    public struct OBSVersion
    {
        /// <summary>
        /// obs-websocket plugin version
        /// </summary>
        public readonly string PluginVersion;

        /// <summary>
        /// OBS Studio version
        /// </summary>
        public readonly string OBSStudioVersion;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSVersion(JObject data)
        {
            PluginVersion = (string)data["obs-websocket-version"];
            OBSStudioVersion = (string)data["obs-studio-version"];
        }
    }

    /// <summary>
    /// Data of a stream status update
    /// </summary>
    public struct OBSStreamStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        public readonly bool Streaming;

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        public readonly bool Recording;

        /// <summary>
        /// Stream bitrate in bytes per second
        /// </summary>
        public readonly int BytesPerSec;

        /// <summary>
        /// Stream bitrate in kilobits per second
        /// </summary>
        public readonly int KbitsPerSec;

        /// <summary>
        /// RTMP output strain
        /// </summary>
        public readonly float Strain;

        /// <summary>
        /// Total time since streaming start
        /// </summary>
        public readonly int TotalStreamTime;

        /// <summary>
        /// Number of frames sent since streaming start
        /// </summary>
        public readonly int TotalFrames;

        /// <summary>
        /// Overall number of frames dropped since streaming start
        /// </summary>
        public readonly int DroppedFrames;

        /// <summary>
        /// Current framerate in Frames Per Second
        /// </summary>
        public readonly float FPS;

        /// <summary>
        /// Builds the object from the JSON event body
        /// </summary>
        /// <param name="data">JSON event body as a <see cref="JObject"/></param>
        public OBSStreamStatus(JObject data)
        {
            Streaming = (bool)data["streaming"];
            Recording = (bool)data["recording"];

            BytesPerSec = (int)data["bytes-per-sec"];
            KbitsPerSec = (int)data["kbits-per-sec"];
            Strain = (float)data["strain"];
            TotalStreamTime = (int)data["total-stream-time"];

            TotalFrames = (int)data["num-total-frames"];
            DroppedFrames = (int)data["num-dropped-frames"];
            FPS = (float)data["fps"];
        }
    }

    /// <summary>
    /// Status of streaming output and recording output
    /// </summary>
    public struct OBSOutputStatus
    {
        /// <summary>
        /// True if streaming is started and running, false otherwise
        /// </summary>
        public readonly bool IsStreaming;

        /// <summary>
        /// True if recording is started and running, false otherwise
        /// </summary>
        public readonly bool IsRecording;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSOutputStatus(JObject data)
        {
            IsStreaming = (bool)data["streaming"];
            IsRecording = (bool)data["recording"];
        }
    }

    /// <summary>
    /// Current transition settings
    /// </summary>
    public struct OBSCurrentTransitionInfo
    {
        /// <summary>
        /// Transition name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        public readonly int Duration;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSCurrentTransitionInfo(JObject data)
        {
            Name = (string)data["name"];
            Duration = (int)data["duration"];
        }
    }

    /// <summary>
    /// Volume settings of an OBS source
    /// </summary>
    public struct OBSVolumeInfo
    {
        /// <summary>
        /// Source volume in linear scale (0.0 to 1.0)
        /// </summary>
        public readonly float Volume;

        /// <summary>
        /// True if source is muted, false otherwise
        /// </summary>
        public readonly bool Muted;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSVolumeInfo(JObject data)
        {
            Volume = (float)data["volume"];
            Muted = (bool)data["muted"];
        }
    }

    /// <summary>
    /// Thrown if authentication fails
    /// </summary>
    public class AuthFailureException : Exception
    {
    }

    /// <summary>
    /// Thrown when the server responds with an error
    /// </summary>
    public class ErrorResponseException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponseException(string message) : base(message)
        {
        }
    }
}
