using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Called by <see cref="OBSWebsocket.SceneTransitionVideoEnded"/>
    /// </summary>
    public class SceneTransitionVideoEndedEventArgs : EventArgs
    {
        /// <summary>
        /// Transition name
        /// </summary>
        public string TransitionName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transitionName">The transition name</param>
        public SceneTransitionVideoEndedEventArgs(string transitionName)
        {
            TransitionName = transitionName;
        }
    }
}