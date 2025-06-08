using System;
using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Status of a Media Input
    /// </summary>
    public class MediaInputStatus
    {
        /// <summary>
        /// State of the media input
        /// </summary>
        [JsonPropertyName("mediaState")]
        public string StateString { get; set; }

        /// <summary>
        /// State of the media input
        /// </summary>
        [JsonIgnore]
        public MediaState? State
        {
            get
            {
                if (!Enum.TryParse(StateString, out MediaState state))
                {
                    return null;
                }
                return state;
            }
        }

        /// <summary>
        /// Total duration of the playing media in milliseconds. `null` if not playing
        /// </summary>
        [JsonPropertyName("mediaDuration")]
        public long? Duration { get; set; }

        /// <summary>
        /// Position of the cursor in milliseconds. `null` if not playing
        /// </summary>
        [JsonPropertyName("mediaCursor")]
        public long? Cursor { get; set; }        /// <summary>
        /// Instantiate from JObject
        /// </summary>
        /// <param name="body"></param>
        public MediaInputStatus(JsonObject body)
        {
            StateString = body["mediaState"]?.GetValue<string>() ?? string.Empty;
            Duration = body["mediaDuration"]?.GetValue<long?>();
            Cursor = body["mediaCursor"]?.GetValue<long?>();
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MediaInputStatus() { }
    }

    /// <summary>
    /// Enum representing the state of a media input
    /// </summary>
    public enum MediaState
    {
        /// <summary>
        /// No media is loaded
        /// </summary>
        OBS_MEDIA_STATE_NONE,

        /// <summary>
        /// Media is playing
        /// </summary>
        OBS_MEDIA_STATE_PLAYING,

        /// <summary>
        /// Media is opening
        /// </summary>
        OBS_MEDIA_STATE_OPENING,

        /// <summary>
        /// Media is buffering
        /// </summary>
        OBS_MEDIA_STATE_BUFFERING,

        /// <summary>
        /// Media is playing but is paused
        /// </summary>
        OBS_MEDIA_STATE_PAUSED, 

        /// <summary>
        /// Media is stopped
        /// </summary>
        OBS_MEDIA_STATE_STOPPED,

        /// <summary>
        /// Media is ended
        /// </summary>
        OBS_MEDIA_STATE_ENDED,

        /// <summary>
        /// Media has errored
        /// </summary>
        OBS_MEDIA_STATE_ERROR
    }
}
