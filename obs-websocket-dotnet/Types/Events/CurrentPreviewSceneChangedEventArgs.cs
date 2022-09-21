using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentPreviewSceneChanged"/>
    /// </summary>
    public class CurrentPreviewSceneChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the scene that was switched to
        /// </summary>
        public string SceneName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneName">The scene name</param>
        public CurrentPreviewSceneChangedEventArgs(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}