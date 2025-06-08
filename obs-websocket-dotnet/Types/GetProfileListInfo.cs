using System.Text.Json;using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Response from <see cref="OBSWebsocket.GetProfileList"/>
    /// </summary>
    public class GetProfileListInfo
    {
        /// <summary>
        /// Name of the currently active profile
        /// </summary>
        [JsonPropertyName("currentProfileName")]
        public string CurrentProfileName { set; get; }

        /// <summary>
        /// List of all profiles
        /// </summary>
        [JsonPropertyName("profiles")]
        public List<string> Profiles { set; get; }
    }
}