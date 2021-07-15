using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class AdvancedWindow : Form
    {
#pragma warning disable IDE1006 // Naming Styles
        // Source to test on
        private const string SOURCE_NAME = "BarRaider";
        private readonly Random random = new Random();

        protected OBSWebsocket obs;

        public void SetOBS(OBSWebsocket obs)
        {
            this.obs = obs;
        }

        public AdvancedWindow()
        {
            InitializeComponent();
        }

        private void AdvancedWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            if (obs == null)
            {
                LogMessage("Error: OBS is null!");
                return;
            }

            obs.RecordingStateChanged += OBS_RecordingStateChanged;
            obs.TransitionBegin += OBS_TransitionBegin;
            obs.TransitionEnd += OBS_TransitionEnd;
            obs.TransitionVideoEnd += OBS_TransitionVideoEnd;
            obs.RecordingPaused += OBS_RecordingPaused;
            obs.RecordingResumed += OBS_RecordingResumed;
            obs.SourceFilterAdded += OBS_SourceFilterAdded;
            obs.SourceFilterRemoved += OBS_SourceFilterRemoved;
            obs.SourceFilterVisibilityChanged += OBS_SourceFilterVisibilityChanged;
            obs.SourceOrderChanged += OBS_SourceOrderChanged;
            obs.SourceFiltersReordered += OBS_SourceFiltersReordered;
            obs.SceneItemLockChanged += OBS_SceneItemLockChanged;
            obs.SceneItemVisibilityChanged += OBS_SceneItemVisibilityChanged;
            obs.SourceRenamed += OBS_SourceRenamed;
        }

        private void OBS_SourceRenamed(OBSWebsocket sender, string newName, string previousName)
        {
            LogMessage($"[SourceRenamed] Previous Name: {previousName} New Name: {newName}");
        }

        private void OBS_SceneItemVisibilityChanged(OBSWebsocket sender, string sceneName, string itemName, bool isVisible)
        {
            LogMessage($"[SceneItemLockChanged] Scene: {sceneName} Item: {itemName} IsVisible: {isVisible}");
        }

        private void OBS_SceneItemLockChanged(OBSWebsocket sender, string sceneName, string itemName, int itemId, bool isLocked)
        {
            LogMessage($"[SceneItemLockChanged] Scene: {sceneName} Item: {itemName} ItemId: {itemId} IsLocked: {isLocked}");
        }

        private void OBS_SourceFiltersReordered(OBSWebsocket sender, string sourceName, List<OBSWebsocketDotNet.Types.FilterReorderItem> filters)
        {
            LogMessage($"[SourceFiltersReordered] Source: {sourceName}");
            foreach(var filter in filters)
            {
                LogMessage($"\t{filter.Name}");
            }
        }

        private void OBS_SourceOrderChanged(OBSWebsocket sender, string sceneName)
        {
            LogMessage($"[SourceOrderChanged] Scene: {sceneName}");
        }

        private void OBS_SourceFilterVisibilityChanged(OBSWebsocket sender, string sourceName, string filterName, bool filterEnabled)
        {
            LogMessage($"[SourceFilterVisibilityChanged] Source: {sourceName} Filter: {filterName} Visible: {filterEnabled}");
        }

        private void OBS_SourceFilterRemoved(OBSWebsocket sender, string sourceName, string filterName)
        {
            LogMessage($"[SourceFilterRemoved] Source: {sourceName} Filter: {filterName}");
        }

        private void OBS_SourceFilterAdded(OBSWebsocket sender, string sourceName, string filterName, string filterType, JObject filterSettings)
        {
            LogMessage($"[SourceFilterAdded] Source: {sourceName} Filter: {filterName} FilterType: {filterType}{Environment.NewLine}\tSettings: {filterSettings}");
        }

        private void OBS_RecordingResumed(object sender, EventArgs e)
        {
            LogMessage($"[RecordingResumed]");
        }

        private void OBS_RecordingPaused(object sender, EventArgs e)
        {
            LogMessage($"[RecordingPaused]");
        }

        private void OBS_TransitionVideoEnd(OBSWebsocket sender, string transitionName, string transitionType, int duration, string fromScene, string toScene)
        {
            LogMessage($"[TransitionVideoEnd] Name: {transitionName} Type: {transitionType} Duration: {duration} From: {fromScene} To: {toScene}");
        }

        private void OBS_TransitionEnd(OBSWebsocket sender, string transitionName, string transitionType, int duration, string toScene)
        {
            LogMessage($"[TransitionEnd] Name: {transitionName} Type: {transitionType} Duration: {duration} To: {toScene}");
        }

        private void OBS_TransitionBegin(OBSWebsocket sender, string transitionName, string transitionType, int duration, string fromScene, string toScene)
        {
            LogMessage($"[TransitionBegin] Name: {transitionName} Type: {transitionType} Duration: {duration} From: {fromScene} To: {toScene}");
        }

        private void OBS_RecordingStateChanged(OBSWebsocket sender, OBSWebsocketDotNet.Types.OutputState type)
        {
            LogMessage($"[RecordingStateChanged] State: {type}");
        }

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    tbLog.AppendText($"{Environment.NewLine}[{DateTime.Now:HH:mm:ss}]{message}");
                }));
            }
            else
            {
                tbLog.AppendText($"{Environment.NewLine}[{DateTime.Now:HH:mm:ss}]{message}");
            }
        }

        private void btnProjector_Click(object sender, EventArgs e)
        {
            const string SCENE_NAME = "Webcam Full";
            obs.OpenProjector();
            MessageBox.Show("Press Ok to continue");
            obs.OpenProjector("preview", 0);
            MessageBox.Show("Press Ok to continue");
            // Should not do anything as sceneName only works in "Source" and "Scene"
            obs.OpenProjector("preview", 0, null, SOURCE_NAME);
            MessageBox.Show("Press Ok to continue");
            obs.OpenProjector("source", 0, null, SOURCE_NAME);
            MessageBox.Show("Press Ok to continue");
            obs.OpenProjector("scene", 0, null, SCENE_NAME);
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var active = obs.GetSourceActive(SOURCE_NAME);
            LogMessage($"GetSourceActive for {SOURCE_NAME}: {active}. Renaming source");
            obs.SetSourceName(SOURCE_NAME, SOURCE_NAME + random.Next(100));
        }

        private void btnSourceFilters_Click(object sender, EventArgs e)
        {
            try
            {
                LogMessage("GetSourceFilters:");
                var filters = obs.GetSourceFilters(SOURCE_NAME);

                foreach (var filter in filters)
                {
                    LogFilter(filter);
                }

                var firstFilter = filters.FirstOrDefault();
                if (firstFilter == null)
                {
                    LogMessage("ERROR: No filters found");
                    return;
                }

                LogMessage("GetSourceFilterInfo:");
                LogFilter(obs.GetSourceFilterInfo(SOURCE_NAME, firstFilter.Name));
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR: {ex}");
            }
        }

        private void LogFilter(FilterSettings filter)
        {
            LogMessage($"Filter: {filter.Name} Type: {filter.Type} Enabled: {filter.IsEnabled}{Environment.NewLine}Settings: {filter.Settings}");
        }

        private void btnCreateScene_Click(object sender, EventArgs e)
        {
            string newScene = SOURCE_NAME + random.Next(100);
            
            obs.CreateScene(newScene); 
            var createdScene = obs.GetSceneList().Scenes.FirstOrDefault(s => s.Name == newScene);
            if (createdScene == null)
            {
                LogMessage($"ERROR: Scene was not created!");
                return;
            }
            LogMessage($"Created scene: {createdScene.Name}");
        }

        private void btnOutputs_Click(object sender, EventArgs e)
        {
            LogMessage("Testing ListOutputs:");
            var outputs = obs.ListOutputs();
            foreach (var output in outputs)
            {
                LogOutput(output);
            }

            LogMessage("Testing GetOutputInfo:");
            var firstOutput = outputs.Skip(1).FirstOrDefault();
            if (firstOutput == null)
            {
                LogMessage($"ERROR: No outputs retrieved!");
                return;
            }

            // TODO: Reuse when properly works on Windows
            /* Output information does not work properly on OBS Websocket Window

            string outputName = firstOutput.Name;
            var retrievedOutput = obs.GetOutputInfo(outputName);
            LogOutput(retrievedOutput);

            LogMessage("Testing StartOutput:");
            obs.StartOutput(outputName);
            retrievedOutput = obs.GetOutputInfo(outputName);
            LogOutput(retrievedOutput);

            LogMessage("Testing StopOutput:");
            obs.StopOutput(outputName);
            retrievedOutput = obs.GetOutputInfo(outputName);
            LogOutput(retrievedOutput);
            */
        }

        private void LogOutput(OBSOutputInfo output)
        {
            if (output == null)
            {
                LogMessage("ERROR: Output is null!");
                return;
            }
            LogMessage($"Output: {output.Name} Type: {output.Type} Width: {output.Width} Height: {output.Height} Active: {output.IsActive} Reconnecting: {output.IsReconnecting} Congestion: {output.Congestion} TotalFrames: {output.TotalFrames} DroppedFrames: {output.DroppedFrames} TotalBytes: {output.TotalBytes}");
            LogMessage($"\tFlags: {output.Flags.RawValue} Audio: {output.Flags.IsAudio} Video: {output.Flags.IsVideo} Encoded: {output.Flags.IsEncoded} MultiTrack: {output.Flags.IsMultiTrack} Service: {output.Flags.IsService}");
            LogMessage($"\tSettings: {output.Settings}");
        }

        private void btnTransition_Click(object sender, EventArgs e)
        {
            LogMessage($"Getting Transitions");
            var transitions = obs.GetTransitionList();

            LogMessage($"Found {transitions.Transitions.Count} transitions. Active: {transitions.CurrentTransition}");
            foreach (var transition in transitions.Transitions)
            {
                var info = obs.GetTransitionSettings(transition.Name);
                LogMessage($"Transition: {transition.Name} has {info.Count} settings");
            }
        }

        private void btnTracks_Click(object sender, EventArgs e)
        {
            try
            {
                LogMessage($"Getting tracks for source {SOURCE_NAME}:");
                var tracks = obs.GetAudioTracks(SOURCE_NAME);
                if (tracks == null)
                {
                    LogMessage("ERROR: No tracks returned");
                    return;
                }
                LogMessage($"Active Tracks: 1 {tracks.IsTrack1Active}, 2 {tracks.IsTrack2Active}, 3 {tracks.IsTrack3Active}, 4 {tracks.IsTrack4Active}, 5 {tracks.IsTrack5Active}, 6 {tracks.IsTrack6Active}");

                bool trackToggle = !tracks.IsTrack3Active;
                LogMessage($"Setting Track 3 to {trackToggle}");

                obs.SetAudioTrack(SOURCE_NAME, 3, trackToggle);
                tracks = obs.GetAudioTracks(SOURCE_NAME);
                LogMessage($"Active Tracks: 1 {tracks.IsTrack1Active}, 2 {tracks.IsTrack2Active}, 3 {tracks.IsTrack3Active}, 4 {tracks.IsTrack4Active}, 5 {tracks.IsTrack5Active}, 6 {tracks.IsTrack6Active}");
                LogMessage($"Value is {tracks.IsTrack3Active} expected {trackToggle}");
                
                if (tracks.IsTrack3Active != trackToggle)
                {
                    LogMessage("ERROR: FAILED!");
                    return;
                }

                trackToggle = !tracks.IsTrack3Active;
                LogMessage($"Setting Track 3 back to to {trackToggle}");

                obs.SetAudioTrack(SOURCE_NAME, 3, trackToggle);
                tracks = obs.GetAudioTracks(SOURCE_NAME);
                LogMessage($"Active Tracks: 1 {tracks.IsTrack1Active}, 2 {tracks.IsTrack2Active}, 3 {tracks.IsTrack3Active}, 4 {tracks.IsTrack4Active}, 5 {tracks.IsTrack5Active}, 6 {tracks.IsTrack6Active}");
                LogMessage($"Value is {tracks.IsTrack3Active} expected {trackToggle}");

                if (tracks.IsTrack3Active != trackToggle)
                {
                    LogMessage("ERROR: FAILED!");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR: {ex}");
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
