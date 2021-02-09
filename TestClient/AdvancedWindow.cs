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
            obs.SetSourceName(SOURCE_NAME, SOURCE_NAME + "1");
        }

        private async void btnSourceFilters_Click(object sender, EventArgs e)
        {
            try
            {
                LogMessage("GetSourceFilters:");
                var filters = await obs.GetSourceFilters(SOURCE_NAME).ConfigureAwait(false);

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
                LogFilter(await obs.GetSourceFilterInfo(SOURCE_NAME, firstFilter.Name));
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
            // TODO: Add in v4.9
            /*
            string newScene = SOURCE_NAME + random.Next(100);
            
            //_obs.CreateScene(newScene); 
            var createdScene = _obs.GetSceneList().Scenes.FirstOrDefault(s => s.Name == newScene);
            if (createdScene == null)
            {
                LogMessage($"ERROR: Scene was not created!");
                return;
            }
            LogMessage($"Created scene: {createdScene.Name}");
            */
        }

        private async void btnOutputs_Click(object sender, EventArgs e)
        {
            LogMessage("Testing ListOutputs:");
            var outputs = await obs.ListOutputs();
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
            /*
            string outputName = firstOutput.Name;
            
            var retrievedOutput = _obs.GetOutputInfo(outputName);
            LogOutput(retrievedOutput);

            LogMessage("Testing StartOutput:");
            _obs.StartOutput(outputName);
            retrievedOutput = _obs.GetOutputInfo(outputName);
            LogOutput(retrievedOutput);

            LogMessage("Testing StopOutput:");
            _obs.StopOutput(outputName);
            retrievedOutput = _obs.GetOutputInfo(outputName);
            LogOutput(retrievedOutput);*/
        }

        private void LogOutput(OBSOutputInfo output)
        {
            if (output == null)
            {
                LogMessage("ERROR: Output is null!");
                return;
            }
            LogMessage($"Output: {output.Name} Type: {output.Type} Width: {output.Width} Height: {output.Height} Active: {output.Active} Reconnecting: {output.Reconnecting} Congestion: {output.Congestion} TotalFrames: {output.TotalFrames} DroppedFrames: {output.DroppedFrames} TotalBytes: {output.TotalBytes}");
            
            LogMessage($"\tFlags: {output.Flags} Audio: {output.Flags.HasFlag(OutputFlags.Audio)} Video: {output.Flags.HasFlag(OutputFlags.Video)} Encoded: {output.Flags.HasFlag(OutputFlags.Encoded)} MultiTrack: {output.Flags.HasFlag(OutputFlags.Multitrack)} Service: {output.Flags.HasFlag(OutputFlags.UsesService)}");
            //LogMessage($"\tSettings: {output.Settings}");
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
