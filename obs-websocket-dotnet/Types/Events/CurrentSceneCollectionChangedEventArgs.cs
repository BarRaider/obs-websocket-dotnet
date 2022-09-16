namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneCollectionChanged"/> 
    /// </summary>
    public class CurrentSceneCollectionChangedEventArgs
    {
        /// <summary>
        /// Name of the new scene collection
        /// </summary>
        public string SceneCollectionName { get; }

        public CurrentSceneCollectionChangedEventArgs(string sceneCollectionName)
        {
            SceneCollectionName = sceneCollectionName;
        }
    }
}