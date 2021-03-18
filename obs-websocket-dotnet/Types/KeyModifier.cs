using System;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Key Modifiers
    /// </summary>
    [Flags]
    public enum KeyModifier
    {
        /// <summary>
        /// No modifiers
        /// </summary>
        None    = 0b_0000_0000,  // 0
        
        /// <summary>
        /// Shift Key
        /// </summary>
        Shift   = 0b_0000_0001,  // 1

        /// <summary>
        /// Alt Key
        /// </summary>
        Alt     = 0b_0000_0010,  // 2
        
        /// <summary>
        /// Control Key
        /// </summary>
        Control = 0b_0000_0100,  // 4,
        
        /// <summary>
        /// Command (Mac) / WinKey (?) Windows
        /// </summary>
        Command = 0b_0000_1000   // 8
    }
}
