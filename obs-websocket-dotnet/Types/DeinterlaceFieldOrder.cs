namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Deinterlace field order of an input. Deinterlacing is restricted to async inputs only.
    /// </summary>
    public enum DeinterlaceFieldOrder
    {
        /// <summary>
        /// Top
        /// </summary>
        OBS_DEINTERLACE_FIELD_ORDER_TOP,

        /// <summary>
        /// Bottom
        /// </summary>
        OBS_DEINTERLACE_FIELD_ORDER_BOTTOM
    }
}
