using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
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
        [JsonPropertyName("sceneItemId")]
        public int ItemId { set; get; }

        /// <summary>
        /// Kind of source (Example: vlc_source or image_source)
        /// </summary>
        [JsonPropertyName("inputKind")]
        public string SourceKind { set; get; }

        /// <summary>
        /// Name of the scene item's source
        /// </summary>
        [JsonPropertyName("sourceName")]
        public string SourceName { set; get; }

        /// <summary>
        /// Type of the scene item's source.
        /// </summary>
        [JsonPropertyName("sourceType")]
        public SceneItemSourceType SourceType { set; get; }        /// <summary>
        /// Builds the object from the JSON data
        /// </summary>
        /// <param name="data">JSON item description as a <see cref="JsonObject"/></param>
        public SceneItemDetails(JsonObject data)
        {
            if (data != null)
            {
                ItemId = data["sceneItemId"]?.GetValue<int>() ?? 0;
                SourceKind = data["inputKind"]?.GetValue<string>() ?? string.Empty;
                SourceName = data["sourceName"]?.GetValue<string>() ?? string.Empty;
                
                // Handle enum conversion safely
                var sourceTypeValue = data["sourceType"]?.GetValue<string>();
                if (Enum.TryParse<SceneItemSourceType>(sourceTypeValue, true, out var sourceType))
                {
                    SourceType = sourceType;
                }
                else
                {
                    SourceType = SceneItemSourceType.OBS_SOURCE_TYPE_INPUT; // Default value
                }
            }
        }

        /// <summary>
        /// Default Constructor for deserialization
        /// </summary>
        public SceneItemDetails() { }
    }
}
