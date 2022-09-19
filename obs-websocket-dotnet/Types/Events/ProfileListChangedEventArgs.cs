using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ProfileListChanged"/>
    /// </summary>
    public class ProfileListChangedEventArgs
    {
        public IReadOnlyCollection<string> Profiles { get; }

        public ProfileListChangedEventArgs(IReadOnlyCollection<string> profiles)
        {
            Profiles = profiles;
        }
    }
}