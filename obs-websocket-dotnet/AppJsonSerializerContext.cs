using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using OBSWebsocketDotNet.Types;
using OBSWebsocketDotNet.Communication;

namespace OBSWebsocketDotNet
{    [JsonSerializable(typeof(ObsVideoSettings))]
    [JsonSerializable(typeof(ObsStats))]
    [JsonSerializable(typeof(GetSceneListInfo))]
    [JsonSerializable(typeof(List<FilterSettings>))]
    [JsonSerializable(typeof(FilterSettings))]
    [JsonSerializable(typeof(List<string>))]
    [JsonSerializable(typeof(GetProfileListInfo))]
    [JsonSerializable(typeof(RecordingStatus))]    [JsonSerializable(typeof(GetTransitionListInfo))]
    [JsonSerializable(typeof(StreamingService))]
    [JsonSerializable(typeof(StreamingServiceSettings))]
    [JsonSerializable(typeof(SceneItemTransformInfo))]
    [JsonSerializable(typeof(ServerMessage))]
    [JsonSerializable(typeof(JsonObject))]
    [JsonSerializable(typeof(List<JsonObject>))]
    // Types used in PopulateObject calls
    [JsonSerializable(typeof(List<FilterReorderItem>))]
    [JsonSerializable(typeof(FilterReorderItem))]
    [JsonSerializable(typeof(VolumeInfo))]
    [JsonSerializable(typeof(VirtualCamStatus))]
    [JsonSerializable(typeof(TransitionSettings))]
    [JsonSerializable(typeof(SourceTracks))]
    [JsonSerializable(typeof(SourceActiveInfo))]
    [JsonSerializable(typeof(SceneItemDetails))]
    [JsonSerializable(typeof(RecordStateChanged))]    [JsonSerializable(typeof(OutputStatus))]
    [JsonSerializable(typeof(OutputStateChanged))]
    [JsonSerializable(typeof(ObsVersion))]
    [JsonSerializable(typeof(ObsScene))]
    [JsonSerializable(typeof(Monitor))]
    [JsonSerializable(typeof(MediaInputStatus))]
    [JsonSerializable(typeof(InputVolume))]
    [JsonSerializable(typeof(InputSettings))]
    [JsonSerializable(typeof(InputFFMpegSettings))]    [JsonSerializable(typeof(InputBrowserSourceSettings))]
    [JsonSerializable(typeof(InputBasicInfo))]
    [JsonSerializable(typeof(Input))]
    [JsonSerializable(typeof(OBSAuthInfo))]
    [JsonSerializable(typeof(TransitionOverrideInfo))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}
