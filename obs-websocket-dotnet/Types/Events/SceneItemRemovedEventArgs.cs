namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemRemoved"/>
    /// </summary>
    public class SceneItemRemovedEventArgs
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


        public SceneItemRemovedEventArgs(string sceneName, string sourceName, int sceneItemId)
        {
            SceneName = sceneName;
            SourceName = sourceName;
            SceneItemId = sceneItemId;
        }
    }
}