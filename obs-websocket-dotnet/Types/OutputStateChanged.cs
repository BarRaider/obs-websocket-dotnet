using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        [JsonProperty(PropertyName = "outputActive")]
        public bool IsActive { set; get; }

        /// <summary>
        /// Output state as string
        /// </summary>
        [JsonProperty(PropertyName = "outputState")]
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

                switch (StateStr)
                {
                    case "OBS_WEBSOCKET_OUTPUT_STARTING":
                        state = OutputState.OBS_WEBSOCKET_OUTPUT_STARTING;
                        break;
                    case "OBS_WEBSOCKET_OUTPUT_STARTED":
                        state = OutputState.OBS_WEBSOCKET_OUTPUT_STARTED;
                        break;
                    case "OBS_WEBSOCKET_OUTPUT_STOPPING":
                        state = OutputState.OBS_WEBSOCKET_OUTPUT_STOPPING;
                        break;
                    case "OBS_WEBSOCKET_OUTPUT_STOPPED":
                        state = OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED;
                        break;
                    case "OBS_WEBSOCKET_OUTPUT_PAUSED":
                        state = OutputState.OBS_WEBSOCKET_OUTPUT_PAUSED;
                        break;
                    case "OBS_WEBSOCKET_OUTPUT_RESUMED":
                        state = OutputState.OBS_WEBSOCKET_OUTPUT_RESUMED;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return state.Value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body"></param>
        public OutputStateChanged(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OutputStateChanged() { }
    }
}
