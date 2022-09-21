using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemRemoved"/>
    /// </summary>
    public class SceneItemRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the scene where the item was removed from
        /// </summary>
        public string SceneName { get; } 
        
        /// <summary>
        /// Name of the concerned item
        /// </summary>
        public string SourceName { get; }
        
        /// <summary>
        /// Numeric ID of the scene item
        /// </summary>
        public int SceneItemId { get; } 

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sceneName">The scene name</param>
        /// <param name="sourceName">The source name</param>
        /// <param name="sceneItemId">The scene items id</param>
        public SceneItemRemovedEventArgs(string sceneName, string sourceName, int sceneItemId)
        {
            SceneName = sceneName;
            SourceName = sourceName;
            SceneItemId = sceneItemId;
        }
    }
}