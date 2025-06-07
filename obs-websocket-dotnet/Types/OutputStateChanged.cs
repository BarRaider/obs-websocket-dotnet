using System;
using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Data when Stream/Recording/Instant_Replay change states
    /// </summary>
    public class OutputStateChanged
    {
        private OutputState? state;
        /// <summary>
        /// Is output currently active (streaming/recording)
        /// </summary>
        [JsonPropertyName("outputActive")]
        public bool IsActive { set; get; }

        /// <summary>
        /// Output state as string
        /// </summary>
        [JsonPropertyName("outputState")]
        public string StateStr { set; get; }

        /// <summary>
        /// OutputState enum of current state
        /// </summary>
        public OutputState State {
            get
            {
                if (state.HasValue)
                {
                    return state.Value;
                }

                if (!Enum.TryParse(StateStr, ignoreCase: true, out OutputState stateTmp))
                {
                    throw new ArgumentOutOfRangeException($"Couldn't parse '{StateStr}' as {nameof(OutputState)}");
                }
                state = stateTmp;

                return state.Value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body"></param>
        public OutputStateChanged(JsonObject body)
        {
            JsonSerializer2.PopulateObject(body.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OutputStateChanged() { }
    }
}
