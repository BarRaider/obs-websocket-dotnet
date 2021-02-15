using System;

namespace OBSWebsocketDotNet.Types
{
    [Flags]
    /// <summary>
    /// Key Modifiers
    /// </summary>
    public enum KeyModifier
    {
        None    = 0b_0000_0000,  // 0
        Shift   = 0b_0000_0001,  // 1
        Alt     = 0b_0000_0010,  // 2
        Control = 0b_0000_0100,  // 4,
        Command = 0b_0000_1000   // 8
    }
}
