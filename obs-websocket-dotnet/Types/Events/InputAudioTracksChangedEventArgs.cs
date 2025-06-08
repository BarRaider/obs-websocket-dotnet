using System;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputAudioTracksChanged"/>
    /// </summary>
    public class InputAudioTracksChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; }
        
        /// <summary>
        /// Object of audio tracks along with their associated enable states
        /// </summary>
        public JsonObject InputAudioTracks {get;}

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="inputAudioTracks">The audio track data as a JObject</param>
        public InputAudioTracksChangedEventArgs(string inputName, JsonObject inputAudioTracks)
        {
            InputName = inputName;
            InputAudioTracks = inputAudioTracks;
        }
    }
}