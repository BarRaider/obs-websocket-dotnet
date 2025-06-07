using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Communication
{
    /// <summary>
    /// Data required by authentication
    /// </summary>
    public class OBSAuthInfo
    {
        /// <summary>
        /// Authentication challenge
        /// </summary>
        [JsonPropertyName("challenge")]
        public readonly string Challenge;

        /// <summary>
        /// Password salt
        /// </summary>
        [JsonPropertyName("salt")]
        public readonly string PasswordSalt;

        /// <summary>
        /// Builds the object from JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public OBSAuthInfo(JsonObject data)
        {
            JsonSerializer2.PopulateObject(data.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public OBSAuthInfo() { }
    }
}