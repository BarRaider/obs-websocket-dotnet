using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OBSWebsocketSharp
{
    public enum OutputStateUpdate
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
    public delegate void OutputStateCallback(OBSWebsocket sender, OutputStateUpdate type);
    public delegate void StreamStatusCallback(OBSWebsocket sender, OBSStreamStatus status);

    public struct OBSScene
    {
        public string Name;
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

    public struct OBSSceneItem
    {
        public string Name;
        public string InternalType;

        public float AudioVolume;
        public float XPos;
        public float YPos;
        public int SourceWidth;
        public int SourceHeight;
        public float Width;
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

    public struct OBSAuthInfo
    {
        public readonly bool AuthRequired;
        public readonly string Challenge;
        public readonly string PasswordSalt;

        public OBSAuthInfo(JObject data)
        {
            AuthRequired = (bool)data["authRequired"];
            Challenge = (string)data["challenge"];
            PasswordSalt = (string)data["salt"];
        }
    }

    public struct OBSVersion
    {
        public readonly string APIVersion;
        public readonly string PluginVersion;
        public readonly string OBSStudioVersion;

        public OBSVersion(JObject data)
        {
            APIVersion = (string)data["version"];
            PluginVersion = (string)data["obs-websocket-version"];
            OBSStudioVersion = (string)data["obs-studio-version"];
        }
    }

    public struct OBSStreamStatus
    {
        public readonly bool Streaming;
        public readonly bool Recording;
        
        public readonly int BytesPerSec;
        public readonly int KbitsPerSec;
        public readonly float Strain;
        public readonly int TotalStreamTime;

        public readonly int TotalFrames;
        public readonly int DroppedFrames;
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

    public struct OBSOutputStatus
    {
        public readonly bool IsStreaming;
        public readonly bool IsRecording;

        public OBSOutputStatus(JObject data)
        {
            IsStreaming = (bool)data["streaming"];
            IsRecording = (bool)data["recording"];
        }
    }

    public struct OBSCurrentTransitionInfo
    {
        public readonly string Name;
        public readonly int Duration;

        public OBSCurrentTransitionInfo(JObject data)
        {
            Name = (string)data["name"];
            Duration = (int)data["duration"];
        }
    }

    public struct OBSVolumeInfo
    {
        public readonly float Volume;
        public readonly bool Muted;

        public OBSVolumeInfo(JObject data)
        {
            Volume = (float)data["volume"];
            Muted = (bool)data["muted"];
        }
    }
}
