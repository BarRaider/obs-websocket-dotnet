using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    public class InputVolumeMeter
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string InputName { set; get; }

        // Convert json Array of 3 scalars, into a struct
        private class ChannelLevelConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ChannelLevel);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType != JsonToken.StartArray)
                    throw new ProtocolViolationException("Expected InputVolumeMeter/inputLevelsMul to be an array");

                JToken token = JToken.Load(reader);
                var items = token.ToObject<float[]>();

                if (items.Length != 3)
                    throw new ProtocolViolationException($"Expected InputVolumeMeter/inputLevelsMul to be an 3 element array, but instead got {items.Length} element array");

                ChannelLevel contentStruct;
                contentStruct.PeakRaw = items[0];
                contentStruct.PeakWithVolume = items[1];
                contentStruct.magnitudeWithVolume = items[2];
                return contentStruct;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        [JsonConverter(typeof(ChannelLevelConverter))]
        public struct ChannelLevel
        {
            // https://github.com/obsproject/obs-websocket/blob/f4b72b69ce7f9ec6a5fdb1b06971e00d2b091bec/src/utils/Obs_VolumeMeter.cpp#L87


            public float magnitudeWithVolume;
            public float PeakWithVolume;
            public float PeakRaw;
        }

        /// <summary>
        /// Array of channels on this input
        /// </summary>
        [JsonProperty(PropertyName = "inputLevelsMul")]
        public List<ChannelLevel> InputLevels { set; get; }


    }
}
