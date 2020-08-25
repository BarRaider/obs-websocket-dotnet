using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OBSWebsocketDotNet.Types
{
    /// <summary>
    /// Immutable class to be created from HeartBeatEventArgs.
    /// </summary>
    public class HeartBeat
    {

        /// <summary>
        /// Toggles between every JSON message as an "I am alive" indicator.
        /// </summary>
        public readonly bool Pulse;

        /// <summary>
        /// Current active profile.
        /// </summary>
        public readonly string CurrentProfile;

        /// <summary>
        /// Current active scene.
        /// </summary>
        public readonly string CurrentScene;

        /// <summary>
        /// Current streaming state.
        /// </summary>
        public readonly bool Streaming;

        /// <summary>
        /// Total time (in seconds) since the stream started.
        /// </summary>
        public readonly int TotalStreamTime;

        /// <summary>
        /// Total time since the stream started.
        /// </summary>
        public readonly TimeSpan StreamTime;

        /// <summary>
        /// Total bytes sent since the stream started.
        /// </summary>
        public readonly ulong TotalStreamBytes;

        /// <summary>
        /// Total frames streamed since the stream started.
        /// </summary>
        public readonly ulong TotalStreamFrames;

        /// <summary>
        /// Current recording state.
        /// </summary>
        public readonly bool Recording;

        /// <summary>
        /// True if recording is paused.
        /// </summary>
        public readonly bool RecordingPaused;

        /// <summary>
        /// Total time (in seconds) since recording started.
        /// </summary>
        public readonly int TotalRecordTime;

        /// <summary>
        /// Total time since recording started.
        /// </summary>
        public readonly TimeSpan RecordTime;

        /// <summary>
        /// Total bytes recorded since the recording started.
        /// </summary>
        public readonly int TotalRecordBytes;

        /// <summary>
        /// Total frames recorded since the recording started.
        /// </summary>
        public readonly int TotalRecordFrames;

        /// <summary>
        /// Current framerate.
        /// </summary>
        public readonly double FPS;

        /// <summary>
        /// Number of frames rendered
        /// </summary>
        public readonly int RenderTotalFrames;

        /// <summary>
        /// Number of frames missed due to rendering lag
        /// </summary>
        public readonly int RenderMissedFrames;

        /// <summary>
        /// Number of frames outputted
        /// </summary>
        public readonly int OutputTotalFrames;

        /// <summary>
        /// Number of frames skipped due to encoding lag
        /// </summary>
        public readonly int OutputSkippedFrames;

        /// <summary>
        /// Average frame render time (in milliseconds)
        /// </summary>
        public readonly double AverageFrameTime;

        /// <summary>
        /// Current CPU usage (percentage)
        /// </summary>
        public readonly double CpuUsage;

        /// <summary>
        /// Current RAM usage (in megabytes)
        /// </summary>
        public readonly double MemoryUsage;

        /// <summary>
        /// Free recording disk space (in megabytes)
        /// </summary>
        public readonly double FreeDiskSpace;

        /// <summary>
        /// Creates a new <see cref="HeartBeat"/> from <see cref="HeartBeatEventArgs"/>.
        /// </summary>
        /// <param name="args"></param>
        public HeartBeat(HeartBeatEventArgs args)
        {
            Pulse = args.Pulse;
            CurrentProfile = args.CurrentProfile;
            CurrentScene = args.CurrentScene;
            Streaming = args.Streaming;
            TotalStreamTime = args.TotalStreamTime;
            StreamTime = args.StreamTime;
            TotalStreamBytes = args.TotalStreamBytes;
            TotalStreamFrames = args.TotalStreamFrames;
            Recording = args.Recording;
            RecordingPaused = args.RecordingPaused;
            TotalRecordTime = args.TotalRecordTime;
            RecordTime = args.RecordTime;
            TotalRecordBytes = args.TotalRecordBytes;
            TotalRecordFrames = args.TotalRecordFrames;
            FPS = args.Stats.FPS;
            RenderTotalFrames = args.Stats.RenderTotalFrames;
            RenderMissedFrames = args.Stats.RenderMissedFrames;
            OutputTotalFrames = args.Stats.OutputTotalFrames;
            OutputSkippedFrames = args.Stats.OutputSkippedFrames;
            AverageFrameTime = args.Stats.AverageFrameTime;
            CpuUsage = args.Stats.CpuUsage;
            MemoryUsage = args.Stats.MemoryUsage;
            FreeDiskSpace = args.Stats.FreeDiskSpace;
        }
    }
}