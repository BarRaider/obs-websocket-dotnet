﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Volume settings of an OBS source
    /// </summary>
    public class VolumeInfo
    {
        /// <summary>
        /// Source volume in linear scale (0.0 to 1.0)
        /// </summary>
        [JsonProperty(PropertyName = "volume")]
        public readonly float Volume;

        /// <summary>
        /// True if source is muted, false otherwise
        /// </summary>
        [JsonProperty(PropertyName = "muted")]
        public readonly bool Muted;

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public VolumeInfo(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}