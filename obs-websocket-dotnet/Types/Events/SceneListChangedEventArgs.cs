using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.SceneListChanged"/>
    /// </summary>
    public class SceneListChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Updated array of scenes
        /// </summary>
        public List<JsonObject> Scenes { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="scenes">Collection of scene data as JObjects</param>
        public SceneListChangedEventArgs(List<JsonObject> scenes)
        {
            Scenes = scenes;
        }
    }
}