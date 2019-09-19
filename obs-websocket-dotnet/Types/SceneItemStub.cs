using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Stub for scene item that only contains the name or ID of an item
    /// </summary>
    public class SceneItemStub
    {
        /// <summary>
        /// Source name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string SourceName;

        /// <summary>
        /// Scene item ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ID { set; get; }
    }
}
