namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneTransitionEnded"/>
    /// </summary>
    public class SceneTransitionEndedEventArgs
    {
        /// <summary>
        /// Scene transition name
        /// </summary>
        public string TransitionName { get; }

        public SceneTransitionEndedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}