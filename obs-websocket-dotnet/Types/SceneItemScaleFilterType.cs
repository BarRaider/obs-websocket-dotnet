namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Types of scene scale filter for scene items
    /// </summary>
    public enum SceneItemScaleFilterType
    {
        /// <summary>
        /// No scale filtering
        /// </summary>
        OBS_SCALE_DISABLE,
        /// <summary>
        /// Point scale filtering
        /// </summary>
        OBS_SCALE_POINT,
        /// <summary>
        /// Bicubic scale filtering
        /// </summary>
        OBS_SCALE_BICUBIC,
        /// <summary>
        /// Bilinear scale filtering
        /// </summary>
        OBS_SCALE_BILINEAR,
        /// <summary>
        /// Lanczos scale filtering
        /// </summary>
        OBS_SCALE_LANCZOS,
        /// <summary>
        /// Area scale filtering
        /// </summary>
        OBS_SCALE_AREA,
    }
}