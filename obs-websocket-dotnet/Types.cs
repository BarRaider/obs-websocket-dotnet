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
    /// Describes the state of an output (streaming or recording)
    /// </summary>
    public enum OutputState
    {
        Starting,
        Started,
        Stopping,
        Stopped
    }

    public delegate void SceneChangeCallback(OBSWebsocket sender, string newSceneName);
    public delegate void SourceOrderChangeCallback(OBSWebsocket sender, string sceneName);
    public delegate void SceneItemUpdateCallback(OBSWebsocket sender, string sceneName, string itemName);
    public delegate void TransitionChangeCallback(OBSWebsocket sender, string newTransitionName);
    public delegate void TransitionDurationChangeCallback(OBSWebsocket sender, int newDuration);
    public delegate void OutputStateCallback(OBSWebsocket sender, OutputState type);
    public delegate void StreamStatusCallback(OBSWebsocket sender, OBSStreamStatus status);

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

        public OBSScene(JObject data)
        {
            Name = (string)data["name"];
            Items = new List<OBSSceneItem>();

            var sceneItems = (JArray)data["sources"];
            foreach (JObject item in sceneItems)
            {
                var sceneItem = new OBSSceneItem(item);
                Items.Add(sceneItem);
            }
        }
    }

    /// <summary>
    /// Describes a scene item in an OBS scene
    /// </summary>
    public struct OBSSceneItem
    {
        /// <summary>
        /// Scene item name
        /// </summary>
        public string Name;

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

        public OBSSceneItem(JObject data)
        {
            Name = (string)data["name"];
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
        /// obs-websocket protocol version
        /// </summary>
        public readonly string APIVersion;

        /// <summary>
        /// obs-websocket plugin version
        /// </summary>
        public readonly string PluginVersion;

        /// <summary>
        /// OBS Studio version
        /// </summary>
        public readonly string OBSStudioVersion;

        public OBSVersion(JObject data)
        {
            APIVersion = (string)data["version"];
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

        public OBSVolumeInfo(JObject data)
        {
            Volume = (float)data["volume"];
            Muted = (bool)data["muted"];
        }
    }
}
