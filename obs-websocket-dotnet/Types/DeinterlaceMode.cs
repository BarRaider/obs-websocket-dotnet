namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Deinterlace mode of an input. Deinterlacing is restricted to async inputs only.
    /// </summary>
    public enum DeinterlaceMode
    {
        /// <summary>
        /// Disable
        /// </summary>
        OBS_DEINTERLACE_MODE_DISABLE,

        /// <summary>
        /// Discard
        /// </summary>
        OBS_DEINTERLACE_MODE_DISCARD,

        /// <summary>
        /// Retro
        /// </summary>
        OBS_DEINTERLACE_MODE_RETRO,

        /// <summary>
        /// Blend
        /// </summary>
        OBS_DEINTERLACE_MODE_BLEND,

        /// <summary>
        /// Blend 2x
        /// </summary>
        OBS_DEINTERLACE_MODE_BLEND_2X,

        /// <summary>
        /// Linear
        /// </summary>
        OBS_DEINTERLACE_MODE_LINEAR,

        /// <summary>
        /// Linear 2x
        /// </summary>
        OBS_DEINTERLACE_MODE_LINEAR_2X,

        /// <summary>
        /// Yadif
        /// </summary>
        OBS_DEINTERLACE_MODE_YADIF,

        /// <summary>
        /// Yadif 2x
        /// </summary>
        OBS_DEINTERLACE_MODE_YADIF_2X
    }
}
