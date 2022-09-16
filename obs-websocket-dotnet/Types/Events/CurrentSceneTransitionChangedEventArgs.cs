namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneTransitionChanged"/>
    /// </summary>
    public class CurrentSceneTransitionChangedEventArgs
    {
        /// <summary>
        /// Name of the new transition
        /// </summary>
        public string TransitionName { get; }

        public CurrentSceneTransitionChangedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}