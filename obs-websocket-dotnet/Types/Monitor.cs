using System.Text.Json;using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Information on a connected Monitor
    /// </summary>
    public class Monitor
    {
        /// <summary>
        /// Monitor height (px)
        /// </summary>
        [JsonPropertyName("monitorHeight")]
        public int Height { get; set; }

        /// <summary>
        /// Monitor width (px)
        /// </summary>
        [JsonPropertyName("monitorWidth")]
        public int Width { get; set; }

        /// <summary>
        /// Monitor Name
        /// </summary>
        [JsonPropertyName("monitorName")]
        public string Name { get; set; }

        /// <summary>
        /// Monitor Index
        /// </summary>
        [JsonPropertyName("monitorIndex")]
        public int Index { get; set; }

        /// <summary>
        /// Monitor Position X
        /// </summary>
        [JsonPropertyName("monitorPositionX")]
        public int PositionX { get; set; }

        /// <summary>
        /// Monitor Position Y
        /// </summary>
        [JsonPropertyName("monitorPositionY")]
        public int PositionY { get; set; }        /// <summary>
        /// Constructor to auto populate
        /// </summary>
        /// <param name="data"></param>
        public Monitor (JsonObject data)
        {
            Height = data["monitorHeight"]?.GetValue<int>() ?? 0;
            Width = data["monitorWidth"]?.GetValue<int>() ?? 0;
            Name = data["monitorName"]?.GetValue<string>() ?? string.Empty;
            Index = data["monitorIndex"]?.GetValue<int>() ?? 0;
            PositionX = data["monitorPositionX"]?.GetValue<int>() ?? 0;
            PositionY = data["monitorPositionY"]?.GetValue<int>() ?? 0;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Monitor() { }
    }
}
