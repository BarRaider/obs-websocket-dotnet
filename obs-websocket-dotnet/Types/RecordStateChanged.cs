using System.Text.Json.Nodes;
using System.Text.Json;using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Data when Recording change states
    /// </summary>
    public class RecordStateChanged : OutputStateChanged
    {
        /// <summary>
        /// File name for the saved recording, if record stopped. null otherwise
        /// </summary>
        [JsonPropertyName("outputPath")]
        public string OutputPath { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body"></param>
        public RecordStateChanged(JsonObject body) :base(body)
        {
            JsonSerializer2.PopulateObject(body.ToString(), this, AppJsonSerializerContext.Default);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public RecordStateChanged() { }
    }
}
