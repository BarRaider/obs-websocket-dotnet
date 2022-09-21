using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneTransitionEnded"/>
    /// </summary>
    public class SceneTransitionEndedEventArgs : EventArgs
    {
        /// <summary>
        /// Scene transition name
        /// </summary>
        public string TransitionName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transitionName">The transition name</param>
        public SceneTransitionEndedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}