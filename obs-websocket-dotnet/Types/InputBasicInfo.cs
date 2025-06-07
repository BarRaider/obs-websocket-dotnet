using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Input class which also shows the Unversioned Input Kind
    /// </summary>
    public class InputBasicInfo : Input
    {
        /// <summary>
        /// Unversioned Kind of the Input
        /// </summary>
        [JsonPropertyName("unversionedInputKind")]
        public string UnversionedKind { get; set; }

        /// <summary>
        /// Instantiate object from response data
        /// </summary>
        /// <param name="body"></param>
        public InputBasicInfo(JsonObject body) : base(body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InputBasicInfo() { }
    }
}
