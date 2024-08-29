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
using System.Linq;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using OBSWebsocketDotNet.Types.Events;

namespace TestClient
{
    public partial class MainWindow : Form
    {
        protected OBSWebsocket obs;


        private CancellationTokenSource keepAliveTokenSource;
        private readonly int keepAliveInterval = 500;

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
                if (streamStatus.IsActive)
                {
                    onStreamStateChanged(obs, new StreamStateChangedEventArgs(new OutputStateChanged() { IsActive = true, StateStr = nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STARTED) }));
                }
                else
                {
                    onStreamStateChanged(obs, new StreamStateChangedEventArgs(new OutputStateChanged() { IsActive = false, StateStr = nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED) }));
                }

                var recordStatus = obs.GetRecordStatus();
                if (recordStatus.IsRecording)
                {
                    onRecordStateChanged(obs, new RecordStateChangedEventArgs(new RecordStateChanged() { IsActive = true, StateStr = nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STARTED) }));
                }
                else
                {
                    onRecordStateChanged(obs, new RecordStateChangedEventArgs(new RecordStateChanged() { IsActive = false, StateStr = nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED) }));
                }

                var camStatus = obs.GetVirtualCamStatus();
                if (camStatus.IsActive)
                {
                    onVirtualCamStateChanged(this, new VirtualcamStateChangedEventArgs(new OutputStateChanged() { IsActive = true, StateStr = nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STARTED) }));
                }
                else
                {
                    onVirtualCamStateChanged(this, new VirtualcamStateChangedEventArgs(new OutputStateChanged() { IsActive = false, StateStr = nameof(OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED) }));
                }

                keepAliveTokenSource = new CancellationTokenSource();
                CancellationToken keepAliveToken = keepAliveTokenSource.Token;
                Task statPollKeepAlive = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(keepAliveInterval);
                        if (keepAliveToken.IsCancellationRequested)
                        {
                            break;
                        }

                        BeginInvoke((MethodInvoker)(() =>
                        {
                            switch (tabStats.SelectedIndex)
                            {
                                case 0: // OBS
                                    var stats = obs.GetStats();
                                    UpdateOBSStats(stats);
                                    break;
                                case 1: // Stream
                                    var streamStats = obs.GetStreamStatus();
                                    UpdateStreamStats(streamStats);
                                    break;

                                case 2: // Recording
                                    var recStats = obs.GetRecordStatus();
                                    UpdateRecordingStats(recStats);
                                    break;
                            }
                        }));



                    }
                }, keepAliveToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }));
        }

        private void onDisconnect(object sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                if (keepAliveTokenSource != null)
                {
                    keepAliveTokenSource.Cancel();
                }
                gbControls.Enabled = false;

                txtServerIP.Enabled = true;
                txtServerPassword.Enabled = true;
                btnConnect.Text = "Connect";

                if (e.ObsCloseCode == OBSWebsocketDotNet.Communication.ObsCloseCodes.AuthenticationFailed)
                {
                    MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (e.WebsocketDisconnectionInfo != null)
                {
                    if (e.WebsocketDisconnectionInfo.Exception != null)
                    {
                        MessageBox.Show($"Connection failed: CloseCode: {e.ObsCloseCode} Desc: {e.WebsocketDisconnectionInfo?.CloseStatusDescription} Exception:{e.WebsocketDisconnectionInfo?.Exception?.Message}\nType: {e.WebsocketDisconnectionInfo.Type}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show($"Connection failed: CloseCode: {e.ObsCloseCode} Desc: {e.WebsocketDisconnectionInfo?.CloseStatusDescription}\nType: {e.WebsocketDisconnectionInfo.Type}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                    }
                }
                else
                {
                    MessageBox.Show($"Connection failed: CloseCode: {e.ObsCloseCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }));

        }

        private void onCurrentProgramSceneChanged(object sender, ProgramSceneChangedEventArgs args)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbCurrentScene.Text = args.SceneName;
            });
        }

        private void onSceneCollectionChanged(object sender, CurrentSceneCollectionChangedEventArgs args)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbSceneCol.Text = obs.GetCurrentSceneCollection();
            });
        }

        private void onCurrentProfileChanged(object sender, CurrentProfileChangedEventArgs args)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbProfile.Text = obs.GetProfileList().CurrentProfileName;
            });
        }

        private void onCurrentSceneTransitionChanged(object sender, CurrentSceneTransitionChangedEventArgs args)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransition.Text = args.TransitionName;
            });
        }

        private void onCurrentSceneTransitionDurationChanged(object sender, CurrentSceneTransitionDurationChangedEventArgs args)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbTransitionDuration.Value = args.TransitionDuration;
            });
        }

        private void onStreamStateChanged(object sender, StreamStateChangedEventArgs args)
        {
            string state = "";
            switch (args.OutputState.State)
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTING:
                    state = "Stream starting...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED:
                    state = "Stop streaming";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPING:
                    state = "Stream stopping...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED:
                    state = "Start streaming";
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

        private void onRecordStateChanged(object sender, RecordStateChangedEventArgs args)
        {
            string state = "";
            switch (args.OutputState.State)
            {
                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTING:
                    state = "Recording starting...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STARTED:
                case OutputState.OBS_WEBSOCKET_OUTPUT_RESUMED:
                    state = "Stop recording";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPING:
                    state = "Recording stopping...";
                    break;

                case OutputState.OBS_WEBSOCKET_OUTPUT_STOPPED:
                    state = "Start recording";
                    break;
                case OutputState.OBS_WEBSOCKET_OUTPUT_PAUSED:
                    state = "(P) Stop recording";
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!obs.IsConnected)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        obs.ConnectAsync(txtServerIP.Text, txtServerPassword.Text);
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
                var sources = new List<SceneItemDetails>();
                sources.AddRange(obs.GetSceneItemList(scene.Name));
                foreach (var item in sources)
                {
                    node.Nodes.Add(item.SourceName);
                }

                tvScenes.Nodes.Add(node);
            }
        }

        private void btnGetCurrentScene_Click(object sender, EventArgs e)
        {
            tbCurrentScene.Text = obs.GetCurrentProgramScene();
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
            foreach (var profile in profiles.Profiles)
            {
                tvProfiles.Nodes.Add(profile);
            }
        }

        private void btnGetCurrentProfile_Click(object sender, EventArgs e)
        {
            tbProfile.Text = obs.GetProfileList().CurrentProfileName;
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
            var transitions = obs.GetSceneTransitionList();

            tvTransitions.Nodes.Clear();
            foreach (var transition in transitions.Transitions)
            {
                tvTransitions.Nodes.Add(transition.Name);
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

        private void onVirtualCamStateChanged(object sender, VirtualcamStateChangedEventArgs args)
        {
            string state = "";
            switch (args.OutputState.State)
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

        private void UpdateOBSStats(ObsStats data)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblRendered.Text = $"{data.RenderTotalFrames} frames";
                lblMissed.Text = $"{data.RenderMissedFrames} frames";
                lblOutputTotal.Text = $"{data.OutputTotalFrames} frames";
                lblSkipped.Text = $"{data.OutputSkippedFrames} frames";
                lblAvgRender.Text = $"{data.AverageFrameTime:F2} ms";
                lblFPS.Text = $"{(int)data.FPS}";
                lblCPU.Text = $"{data.CpuUsage:F2}%";
                lblMemory.Text = $"{data.MemoryUsage:F2} MB";
                lblDisk.Text = $"{data.FreeDiskSpace:F2} MB";
                lblIncomingMessages.Text = $"{data.SessionIncomingMessages}";
                lblOutgoingMessages.Text = $"{data.SessionOutgoingMessages}";
            });
        }

        private void UpdateStreamStats(OutputStatus data)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblStreamActive.Text = $"{(data.IsActive ? "True" : "False")}";
                lblStreamReconnect.Text = $"{(data.IsReconnecting? "True" : "False")}";
                lblStreamTimeCode.Text = $"{data.TimeCode}";
                lblStreamDuration.Text = $"{data.Duration} ms";
                lblStreamCongestion.Text = $"{data.Congestion:F2}";
                lblStreamTotalFrames.Text = $"{data.TotalFrames} frames";
                lblStreamSkippedFrames.Text = $"{data.SkippedFrames} frames";
                lblStreamOutputBytes.Text = $"{data.BytesSent} bytes";
            });
        }

        private void UpdateRecordingStats(RecordingStatus data)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                lblRecording.Text = $"{(data.IsRecording ? "True" : "False")}";
                lblRecordingPaused.Text = $"{(data.IsRecordingPaused ? "True" : "False")}";
                lblRecordingTimeCode.Text = $"{data.RecordTimecode}";
                lblRecordingDuration.Text = $"{data.RecordingDuration} ms";
                lblRecordingBytes.Text = $"{data.RecordingBytes:F2} bytes";
            });
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += Application_ThreadException;

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"{((Exception)e.ExceptionObject).Message}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"{((Exception)e.Exception).Message}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnPauseRecording_Click(object sender, EventArgs e)
        {
            obs.PauseRecord();
        }
    }
}