using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    public class SceneItem2
    {
        /// <summary>
        /// Unique item id of the source item
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public int ItemId { set; get; }

        /// <summary>
        /// Kind of source
        /// </summary>
        [JsonProperty(PropertyName = "sourceKind")]
        public string SourceKind { set; get; }

        /// <summary>
        /// Name of the scene item's source
        /// </summary>
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { set; get; }

        /// <summary>
        /// Type of the scene item's source.
        /// </summary>
        [JsonProperty(PropertyName = "sourceType")]
        public SourceType2 SourceType { set; get; }


        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public SceneItem2(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}
