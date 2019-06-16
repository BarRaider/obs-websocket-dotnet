using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneItemTransform
    {
        /// <summary>
        /// Initialize the scene item transform
        /// </summary>
        /// <param name="body"></param>
        public SceneItemTransform(JObject body)
        {
            JsonConvert.PopulateObject(body.ToString(), this);
        }

        /// <summary>
        /// Crop Information
        /// </summary>
        public SceneItemCropInfo Crop { set; get; }

        /// <summary>
        /// Bounds Information
        /// </summary>
        public SceneItemBoundsInfo Bounds { set; get; }

        /// <summary>
        /// Scale Information
        /// </summary>
        public SceneItemPointInfo Scale { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public SceneItemPositionInfo Position { set; get; }

        /// <summary>
        /// Scene item height (base source height multiplied by the vertical scaling factor)
        /// </summary>
        public double Height { set; get; }


        /// <summary>
        /// Scene item width (base source width multiplied by the horizontal scaling factor)
        /// </summary>
        public double Width { set; get; }

        /// <summary>
        /// If the scene item is locked in position
        /// </summary>
        public bool Locked { set; get; }

        /// <summary>
        /// If the scene item is visible
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// Base height (without scaling) of the source
        /// </summary>
        public int SourceHeight { set; get; }

        /// <summary>
        /// Base width (without scaling) of the source
        /// </summary>
        public int SourceWidth { set; get; }

        /// <summary>
        /// The clockwise rotation of the scene item in degrees around the point of alignment.
        /// </summary>
        public double Rotation { set; get; }

    }
}
