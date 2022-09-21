using System;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputCreated"/>
    /// </summary>
    public class InputCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// Name of the input
        /// </summary>
        public string InputName { get; } 
        
        /// <summary>
        /// The kind of the input
        /// </summary>
        public string InputKind { get; }
        
        /// <summary>
        /// The unversioned kind of input (aka no `_v2` stuff)
        /// </summary>
        public string UnversionedInputKind { get; } 
        
        /// <summary>
        /// The settings configured to the input when it was created
        /// </summary>
        public JObject InputSettings { get; } 
        
        /// <summary>
        /// The default settings for the input
        /// </summary>
        public JObject DefaultInputSettings { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputName">The input name</param>
        /// <param name="inputKind">The kind of input</param>
        /// <param name="unversionedInputKind">The unversioned kind of input</param>
        /// <param name="inputSettings">The input settings as a JObject</param>
        /// <param name="defaultInputSettings">The default input settings as a JObject</param>
        public InputCreatedEventArgs(string inputName, string inputKind, string unversionedInputKind, JObject inputSettings, JObject defaultInputSettings)
        {
            InputName = inputName;
            InputKind = inputKind;
            UnversionedInputKind = unversionedInputKind;
            InputSettings = inputSettings;
            DefaultInputSettings = defaultInputSettings;
        }
    }
}