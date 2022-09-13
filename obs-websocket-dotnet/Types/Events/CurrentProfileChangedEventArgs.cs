namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProfileChanged"/>
    /// </summary>
    public class CurrentProfileChangedEventArgs
    {
        /// <summary>
        /// Name of the new profile
        /// </summary>
        public string ProfileName { get; }

        public CurrentProfileChangedEventArgs(string profileName)
        {
            ProfileName = profileName;
        }
    }
}