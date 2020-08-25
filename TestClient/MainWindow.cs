/*
    TestClient for obs-websocket-dotnet
    Copyright (C) 2017	Stéphane Lepin <stephane.lepin@gmail.com>

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program. If not, see <https://www.gnu.org/licenses/>
*/

using System;
using System.Text;
using System.Windows.Forms;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

namespace TestClient
{
    public partial class MainWindow : Form
    {
        protected OBSWebsocket _obs;

        public MainWindow()
        {
            InitializeComponent();
            _obs = new OBSWebsocket();
            _obs.OBSError += OnError;
            _obs.Connected += onConnect;
            _obs.Disconnected += onDisconnect;
            _obs.OnEvent += onEvent;

            _obs.SceneChanged += onSceneChange;
            _obs.SceneCollectionChanged += onSceneColChange;
            _obs.ProfileChanged += onProfileChange;
            _obs.TransitionChanged += onTransitionChange;
            _obs.TransitionDurationChanged += onTransitionDurationChange;

            _obs.StreamingStateChanged += onStreamingStateChange;
            _obs.RecordingStateChanged += onRecordingStateChange;

            _obs.StreamStatus += onStreamData;
        }
        readonly StringBuilder ConsoleBuilder = new StringBuilder();
        bool ConsoleActive = true;
        private void onEvent(object sender, Newtonsoft.Json.Linq.JObject e)
        {
            if (!ConsoleActive) return;
            BeginInvoke((MethodInvoker)(() =>
            {
                ConsoleBuilder.Insert(0, $"'{e["update-type"]}' : {e.ToString(Newtonsoft.Json.Formatting.Indented)}");
                tbConsole.Text = ConsoleBuilder.ToString();
            }));
        }

        private void btnToggleConsole_Click(object sender, EventArgs e)
        {
            ConsoleActive = !ConsoleActive;
            if (ConsoleActive)
                btnToggleConsole.Text = "Stop Console";
            else
                btnToggleConsole.Text = "Start Console";
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            ConsoleBuilder.Clear();
            tbConsole.Text = "";
        }

        private void OnError(object sender, OBSErrorEventArgs e)
        {
            string msg = string.Join("\n", e.Message, e.Data?.ToString(), e.Exception);
            MessageBox.Show(msg);
        }

        private void onConnect(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)(async () =>
            {
                txtServerIP.Enabled = false;
                txtServerPassword.Enabled = false;
                btnConnect.Text = "Disconnect";

                gbControls.Enabled = true;

                var versionInfo = await _obs.GetVersion();
                tbPluginVersion.Text = versionInfo.PluginVersion;
                tbOBSVersion.Text = versionInfo.OBSStudioVersion;

                btnListScenes.PerformClick();
                btnGetCurrentScene.PerformClick();

                btnListSceneCol.PerformClick();
                btnGetCurrentSceneCol.PerformClick();

                btnListProfiles.PerformClick();
                btnGetCurrentProfile.PerformClick();

                btnListTransitions.PerformClick();
                btnGetCurrentTransition.PerformClick();

                btnGetTransitionDuration.PerformClick();

                var streamStatus = await _obs.GetStreamingStatus();
                if (streamStatus.IsStreaming)
                    onStreamingStateChange(_obs, new OutputStateChangedEventArgs() { OutputState = OutputState.Started });
                else
                    onStreamingStateChange(_obs, new OutputStateChangedEventArgs() { OutputState = OutputState.Stopped });

                if (streamStatus.IsRecording)
                    onRecordingStateChange(_obs, new OutputStateChangedEventArgs() { OutputState = OutputState.Started });
                else
                    onRecordingStateChange(_obs, new OutputStateChangedEventArgs() { OutputState = OutputState.Stopped });
            }));
        }

        private void onDisconnect(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                gbControls.Enabled = false;

                txtServerIP.Enabled = true;
                txtServerPassword.Enabled = true;
                btnConnect.Text = "Connect";
            }));
        }

        private void onSceneChange(object sender, SceneChangeEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbCurrentScene.Text = e.NewSceneName;
            });
        }

        private async void onSceneColChange(object sender, EventArgs e)
        {
            tbSceneCol.Text = await _obs.GetCurrentSceneCollection();
        }

        private async void onProfileChange(object sender, EventArgs e)
        {

            tbProfile.Text = await _obs.GetCurrentProfile();

        }

        private void onTransitionChange(object sender, TransitionChangeEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransition.Text = e.NewTransitionName;
            });
        }

        private void onTransitionDurationChange(object sender, TransitionDurationChangeEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransitionDuration.Value = e.NewDuration;
            });
        }

        private void onStreamingStateChange(object sender, OutputStateChangedEventArgs e)
        {
            OutputState newState = e.OutputState;
            string state = "";
            switch (newState)
            {
                case OutputState.Starting:
                    state = "Stream starting...";
                    break;

                case OutputState.Started:
                    state = "Stop streaming";
                    BeginInvoke((MethodInvoker)delegate
                    {
                        gbStatus.Enabled = true;
                    });
                    break;

                case OutputState.Stopping:
                    state = "Stream stopping...";
                    break;

                case OutputState.Stopped:
                    state = "Start streaming";
                    BeginInvoke((MethodInvoker)delegate
                    {
                        gbStatus.Enabled = false;
                    });
                    break;

                default:
                    state = "State unknown";
                    break;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                btnToggleStreaming.Text = state;
            });
        }

        private void onRecordingStateChange(object sender, OutputStateChangedEventArgs e)
        {
            OutputState newState = e.OutputState;
            string state = "";
            switch (newState)
            {
                case OutputState.Starting:
                    state = "Recording starting...";
                    break;
                case OutputState.Started:
                    state = "Stop recording";
                    break;
                case OutputState.Stopping:
                    state = "Recording stopping...";
                    break;
                case OutputState.Stopped:
                    state = "Start recording";
                    break;
                case OutputState.Paused:
                    state = "Recording paused...";
                    break;
                case OutputState.Resumed:
                    state = "Stop recording";
                    break;
                default:
                    state = "State unknown";
                    break;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                btnToggleRecording.Text = state;
            });
        }

        private void onStreamData(object sender, StreamStatusEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                txtStreamTime.Text = e.TotalStreamTime.ToString() + " sec";
                txtKbitsSec.Text = e.KbitsPerSec.ToString() + " kbit/s";
                txtBytesSec.Text = e.BytesPerSec.ToString() + " bytes/s";
                txtFramerate.Text = e.FPS.ToString() + " FPS";
                txtStrain.Text = (e.Strain * 100).ToString() + " %";
                txtDroppedFrames.Text = e.DroppedFrames.ToString();
                txtTotalFrames.Text = e.TotalFrames.ToString();
            });
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_obs.IsConnected)
            {
                try
                {
                    await _obs.Connect(txtServerIP.Text, txtServerPassword.Text);
                }
                catch (AuthFailureException)
                {
                    MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                catch (ErrorResponseException ex)
                {
                    MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                _obs.Disconnect();
            }
        }

        private async void btnListScenes_Click(object sender, EventArgs e)
        {
            var scenes = await _obs.ListScenes();

            tvScenes.Nodes.Clear();
            foreach (var scene in scenes)
            {
                var node = new TreeNode(scene.Name);
                foreach (var item in scene.Items)
                {
                    node.Nodes.Add(item.SourceName);
                }

                tvScenes.Nodes.Add(node);
            }
        }

        private async void btnGetCurrentScene_Click(object sender, EventArgs e)
        {
            if (SceneListener != null)
                SceneListener.Cancel();
            tbCurrentScene.Text = (await _obs.GetCurrentScene()).Name;
        }

        AsyncEventListener<bool, SceneChangeEventArgs> SceneListener;

        private async void btnSetCurrentScene_Click(object sender, EventArgs e)
        {
            btnSetCurrentScene.Enabled = false;
            try
            {
                if (SceneListener == null)
                {
                    SceneListener = new AsyncEventListener<bool, SceneChangeEventArgs>((s, args) =>
                    {
                        bool sceneResult = tbCurrentScene.Text == args.NewSceneName;
                        return new EventListenerResult<bool>(sceneResult, true);
                    }, 5000);
                    _obs.SceneChanged += SceneListener.OnEvent;
                }
                else
                    SceneListener.Reset();
                SceneListener.StartListening();
                string currentScene = (await _obs.GetCurrentScene()).Name;
                await _obs.SetCurrentScene(tbCurrentScene.Text);
                if (currentScene == tbCurrentScene.Text)
                {
                    SceneListener.SetResult(true);
                }
                bool result = await SceneListener.Task;
                if (!result)
                    MessageBox.Show("Scene change failed.");
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show($"Timed out: {ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show($"Canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btnSetCurrentScene.Enabled = true;
        }

        private void tvScenes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbCurrentScene.Text = e.Node.Text;
            }
        }

        private async void btnListSceneCol_Click(object sender, EventArgs e)
        {
            var sc = await _obs.ListSceneCollections();

            tvSceneCols.Nodes.Clear();
            foreach (var sceneCol in sc)
            {
                tvSceneCols.Nodes.Add(sceneCol);
            }
        }

        private async void btnGetCurrentSceneCol_Click(object sender, EventArgs e)
        {
            tbSceneCol.Text = await _obs.GetCurrentSceneCollection();
        }

        private async void btnSetCurrentSceneCol_Click(object sender, EventArgs e)
        {

            await _obs.SetCurrentSceneCollection(tbSceneCol.Text);

        }

        private void tvSceneCols_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbSceneCol.Text = e.Node.Text;
            }
        }

        private async void btnListProfiles_Click(object sender, EventArgs e)
        {
            var profiles = await _obs.ListProfiles();

            tvProfiles.Nodes.Clear();
            foreach (var profile in profiles)
            {
                tvProfiles.Nodes.Add(profile);
            }
        }

        private async void btnGetCurrentProfile_Click(object sender, EventArgs e)
        {
            tbProfile.Text = await _obs.GetCurrentProfile();
        }

        private async void btnSetCurrentProfile_Click(object sender, EventArgs e)
        {
            await _obs.SetCurrentProfile(tbProfile.Text);
        }

        private void tvProfiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbProfile.Text = e.Node.Text;
            }
        }

        AsyncEventListener<OutputState, OutputStateChangedEventArgs> StreamListener;

        private async void btnToggleStreaming_Click(object sender, EventArgs e)
        {
            btnToggleStreaming.Enabled = false;
            try
            {
                if (StreamListener == null)
                {
                    StreamListener = new AsyncEventListener<OutputState, OutputStateChangedEventArgs>((s, state) =>
                    {
                        bool finished = state.OutputState == OutputState.Started;
                        return new EventListenerResult<OutputState>(state.OutputState, finished);
                    }, 5000);
                    _obs.StreamingStateChanged += StreamListener.OnEvent;
                }
                else
                    StreamListener.Reset();
                StreamListener.StartListening();
                bool isStreaming = (await _obs.GetStreamingStatus()).IsStreaming;
                await _obs.ToggleStreaming();
                if (isStreaming)
                {
                    StreamListener.SetResult(OutputState.Started);
                }
                OutputState result = await StreamListener.Task;
                if (result != OutputState.Started)
                    MessageBox.Show("StartStreaming failed.");
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show($"Timed out: {ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show($"Canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btnToggleStreaming.Enabled = true;
        }

        private async void btnToggleRecording_Click(object sender, EventArgs e)
        {
            await _obs.ToggleRecording();
        }

        private async void btnListTransitions_Click(object sender, EventArgs e)
        {
            var transitions = await _obs.ListTransitions();

            tvTransitions.Nodes.Clear();
            foreach (var transition in transitions)
            {
                tvTransitions.Nodes.Add(transition);
            }
        }

        private async void btnGetCurrentTransition_Click(object sender, EventArgs e)
        {
            tbTransition.Text = (await _obs.GetCurrentTransition()).Name;
        }

        private async void btnSetCurrentTransition_Click(object sender, EventArgs e)
        {
            await _obs.SetCurrentTransition(tbTransition.Text);
        }

        private void tvTransitions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbTransition.Text = e.Node.Text;
            }
        }

        private async void btnGetTransitionDuration_Click(object sender, EventArgs e)
        {
            tbTransitionDuration.Value = (await _obs.GetCurrentTransition()).Duration;
        }

        private async void btnSetTransitionDuration_Click(object sender, EventArgs e)
        {
            await _obs.SetTransitionDuration((int)tbTransitionDuration.Value);
        }

        private async void btnGetOutput_Click(object sender, EventArgs e)
        {
            string outputName = tbOutput.Text;
            if (!string.IsNullOrEmpty(outputName))
            {
                try
                {
                    var output = await _obs.GetOutput(outputName);
                    MessageBox.Show($"Output: {output.Name} is {(output.Active ? "active." : "inactive.")}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in GetOutput: {ex.Message}");
                }
            }
            else
                MessageBox.Show("An output name must be specified.");
        }

        private async void btnListOutputs_Click(object sender, EventArgs e)
        {
            var outputs = await _obs.ListOutputs();

            tvOutputs.Nodes.Clear();
            foreach (var scene in outputs)
            {
                var node = new TreeNode(scene.Name);
                tvOutputs.Nodes.Add(node);
            }
        }

        private async void btnStartOutput_Click(object sender, EventArgs e)
        {
            string outputName = tbOutput.Text;
            if (!string.IsNullOrEmpty(outputName))
            {
                try
                {
                    await _obs.StartOutput(outputName);
                    MessageBox.Show($"Started {outputName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in StartOutput: {ex.Message}");
                }
            }
            else
                MessageBox.Show("An output name must be specified.");
        }

        private void tvOutputs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbOutput.Text = e.Node.Text;
            }
        }

        private async void btnStopOutput_Click(object sender, EventArgs e)
        {
            string outputName = tbOutput.Text;
            if (!string.IsNullOrEmpty(outputName))
            {
                try
                {
                    await _obs.StopOutput(outputName);
                    MessageBox.Show($"Stopped {outputName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in StartOutput: {ex.Message}");
                }
            }
            else
                MessageBox.Show("An output name must be specified.");
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            AdvancedWindow advanced = new AdvancedWindow();
            advanced.SetOBS(_obs);
            advanced.ShowDialog();
        }

        private async void btnGetStats_Click(object sender, EventArgs e)
        {
            try
            {
                OBSStats stats = await _obs.GetStats();
                MessageBox.Show($"RenderTotalFrames: {stats.RenderTotalFrames}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting stats: {ex.Message}");
            }
        }
    }
}