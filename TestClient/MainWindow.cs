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

            _obs.Connected += onConnect;
            _obs.Disconnected += onDisconnect;

            _obs.SceneChanged += onSceneChange;
            _obs.SceneCollectionChanged += onSceneColChange;
            _obs.ProfileChanged += onProfileChange;
            _obs.TransitionChanged += onTransitionChange;
            _obs.TransitionDurationChanged += onTransitionDurationChange;

            _obs.StreamingStateChanged += onStreamingStateChange;
            _obs.RecordingStateChanged += onRecordingStateChange;

            _obs.StreamStatus += onStreamData;
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
                    onStreamingStateChange(_obs, OutputState.Started);
                else
                    onStreamingStateChange(_obs, OutputState.Stopped);

                if (streamStatus.IsRecording)
                    onRecordingStateChange(_obs, OutputState.Started);
                else
                    onRecordingStateChange(_obs, OutputState.Stopped);
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

        private void onSceneChange(OBSWebsocket sender, string newSceneName)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbCurrentScene.Text = newSceneName;
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

        private void onTransitionChange(OBSWebsocket sender, string newTransitionName)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransition.Text = newTransitionName;
            });
        }

        private void onTransitionDurationChange(OBSWebsocket sender, int newDuration)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransitionDuration.Value = newDuration;
            });
        }

        private void onStreamingStateChange(OBSWebsocket sender, OutputState newState)
        {
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

        private void onRecordingStateChange(OBSWebsocket sender, OutputState newState)
        {
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

        private void onStreamData(OBSWebsocket sender, StreamStatus data)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                txtStreamTime.Text = data.TotalStreamTime.ToString() + " sec";
                txtKbitsSec.Text = data.KbitsPerSec.ToString() + " kbit/s";
                txtBytesSec.Text = data.BytesPerSec.ToString() + " bytes/s";
                txtFramerate.Text = data.FPS.ToString() + " FPS";
                txtStrain.Text = (data.Strain * 100).ToString() + " %";
                txtDroppedFrames.Text = data.DroppedFrames.ToString();
                txtTotalFrames.Text = data.TotalFrames.ToString();
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
            tbCurrentScene.Text = (await _obs.GetCurrentScene()).Name;
        }

        private async void btnSetCurrentScene_Click(object sender, EventArgs e)
        {
            await _obs.SetCurrentScene(tbCurrentScene.Text);
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

        private async void btnToggleStreaming_Click(object sender, EventArgs e)
        {
            await _obs.ToggleStreaming();
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

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            AdvancedWindow advanced = new AdvancedWindow();
            advanced.SetOBS(_obs);
            advanced.ShowDialog();
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            AdvancedWindow advanced = new AdvancedWindow();
            advanced.SetOBS(_obs);
            advanced.ShowDialog();
        }
    }
}