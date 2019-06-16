using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Direction to move filters
    /// </summary>
    public enum FilterMovementType
    {
        /// <summary>
        /// Up one
        /// </summary>
        UP,
        /// <summary>
        /// Down one
        /// </summary>
        DOWN,
        /// <summary>
        /// Top of the list
        /// </summary>
        TOP,
        /// <summary>
        /// Bottom of the list
        /// </summary>
        BOTTOM
    }
}
