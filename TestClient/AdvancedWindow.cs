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

            obs.RecordingStateChanged += _obs_RecordingStateChanged;
            obs.TransitionBegin += _obs_TransitionBegin;
            obs.TransitionEnd += _obs_TransitionEnd;
            obs.TransitionVideoEnd += _obs_TransitionVideoEnd;
            obs.SourceFilterAdded += _obs_SourceFilterAdded;
            obs.SourceFilterRemoved += _obs_SourceFilterRemoved;
            obs.SourceFilterVisibilityChanged += _obs_SourceFilterVisibilityChanged;
            obs.SourceOrderChanged += _obs_SourceOrderChanged;
            obs.SourceFiltersReordered += _obs_SourceFiltersReordered;
            obs.SceneItemLockChanged += _obs_SceneItemLockChanged;
            obs.SceneItemVisibilityChanged += _obs_SceneItemVisibilityChanged;
            obs.SourceRenamed += _obs_SourceRenamed;

        }

        private void _obs_SourceRenamed(object sender, SourceRenamedEventArgs e)
        {
            LogMessage($"[SourceRenamed] Previous Name: {e.PreviousName} New Name: {e.NewName}");
        }

        private void _obs_SceneItemVisibilityChanged(object sender, SceneItemVisibilityChangedEventArgs e)
        {
            LogMessage($"[SceneItemLockChanged] Scene: {e.SceneName} Item: {e.ItemName} IsVisible: {e.IsVisible}");
        }

        private void _obs_SceneItemLockChanged(object sender, SceneItemLockChangedEventArgs e)
        {
            LogMessage($"[SceneItemLockChanged] Scene: {e.SceneName} Item: {e.ItemName} ItemId: {e.ItemId} IsLocked: {e.IsLocked}");
        }

        private void _obs_SourceFiltersReordered(object sender, SourceFiltersReorderedEventArgs e)
        {
            LogMessage($"[SourceFiltersReordered] Source: {e.SourceName}");
            foreach (var filter in e.Filters)
            {
                LogMessage($"\t{filter.Name}");
            }
        }

        private void _obs_SourceOrderChanged(object sender, SourceOrderChangedEventArgs e)
        {
            LogMessage($"[SourceOrderChanged] Scene: {e.SceneName}");
        }

        private void _obs_SourceFilterVisibilityChanged(object sender, SourceFilterVisibilityChangedEventArgs e)
        {
            LogMessage($"[SourceFilterVisibilityChanged] Source: {e.SourceName} Filter: {e.FilterName} Visible: {e.FilterEnabled}");
        }

        private void _obs_SourceFilterRemoved(object sender, SourceFilterRemovedEventArgs e)
        {
            LogMessage($"[SourceFilterRemoved] Source: {e.SourceName} Filter: {e.FilterName}");
        }

        private void _obs_SourceFilterAdded(object sender, SourceFilterAddedEventArgs e)
        {
            LogMessage($"[SourceFilterAdded] Source: {e.SourceName} Filter: {e.FilterName} FilterType: {e.FilterType}{Environment.NewLine}\tSettings: {e.FilterSettings}");
        }

        private void _obs_TransitionVideoEnd(object sender, TransitionVideoEndEventArgs e)
        {
            LogMessage($"[TransitionVideoEnd] Name: {e.TransitionName} Type: {e.TransitionType} Duration: {e.Duration} From: {e.FromScene} To: {e.ToScene}");
        }

        private void _obs_TransitionEnd(object sender, TransitionEndEventArgs e)
        {
            LogMessage($"[TransitionEnd] Name: {e.TransitionName} Type: {e.TransitionType} Duration: {e.Duration} To: {e.ToScene}");
        }

        private void _obs_TransitionBegin(object sender, TransitionBeginEventArgs e)
        {
            LogMessage($"[TransitionBegin] Name: {e.TransitionName} Type: {e.TransitionType} Duration: {e.Duration} From: {e.FromScene} To: {e.ToScene}");
        }

        private void _obs_RecordingStateChanged(object sender, OutputStateChangedEventArgs e)
        {
            LogMessage($"[RecordingStateChanged] State: {e.OutputState}");
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

        private async void btnProjector_Click(object sender, EventArgs e)
        {
            try
            {
                const string SCENE_NAME = "Webcam Full";
                await obs.OpenProjector();
                MessageBox.Show("Press Ok to continue");
                await obs.OpenProjector("preview", 0);
                MessageBox.Show("Press Ok to continue");
                // Should not do anything as sceneName only works in "Source" and "Scene"
                await obs.OpenProjector("preview", 0, null, SOURCE_NAME);
                MessageBox.Show("Press Ok to continue");
                await obs.OpenProjector("source", 0, null, SOURCE_NAME);
                MessageBox.Show("Press Ok to continue");
                await obs.OpenProjector("scene", 0, null, SCENE_NAME);
            }
            catch (Exception ex)
            {
                LogMessage($"OpenProjector: Error - {ex.Message}");
            }
        }

        private async void btnRename_Click(object sender, EventArgs e)
        {
            try
            {
                await obs.SetSourceName(SOURCE_NAME, SOURCE_NAME + "1");
            }
            catch (Exception ex)
            {

                LogMessage($"SetSourceName: Error - {ex.Message}");
            }
        }

        private async void btnSourceFilters_Click(object sender, EventArgs e)
        {
            try
            {
                LogMessage("GetSourceFilters:");
                var filters = await obs.GetSourceFilters(SOURCE_NAME);

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
                LogMessage($"GetSourceFilterInfo: Error - {ex.Message}");
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
            
            LogMessage($"\tFlags: {output.Flags}");
            //LogMessage($"\tSettings: {output.Settings}");
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
