using System;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.CurrentProfileChanging"/>
    /// </summary>
    public class CurrentProfileChangingEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the current profile
        /// </summary>
        public string ProfileName { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="profileName">The profile name</param>
        public CurrentProfileChangingEventArgs(string profileName)
        {
            ProfileName = profileName;
        }
    }
}