namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Types of bounding boxes for scene items
    /// </summary>
    public enum SceneItemBoundsType
    {
        OBS_BOUNDS_STRETCH,
        OBS_BOUNDS_SCALE_INNER,
        OBS_BOUNDS_SCALE_OUTER,
        OBS_BOUNDS_SCALE_TO_WIDTH,
        OBS_BOUNDS_SCALE_TO_HEIGHT,
        OBS_BOUNDS_MAX_ONLY,
        OBS_BOUNDS_NONE
    }
}