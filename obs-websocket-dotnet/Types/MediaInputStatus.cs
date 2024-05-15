using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [JsonProperty(PropertyName = "mediaState")]
        internal string StateString { get; set; }

        /// <summary>
        /// State of the media input
        /// </summary>
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
        [JsonProperty(PropertyName = "mediaDuration")]
        public long? Duration { get; set; }

        /// <summary>
        /// Position of the cursor in milliseconds. `null` if not playing
        /// </summary>
        [JsonProperty(PropertyName = "mediaCursor")]
        public long? Cursor { get; set; }

        /// <summary>
        /// Instantiate from JObject
        /// </summary>
        /// <param name="body"></param>
        public MediaInputStatus(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MediaInputStatus() { }
    }

    public enum MediaState
    {
        OBS_MEDIA_STATE_NONE,
        OBS_MEDIA_STATE_PLAYING,
        OBS_MEDIA_STATE_OPENING,
        OBS_MEDIA_STATE_BUFFERING,
        OBS_MEDIA_STATE_PAUSED,
        OBS_MEDIA_STATE_STOPPED,
        OBS_MEDIA_STATE_ENDED,
        OBS_MEDIA_STATE_ERROR
    }
}
