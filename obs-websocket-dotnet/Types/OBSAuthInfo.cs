using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Data required by authentication
    /// </summary>
    public class OBSAuthInfo
    {
        /// <summary>
        /// True if authentication is required, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "authRequired")]
        public readonly bool AuthRequired;

        /// <summary>
        /// Authentication challenge
        /// </summary>
        [JsonProperty(PropertyName = "challenge")]
        public readonly string Challenge;

        /// <summary>
        /// Password salt
        /// </summary>
        [JsonProperty(PropertyName = "salt")]
        public readonly string PasswordSalt;

        /// <summary>
        /// Builds the object from JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSAuthInfo(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}