namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneTransitionDurationChanged"/>
    /// </summary>
    public class CurrentSceneTransitionDurationChangedEventArgs
    {
        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        public int TransitionDuration { get; }

        public CurrentSceneTransitionDurationChangedEventArgs(int transitionDuration)
        {
            TransitionDuration = transitionDuration;
        }
    }
}