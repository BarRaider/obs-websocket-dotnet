using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ProfileListChanged"/>
    /// </summary>
    public class ProfileListChangedEventArgs
    {
        public List<string> Profiles { get; }

        public ProfileListChangedEventArgs(List<string> profiles)
        {
            Profiles = profiles;
        }
    }
}