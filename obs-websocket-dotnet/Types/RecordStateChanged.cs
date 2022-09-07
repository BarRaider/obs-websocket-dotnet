using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "outputPath")]
        public string OutputPath { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body"></param>
        public RecordStateChanged(JObject body) :base(body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public RecordStateChanged() { }
    }
}
