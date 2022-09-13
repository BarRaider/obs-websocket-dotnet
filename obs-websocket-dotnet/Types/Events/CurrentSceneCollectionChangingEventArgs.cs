namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentSceneCollectionChanging"/>
    /// </summary>
    public class CurrentSceneCollectionChangingEventArgs
    {
        /// <summary>
        /// Name of the current scene collection
        /// </summary>
        public string SceneCollectionName { get; }

        public CurrentSceneCollectionChangingEventArgs(string sceneCollectionName)
        {
            SceneCollectionName = sceneCollectionName;
        }
    }
}