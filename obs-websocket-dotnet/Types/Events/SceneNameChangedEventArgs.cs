using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneNameChanged"/>
    /// </summary>
    public class SceneNameChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Old name of the scene
        /// </summary>
        public string OldSceneName { get; }
        
        /// <summary>
        /// New name of the scene
        /// </summary>
        public string SceneName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="oldSceneName">The previous scene name</param>
        /// <param name="sceneName">The new scene name</param>
        public SceneNameChangedEventArgs(string oldSceneName, string sceneName)
        {
            OldSceneName = oldSceneName;
            SceneName = sceneName;
        }
    }
}