using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;
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
        protected OBSWebsocket _obs;

        public void SetOBS(OBSWebsocket obs)
        {
            _obs = obs;
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
            if (_obs == null)
            {
                LogMessage("Error: OBS is null!");
                return;
            }

            _obs.RecordingStateChanged += _obs_RecordingStateChanged;
            _obs.TransitionBegin += _obs_TransitionBegin;
            _obs.TransitionEnd += _obs_TransitionEnd;
            _obs.TransitionVideoEnd += _obs_TransitionVideoEnd;
            _obs.RecordingPaused += _obs_RecordingPaused;
            _obs.RecordingResumed += _obs_RecordingResumed;
            _obs.SourceFilterAdded += _obs_SourceFilterAdded;
            _obs.SourceFilterRemoved += _obs_SourceFilterRemoved;
            _obs.SourceFilterVisibilityChanged += _obs_SourceFilterVisibilityChanged;
            _obs.SourceOrderChanged += _obs_SourceOrderChanged;
            _obs.SourceFiltersReordered += _obs_SourceFiltersReordered;
            _obs.SceneItemLockChanged += _obs_SceneItemLockChanged;
            _obs.SceneItemVisibilityChanged += _obs_SceneItemVisibilityChanged;

        }

        private void _obs_SceneItemVisibilityChanged(OBSWebsocket sender, string sceneName, string itemName, bool isVisible)
        {
            LogMessage($"[SceneItemLockChanged] Scene: {sceneName} Item: {itemName} IsVisible: {isVisible}");
        }

        private void _obs_SceneItemLockChanged(OBSWebsocket sender, string sceneName, string itemName, int itemId, bool isLocked)
        {
            LogMessage($"[SceneItemLockChanged] Scene: {sceneName} Item: {itemName} ItemId: {itemId} IsLocked: {isLocked}");
        }

        private void _obs_SourceFiltersReordered(OBSWebsocket sender, string sourceName, List<OBSWebsocketDotNet.Types.FilterReorderItem> filters)
        {
            LogMessage($"[SourceFiltersReordered] Source: {sourceName}");
            foreach(var filter in filters)
            {
                LogMessage($"\t{filter.Name}");
            }
        }

        private void _obs_SourceOrderChanged(OBSWebsocket sender, string sceneName)
        {
            LogMessage($"[SourceOrderChanged] Scene: {sceneName}");
        }

        private void _obs_SourceFilterVisibilityChanged(OBSWebsocket sender, string sourceName, string filterName, bool filterEnabled)
        {
            LogMessage($"[SourceFilterVisibilityChanged] Source: {sourceName} Filter: {filterName} Visible: {filterEnabled}");
        }

        private void _obs_SourceFilterRemoved(OBSWebsocket sender, string sourceName, string filterName)
        {
            LogMessage($"[SourceFilterRemoved] Source: {sourceName} Filter: {filterName}");
        }

        private void _obs_SourceFilterAdded(OBSWebsocket sender, string sourceName, string filterName, string filterType, JObject filterSettings)
        {
            LogMessage($"[SourceFilterAdded] Source: {sourceName} Filter: {filterName} FilterType: {filterType}{Environment.NewLine}\tSettings: {filterSettings}");
        }

        private void _obs_RecordingResumed(object sender, EventArgs e)
        {
            LogMessage($"[RecordingResumed]");
        }

        private void _obs_RecordingPaused(object sender, EventArgs e)
        {
            LogMessage($"[RecordingPaused]");
        }

        private void _obs_TransitionVideoEnd(OBSWebsocket sender, string transitionName, string transitionType, int duration, string fromScene, string toScene)
        {
            LogMessage($"[TransitionVideoEnd] Name: {transitionName} Type: {transitionType} Duration: {duration} From: {fromScene} To: {toScene}");
        }

        private void _obs_TransitionEnd(OBSWebsocket sender, string transitionName, string transitionType, int duration, string toScene)
        {
            LogMessage($"[TransitionEnd] Name: {transitionName} Type: {transitionType} Duration: {duration} To: {toScene}");
        }

        private void _obs_TransitionBegin(OBSWebsocket sender, string transitionName, string transitionType, int duration, string fromScene, string toScene)
        {
            LogMessage($"[TransitionBegin] Name: {transitionName} Type: {transitionType} Duration: {duration} From: {fromScene} To: {toScene}");
        }

        private void _obs_RecordingStateChanged(OBSWebsocket sender, OBSWebsocketDotNet.Types.OutputState type)
        {
            LogMessage($"[RecordingStateChanged] State: {type}");
        }

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    tbLog.AppendText($"{Environment.NewLine}{message}");
                }));
            }
            else
            {
                tbLog.AppendText($"{Environment.NewLine}{message}");
            }
        }

        private void btnProjector_Click(object sender, EventArgs e)
        {
            const string SOURCE_NAME = "Live";
            const string SCENE_NAME = "Webcam Full";
            _obs.OpenProjector();
            MessageBox.Show("Press Ok to continue");
            _obs.OpenProjector("preview", 0);
            MessageBox.Show("Press Ok to continue");
            // Should not do anything as sceneName only works in "Source" and "Scene"
            _obs.OpenProjector("preview", 0, null, SOURCE_NAME);
            MessageBox.Show("Press Ok to continue");
            _obs.OpenProjector("source", 0, null, SOURCE_NAME);
            MessageBox.Show("Press Ok to continue");
            _obs.OpenProjector("scene", 0, null, SCENE_NAME);
        }
    }
}
