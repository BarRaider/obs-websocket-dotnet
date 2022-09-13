namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneTransitionStarted"/>
    /// </summary>
    public class SceneTransitionStartedEventArgs
    {
        /// <summary>
        /// Transition name
        /// </summary>
        public string TransitionName { get; }

        public SceneTransitionStartedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}