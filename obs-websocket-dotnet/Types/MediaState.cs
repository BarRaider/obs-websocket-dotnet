namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// The media state.
    /// </summary>
    public enum MediaState
    {
        None,
        Playing,
        Opening,
        Buffering,
        Paused,
        Stopped,
        Ended,
        Error,
        Unknown
    }
}
