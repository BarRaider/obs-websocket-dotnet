using System;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.ProfileListChanged"/>
    /// </summary>
    public class ProfileListChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The profiles that have changed
        /// </summary>
        public List<string> Profiles { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="profiles">Collection of profile names as strings</param>
        public ProfileListChangedEventArgs(List<string> profiles)
        {
            Profiles = profiles;
        }
    }
}