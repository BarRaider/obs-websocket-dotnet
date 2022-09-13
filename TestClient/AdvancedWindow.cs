using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OBSWebsocketDotNet.Types.Events;

namespace TestClient
{
    public partial class AdvancedWindow : Form
    {
#pragma warning disable IDE1006 // Naming Styles
        // Source to test on
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


            obs.StreamStateChanged += Obs_StreamStateChanged;
            obs.RecordStateChanged += Obs_RecordStateChanged;
            obs.Disconnected += Obs_Disconnected;

            obs.CurrentProgramSceneChanged += Obs_CurrentProgramSceneChanged;
            obs.CurrentPreviewSceneChanged += Obs_CurrentPreviewSceneChanged;
            obs.CurrentSceneCollectionChanged += Obs_CurrentSceneCollectionChanged;
            obs.CurrentSceneTransitionChanged += Obs_CurrentSceneTransitionChanged;
            obs.CurrentSceneTransitionDurationChanged += Obs_CurrentSceneTransitionDurationChanged;
            obs.CurrentProfileChanged += Obs_CurrentProfileChanged;

            obs.InputActiveStateChanged += Obs_InputActiveStateChanged;
            obs.InputMuteStateChanged += Obs_InputMuteStateChanged;
            obs.InputVolumeChanged += OBS_onInputVolumeChanged;

            obs.ProfileListChanged += Obs_ProfileListChanged;
            obs.ReplayBufferStateChanged += Obs_ReplayBufferStateChanged;
            obs.ReplayBufferSaved += Obs_ReplayBufferSaved;

            obs.SceneCollectionListChanged += Obs_SceneCollectionListChanged;

            obs.SceneItemCreated += Obs_SceneItemCreated;
            obs.SceneItemRemoved += Obs_SceneItemRemoved;
            obs.SceneItemSelected += Obs_SceneItemSelected;
            obs.SceneItemLockStateChanged += OBS_onSceneItemLockStateChanged;
            obs.SceneItemEnableStateChanged += OBS_onSceneItemEnableStateChanged;
            obs.SceneItemTransformChanged += Obs_SceneItemTransformChanged;

            obs.SceneItemListReindexed += OBS_onSceneItemListIndexingChanged;
            obs.SceneListChanged += Obs_SceneListChanged;

            obs.SceneTransitionStarted += OBS_onSceneTransitionStarted;
            obs.SceneTransitionEnded += OBS_onSceneTransitionEnded;
            obs.SceneTransitionVideoEnded += OBS_onSceneTransitionVideoEnded;

            obs.SourceFilterCreated += OBS_onSourceFilterCreated;
            obs.SourceFilterEnableStateChanged += OBS_onSourceFilterEnableStateChanged;
            obs.SourceFilterRemoved += OBS_onSourceFilterRemoved;
            obs.SourceFilterListReindexed += OBS_onSourceFilterListReindexed;

            obs.StudioModeStateChanged += Obs_StudioModeStateChanged;

            obs.VirtualcamStateChanged += Obs_VirtualcamStateChanged;
        }

        private void Obs_InputActiveStateChanged(object sender, InputActiveStateChangedEventArgs args)
        {
            LogMessage($"[InputActiveStateChanged] Name: {args.InputName} Video Active: {args.VideoActive}");
        }

        private void Obs_InputMuteStateChanged(object sender, InputMuteStateChangedEventArgs args)
        {
            LogMessage($"[InputMuteStateChanged] Name: {args.InputName} Muted: {args.InputMuted}");
        }

        private void Obs_SceneItemTransformChanged(object sender, SceneItemTransformEventArgs args)
        {
            LogMessage($"[SceneItemTransformChanged] Scene: {args.SceneName} ItemId: {args.SceneItemId} Transform: {args.Transform.X},{args.Transform.Y}");
        }

        private void Obs_SceneItemSelected(object sender, SceneItemSelectedEventArgs args)
        {
            LogMessage($"[SceneItemSelected] Scene: {args.SceneName} ItemId: {args.SceneItemId}");
        }

        private void Obs_Disconnected(object sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            LogMessage($"[OBS DISCONNECTED] CloseCode: {e.ObsCloseCode} Reason: {e.DisconnectReason} Type: {e.WebsocketDisconnectionInfo?.Type} Exception: {e.WebsocketDisconnectionInfo?.Exception?.Message}");
        }

        private void Obs_StudioModeStateChanged(object sender, StudioModeStateChangedEventArgs args)
        {
            LogMessage($"[Preview/Studio ModeChanged] Enabled: {args.StudioModeEnabled}");
        }

        private void Obs_ReplayBufferSaved(object sender, ReplayBufferSavedEventArgs args)
        {
            LogMessage($"[ReplayBufferSaved] Save Location: {args.SavedReplayPath}");
        }

        private void Obs_ReplayBufferStateChanged(object sender, ReplayBufferStateChangedEventArgs args)
        {
            LogMessage($"[ReplayBufferStateChanged] Active: {args.OutputState.IsActive} State: {args.OutputState.StateStr} {args.OutputState.State}");
        }

        private void Obs_RecordStateChanged(object sender, RecordStateChangedEventArgs args)
        {
            LogMessage($"[RecordingStateChanged] Active: {args.OutputState.IsActive} Output: {args.OutputState.OutputPath} State: {args.OutputState.StateStr} {args.OutputState.State}");
        }

        private void Obs_StreamStateChanged(object sender, StreamStateChangedEventArgs args)
        {
            LogMessage($"[StreamStateChanged] Active: {args.OutputState.IsActive} State: {args.OutputState.StateStr} {args.OutputState.State}");
        }

        private void Obs_VirtualcamStateChanged(object sender, VirtualcamStateChangedEventArgs args)
        {
            LogMessage($"[VirtualcamStateChanged] Active: {args.OutputState.IsActive} State: {args.OutputState.StateStr} {args.OutputState.State}");
        }

        private void Obs_ProfileListChanged(object sender, ProfileListChangedEventArgs args)
        {
            LogMessage($"[ProfileListchanged] Count: {args.Profiles.Count}");
            foreach (var profile in args.Profiles)
            {
                LogMessage($"\t{profile}");
            }
        }

        private void Obs_CurrentProfileChanged(object sender, CurrentProfileChangedEventArgs args)
        {
            LogMessage($"[CurrentProfileChanged] Current: {args.ProfileName}");
        }

        private void Obs_CurrentSceneTransitionDurationChanged(object sender, CurrentSceneTransitionDurationChangedEventArgs args)
        {
            LogMessage($"[CurrentSceneTransitionDurationChanged] Current: {args.TransitionDuration}");
        }

        private void Obs_CurrentSceneTransitionChanged(object sender, CurrentSceneTransitionChangedEventArgs args)
        {
            LogMessage($"[CurrentSceneTransitionChanged] Current: {args.TransitionName}");
        }

        private void Obs_SceneCollectionListChanged(object sender, SceneCollectionListChangedEventArgs args)
        {
            LogMessage($"[SceneCollectionListChanged] Count: {args.SceneCollections.Count}");
            foreach (var sc in args.SceneCollections)
            {
                LogMessage($"\t{sc}");
            }
        }

        private void Obs_CurrentSceneCollectionChanged(object sender, CurrentSceneCollectionChangedEventArgs args)
        {
            LogMessage($"[CurrentSceneCollectionChanged] Current: {args.SceneCollectionName}");
        }

        private void Obs_SceneItemRemoved(object sender, SceneItemRemovedEventArgs args)
        {
            LogMessage($"[SceneItemRemoved] Scene: {args.SourceName} Source: {args.SourceName} ItemId: {args.SceneItemId}");
        }

        private void Obs_SceneItemCreated(object sender, SceneItemCreatedEventArgs args)
        {
            LogMessage($"[SceneItemCreated] Scene: {args.SourceName} Source: {args.SourceName} ItemId: {args.SceneItemId} ItemIndex: {args.SceneItemIndex}");
        }

        private void Obs_SceneListChanged(object sender, SceneListChangedEventArgs args)
        {
            LogMessage($"[SceneListChanged] Count: {args.Scenes.Count}");
            foreach (var scene in args.Scenes)
            {
                LogMessage($"\n{scene}");
            }
        }

        private void Obs_CurrentProgramSceneChanged(object sender, ProgramSceneChangedEventArgs args)
        {
            LogMessage($"[SceneChanged] Current: {args.SceneName}");
        }

        private void Obs_CurrentPreviewSceneChanged(object sender, CurrentPreviewSceneChangedEventArgs args)
        {
            LogMessage($"[Preview/Studio SceneChanged] Current: {args.SceneName}");
        }

        private void OBS_onInputVolumeChanged(object sender, InputVolumeChangedEventArgs args)
        {
            LogMessage($"[SourceVolumeChanged] Source: {args.Volume.InputName} Volume: {args.Volume.InputVolumeMul} VolumeDB: {args.Volume.InputVolumeDb}");
        }

        private void OBS_onSceneItemEnableStateChanged(object sender, SceneItemEnableStateChangedEventArgs args)
        {
            LogMessage($"[SceneItemEnableStateChanged] Scene: {args.SceneName} ItemId: {args.SceneItemId} Enabled?: {args.SceneItemEnabled}");
        }

        private void OBS_onSceneItemLockStateChanged(object sender, SceneItemLockStateChangedEventArgs args)
        {
            LogMessage($"[SceneItemLockStateChanged] Scene: {args.SceneName} ItemId: {args.SceneItemId} IsLocked: {args.SceneItemLocked}");
        }

        private void OBS_onSourceFilterListReindexed(object sender, SourceFilterListReindexedEventArgs args)
        {
            LogMessage($"[SourceFilterListReindexed] Source: {args.SourceName}");
            foreach (var filter in args.Filters)
            {
                LogMessage($"\t{filter.Name}");
            }
        }

        private void OBS_onSceneItemListIndexingChanged(object sender, SceneItemListReindexedEventArgs args)
        {
            LogMessage($"[SceneItemListReindexed] Scene: {args.SceneName}{Environment.NewLine}\tSceneItems: {args.SceneItems}");
        }

        private void OBS_onSourceFilterEnableStateChanged(object sender, SourceFilterEnableStateChangedEventArgs args)
        {
            LogMessage($"[SourceFilterEnableStateChanged] Source: {args.SourceName} Filter: {args.FilterName} Enabled: {args.FilterEnabled}");
        }

        private void OBS_onSourceFilterRemoved(object sender, SourceFilterRemovedEventArgs args)
        {
            LogMessage($"[SourceFilterRemoved] Source: {args.SourceName} Filter: {args.FilterName}");
        }

        private void OBS_onSourceFilterCreated(object sender, SourceFilterCreatedEventArgs args)
        {
            LogMessage($"[SourceFilterCreated] Source: {args.SourceName} Filter: {args.FilterName} FilterKind: {args.FilterKind} FilterIndex: {args.FilterIndex}{Environment.NewLine}\tSettings: {args.FilterSettings}{Environment.NewLine}\tDefaultSettings: {args.DefaultFilterSettings}");
        }

        private void OBS_onSceneTransitionVideoEnded(object sender, SceneTransitionVideoEndedEventArgs args)
        {
            LogMessage($"[SceneTransitionVideoEnded] Name: {args.TransitionName}");
        }

        private void OBS_onSceneTransitionEnded(object sender, SceneTransitionEndedEventArgs args)
        {
            LogMessage($"[SceneTransitionEnded] Name: {args.TransitionName}");
        }

        private void OBS_onSceneTransitionStarted(object sender, SceneTransitionStartedEventArgs args)
        {
            LogMessage($"[SceneTransitionStarted] Name: {args.TransitionName}");
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

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSourceName.Text))
            {
                MessageBox.Show("Enter name of Source in input box");
                return;
            }
            string source = tbSourceName.Text;

            var active = obs.GetSourceActive(source).VideoActive;
            LogMessage($"GetSourceActive for {source}: {active}. Renaming source");
            obs.SetInputName(source, source + random.Next(100));
        }


        private void btnSourceInfo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSceneName.Text))
            {
                MessageBox.Show("Enter name of Scene in input box");
                return;
            }

            if (String.IsNullOrEmpty(tbSourceName.Text))
            {
                MessageBox.Show("Enter name of Source in input box");
                return;
            }

            string scene = tbSceneName.Text;
            string source = tbSourceName.Text;

            int itemId = obs.GetSceneItemId(scene, source, 0);
            LogMessage($"Item Id for {source} is {itemId}");

            bool isEnabled = obs.GetSceneItemEnabled(scene, itemId);
            LogMessage($"Source Enabled: {isEnabled}");

            bool isLocked = obs.GetSceneItemLocked(scene, itemId);
            LogMessage($"Source Locked: {isLocked}");

            JObject data = obs.GetSceneItemTransformRaw(scene, itemId);
            LogMessage($"Raw Data: {data}");

            var transform = obs.GetSceneItemTransform(scene, itemId);
            LogMessage($"Transform Object: X: {transform.X}, Y: {transform.Y}, ScaleX: {transform.ScaleX}, ScaleY: {transform.ScaleY}, Height: {transform.Height}, Width: {transform.Width}, SourceHeight: {transform.SourceHeight}, SourceWidth: {transform.SourceWidth}, Rotation: {transform.Rotation}, Crop Top: {transform.CropTop}, Crop Bottom: {transform.CropBottom}, Crop Left: {transform.CropLeft}, Crop Right: {transform.CropRight}");
            LogMessage($"Alignment: {transform.Alignnment}, BoundsHeight: {transform.BoundsHeight}, BoundsWidth: {transform.BoundsWidth}, BoundsType: {transform.BoundsType}");
        }

        private void btnSourceFilters_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(tbSourceName.Text))
                {
                    MessageBox.Show("Enter name of Source in input box");
                    return;
                }
                string source = tbSourceName.Text;

                LogMessage("GetSourceFilters:");
                var filters = obs.GetSourceFilterList(source);

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
                LogFilter(obs.GetSourceFilter(source, firstFilter.Name));
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR: {ex}");
            }
        }

        private void LogFilter(FilterSettings filter)
        {
            LogMessage($"Filter: {filter.Name} Type: {filter.Kind} Enabled: {filter.IsEnabled}{Environment.NewLine}Settings: {filter.Settings}");
        }

        private void btnCreateScene_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSceneName.Text))
            {
                MessageBox.Show("Enter name of Scene in input box");
                return;
            }
            string newScene = tbSceneName.Text;

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
            // TODO refactor for v5.0.0 if possible
            /*LogMessage("Testing ListOutputs:");
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
            }*/

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

        private void btnTransition_Click(object sender, EventArgs e)
        {
            LogMessage($"Getting Transitions");
            var transitions = obs.GetSceneTransitionList();

            LogMessage($"Found {transitions.Transitions.Count} transitions. Active: {transitions.CurrentTransition}");
            var enteringTransition = obs.GetCurrentSceneTransition();
            foreach (var transition in transitions.Transitions)
            {
                obs.SetCurrentSceneTransition(transition.Name);
                var activeTransition = obs.GetCurrentSceneTransition();
                var info = activeTransition.Settings;
                info ??= new JObject();
                LogMessage($"Transition: {transition.Name} has {info.Count} settings");
            }
            obs.SetCurrentSceneTransition(enteringTransition.Name);
        }

        private void btnTracks_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(tbSourceName.Text))
                {
                    MessageBox.Show("Enter name of Source in input box");
                    return;
                }
                string source = tbSourceName.Text;

                LogMessage($"Getting tracks for source {source}:");
                var tracks = obs.GetInputAudioTracks(source);

                if (tracks == null)
                {
                    LogMessage("ERROR: No tracks returned");
                    return;
                }
                LogMessage($"Active Tracks: 1 {tracks.IsTrack1Active}, 2 {tracks.IsTrack2Active}, 3 {tracks.IsTrack3Active}, 4 {tracks.IsTrack4Active}, 5 {tracks.IsTrack5Active}, 6 {tracks.IsTrack6Active}");

                bool trackToggle = !tracks.IsTrack3Active;
                LogMessage($"Setting Track 3 to {trackToggle}");

                // TODO: Get track settings structure to set track values appropriately
                //obs.SetInputAudioTracks(SOURCE_NAME, 3, trackToggle);
                tracks = obs.GetInputAudioTracks(source);
                LogMessage($"Active Tracks: 1 {tracks.IsTrack1Active}, 2 {tracks.IsTrack2Active}, 3 {tracks.IsTrack3Active}, 4 {tracks.IsTrack4Active}, 5 {tracks.IsTrack5Active}, 6 {tracks.IsTrack6Active}");
                LogMessage($"Value is {tracks.IsTrack3Active} expected {trackToggle}");

                if (tracks.IsTrack3Active != trackToggle)
                {
                    LogMessage("ERROR: FAILED!");
                    return;
                }

                trackToggle = !tracks.IsTrack3Active;
                LogMessage($"Setting Track 3 back to to {trackToggle}");

                // TODO: Get track settings structure to set track values appropriately
                //obs.SetInputAudioTracks(SOURCE_NAME, 3, trackToggle);
                tracks = obs.GetInputAudioTracks(source);
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

        private void btnToggleVidCapDvc_Click(object sender, EventArgs e)
        {
            var sceneName = obs.GetCurrentProgramScene();
            var sourceItems = obs.GetSceneItemList(sceneName);
            var vidCapItems = sourceItems.Where(x => x.SourceKind.Equals("dshow_input"));
            var itemListSettings = new List<InputSettings>();
            foreach (var vidCapItem in vidCapItems)
            {
                var enabled = obs.GetSceneItemEnabled(sceneName, vidCapItem.ItemId);
                obs.SetSceneItemEnabled(sceneName, vidCapItem.ItemId, enabled ? false : true);
                LogMessage($"{vidCapItem.SourceName} active button toggled.");
            }
        }

        private void btnGetInputList_Click(object sender, EventArgs e)
        {
            LogMessage("Getting OBS Input List...");
            var inputList = obs.GetInputList();
            foreach (var input in inputList)
            {
                LogMessage($"{input.InputName} {input.InputKind} {input.UnversionedKind}");
            }
            LogMessage("Getting Input Kind list");
            var inputKinds = obs.GetInputKindList();
            foreach (var kind in inputKinds)
            {
                LogMessage($"{kind}\n");
            }
        }

        private void btnGetGroupList_Click(object sender, EventArgs e)
        {
            LogMessage("Getting Group Item List...");
            var groupItems = obs.GetGroupList();
            foreach (var groupItem in groupItems)
            {
                LogMessage(groupItem.ToString());
            }
        }

        private void btnGetMonitorList_Click(object sender, EventArgs e)
        {
            LogMessage("Getting Monitor List...");
            var monitorList = obs.GetMonitorList();
            foreach (var monitor in monitorList)
            {
                LogMessage($"{monitor.Index} {monitor.Name} {monitor.Width}x{monitor.Height} {monitor.PositionX},{monitor.PositionY}");
            }

        }

        private void btnInputInfo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSourceName.Text))
            {
                MessageBox.Show("Enter name of Input in input box");
                return;
            }

            string input = tbSourceName.Text;

            var inputSettings = obs.GetInputSettings(input);
            LogMessage($"Name: {inputSettings.InputName} Kind: {inputSettings.InputKind} \r\nSettings: {inputSettings.Settings}");

            var defaults = obs.GetInputDefaultSettings(inputSettings.InputKind);
            LogMessage($"*** Defaults for {inputSettings.InputKind} ***:\r\n {defaults}");
        }

        private void btnSourcesList_Click(object sender, EventArgs e)
        {
            var scenes = obs.GetSceneList();
            foreach (var scene in scenes.Scenes)
            {
                LogMessage($"Scene: {scene.Name} Index: {scene.Index}");
                var sources = obs.GetSceneItemList(scene.Name);
                LogMessage($"\t Total Items: {sources.Count} \tItems' Names: {String.Join(',', sources.Select(i => i.SourceName).ToArray())}");
            }
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            string output = obs.StopRecord();
            LogMessage($"Stop record: Output is {output}");
        }

        private void btnDoNothing_Click(object sender, EventArgs e)
        {

        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
