namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Types of bounding boxes for scene items
    /// </summary>
    public enum SceneItemBoundsType
    {
        /// <summary>
        /// Stretch
        /// </summary>
        OBS_BOUNDS_STRETCH,
        /// <summary>
        /// Inner scale
        /// </summary>
        OBS_BOUNDS_SCALE_INNER,
        /// <summary>
        /// Outer scale
        /// </summary>
        OBS_BOUNDS_SCALE_OUTER,
        /// <summary>
        /// Scale to width
        /// </summary>
        OBS_BOUNDS_SCALE_TO_WIDTH,
        /// <summary>
        /// Scale to height
        /// </summary>
        OBS_BOUNDS_SCALE_TO_HEIGHT,
        /// <summary>
        /// Max only
        /// </summary>
        OBS_BOUNDS_MAX_ONLY,
        /// <summary>
        /// No bounds
        /// </summary>
        OBS_BOUNDS_NONE
    }
}