using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneTransitionStarted"/>
    /// </summary>
    public class SceneTransitionStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Transition name
        /// </summary>
        public string TransitionName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transitionName">The transition name</param>
        public SceneTransitionStartedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}