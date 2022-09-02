using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Information on a specific Input
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Name of the Input
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string Name { get; set; }

        /// <summary>
        /// Kind of the Input
        /// </summary>
        [JsonProperty(PropertyName = "inputKind")]
        public string Kind { get; set; }

        /// <summary>
        /// Unversioned Kind of the Input
        /// </summary>
        [JsonProperty(PropertyName = "unversionedInputKind")]
        public string UnversionedKind { get; set; }

        /// <summary>
        /// Instantiate object from response data
        /// </summary>
        /// <param name="body"></param>
        public Input(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Input() { }
    }
}
