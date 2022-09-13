namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneItemCreated"/>
    /// </summary>
    public class SceneItemCreatedEventArgs
    {
        /// <summary>
        /// Name of the scene where the item is
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
        /// Index position of the item
        /// </summary>
        public int SceneItemIndex { get; }

        public SceneItemCreatedEventArgs(string sceneName, string sourceName, int sceneItemId, int sceneItemIndex)
        {
            SceneName = sceneName;
            SourceName = sourceName;
            SceneItemId = sceneItemId;
            SceneItemIndex = sceneItemIndex;
        }
    }
}