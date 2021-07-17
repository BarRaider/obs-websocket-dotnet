namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Source volume values
    /// </summary>
    public class SourceVolume
    {
        /// <summary>
        /// The source volume in percent
        /// </summary>
        public float Volume { get; set; }
        /// <summary>
        /// The source volume in decibels
        /// </summary>
        public float VolumeDb { get; set; }
    }
}