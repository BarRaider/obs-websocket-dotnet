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

namespace TestClient
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtServerPassword = new System.Windows.Forms.TextBox();
            this.tvScenes = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetCurrentScene = new System.Windows.Forms.Button();
            this.btnGetCurrentScene = new System.Windows.Forms.Button();
            this.btnListScenes = new System.Windows.Forms.Button();
            this.tbCurrentScene = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lblVirtualCamStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVirtualCamToggle = new System.Windows.Forms.Button();
            this.btnVirtualCamStop = new System.Windows.Forms.Button();
            this.btnVirtualCamStart = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbFolderPath = new System.Windows.Forms.TextBox();
            this.btnSetPath = new System.Windows.Forms.Button();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSetTransitionDuration = new System.Windows.Forms.Button();
            this.btnGetTransitionDuration = new System.Windows.Forms.Button();
            this.tbTransitionDuration = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSetCurrentTransition = new System.Windows.Forms.Button();
            this.btnGetCurrentTransition = new System.Windows.Forms.Button();
            this.tbTransition = new System.Windows.Forms.TextBox();
            this.btnListTransitions = new System.Windows.Forms.Button();
            this.tvTransitions = new System.Windows.Forms.TreeView();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.tabStats = new System.Windows.Forms.TabControl();
            this.obsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOutgoingMessages = new System.Windows.Forms.Label();
            this.lblIncomingMessages = new System.Windows.Forms.Label();
            this.lblDisk = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.lblCPU = new System.Windows.Forms.Label();
            this.lblFPS = new System.Windows.Forms.Label();
            this.lblAvgRender = new System.Windows.Forms.Label();
            this.lblSkipped = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblOutputTotal = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblMissed = new System.Windows.Forms.Label();
            this.lblRendered = new System.Windows.Forms.Label();
            this.streamPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStreamSkippedFrames = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblStreamOutputBytes = new System.Windows.Forms.Label();
            this.lblStreamTotalFrames = new System.Windows.Forms.Label();
            this.lblStreamCongestion = new System.Windows.Forms.Label();
            this.lblStreamDuration = new System.Windows.Forms.Label();
            this.lblStreamTimeCode = new System.Windows.Forms.Label();
            this.lblStreamReconnect = new System.Windows.Forms.Label();
            this.lblStreamActive = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.recPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRecordingBytes = new System.Windows.Forms.Label();
            this.lblRecordingDuration = new System.Windows.Forms.Label();
            this.lblRecordingTimeCode = new System.Windows.Forms.Label();
            this.lblRecordingPaused = new System.Windows.Forms.Label();
            this.lblRecording = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnPauseRecording = new System.Windows.Forms.Button();
            this.btnToggleRecording = new System.Windows.Forms.Button();
            this.btnToggleStreaming = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSetCurrentProfile = new System.Windows.Forms.Button();
            this.btnGetCurrentProfile = new System.Windows.Forms.Button();
            this.tbProfile = new System.Windows.Forms.TextBox();
            this.btnListProfiles = new System.Windows.Forms.Button();
            this.tvProfiles = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetCurrentSceneCol = new System.Windows.Forms.Button();
            this.btnGetCurrentSceneCol = new System.Windows.Forms.Button();
            this.tbSceneCol = new System.Windows.Forms.TextBox();
            this.btnListSceneCol = new System.Windows.Forms.Button();
            this.tvSceneCols = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbOBSVersion = new System.Windows.Forms.Label();
            this.tbPluginVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.groupBox1.SuspendLayout();
            this.gbControls.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransitionDuration)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.gbStatus.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.obsPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.streamPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.recPage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(681, 8);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 36);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerIP.Location = new System.Drawing.Point(271, 11);
            this.txtServerIP.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(150, 27);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "ws://127.0.0.1:4455";
            // 
            // txtServerPassword
            // 
            this.txtServerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerPassword.Location = new System.Drawing.Point(521, 11);
            this.txtServerPassword.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtServerPassword.Name = "txtServerPassword";
            this.txtServerPassword.Size = new System.Drawing.Size(150, 27);
            this.txtServerPassword.TabIndex = 3;
            this.txtServerPassword.UseSystemPasswordChar = true;
            // 
            // tvScenes
            // 
            this.tvScenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvScenes.Location = new System.Drawing.Point(8, 76);
            this.tvScenes.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tvScenes.Name = "tvScenes";
            this.tvScenes.Size = new System.Drawing.Size(182, 180);
            this.tvScenes.TabIndex = 4;
            this.tvScenes.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvScenes_NodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetCurrentScene);
            this.groupBox1.Controls.Add(this.btnGetCurrentScene);
            this.groupBox1.Controls.Add(this.btnListScenes);
            this.groupBox1.Controls.Add(this.tvScenes);
            this.groupBox1.Controls.Add(this.tbCurrentScene);
            this.groupBox1.Location = new System.Drawing.Point(8, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(200, 373);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scene List with Items";
            // 
            // btnSetCurrentScene
            // 
            this.btnSetCurrentScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetCurrentScene.Location = new System.Drawing.Point(104, 307);
            this.btnSetCurrentScene.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetCurrentScene.Name = "btnSetCurrentScene";
            this.btnSetCurrentScene.Size = new System.Drawing.Size(88, 59);
            this.btnSetCurrentScene.TabIndex = 1;
            this.btnSetCurrentScene.Text = "Set\r\nCurScene";
            this.btnSetCurrentScene.UseVisualStyleBackColor = true;
            this.btnSetCurrentScene.Click += new System.EventHandler(this.btnSetCurrentScene_Click);
            // 
            // btnGetCurrentScene
            // 
            this.btnGetCurrentScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetCurrentScene.Location = new System.Drawing.Point(8, 307);
            this.btnGetCurrentScene.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetCurrentScene.Name = "btnGetCurrentScene";
            this.btnGetCurrentScene.Size = new System.Drawing.Size(82, 59);
            this.btnGetCurrentScene.TabIndex = 2;
            this.btnGetCurrentScene.Text = "Get\r\nCurScene";
            this.btnGetCurrentScene.UseVisualStyleBackColor = true;
            this.btnGetCurrentScene.Click += new System.EventHandler(this.btnGetCurrentScene_Click);
            // 
            // btnListScenes
            // 
            this.btnListScenes.Location = new System.Drawing.Point(9, 31);
            this.btnListScenes.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnListScenes.Name = "btnListScenes";
            this.btnListScenes.Size = new System.Drawing.Size(101, 36);
            this.btnListScenes.TabIndex = 5;
            this.btnListScenes.Text = "ListScenes";
            this.btnListScenes.UseVisualStyleBackColor = true;
            this.btnListScenes.Click += new System.EventHandler(this.btnListScenes_Click);
            // 
            // tbCurrentScene
            // 
            this.tbCurrentScene.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCurrentScene.Location = new System.Drawing.Point(8, 267);
            this.tbCurrentScene.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbCurrentScene.Name = "tbCurrentScene";
            this.tbCurrentScene.Size = new System.Drawing.Size(182, 27);
            this.tbCurrentScene.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP:PORT :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(440, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password :";
            // 
            // gbControls
            // 
            this.gbControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbControls.Controls.Add(this.groupBox9);
            this.gbControls.Controls.Add(this.groupBox8);
            this.gbControls.Controls.Add(this.btnAdvanced);
            this.gbControls.Controls.Add(this.groupBox7);
            this.gbControls.Controls.Add(this.groupBox6);
            this.gbControls.Controls.Add(this.gbStatus);
            this.gbControls.Controls.Add(this.groupBox5);
            this.gbControls.Controls.Add(this.groupBox4);
            this.gbControls.Controls.Add(this.groupBox2);
            this.gbControls.Controls.Add(this.groupBox3);
            this.gbControls.Controls.Add(this.groupBox1);
            this.gbControls.Enabled = false;
            this.gbControls.Location = new System.Drawing.Point(15, 52);
            this.gbControls.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbControls.Name = "gbControls";
            this.gbControls.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbControls.Size = new System.Drawing.Size(766, 856);
            this.gbControls.TabIndex = 9;
            this.gbControls.TabStop = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.lblVirtualCamStatus);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.btnVirtualCamToggle);
            this.groupBox9.Controls.Add(this.btnVirtualCamStop);
            this.groupBox9.Controls.Add(this.btnVirtualCamStart);
            this.groupBox9.Location = new System.Drawing.Point(562, 571);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox9.Size = new System.Drawing.Size(194, 89);
            this.groupBox9.TabIndex = 13;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Virtual Cam";
            // 
            // lblVirtualCamStatus
            // 
            this.lblVirtualCamStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVirtualCamStatus.Location = new System.Drawing.Point(65, 23);
            this.lblVirtualCamStatus.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblVirtualCamStatus.Name = "lblVirtualCamStatus";
            this.lblVirtualCamStatus.Size = new System.Drawing.Size(122, 20);
            this.lblVirtualCamStatus.TabIndex = 8;
            this.lblVirtualCamStatus.Text = "Unknown";
            this.lblVirtualCamStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Status:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnVirtualCamToggle
            // 
            this.btnVirtualCamToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVirtualCamToggle.Location = new System.Drawing.Point(125, 44);
            this.btnVirtualCamToggle.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnVirtualCamToggle.Name = "btnVirtualCamToggle";
            this.btnVirtualCamToggle.Size = new System.Drawing.Size(64, 36);
            this.btnVirtualCamToggle.TabIndex = 3;
            this.btnVirtualCamToggle.Text = "Toggle";
            this.btnVirtualCamToggle.UseVisualStyleBackColor = true;
            this.btnVirtualCamToggle.Click += new System.EventHandler(this.btnVirtualCamToggle_Click);
            // 
            // btnVirtualCamStop
            // 
            this.btnVirtualCamStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVirtualCamStop.Location = new System.Drawing.Point(67, 44);
            this.btnVirtualCamStop.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnVirtualCamStop.Name = "btnVirtualCamStop";
            this.btnVirtualCamStop.Size = new System.Drawing.Size(54, 36);
            this.btnVirtualCamStop.TabIndex = 2;
            this.btnVirtualCamStop.Text = "Stop";
            this.btnVirtualCamStop.UseVisualStyleBackColor = true;
            this.btnVirtualCamStop.Click += new System.EventHandler(this.btnVirtualCamStop_Click);
            // 
            // btnVirtualCamStart
            // 
            this.btnVirtualCamStart.Location = new System.Drawing.Point(7, 44);
            this.btnVirtualCamStart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnVirtualCamStart.Name = "btnVirtualCamStart";
            this.btnVirtualCamStart.Size = new System.Drawing.Size(55, 36);
            this.btnVirtualCamStart.TabIndex = 1;
            this.btnVirtualCamStart.Text = "Start";
            this.btnVirtualCamStart.UseVisualStyleBackColor = true;
            this.btnVirtualCamStart.Click += new System.EventHandler(this.btnVirtualCamStart_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnBrowse);
            this.groupBox8.Controls.Add(this.tbFolderPath);
            this.groupBox8.Controls.Add(this.btnSetPath);
            this.groupBox8.Location = new System.Drawing.Point(8, 776);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox8.Size = new System.Drawing.Size(482, 68);
            this.groupBox8.TabIndex = 15;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Recordings Directory";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(277, 29);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(38, 31);
            this.btnBrowse.TabIndex = 17;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbFolderPath
            // 
            this.tbFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolderPath.Location = new System.Drawing.Point(9, 29);
            this.tbFolderPath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbFolderPath.Name = "tbFolderPath";
            this.tbFolderPath.Size = new System.Drawing.Size(265, 27);
            this.tbFolderPath.TabIndex = 15;
            // 
            // btnSetPath
            // 
            this.btnSetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetPath.Enabled = false;
            this.btnSetPath.Location = new System.Drawing.Point(365, 28);
            this.btnSetPath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetPath.Name = "btnSetPath";
            this.btnSetPath.Size = new System.Drawing.Size(107, 31);
            this.btnSetPath.TabIndex = 16;
            this.btnSetPath.Text = "Set Path";
            this.btnSetPath.UseVisualStyleBackColor = true;
            this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(657, 799);
            this.btnAdvanced.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(101, 36);
            this.btnAdvanced.TabIndex = 13;
            this.btnAdvanced.Text = "Advanced >>";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnSetTransitionDuration);
            this.groupBox7.Controls.Add(this.btnGetTransitionDuration);
            this.groupBox7.Controls.Add(this.tbTransitionDuration);
            this.groupBox7.Location = new System.Drawing.Point(563, 668);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox7.Size = new System.Drawing.Size(194, 81);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Transition Duration";
            // 
            // btnSetTransitionDuration
            // 
            this.btnSetTransitionDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetTransitionDuration.Location = new System.Drawing.Point(129, 33);
            this.btnSetTransitionDuration.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetTransitionDuration.Name = "btnSetTransitionDuration";
            this.btnSetTransitionDuration.Size = new System.Drawing.Size(54, 36);
            this.btnSetTransitionDuration.TabIndex = 2;
            this.btnSetTransitionDuration.Text = "Set";
            this.btnSetTransitionDuration.UseVisualStyleBackColor = true;
            this.btnSetTransitionDuration.Click += new System.EventHandler(this.btnSetTransitionDuration_Click);
            // 
            // btnGetTransitionDuration
            // 
            this.btnGetTransitionDuration.Location = new System.Drawing.Point(9, 33);
            this.btnGetTransitionDuration.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetTransitionDuration.Name = "btnGetTransitionDuration";
            this.btnGetTransitionDuration.Size = new System.Drawing.Size(55, 36);
            this.btnGetTransitionDuration.TabIndex = 1;
            this.btnGetTransitionDuration.Text = "Get";
            this.btnGetTransitionDuration.UseVisualStyleBackColor = true;
            this.btnGetTransitionDuration.Click += new System.EventHandler(this.btnGetTransitionDuration_Click);
            // 
            // tbTransitionDuration
            // 
            this.tbTransitionDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTransitionDuration.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.tbTransitionDuration.Location = new System.Drawing.Point(67, 36);
            this.tbTransitionDuration.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbTransitionDuration.Maximum = new decimal(new int[] {
            120000,
            0,
            0,
            0});
            this.tbTransitionDuration.Name = "tbTransitionDuration";
            this.tbTransitionDuration.Size = new System.Drawing.Size(53, 27);
            this.tbTransitionDuration.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSetCurrentTransition);
            this.groupBox6.Controls.Add(this.btnGetCurrentTransition);
            this.groupBox6.Controls.Add(this.tbTransition);
            this.groupBox6.Controls.Add(this.btnListTransitions);
            this.groupBox6.Controls.Add(this.tvTransitions);
            this.groupBox6.Location = new System.Drawing.Point(234, 401);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox6.Size = new System.Drawing.Size(256, 373);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Transitions";
            // 
            // btnSetCurrentTransition
            // 
            this.btnSetCurrentTransition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetCurrentTransition.Location = new System.Drawing.Point(138, 307);
            this.btnSetCurrentTransition.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetCurrentTransition.Name = "btnSetCurrentTransition";
            this.btnSetCurrentTransition.Size = new System.Drawing.Size(107, 59);
            this.btnSetCurrentTransition.TabIndex = 5;
            this.btnSetCurrentTransition.Text = "Set\r\nCurTransition";
            this.btnSetCurrentTransition.UseVisualStyleBackColor = true;
            this.btnSetCurrentTransition.Click += new System.EventHandler(this.btnSetCurrentTransition_Click);
            // 
            // btnGetCurrentTransition
            // 
            this.btnGetCurrentTransition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetCurrentTransition.Location = new System.Drawing.Point(8, 307);
            this.btnGetCurrentTransition.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetCurrentTransition.Name = "btnGetCurrentTransition";
            this.btnGetCurrentTransition.Size = new System.Drawing.Size(114, 59);
            this.btnGetCurrentTransition.TabIndex = 4;
            this.btnGetCurrentTransition.Text = "Get\r\nCurTransition";
            this.btnGetCurrentTransition.UseVisualStyleBackColor = true;
            this.btnGetCurrentTransition.Click += new System.EventHandler(this.btnGetCurrentTransition_Click);
            // 
            // tbTransition
            // 
            this.tbTransition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTransition.Location = new System.Drawing.Point(8, 267);
            this.tbTransition.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbTransition.Name = "tbTransition";
            this.tbTransition.Size = new System.Drawing.Size(237, 27);
            this.tbTransition.TabIndex = 3;
            // 
            // btnListTransitions
            // 
            this.btnListTransitions.Location = new System.Drawing.Point(8, 29);
            this.btnListTransitions.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnListTransitions.Name = "btnListTransitions";
            this.btnListTransitions.Size = new System.Drawing.Size(114, 36);
            this.btnListTransitions.TabIndex = 2;
            this.btnListTransitions.Text = "ListTransitions";
            this.btnListTransitions.UseVisualStyleBackColor = true;
            this.btnListTransitions.Click += new System.EventHandler(this.btnListTransitions_Click);
            // 
            // tvTransitions
            // 
            this.tvTransitions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvTransitions.Location = new System.Drawing.Point(8, 76);
            this.tvTransitions.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tvTransitions.Name = "tvTransitions";
            this.tvTransitions.Size = new System.Drawing.Size(238, 180);
            this.tvTransitions.TabIndex = 1;
            this.tvTransitions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTransitions_NodeMouseClick);
            // 
            // gbStatus
            // 
            this.gbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStatus.Controls.Add(this.tabStats);
            this.gbStatus.Location = new System.Drawing.Point(453, 19);
            this.gbStatus.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbStatus.Size = new System.Drawing.Size(305, 269);
            this.gbStatus.TabIndex = 11;
            this.gbStatus.TabStop = false;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.obsPage);
            this.tabStats.Controls.Add(this.streamPage);
            this.tabStats.Controls.Add(this.recPage);
            this.tabStats.Location = new System.Drawing.Point(4, 0);
            this.tabStats.Name = "tabStats";
            this.tabStats.SelectedIndex = 0;
            this.tabStats.Size = new System.Drawing.Size(294, 262);
            this.tabStats.TabIndex = 1;
            // 
            // obsPage
            // 
            this.obsPage.Controls.Add(this.tableLayoutPanel3);
            this.obsPage.Location = new System.Drawing.Point(4, 29);
            this.obsPage.Name = "obsPage";
            this.obsPage.Padding = new System.Windows.Forms.Padding(3);
            this.obsPage.Size = new System.Drawing.Size(286, 229);
            this.obsPage.TabIndex = 0;
            this.obsPage.Text = "OBS";
            this.obsPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.lblOutgoingMessages, 1, 10);
            this.tableLayoutPanel3.Controls.Add(this.lblIncomingMessages, 1, 9);
            this.tableLayoutPanel3.Controls.Add(this.lblDisk, 1, 8);
            this.tableLayoutPanel3.Controls.Add(this.lblMemory, 1, 7);
            this.tableLayoutPanel3.Controls.Add(this.lblCPU, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.lblFPS, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.lblAvgRender, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.lblSkipped, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label26, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label25, 0, 10);
            this.tableLayoutPanel3.Controls.Add(this.label24, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label23, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.label22, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.lblOutputTotal, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label20, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.label19, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label18, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.label17, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label15, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblMissed, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblRendered, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 13;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(286, 215);
            this.tableLayoutPanel3.TabIndex = 0;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // lblOutgoingMessages
            // 
            this.lblOutgoingMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutgoingMessages.AutoSize = true;
            this.lblOutgoingMessages.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOutgoingMessages.Location = new System.Drawing.Point(139, 199);
            this.lblOutgoingMessages.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOutgoingMessages.Name = "lblOutgoingMessages";
            this.lblOutgoingMessages.Size = new System.Drawing.Size(142, 19);
            this.lblOutgoingMessages.TabIndex = 23;
            this.lblOutgoingMessages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIncomingMessages
            // 
            this.lblIncomingMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIncomingMessages.AutoSize = true;
            this.lblIncomingMessages.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblIncomingMessages.Location = new System.Drawing.Point(139, 180);
            this.lblIncomingMessages.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblIncomingMessages.Name = "lblIncomingMessages";
            this.lblIncomingMessages.Size = new System.Drawing.Size(142, 19);
            this.lblIncomingMessages.TabIndex = 22;
            this.lblIncomingMessages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDisk
            // 
            this.lblDisk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDisk.AutoSize = true;
            this.lblDisk.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDisk.Location = new System.Drawing.Point(139, 160);
            this.lblDisk.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDisk.Name = "lblDisk";
            this.lblDisk.Size = new System.Drawing.Size(142, 19);
            this.lblDisk.TabIndex = 21;
            this.lblDisk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMemory
            // 
            this.lblMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMemory.AutoSize = true;
            this.lblMemory.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMemory.Location = new System.Drawing.Point(139, 140);
            this.lblMemory.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(142, 19);
            this.lblMemory.TabIndex = 20;
            this.lblMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPU
            // 
            this.lblCPU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCPU.AutoSize = true;
            this.lblCPU.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCPU.Location = new System.Drawing.Point(139, 120);
            this.lblCPU.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCPU.Name = "lblCPU";
            this.lblCPU.Size = new System.Drawing.Size(142, 19);
            this.lblCPU.TabIndex = 19;
            this.lblCPU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFPS
            // 
            this.lblFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFPS.AutoSize = true;
            this.lblFPS.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFPS.Location = new System.Drawing.Point(139, 100);
            this.lblFPS.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFPS.Name = "lblFPS";
            this.lblFPS.Size = new System.Drawing.Size(142, 19);
            this.lblFPS.TabIndex = 18;
            this.lblFPS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAvgRender
            // 
            this.lblAvgRender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvgRender.AutoSize = true;
            this.lblAvgRender.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAvgRender.Location = new System.Drawing.Point(139, 80);
            this.lblAvgRender.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAvgRender.Name = "lblAvgRender";
            this.lblAvgRender.Size = new System.Drawing.Size(142, 19);
            this.lblAvgRender.TabIndex = 17;
            this.lblAvgRender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSkipped
            // 
            this.lblSkipped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSkipped.AutoSize = true;
            this.lblSkipped.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSkipped.Location = new System.Drawing.Point(139, 60);
            this.lblSkipped.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSkipped.Name = "lblSkipped";
            this.lblSkipped.Size = new System.Drawing.Size(142, 19);
            this.lblSkipped.TabIndex = 16;
            this.lblSkipped.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label26.Location = new System.Drawing.Point(5, 20);
            this.label26.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(124, 19);
            this.label26.TabIndex = 15;
            this.label26.Text = "Missed:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label25.Location = new System.Drawing.Point(5, 199);
            this.label25.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(124, 19);
            this.label25.TabIndex = 14;
            this.label25.Text = "OUT Messages:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label24.Location = new System.Drawing.Point(5, 0);
            this.label24.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(124, 19);
            this.label24.TabIndex = 13;
            this.label24.Text = "Frames Rendered:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label23.Location = new System.Drawing.Point(5, 160);
            this.label23.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(124, 19);
            this.label23.TabIndex = 12;
            this.label23.Text = "Free Disk:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(5, 180);
            this.label22.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(124, 19);
            this.label22.TabIndex = 11;
            this.label22.Text = "IN Messages:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOutputTotal
            // 
            this.lblOutputTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputTotal.AutoSize = true;
            this.lblOutputTotal.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOutputTotal.Location = new System.Drawing.Point(139, 40);
            this.lblOutputTotal.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOutputTotal.Name = "lblOutputTotal";
            this.lblOutputTotal.Size = new System.Drawing.Size(142, 19);
            this.lblOutputTotal.TabIndex = 10;
            this.lblOutputTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label20.Location = new System.Drawing.Point(5, 140);
            this.label20.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 19);
            this.label20.TabIndex = 9;
            this.label20.Text = "Memory:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label19.Location = new System.Drawing.Point(5, 100);
            this.label19.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(124, 19);
            this.label19.TabIndex = 8;
            this.label19.Text = "FPS:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(5, 120);
            this.label18.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(124, 19);
            this.label18.TabIndex = 7;
            this.label18.Text = "CPU:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(5, 80);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(124, 19);
            this.label17.TabIndex = 6;
            this.label17.Text = "Avg Render Time:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(5, 40);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(124, 19);
            this.label16.TabIndex = 5;
            this.label16.Text = "Output Total:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(5, 60);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(124, 19);
            this.label15.TabIndex = 4;
            this.label15.Text = "Skipped:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMissed
            // 
            this.lblMissed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMissed.AutoSize = true;
            this.lblMissed.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMissed.Location = new System.Drawing.Point(139, 20);
            this.lblMissed.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMissed.Name = "lblMissed";
            this.lblMissed.Size = new System.Drawing.Size(142, 19);
            this.lblMissed.TabIndex = 3;
            this.lblMissed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRendered
            // 
            this.lblRendered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRendered.AutoSize = true;
            this.lblRendered.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRendered.Location = new System.Drawing.Point(139, 0);
            this.lblRendered.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRendered.Name = "lblRendered";
            this.lblRendered.Size = new System.Drawing.Size(142, 19);
            this.lblRendered.TabIndex = 2;
            this.lblRendered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // streamPage
            // 
            this.streamPage.Controls.Add(this.tableLayoutPanel2);
            this.streamPage.Location = new System.Drawing.Point(4, 29);
            this.streamPage.Name = "streamPage";
            this.streamPage.Padding = new System.Windows.Forms.Padding(3);
            this.streamPage.Size = new System.Drawing.Size(286, 229);
            this.streamPage.TabIndex = 1;
            this.streamPage.Text = "Stream";
            this.streamPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblStreamSkippedFrames, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label32, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label31, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamOutputBytes, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamTotalFrames, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamCongestion, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamDuration, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamTimeCode, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamReconnect, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblStreamActive, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 7);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 208);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblStreamSkippedFrames
            // 
            this.lblStreamSkippedFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamSkippedFrames.AutoSize = true;
            this.lblStreamSkippedFrames.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamSkippedFrames.Location = new System.Drawing.Point(139, 120);
            this.lblStreamSkippedFrames.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamSkippedFrames.Name = "lblStreamSkippedFrames";
            this.lblStreamSkippedFrames.Size = new System.Drawing.Size(134, 19);
            this.lblStreamSkippedFrames.TabIndex = 17;
            this.lblStreamSkippedFrames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label32.Location = new System.Drawing.Point(5, 20);
            this.label32.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(124, 19);
            this.label32.TabIndex = 16;
            this.label32.Text = "Reconnecting:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label31.Location = new System.Drawing.Point(5, 0);
            this.label31.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(124, 19);
            this.label31.TabIndex = 15;
            this.label31.Text = "Streaming:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamOutputBytes
            // 
            this.lblStreamOutputBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamOutputBytes.AutoSize = true;
            this.lblStreamOutputBytes.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamOutputBytes.Location = new System.Drawing.Point(139, 140);
            this.lblStreamOutputBytes.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamOutputBytes.Name = "lblStreamOutputBytes";
            this.lblStreamOutputBytes.Size = new System.Drawing.Size(134, 19);
            this.lblStreamOutputBytes.TabIndex = 14;
            this.lblStreamOutputBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamTotalFrames
            // 
            this.lblStreamTotalFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamTotalFrames.AutoSize = true;
            this.lblStreamTotalFrames.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamTotalFrames.Location = new System.Drawing.Point(139, 100);
            this.lblStreamTotalFrames.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamTotalFrames.Name = "lblStreamTotalFrames";
            this.lblStreamTotalFrames.Size = new System.Drawing.Size(134, 19);
            this.lblStreamTotalFrames.TabIndex = 13;
            this.lblStreamTotalFrames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamCongestion
            // 
            this.lblStreamCongestion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamCongestion.AutoSize = true;
            this.lblStreamCongestion.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamCongestion.Location = new System.Drawing.Point(139, 80);
            this.lblStreamCongestion.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamCongestion.Name = "lblStreamCongestion";
            this.lblStreamCongestion.Size = new System.Drawing.Size(134, 19);
            this.lblStreamCongestion.TabIndex = 12;
            this.lblStreamCongestion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamDuration
            // 
            this.lblStreamDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamDuration.AutoSize = true;
            this.lblStreamDuration.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamDuration.Location = new System.Drawing.Point(139, 60);
            this.lblStreamDuration.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamDuration.Name = "lblStreamDuration";
            this.lblStreamDuration.Size = new System.Drawing.Size(134, 19);
            this.lblStreamDuration.TabIndex = 11;
            this.lblStreamDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamTimeCode
            // 
            this.lblStreamTimeCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamTimeCode.AutoSize = true;
            this.lblStreamTimeCode.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamTimeCode.Location = new System.Drawing.Point(139, 40);
            this.lblStreamTimeCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamTimeCode.Name = "lblStreamTimeCode";
            this.lblStreamTimeCode.Size = new System.Drawing.Size(134, 19);
            this.lblStreamTimeCode.TabIndex = 10;
            this.lblStreamTimeCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamReconnect
            // 
            this.lblStreamReconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamReconnect.AutoSize = true;
            this.lblStreamReconnect.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamReconnect.Location = new System.Drawing.Point(139, 20);
            this.lblStreamReconnect.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamReconnect.Name = "lblStreamReconnect";
            this.lblStreamReconnect.Size = new System.Drawing.Size(134, 19);
            this.lblStreamReconnect.TabIndex = 9;
            this.lblStreamReconnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStreamActive
            // 
            this.lblStreamActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreamActive.AutoSize = true;
            this.lblStreamActive.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStreamActive.Location = new System.Drawing.Point(139, 0);
            this.lblStreamActive.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStreamActive.Name = "lblStreamActive";
            this.lblStreamActive.Size = new System.Drawing.Size(134, 19);
            this.lblStreamActive.TabIndex = 8;
            this.lblStreamActive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(5, 40);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "Time Code:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(5, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 19);
            this.label7.TabIndex = 2;
            this.label7.Text = "Duration:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(5, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 19);
            this.label8.TabIndex = 3;
            this.label8.Text = "Congestion:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(5, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 19);
            this.label9.TabIndex = 4;
            this.label9.Text = "Total Frames:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(5, 120);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 19);
            this.label10.TabIndex = 5;
            this.label10.Text = "Skipped:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(5, 140);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 19);
            this.label11.TabIndex = 6;
            this.label11.Text = "OUT Bytes:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // recPage
            // 
            this.recPage.Controls.Add(this.tableLayoutPanel5);
            this.recPage.Location = new System.Drawing.Point(4, 29);
            this.recPage.Name = "recPage";
            this.recPage.Size = new System.Drawing.Size(286, 229);
            this.recPage.TabIndex = 2;
            this.recPage.Text = "Recording";
            this.recPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.lblRecordingBytes, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.lblRecordingDuration, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.lblRecordingTimeCode, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblRecordingPaused, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblRecording, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label30, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.label29, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.label28, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label27, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label21, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(278, 227);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lblRecordingBytes
            // 
            this.lblRecordingBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordingBytes.AutoSize = true;
            this.lblRecordingBytes.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecordingBytes.Location = new System.Drawing.Point(139, 80);
            this.lblRecordingBytes.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRecordingBytes.Name = "lblRecordingBytes";
            this.lblRecordingBytes.Size = new System.Drawing.Size(134, 19);
            this.lblRecordingBytes.TabIndex = 23;
            this.lblRecordingBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordingDuration
            // 
            this.lblRecordingDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordingDuration.AutoSize = true;
            this.lblRecordingDuration.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecordingDuration.Location = new System.Drawing.Point(139, 60);
            this.lblRecordingDuration.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRecordingDuration.Name = "lblRecordingDuration";
            this.lblRecordingDuration.Size = new System.Drawing.Size(134, 19);
            this.lblRecordingDuration.TabIndex = 22;
            this.lblRecordingDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordingTimeCode
            // 
            this.lblRecordingTimeCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordingTimeCode.AutoSize = true;
            this.lblRecordingTimeCode.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecordingTimeCode.Location = new System.Drawing.Point(139, 40);
            this.lblRecordingTimeCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRecordingTimeCode.Name = "lblRecordingTimeCode";
            this.lblRecordingTimeCode.Size = new System.Drawing.Size(134, 19);
            this.lblRecordingTimeCode.TabIndex = 21;
            this.lblRecordingTimeCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordingPaused
            // 
            this.lblRecordingPaused.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordingPaused.AutoSize = true;
            this.lblRecordingPaused.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecordingPaused.Location = new System.Drawing.Point(139, 20);
            this.lblRecordingPaused.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRecordingPaused.Name = "lblRecordingPaused";
            this.lblRecordingPaused.Size = new System.Drawing.Size(134, 19);
            this.lblRecordingPaused.TabIndex = 20;
            this.lblRecordingPaused.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecording
            // 
            this.lblRecording.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecording.AutoSize = true;
            this.lblRecording.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecording.Location = new System.Drawing.Point(139, 0);
            this.lblRecording.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRecording.Name = "lblRecording";
            this.lblRecording.Size = new System.Drawing.Size(134, 19);
            this.lblRecording.TabIndex = 19;
            this.lblRecording.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label30.Location = new System.Drawing.Point(5, 80);
            this.label30.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(124, 19);
            this.label30.TabIndex = 18;
            this.label30.Text = "Bytes:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label29.Location = new System.Drawing.Point(5, 60);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(124, 19);
            this.label29.TabIndex = 17;
            this.label29.Text = "Duration:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label28.Location = new System.Drawing.Point(5, 40);
            this.label28.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(124, 19);
            this.label28.TabIndex = 16;
            this.label28.Text = "Time Code:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label27.Location = new System.Drawing.Point(5, 20);
            this.label27.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(124, 19);
            this.label27.TabIndex = 15;
            this.label27.Text = "Paused:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(5, 0);
            this.label21.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(124, 19);
            this.label21.TabIndex = 14;
            this.label21.Text = "Recording:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnPauseRecording);
            this.groupBox5.Controls.Add(this.btnToggleRecording);
            this.groupBox5.Controls.Add(this.btnToggleStreaming);
            this.groupBox5.Location = new System.Drawing.Point(563, 441);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox5.Size = new System.Drawing.Size(193, 121);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Streaming / Recording";
            // 
            // btnPauseRecording
            // 
            this.btnPauseRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseRecording.Location = new System.Drawing.Point(157, 75);
            this.btnPauseRecording.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnPauseRecording.Name = "btnPauseRecording";
            this.btnPauseRecording.Size = new System.Drawing.Size(27, 36);
            this.btnPauseRecording.TabIndex = 2;
            this.btnPauseRecording.Text = "||";
            this.btnPauseRecording.UseVisualStyleBackColor = true;
            this.btnPauseRecording.Click += new System.EventHandler(this.btnPauseRecording_Click);
            // 
            // btnToggleRecording
            // 
            this.btnToggleRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleRecording.Location = new System.Drawing.Point(6, 75);
            this.btnToggleRecording.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnToggleRecording.Name = "btnToggleRecording";
            this.btnToggleRecording.Size = new System.Drawing.Size(151, 36);
            this.btnToggleRecording.TabIndex = 1;
            this.btnToggleRecording.Text = "State unknown";
            this.btnToggleRecording.UseVisualStyleBackColor = true;
            this.btnToggleRecording.Click += new System.EventHandler(this.btnToggleRecording_Click);
            // 
            // btnToggleStreaming
            // 
            this.btnToggleStreaming.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleStreaming.Location = new System.Drawing.Point(6, 31);
            this.btnToggleStreaming.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnToggleStreaming.Name = "btnToggleStreaming";
            this.btnToggleStreaming.Size = new System.Drawing.Size(178, 36);
            this.btnToggleStreaming.TabIndex = 0;
            this.btnToggleStreaming.Text = "State unknown";
            this.btnToggleStreaming.UseVisualStyleBackColor = true;
            this.btnToggleStreaming.Click += new System.EventHandler(this.btnToggleStreaming_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSetCurrentProfile);
            this.groupBox4.Controls.Add(this.btnGetCurrentProfile);
            this.groupBox4.Controls.Add(this.tbProfile);
            this.groupBox4.Controls.Add(this.btnListProfiles);
            this.groupBox4.Controls.Add(this.tvProfiles);
            this.groupBox4.Location = new System.Drawing.Point(8, 401);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox4.Size = new System.Drawing.Size(218, 373);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Profiles";
            // 
            // btnSetCurrentProfile
            // 
            this.btnSetCurrentProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetCurrentProfile.Location = new System.Drawing.Point(118, 307);
            this.btnSetCurrentProfile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetCurrentProfile.Name = "btnSetCurrentProfile";
            this.btnSetCurrentProfile.Size = new System.Drawing.Size(91, 59);
            this.btnSetCurrentProfile.TabIndex = 5;
            this.btnSetCurrentProfile.Text = "Set\r\nCurProfile";
            this.btnSetCurrentProfile.UseVisualStyleBackColor = true;
            this.btnSetCurrentProfile.Click += new System.EventHandler(this.btnSetCurrentProfile_Click);
            // 
            // btnGetCurrentProfile
            // 
            this.btnGetCurrentProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetCurrentProfile.Location = new System.Drawing.Point(8, 307);
            this.btnGetCurrentProfile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetCurrentProfile.Name = "btnGetCurrentProfile";
            this.btnGetCurrentProfile.Size = new System.Drawing.Size(91, 59);
            this.btnGetCurrentProfile.TabIndex = 4;
            this.btnGetCurrentProfile.Text = "Get\r\nCurProfile";
            this.btnGetCurrentProfile.UseVisualStyleBackColor = true;
            this.btnGetCurrentProfile.Click += new System.EventHandler(this.btnGetCurrentProfile_Click);
            // 
            // tbProfile
            // 
            this.tbProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProfile.Location = new System.Drawing.Point(8, 267);
            this.tbProfile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbProfile.Name = "tbProfile";
            this.tbProfile.Size = new System.Drawing.Size(201, 27);
            this.tbProfile.TabIndex = 3;
            // 
            // btnListProfiles
            // 
            this.btnListProfiles.Location = new System.Drawing.Point(8, 29);
            this.btnListProfiles.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnListProfiles.Name = "btnListProfiles";
            this.btnListProfiles.Size = new System.Drawing.Size(106, 36);
            this.btnListProfiles.TabIndex = 2;
            this.btnListProfiles.Text = "ListProfiles";
            this.btnListProfiles.UseVisualStyleBackColor = true;
            this.btnListProfiles.Click += new System.EventHandler(this.btnListProfiles_Click);
            // 
            // tvProfiles
            // 
            this.tvProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvProfiles.Location = new System.Drawing.Point(8, 76);
            this.tvProfiles.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tvProfiles.Name = "tvProfiles";
            this.tvProfiles.Size = new System.Drawing.Size(202, 180);
            this.tvProfiles.TabIndex = 1;
            this.tvProfiles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProfiles_NodeMouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSetCurrentSceneCol);
            this.groupBox2.Controls.Add(this.btnGetCurrentSceneCol);
            this.groupBox2.Controls.Add(this.tbSceneCol);
            this.groupBox2.Controls.Add(this.btnListSceneCol);
            this.groupBox2.Controls.Add(this.tvSceneCols);
            this.groupBox2.Location = new System.Drawing.Point(216, 19);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Size = new System.Drawing.Size(213, 373);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scene Collections";
            // 
            // btnSetCurrentSceneCol
            // 
            this.btnSetCurrentSceneCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetCurrentSceneCol.Location = new System.Drawing.Point(111, 307);
            this.btnSetCurrentSceneCol.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSetCurrentSceneCol.Name = "btnSetCurrentSceneCol";
            this.btnSetCurrentSceneCol.Size = new System.Drawing.Size(91, 59);
            this.btnSetCurrentSceneCol.TabIndex = 5;
            this.btnSetCurrentSceneCol.Text = "Set\r\nCurSC";
            this.btnSetCurrentSceneCol.UseVisualStyleBackColor = true;
            this.btnSetCurrentSceneCol.Click += new System.EventHandler(this.btnSetCurrentSceneCol_Click);
            // 
            // btnGetCurrentSceneCol
            // 
            this.btnGetCurrentSceneCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetCurrentSceneCol.Location = new System.Drawing.Point(8, 307);
            this.btnGetCurrentSceneCol.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetCurrentSceneCol.Name = "btnGetCurrentSceneCol";
            this.btnGetCurrentSceneCol.Size = new System.Drawing.Size(91, 59);
            this.btnGetCurrentSceneCol.TabIndex = 4;
            this.btnGetCurrentSceneCol.Text = "Get\r\nCurSC";
            this.btnGetCurrentSceneCol.UseVisualStyleBackColor = true;
            this.btnGetCurrentSceneCol.Click += new System.EventHandler(this.btnGetCurrentSceneCol_Click);
            // 
            // tbSceneCol
            // 
            this.tbSceneCol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSceneCol.Location = new System.Drawing.Point(8, 267);
            this.tbSceneCol.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbSceneCol.Name = "tbSceneCol";
            this.tbSceneCol.Size = new System.Drawing.Size(194, 27);
            this.tbSceneCol.TabIndex = 3;
            // 
            // btnListSceneCol
            // 
            this.btnListSceneCol.Location = new System.Drawing.Point(8, 29);
            this.btnListSceneCol.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnListSceneCol.Name = "btnListSceneCol";
            this.btnListSceneCol.Size = new System.Drawing.Size(167, 36);
            this.btnListSceneCol.TabIndex = 2;
            this.btnListSceneCol.Text = "ListSceneCollections";
            this.btnListSceneCol.UseVisualStyleBackColor = true;
            this.btnListSceneCol.Click += new System.EventHandler(this.btnListSceneCol_Click);
            // 
            // tvSceneCols
            // 
            this.tvSceneCols.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSceneCols.Location = new System.Drawing.Point(8, 76);
            this.tvSceneCols.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tvSceneCols.Name = "tvSceneCols";
            this.tvSceneCols.Size = new System.Drawing.Size(195, 180);
            this.tvSceneCols.TabIndex = 1;
            this.tvSceneCols.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSceneCols_NodeMouseClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(536, 297);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox3.Size = new System.Drawing.Size(222, 136);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Version Info";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbOBSVersion, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPluginVersion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 29);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(203, 97);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tbOBSVersion
            // 
            this.tbOBSVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOBSVersion.AutoSize = true;
            this.tbOBSVersion.Location = new System.Drawing.Point(124, 36);
            this.tbOBSVersion.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tbOBSVersion.Name = "tbOBSVersion";
            this.tbOBSVersion.Size = new System.Drawing.Size(74, 20);
            this.tbOBSVersion.TabIndex = 5;
            this.tbOBSVersion.Text = "???";
            this.tbOBSVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPluginVersion
            // 
            this.tbPluginVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginVersion.AutoSize = true;
            this.tbPluginVersion.Location = new System.Drawing.Point(124, 5);
            this.tbPluginVersion.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tbPluginVersion.Name = "tbPluginVersion";
            this.tbPluginVersion.Size = new System.Drawing.Size(74, 20);
            this.tbPluginVersion.TabIndex = 3;
            this.tbPluginVersion.Text = "???";
            this.tbPluginVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(5, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "OBS WS Version :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(5, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "OBS Version :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(299, 326);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Set Path";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(139, 40);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 19);
            this.label13.TabIndex = 23;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(139, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 19);
            this.label14.TabIndex = 22;
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.label13, 1, 10);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 11;
            this.tableLayoutPanel4.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 924);
            this.Controls.Add(this.gbControls);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServerPassword);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.btnConnect);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainWindow";
            this.Text = "obs-websocket client";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbControls.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbTransitionDuration)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.gbStatus.ResumeLayout(false);
            this.tabStats.ResumeLayout(false);
            this.obsPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.streamPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.recPage.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtServerPassword;
        private System.Windows.Forms.TreeView tvScenes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnListScenes;
        private System.Windows.Forms.Button btnGetCurrentScene;
        private System.Windows.Forms.Button btnSetCurrentScene;
        private System.Windows.Forms.TextBox tbCurrentScene;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbControls;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label tbOBSVersion;
        private System.Windows.Forms.Label tbPluginVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSetCurrentProfile;
        private System.Windows.Forms.Button btnGetCurrentProfile;
        private System.Windows.Forms.TextBox tbProfile;
        private System.Windows.Forms.Button btnListProfiles;
        private System.Windows.Forms.TreeView tvProfiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSetCurrentSceneCol;
        private System.Windows.Forms.Button btnGetCurrentSceneCol;
        private System.Windows.Forms.TextBox tbSceneCol;
        private System.Windows.Forms.Button btnListSceneCol;
        private System.Windows.Forms.TreeView tvSceneCols;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnToggleRecording;
        private System.Windows.Forms.Button btnToggleStreaming;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSetCurrentTransition;
        private System.Windows.Forms.Button btnGetCurrentTransition;
        private System.Windows.Forms.TextBox tbTransition;
        private System.Windows.Forms.Button btnListTransitions;
        private System.Windows.Forms.TreeView tvTransitions;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnSetTransitionDuration;
        private System.Windows.Forms.Button btnGetTransitionDuration;
        private System.Windows.Forms.NumericUpDown tbTransitionDuration;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbFolderPath;
        private System.Windows.Forms.Button btnSetPath;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label lblVirtualCamStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVirtualCamToggle;
        private System.Windows.Forms.Button btnVirtualCamStop;
        private System.Windows.Forms.Button btnVirtualCamStart;
        private System.Windows.Forms.TabControl tabStats;
        private System.Windows.Forms.TabPage obsPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TabPage streamPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblStreamOutputBytes;
        private System.Windows.Forms.Label lblStreamTotalFrames;
        private System.Windows.Forms.Label lblStreamCongestion;
        private System.Windows.Forms.Label lblStreamDuration;
        private System.Windows.Forms.Label lblStreamTimeCode;
        private System.Windows.Forms.Label lblStreamReconnect;
        private System.Windows.Forms.Label lblStreamActive;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage recPage;
        private System.Windows.Forms.Label lblRendered;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblMissed;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblOutputTotal;
        private System.Windows.Forms.Label lblOutgoingMessages;
        private System.Windows.Forms.Label lblIncomingMessages;
        private System.Windows.Forms.Label lblDisk;
        private System.Windows.Forms.Label lblMemory;
        private System.Windows.Forms.Label lblCPU;
        private System.Windows.Forms.Label lblFPS;
        private System.Windows.Forms.Label lblAvgRender;
        private System.Windows.Forms.Label lblSkipped;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblRecordingBytes;
        private System.Windows.Forms.Label lblRecordingDuration;
        private System.Windows.Forms.Label lblRecordingTimeCode;
        private System.Windows.Forms.Label lblRecordingPaused;
        private System.Windows.Forms.Label lblRecording;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnPauseRecording;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblStreamSkippedFrames;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

