using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneRemoved"/>
    /// </summary>
    public class SceneRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the removed scene
        /// </summary>
        public string SceneName { get; }
        
        /// <summary>
        /// Whether the removed scene was a group
        /// </summary>
        public bool IsGroup { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneName">The scene name</param>
        /// <param name="isGroup">Is the scene name a group</param>
        public SceneRemovedEventArgs(string sceneName, bool isGroup)
        {
            SceneName = sceneName;
            IsGroup = isGroup;
        }
    }
}