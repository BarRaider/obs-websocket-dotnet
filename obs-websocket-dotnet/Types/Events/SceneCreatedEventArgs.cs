using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneCreated"/>
    /// </summary>
    public class SceneCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the new scene
        /// </summary>
        public string SceneName { get; }
        
        /// <summary>
        /// Whether the new scene is a group
        /// </summary>
        public bool IsGroup { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneName">The scene name</param>
        /// <param name="isGroup">Is the scene item a group</param>
        public SceneCreatedEventArgs(string sceneName, bool isGroup)
        {
            SceneName = sceneName;
            IsGroup = isGroup;
        }
    }
}