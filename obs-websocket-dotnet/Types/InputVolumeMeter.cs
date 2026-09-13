using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Volume meter data for a single input, as emitted by the InputVolumeMeters high-volume event
    /// </summary>
    public class InputVolumeMeter
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string InputName { get; set; }

        /// <summary>
        /// Volume levels for each audio channel on this input
        /// </summary>
        [JsonProperty(PropertyName = "inputLevelsMul")]
        public List<ChannelLevel> InputLevels { get; set; }

        /// <summary>
        /// Volume level data for a single audio channel
        /// </summary>
        [JsonConverter(typeof(ChannelLevelConverter))]
        public struct ChannelLevel
        {
            /// <summary>
            /// Magnitude of the signal, with the input's volume applied (0.0 to 1.0)
            /// </summary>
            public float MagnitudeWithVolume { get; set; }

            /// <summary>
            /// Peak of the signal, with the input's volume applied (0.0 to 1.0)
            /// </summary>
            public float PeakWithVolume { get; set; }

            /// <summary>
            /// Raw peak of the signal, without the input's volume applied (0.0 to 1.0)
            /// </summary>
            public float PeakRaw { get; set; }
        }

        private class ChannelLevelConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ChannelLevel);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType != JsonToken.StartArray)
                {
                    throw new ProtocolViolationException("Expected InputVolumeMeter/inputLevelsMul entry to be an array");
                }

                var items = JToken.Load(reader).ToObject<float[]>();

                if (items.Length != 3)
                {
                    throw new ProtocolViolationException($"Expected InputVolumeMeter/inputLevelsMul entry to have 3 elements, but got {items.Length}");
                }

                // obs-websocket emits each channel as [magnitude, peak, inputPeak]
                return new ChannelLevel
                {
                    MagnitudeWithVolume = items[0],
                    PeakWithVolume = items[1],
                    PeakRaw = items[2]
                };
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotSupportedException("InputVolumeMeter is a read-only event payload");
            }
        }
    }
}
