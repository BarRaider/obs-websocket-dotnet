namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// The media state.
    /// </summary>
    public enum MediaState
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        None,
        Playing,
        Opening,
        Buffering,
        Paused,
        Stopped,
        Ended,
        Error,
        Unknown
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
