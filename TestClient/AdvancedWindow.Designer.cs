namespace TestClient
{
    partial class AdvancedWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbLog = new System.Windows.Forms.TextBox();
            this.btnEvents = new System.Windows.Forms.Button();
            this.btnProjector = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSourceFilters = new System.Windows.Forms.Button();
            this.btnCreateScene = new System.Windows.Forms.Button();
            this.btnOutputs = new System.Windows.Forms.Button();
            this.btnTransition = new System.Windows.Forms.Button();
            this.btnTracks = new System.Windows.Forms.Button();
            this.btnToggleVidCapDvc = new System.Windows.Forms.Button();
            this.btn_GetInputList = new System.Windows.Forms.Button();
            this.btn_GetGroupList = new System.Windows.Forms.Button();
            this.btn_GetMonitorList = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSceneName = new System.Windows.Forms.TextBox();
            this.btnSourceInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSourceName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(17, 80);
            this.tbLog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(893, 592);
            this.tbLog.TabIndex = 0;
            // 
            // btnEvents
            // 
            this.btnEvents.Location = new System.Drawing.Point(921, 20);
            this.btnEvents.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(129, 36);
            this.btnEvents.TabIndex = 1;
            this.btnEvents.Text = "Events";
            this.btnEvents.UseVisualStyleBackColor = true;
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnProjector
            // 
            this.btnProjector.Location = new System.Drawing.Point(920, 524);
            this.btnProjector.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnProjector.Name = "btnProjector";
            this.btnProjector.Size = new System.Drawing.Size(129, 36);
            this.btnProjector.TabIndex = 2;
            this.btnProjector.Text = "Projector";
            this.btnProjector.UseVisualStyleBackColor = true;
            this.btnProjector.Click += new System.EventHandler(this.btnProjector_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(921, 104);
            this.btnRename.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(129, 36);
            this.btnRename.TabIndex = 3;
            this.btnRename.Text = "Source Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnSourceFilters
            // 
            this.btnSourceFilters.Location = new System.Drawing.Point(921, 146);
            this.btnSourceFilters.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSourceFilters.Name = "btnSourceFilters";
            this.btnSourceFilters.Size = new System.Drawing.Size(129, 36);
            this.btnSourceFilters.TabIndex = 4;
            this.btnSourceFilters.Text = "Source Filters";
            this.btnSourceFilters.UseVisualStyleBackColor = true;
            this.btnSourceFilters.Click += new System.EventHandler(this.btnSourceFilters_Click);
            // 
            // btnCreateScene
            // 
            this.btnCreateScene.Location = new System.Drawing.Point(921, 188);
            this.btnCreateScene.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCreateScene.Name = "btnCreateScene";
            this.btnCreateScene.Size = new System.Drawing.Size(129, 36);
            this.btnCreateScene.TabIndex = 5;
            this.btnCreateScene.Text = "Create Scene";
            this.btnCreateScene.UseVisualStyleBackColor = true;
            this.btnCreateScene.Click += new System.EventHandler(this.btnCreateScene_Click);
            // 
            // btnOutputs
            // 
            this.btnOutputs.Location = new System.Drawing.Point(920, 230);
            this.btnOutputs.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnOutputs.Name = "btnOutputs";
            this.btnOutputs.Size = new System.Drawing.Size(129, 36);
            this.btnOutputs.TabIndex = 6;
            this.btnOutputs.Text = "Outputs";
            this.btnOutputs.UseVisualStyleBackColor = true;
            this.btnOutputs.Click += new System.EventHandler(this.btnOutputs_Click);
            // 
            // btnTransition
            // 
            this.btnTransition.Location = new System.Drawing.Point(920, 272);
            this.btnTransition.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnTransition.Name = "btnTransition";
            this.btnTransition.Size = new System.Drawing.Size(129, 36);
            this.btnTransition.TabIndex = 7;
            this.btnTransition.Text = "Transition";
            this.btnTransition.UseVisualStyleBackColor = true;
            this.btnTransition.Click += new System.EventHandler(this.btnTransition_Click);
            // 
            // btnTracks
            // 
            this.btnTracks.Location = new System.Drawing.Point(921, 314);
            this.btnTracks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnTracks.Name = "btnTracks";
            this.btnTracks.Size = new System.Drawing.Size(129, 36);
            this.btnTracks.TabIndex = 8;
            this.btnTracks.Text = "Tracks";
            this.btnTracks.UseVisualStyleBackColor = true;
            this.btnTracks.Click += new System.EventHandler(this.btnTracks_Click);
            // 
            // btnToggleVidCapDvc
            // 
            this.btnToggleVidCapDvc.Location = new System.Drawing.Point(921, 356);
            this.btnToggleVidCapDvc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnToggleVidCapDvc.Name = "btnToggleVidCapDvc";
            this.btnToggleVidCapDvc.Size = new System.Drawing.Size(129, 36);
            this.btnToggleVidCapDvc.TabIndex = 9;
            this.btnToggleVidCapDvc.Text = "Toggle VidCapDvc";
            this.btnToggleVidCapDvc.UseVisualStyleBackColor = true;
            this.btnToggleVidCapDvc.Click += new System.EventHandler(this.btnToggleVidCapDvc_Click);
            // 
            // btn_GetInputList
            // 
            this.btn_GetInputList.Location = new System.Drawing.Point(921, 398);
            this.btn_GetInputList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_GetInputList.Name = "btn_GetInputList";
            this.btn_GetInputList.Size = new System.Drawing.Size(129, 36);
            this.btn_GetInputList.TabIndex = 10;
            this.btn_GetInputList.Text = "GetInputList";
            this.btn_GetInputList.UseVisualStyleBackColor = true;
            this.btn_GetInputList.Click += new System.EventHandler(this.btn_GetInputList_Click);
            // 
            // btn_GetGroupList
            // 
            this.btn_GetGroupList.Location = new System.Drawing.Point(921, 440);
            this.btn_GetGroupList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_GetGroupList.Name = "btn_GetGroupList";
            this.btn_GetGroupList.Size = new System.Drawing.Size(129, 36);
            this.btn_GetGroupList.TabIndex = 11;
            this.btn_GetGroupList.Text = "GetGroupList";
            this.btn_GetGroupList.UseVisualStyleBackColor = true;
            this.btn_GetGroupList.Click += new System.EventHandler(this.btn_GetGroupList_Click);
            // 
            // btn_GetMonitorList
            // 
            this.btn_GetMonitorList.Location = new System.Drawing.Point(921, 482);
            this.btn_GetMonitorList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_GetMonitorList.Name = "btn_GetMonitorList";
            this.btn_GetMonitorList.Size = new System.Drawing.Size(129, 36);
            this.btn_GetMonitorList.TabIndex = 12;
            this.btn_GetMonitorList.Text = "GetMonitorList";
            this.btn_GetMonitorList.UseVisualStyleBackColor = true;
            this.btn_GetMonitorList.Click += new System.EventHandler(this.btn_GetMonitorList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbSourceName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSceneName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(901, 61);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inputs (for buttons):";
            // 
            // tbSceneName
            // 
            this.tbSceneName.Location = new System.Drawing.Point(107, 22);
            this.tbSceneName.Name = "tbSceneName";
            this.tbSceneName.Size = new System.Drawing.Size(121, 27);
            this.tbSceneName.TabIndex = 0;
            // 
            // btnSourceInfo
            // 
            this.btnSourceInfo.Location = new System.Drawing.Point(920, 62);
            this.btnSourceInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSourceInfo.Name = "btnSourceInfo";
            this.btnSourceInfo.Size = new System.Drawing.Size(129, 36);
            this.btnSourceInfo.TabIndex = 14;
            this.btnSourceInfo.Text = "Source Info";
            this.btnSourceInfo.UseVisualStyleBackColor = true;
            this.btnSourceInfo.Click += new System.EventHandler(this.btnSourceInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scene Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Source Name:";
            // 
            // tbSourceName
            // 
            this.tbSourceName.Location = new System.Drawing.Point(367, 22);
            this.tbSourceName.Name = "tbSourceName";
            this.tbSourceName.Size = new System.Drawing.Size(121, 27);
            this.tbSourceName.TabIndex = 2;
            // 
            // AdvancedWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 692);
            this.Controls.Add(this.btnSourceInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_GetMonitorList);
            this.Controls.Add(this.btn_GetGroupList);
            this.Controls.Add(this.btn_GetInputList);
            this.Controls.Add(this.btnToggleVidCapDvc);
            this.Controls.Add(this.btnTracks);
            this.Controls.Add(this.btnTransition);
            this.Controls.Add(this.btnOutputs);
            this.Controls.Add(this.btnCreateScene);
            this.Controls.Add(this.btnSourceFilters);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnProjector);
            this.Controls.Add(this.btnEvents);
            this.Controls.Add(this.tbLog);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AdvancedWindow";
            this.Load += new System.EventHandler(this.AdvancedWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnEvents;
        private System.Windows.Forms.Button btnProjector;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnSourceFilters;
        private System.Windows.Forms.Button btnCreateScene;
        private System.Windows.Forms.Button btnOutputs;
        private System.Windows.Forms.Button btnTransition;
        private System.Windows.Forms.Button btnTracks;
        private System.Windows.Forms.Button btnToggleVidCapDvc;
        private System.Windows.Forms.Button btn_GetInputList;
        private System.Windows.Forms.Button btn_GetGroupList;
        private System.Windows.Forms.Button btn_GetMonitorList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbSceneName;
        private System.Windows.Forms.Button btnSourceInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSourceName;
        private System.Windows.Forms.Label label1;
    }
}