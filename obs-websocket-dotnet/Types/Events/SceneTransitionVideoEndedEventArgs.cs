namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneTransitionVideoEnded"/>
    /// </summary>
    public class SceneTransitionVideoEndedEventArgs
    {
        /// <summary>
        /// Transition name
        /// </summary>
        public string TransitionName { get; }

        public SceneTransitionVideoEndedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}