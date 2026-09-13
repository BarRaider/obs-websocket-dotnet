using System;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Bitmask of obs-websocket event categories, sent during identification (and re-identification)
    /// to control which events the server sends. High-volume events are excluded from
    /// <see cref="All"/> and must be requested individually.
    /// </summary>
    [Flags]
    public enum EventSubscription : uint
    {
        /// <summary>
        /// Subscription value used to disable all events
        /// </summary>
        None = 0,

        /// <summary>
        /// Subscription value to receive events in the `General` category
        /// </summary>
        General = 1 << 0,

        /// <summary>
        /// Subscription value to receive events in the `Config` category
        /// </summary>
        Config = 1 << 1,

        /// <summary>
        /// Subscription value to receive events in the `Scenes` category
        /// </summary>
        Scenes = 1 << 2,

        /// <summary>
        /// Subscription value to receive events in the `Inputs` category
        /// </summary>
        Inputs = 1 << 3,

        /// <summary>
        /// Subscription value to receive events in the `Transitions` category
        /// </summary>
        Transitions = 1 << 4,

        /// <summary>
        /// Subscription value to receive events in the `Filters` category
        /// </summary>
        Filters = 1 << 5,

        /// <summary>
        /// Subscription value to receive events in the `Outputs` category
        /// </summary>
        Outputs = 1 << 6,

        /// <summary>
        /// Subscription value to receive events in the `SceneItems` category
        /// </summary>
        SceneItems = 1 << 7,

        /// <summary>
        /// Subscription value to receive events in the `MediaInputs` category
        /// </summary>
        MediaInputs = 1 << 8,

        /// <summary>
        /// Subscription value to receive the `VendorEvent` event
        /// </summary>
        Vendors = 1 << 9,

        /// <summary>
        /// Subscription value to receive events in the `Ui` category
        /// </summary>
        Ui = 1 << 10,

        /// <summary>
        /// Subscription value to receive events in the `Canvases` category
        /// </summary>
        Canvases = 1 << 11,

        /// <summary>
        /// Helper to receive all non-high-volume events
        /// </summary>
        All = General | Config | Scenes | Inputs | Transitions | Filters | Outputs | SceneItems | MediaInputs | Vendors | Ui | Canvases,

        // High-volume events require separate, explicit subscription.

        /// <summary>
        /// Subscription value to receive the `InputVolumeMeters` high-volume event
        /// </summary>
        InputVolumeMeters = 1 << 16,

        /// <summary>
        /// Subscription value to receive the `InputActiveStateChanged` high-volume event
        /// </summary>
        InputActiveStateChanged = 1 << 17,

        /// <summary>
        /// Subscription value to receive the `InputShowStateChanged` high-volume event
        /// </summary>
        InputShowStateChanged = 1 << 18,

        /// <summary>
        /// Subscription value to receive the `SceneItemTransformChanged` high-volume event
        /// </summary>
        SceneItemTransformChanged = 1 << 19
    }
}
