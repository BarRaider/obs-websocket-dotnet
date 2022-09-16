namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProfileChanging"/>
    /// </summary>
    public class CurrentProfileChangingEventArgs
    {
        /// <summary>
        /// Name of the current profile
        /// </summary>
        public string ProfileName { get; }

        public CurrentProfileChangingEventArgs(string profileName)
        {
            ProfileName = profileName;
        }
    }
}