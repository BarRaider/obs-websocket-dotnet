using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Meta data regarding a Scene item
    /// </summary>
    public class SceneItemDetails
    {
        /// <summary>
        /// Unique item id of the source item
        /// </summary>
        [JsonProperty(PropertyName = "sceneItemId")]
        public int ItemId { set; get; }

        /// <summary>
        /// Kind of source (Example: vlc_source or image_source)
        /// </summary>
        [JsonProperty(PropertyName = "inputKind")]
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
        public SceneItemSourceType SourceType { set; get; }


        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JObject"/></param>
        public SceneItemDetails(JObject data)
        {
            if (data != null)
            {
                JsonConvert.PopulateObject(data.ToString(), this);
            }
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SceneItemDetails() { }
    }
}
