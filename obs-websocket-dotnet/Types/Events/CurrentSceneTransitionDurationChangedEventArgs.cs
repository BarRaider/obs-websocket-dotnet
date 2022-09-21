using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneTransitionDurationChanged"/>
    /// </summary>
    public class CurrentSceneTransitionDurationChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Transition duration in milliseconds
        /// </summary>
        public int TransitionDuration { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transitionDuration">The transition duration</param>
        public CurrentSceneTransitionDurationChangedEventArgs(int transitionDuration)
        {
            TransitionDuration = transitionDuration;
        }
    }
}