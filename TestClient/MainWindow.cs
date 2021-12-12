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
using System.Windows.Forms;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using static System.Windows.Forms.AxHost;

namespace TestClient
{
    public partial class MainWindow : Form
    {
        protected OBSWebsocket obs;

        public MainWindow()
        {
            InitializeComponent();
            obs = new OBSWebsocket();

            obs.Connected += onConnect;
            obs.Disconnected += onDisconnect;

            obs.SceneChanged += onSceneChange;
            obs.SceneCollectionChanged += onSceneColChange;
            obs.ProfileChanged += onProfileChange;
            obs.TransitionChanged += onTransitionChange;
            obs.TransitionDurationChanged += onTransitionDurationChange;

            obs.StreamingStateChanged += onStreamingStateChange;
            obs.RecordingStateChanged += onRecordingStateChange;

            obs.VirtualCameraStarted += onVirtualCameraStarted;
            obs.VirtualCameraStopped += onVirtualCameraStopped;

            obs.StreamStatus += onStreamData;
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
                tbFolderPath.Text = obs.GetRecordingFolder().ToString();

                var streamStatus = obs.GetStreamingStatus();
                if (streamStatus.IsStreaming)
                    onStreamingStateChange(obs, OutputState.Started);
                else
                    onStreamingStateChange(obs, OutputState.Stopped);

                if (streamStatus.IsRecording)
                    onRecordingStateChange(obs, OutputState.Started);
                else
                    onRecordingStateChange(obs, OutputState.Stopped);

                var camStatus = obs.GetVirtualCamStatus();
                if (camStatus.IsActive)
                {
                    onVirtualCameraStarted(this, EventArgs.Empty);
                }
                else
                {
                    onVirtualCameraStopped(this, EventArgs.Empty);
                }
            }));
        }

        private void onDisconnect(object sender, Websocket.Client.DisconnectionInfo e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
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
        private void onSceneChange(OBSWebsocket sender, string newSceneName)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    tbCurrentScene.Text = newSceneName;
                });
            }

            private void onSceneColChange(object sender, EventArgs e)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    tbSceneCol.Text = obs.GetCurrentSceneCollection();
                });
            }

            private void onProfileChange(object sender, EventArgs e)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    tbProfile.Text = obs.GetCurrentProfile();
                });
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
                    foreach (var item in scene.Items)
                    {
                        node.Nodes.Add(item.SourceName);
                    }

                    tvScenes.Nodes.Add(node);
                }
            }

            private void btnGetCurrentScene_Click(object sender, EventArgs e)
            {
                tbCurrentScene.Text = obs.GetCurrentScene().Name;
            }

            private void btnSetCurrentScene_Click(object sender, EventArgs e)
            {
                obs.SetCurrentScene(tbCurrentScene.Text);
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
                var sc = obs.ListSceneCollections();

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
                var profiles = obs.ListProfiles();

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
                obs.ToggleStreaming();
            }

            private void btnToggleRecording_Click(object sender, EventArgs e)
            {
                obs.ToggleRecording();
            }

            private void btnListTransitions_Click(object sender, EventArgs e)
            {
                var transitions = obs.ListTransitions();

                tvTransitions.Nodes.Clear();
                foreach (var transition in transitions)
                {
                    tvTransitions.Nodes.Add(transition);
                }
            }

            private void btnGetCurrentTransition_Click(object sender, EventArgs e)
            {
                tbTransition.Text = obs.GetCurrentTransition().Name;
            }

            private void btnSetCurrentTransition_Click(object sender, EventArgs e)
            {
                obs.SetCurrentTransition(tbTransition.Text);
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
                tbTransitionDuration.Value = obs.GetCurrentTransition().Duration;
            }

            private void btnSetTransitionDuration_Click(object sender, EventArgs e)
            {
                obs.SetTransitionDuration((int)tbTransitionDuration.Value);
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
                obs.SetRecordingFolder(tbFolderPath.Text);
            }

            private void onVirtualCameraStopped(object sender, EventArgs e)
            {
                BeginInvoke((MethodInvoker)delegate
            {
                lblVirtualCamStatus.Text = "Stopped";
            });
            }

            private void onVirtualCameraStarted(object sender, EventArgs e)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    lblVirtualCamStatus.Text = "Started";
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