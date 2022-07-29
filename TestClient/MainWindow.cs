/*
    TestClient for obs-websocket-dotnet
    Copyright (C) 2021	Stéphane Lepin, BarRaider

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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

namespace TestClient
{
    public partial class MainWindow : Form
    {
        protected OBSWebsocket obs;


        private CancellationTokenSource _keepAliveTokenSource;
        private readonly int _keepAliveInterval = 500;

        public MainWindow()
        {
            InitializeComponent();
            obs = new OBSWebsocket();

            obs.Connected += onConnect;
            obs.Disconnected += onDisconnect;

            obs.CurrentProgramSceneChanged += onCurrentProgramSceneChanged;
            obs.CurrentSceneCollectionChanged += onSceneCollectionChanged;
            obs.CurrentProfileChanged += onCurrentProfileChanged;
            obs.CurrentSceneTransitionChanged += onCurrentSceneTransitionChanged;
            obs.CurrentSceneTransitionDurationChanged += onCurrentSceneTransitionDurationChanged;

            obs.StreamStateChanged += onStreamStateChanged;
            obs.RecordStateChanged += onRecordStateChanged;

            obs.VirtualcamStateChanged += onVirtualCamStateChanged;
        }

        private void onConnect(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                txtServerIP.Enabled = false;
                txtServerPassword.Enabled = false;
                btnConnect.Text = "Disconnect";

                gbControls.Enabled = true;

                var versionInfo = obs.GetVersion();
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
                tbFolderPath.Text = obs.GetRecordDirectory().ToString();

                var streamStatus = obs.GetStreamStatus();
                if (streamStatus.IsStreaming)
                    onStreamStateChanged(obs, true, nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STARTED));
                else
                    onStreamStateChanged(obs, false, nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED));

                var recordStatus = obs.GetRecordStatus();
                if (recordStatus.IsRecording)
                    onRecordStateChanged(obs, true, nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STARTED));
                else
                    onRecordStateChanged(obs, false, nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED));

                var camStatus = obs.GetVirtualCamStatus();
                if (camStatus.IsActive)
                    onVirtualCamStateChanged(this, true, nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STARTED));
                else
                    onVirtualCamStateChanged(this, false, nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED));

                _keepAliveTokenSource = new CancellationTokenSource();
                CancellationToken keepAliveToken = _keepAliveTokenSource.Token;
                Task statPollKeepAlive = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(_keepAliveInterval);
                        if (keepAliveToken.IsCancellationRequested)
                        {
                            break;
                        }
                        var stats = obs.GetStats();
                    }
                }, keepAliveToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }));
        }

        private void onDisconnect(object sender, Websocket.Client.DisconnectionInfo e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                _keepAliveTokenSource.Cancel();
                gbControls.Enabled = false;

                txtServerIP.Enabled = true;
                txtServerPassword.Enabled = true;
                btnConnect.Text = "Connect";

                if (e.Exception != null)
                {
                    if (e.Exception is AuthFailureException)
                    {
                        MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (e.Exception is ErrorResponseException ere)
                    {
                        MessageBox.Show($"Connection failed: {ere.Message}\nType: {e.Type}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Connection failed: Status: {e.CloseStatus} Desc: {e.CloseStatusDescription}\nType: {e.Type}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }));

        }

        private void onCurrentProgramSceneChanged(OBSWebsocket sender, string newSceneName)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbCurrentScene.Text = newSceneName;
            });
        }

        private void onSceneCollectionChanged(object sender, string sceneCollectionName)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbSceneCol.Text = obs.GetCurrentSceneCollection();
            });
        }

        private void onCurrentProfileChanged(object sender, string profileName)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbProfile.Text = obs.GetCurrentProfile();
            });
        }

        private void onCurrentSceneTransitionChanged(OBSWebsocket sender, string newTransitionName)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransition.Text = newTransitionName;
            });
        }

        private void onCurrentSceneTransitionDurationChanged(OBSWebsocket sender, int newDuration)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransitionDuration.Value = newDuration;
            });
        }

        private void onStreamStateChanged(OBSWebsocket sender, bool outputActive, string outputState)
        {
            string state = "";
            switch (Enum.Parse<OutputState>(outputState))
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTING:
                    state = "Stream starting...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED:
                    state = "Stop streaming";
                    BeginInvoke((MethodInvoker)delegate
                    {
                        gbStatus.Enabled = true;
                    });
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPING:
                    state = "Stream stopping...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED:
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

        private void onRecordStateChanged(OBSWebsocket sender, bool outputActive, string outputState)
        {
            string state = "";
            switch (Enum.Parse<OutputState>(outputState))
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTING:
                    state = "Recording starting...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED:
                    state = "Stop recording";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPING:
                    state = "Recording stopping...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED:
                    state = "Start recording";
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
                // TODO: Need to update these properties with new data
                txtStreamTime.Text = data.TotalStreamTime.ToString() + " sec";
                txtKbitsSec.Text = data.KbitsPerSec.ToString() + " kbit/s";
                txtBytesSec.Text = data.BytesPerSec.ToString() + " bytes/s";
                txtFramerate.Text = data.FPS.ToString() + " FPS";
                txtStrain.Text = (data.Strain * 100).ToString() + " %";
                txtDroppedFrames.Text = data.DroppedFrames.ToString();
                txtTotalFrames.Text = data.TotalFrames.ToString();
            });
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!obs.IsConnected)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        obs.Connect(txtServerIP.Text, txtServerPassword.Text);
                    }
                    catch (Exception ex)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        });
                    }
                });
            }
            else
            {
                obs.Disconnect();
            }
        }

        private void btnListScenes_Click(object sender, EventArgs e)
        {
            var scenes = obs.ListScenes();

            tvScenes.Nodes.Clear();
            foreach (var scene in scenes)
            {
                var node = new TreeNode(scene.Name);
                scene.Items = new List<SceneItemDetails>();
                scene.Items.AddRange(obs.GetSceneItemList(scene.Name));
                foreach (var item in scene.Items)
                {
                    node.Nodes.Add(item.SourceName);
                }

                tvScenes.Nodes.Add(node);
            }
        }

        private void btnGetCurrentScene_Click(object sender, EventArgs e)
        {
            tbCurrentScene.Text = obs.GetCurrentProgramScene().Name;
        }

        private void btnSetCurrentScene_Click(object sender, EventArgs e)
        {
            obs.SetCurrentProgramScene(tbCurrentScene.Text);
        }

        private void tvScenes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbCurrentScene.Text = e.Node.Text;
            }
        }

        private void btnListSceneCol_Click(object sender, EventArgs e)
        {
            var sc = obs.GetSceneCollectionList();

            tvSceneCols.Nodes.Clear();
            foreach (var sceneCol in sc)
            {
                tvSceneCols.Nodes.Add(sceneCol);
            }
        }

        private void btnGetCurrentSceneCol_Click(object sender, EventArgs e)
        {
            tbSceneCol.Text = obs.GetCurrentSceneCollection();
        }

        private void btnSetCurrentSceneCol_Click(object sender, EventArgs e)
        {
            obs.SetCurrentSceneCollection(tbSceneCol.Text);
        }

        private void tvSceneCols_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbSceneCol.Text = e.Node.Text;
            }
        }

        private void btnListProfiles_Click(object sender, EventArgs e)
        {
            var profiles = obs.GetProfileList();

            tvProfiles.Nodes.Clear();
            foreach (var profile in profiles)
            {
                tvProfiles.Nodes.Add(profile);
            }
        }

        private void btnGetCurrentProfile_Click(object sender, EventArgs e)
        {
            tbProfile.Text = obs.GetCurrentProfile();
        }

        private void btnSetCurrentProfile_Click(object sender, EventArgs e)
        {
            obs.SetCurrentProfile(tbProfile.Text);
        }

        private void tvProfiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbProfile.Text = e.Node.Text;
            }
        }

        private void btnToggleStreaming_Click(object sender, EventArgs e)
        {
            obs.ToggleStream();
        }

        private void btnToggleRecording_Click(object sender, EventArgs e)
        {
            obs.ToggleRecord();
        }

        private void btnListTransitions_Click(object sender, EventArgs e)
        {
            var transitions = obs.GetTransitionNameList();

            tvTransitions.Nodes.Clear();
            foreach (var transition in transitions)
            {
                tvTransitions.Nodes.Add(transition);
            }
        }

        private void btnGetCurrentTransition_Click(object sender, EventArgs e)
        {
            tbTransition.Text = obs.GetCurrentSceneTransition().Name;
        }

        private void btnSetCurrentTransition_Click(object sender, EventArgs e)
        {
            obs.SetCurrentSceneTransition(tbTransition.Text);
        }

        private void tvTransitions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                tbTransition.Text = e.Node.Text;
            }
        }

        private void btnGetTransitionDuration_Click(object sender, EventArgs e)
        {
            tbTransitionDuration.Value = obs.GetCurrentSceneTransition().Duration ?? 0;
        }

        private void btnSetTransitionDuration_Click(object sender, EventArgs e)
        {
            obs.SetCurrentSceneTransitionDuration((int)tbTransitionDuration.Value);
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            AdvancedWindow advanced = new AdvancedWindow();
            advanced.SetOBS(obs);
            advanced.Show();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbFolderPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSetPath_Click(object sender, EventArgs e)
        {
            // TODO: Need a method here, or the button must be removed
            //obs.SetRecordingFolder(tbFolderPath.Text);
        }

        private void onVirtualCamStateChanged(object sender, bool outputActive, string outputState)
        {
            string state = "";
            switch (Enum.Parse<OutputState>(outputState))
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTING:
                    state = "VirtualCam starting...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED:
                    state = "VirtualCam Started";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPING:
                    state = "VirtualCam stopping...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED:
                    state = "VirtualCam Stopped";
                    break;

                default:
                    state = "State unknown";
                    break;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                lblVirtualCamStatus.Text = state;
            });
        }

        private void btnVirtualCamStart_Click(object sender, EventArgs e)
        {
            obs.StartVirtualCam();
        }

        private void btnVirtualCamStop_Click(object sender, EventArgs e)
        {
            obs.StopVirtualCam();
        }

        private void btnVirtualCamToggle_Click(object sender, EventArgs e)
        {
            obs.ToggleVirtualCam();
        }
    }
}