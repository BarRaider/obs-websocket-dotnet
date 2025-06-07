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
{
    [JsonSerializable(typeof(ObsVideoSettings))]
    [JsonSerializable(typeof(ObsStats))]
    [JsonSerializable(typeof(GetSceneListInfo))]
    [JsonSerializable(typeof(List<FilterSettings>))]
    [JsonSerializable(typeof(FilterSettings))]
    [JsonSerializable(typeof(List<string>))]
    [JsonSerializable(typeof(GetProfileListInfo))]
    [JsonSerializable(typeof(RecordingStatus))]
    [JsonSerializable(typeof(GetTransitionListInfo))]
    [JsonSerializable(typeof(StreamingService))]
    [JsonSerializable(typeof(SceneItemTransformInfo))]
    [JsonSerializable(typeof(ServerMessage))]
    [JsonSerializable(typeof(JsonObject))]
    [JsonSerializable(typeof(List<JsonObject>))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}
