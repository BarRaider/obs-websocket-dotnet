using Newtonsoft.Json;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Source information returned by GetSourcesList
    /// </summary>
    public class InputInfo
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string Name { set; get; }

        /// <summary>
        /// The kind of the input
        /// </summary>
        [JsonProperty(PropertyName = "inputKind")]
        public string Kind { set; get; }

        /// <summary>
        /// The unversioned kind of input (aka no `_v2` stuff)
        /// </summary>
        [JsonProperty(PropertyName = "unversionedInputKind")]
        public string UnversionedKind { set; get; }
    }
}