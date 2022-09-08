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
            this.btnSourceFilters = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSourceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSceneName = new System.Windows.Forms.TextBox();
            this.btnInputInfo = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSourceInfo = new System.Windows.Forms.Button();
            this.btnGetMonitorList = new System.Windows.Forms.Button();
            this.btnTransitions = new System.Windows.Forms.Button();
            this.btnGetGroupList = new System.Windows.Forms.Button();
            this.btnGetInputList = new System.Windows.Forms.Button();
            this.btnSourcesList = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnToggleVidCapDvc = new System.Windows.Forms.Button();
            this.btnTracks = new System.Windows.Forms.Button();
            this.btnCreateScene = new System.Windows.Forms.Button();
            this.btnDoNothing = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(12, 80);
            this.tbLog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(874, 600);
            this.tbLog.TabIndex = 0;
            // 
            // btnEvents
            // 
            this.btnEvents.Location = new System.Drawing.Point(896, 20);
            this.btnEvents.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(156, 36);
            this.btnEvents.TabIndex = 1;
            this.btnEvents.Text = "Events Subscribe";
            this.btnEvents.UseVisualStyleBackColor = true;
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnSourceFilters
            // 
            this.btnSourceFilters.Location = new System.Drawing.Point(4, 343);
            this.btnSourceFilters.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSourceFilters.Name = "btnSourceFilters";
            this.btnSourceFilters.Size = new System.Drawing.Size(149, 36);
            this.btnSourceFilters.TabIndex = 4;
            this.btnSourceFilters.Text = "Get Source\'s Filters";
            this.btnSourceFilters.UseVisualStyleBackColor = true;
            this.btnSourceFilters.Click += new System.EventHandler(this.btnSourceFilters_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbSourceName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSceneName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 61);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inputs (for buttons):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Source/Input Name:";
            // 
            // tbSourceName
            // 
            this.tbSourceName.Location = new System.Drawing.Point(414, 25);
            this.tbSourceName.Name = "tbSourceName";
            this.tbSourceName.Size = new System.Drawing.Size(121, 27);
            this.tbSourceName.TabIndex = 2;
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
            // tbSceneName
            // 
            this.tbSceneName.Location = new System.Drawing.Point(107, 25);
            this.tbSceneName.Name = "tbSceneName";
            this.tbSceneName.Size = new System.Drawing.Size(121, 27);
            this.tbSceneName.TabIndex = 0;
            // 
            // btnInputInfo
            // 
            this.btnInputInfo.Location = new System.Drawing.Point(4, 386);
            this.btnInputInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnInputInfo.Name = "btnInputInfo";
            this.btnInputInfo.Size = new System.Drawing.Size(149, 36);
            this.btnInputInfo.TabIndex = 15;
            this.btnInputInfo.Text = "Get Input Settings";
            this.btnInputInfo.UseVisualStyleBackColor = true;
            this.btnInputInfo.Click += new System.EventHandler(this.btnInputInfo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(897, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(162, 621);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDoNothing);
            this.tabPage1.Controls.Add(this.btnSourceInfo);
            this.tabPage1.Controls.Add(this.btnGetMonitorList);
            this.tabPage1.Controls.Add(this.btnInputInfo);
            this.tabPage1.Controls.Add(this.btnTransitions);
            this.tabPage1.Controls.Add(this.btnGetGroupList);
            this.tabPage1.Controls.Add(this.btnGetInputList);
            this.tabPage1.Controls.Add(this.btnSourcesList);
            this.tabPage1.Controls.Add(this.btnSourceFilters);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(154, 588);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSourceInfo
            // 
            this.btnSourceInfo.Location = new System.Drawing.Point(4, 300);
            this.btnSourceInfo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSourceInfo.Name = "btnSourceInfo";
            this.btnSourceInfo.Size = new System.Drawing.Size(149, 36);
            this.btnSourceInfo.TabIndex = 21;
            this.btnSourceInfo.Text = "Get Source Info";
            this.btnSourceInfo.UseVisualStyleBackColor = true;
            this.btnSourceInfo.Click += new System.EventHandler(this.btnSourceInfo_Click);
            // 
            // btnGetMonitorList
            // 
            this.btnGetMonitorList.Location = new System.Drawing.Point(4, 139);
            this.btnGetMonitorList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetMonitorList.Name = "btnGetMonitorList";
            this.btnGetMonitorList.Size = new System.Drawing.Size(149, 36);
            this.btnGetMonitorList.TabIndex = 19;
            this.btnGetMonitorList.Text = "List Monitors";
            this.btnGetMonitorList.UseVisualStyleBackColor = true;
            this.btnGetMonitorList.Click += new System.EventHandler(this.btnGetMonitorList_Click);
            // 
            // btnTransitions
            // 
            this.btnTransitions.Location = new System.Drawing.Point(4, 183);
            this.btnTransitions.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnTransitions.Name = "btnTransitions";
            this.btnTransitions.Size = new System.Drawing.Size(149, 36);
            this.btnTransitions.TabIndex = 18;
            this.btnTransitions.Text = "List Transitions";
            this.btnTransitions.UseVisualStyleBackColor = true;
            this.btnTransitions.Click += new System.EventHandler(this.btnTransition_Click);
            // 
            // btnGetGroupList
            // 
            this.btnGetGroupList.Location = new System.Drawing.Point(4, 95);
            this.btnGetGroupList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetGroupList.Name = "btnGetGroupList";
            this.btnGetGroupList.Size = new System.Drawing.Size(149, 36);
            this.btnGetGroupList.TabIndex = 17;
            this.btnGetGroupList.Text = "List Groups";
            this.btnGetGroupList.UseVisualStyleBackColor = true;
            this.btnGetGroupList.Click += new System.EventHandler(this.btnGetGroupList_Click);
            // 
            // btnGetInputList
            // 
            this.btnGetInputList.Location = new System.Drawing.Point(4, 51);
            this.btnGetInputList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnGetInputList.Name = "btnGetInputList";
            this.btnGetInputList.Size = new System.Drawing.Size(149, 36);
            this.btnGetInputList.TabIndex = 16;
            this.btnGetInputList.Text = "List Inputs";
            this.btnGetInputList.UseVisualStyleBackColor = true;
            this.btnGetInputList.Click += new System.EventHandler(this.btnGetInputList_Click);
            // 
            // btnSourcesList
            // 
            this.btnSourcesList.Location = new System.Drawing.Point(4, 7);
            this.btnSourcesList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSourcesList.Name = "btnSourcesList";
            this.btnSourcesList.Size = new System.Drawing.Size(149, 36);
            this.btnSourcesList.TabIndex = 15;
            this.btnSourcesList.Text = "List Sources";
            this.btnSourcesList.UseVisualStyleBackColor = true;
            this.btnSourcesList.Click += new System.EventHandler(this.btnSourcesList_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnStopRecord);
            this.tabPage2.Controls.Add(this.btnRename);
            this.tabPage2.Controls.Add(this.btnToggleVidCapDvc);
            this.tabPage2.Controls.Add(this.btnTracks);
            this.tabPage2.Controls.Add(this.btnCreateScene);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(154, 588);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Location = new System.Drawing.Point(2, 230);
            this.btnStopRecord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(149, 36);
            this.btnStopRecord.TabIndex = 25;
            this.btnStopRecord.Text = "Stop Record";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(2, 51);
            this.btnRename.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(149, 36);
            this.btnRename.TabIndex = 24;
            this.btnRename.Text = "Source Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            // 
            // btnToggleVidCapDvc
            // 
            this.btnToggleVidCapDvc.Location = new System.Drawing.Point(2, 186);
            this.btnToggleVidCapDvc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnToggleVidCapDvc.Name = "btnToggleVidCapDvc";
            this.btnToggleVidCapDvc.Size = new System.Drawing.Size(149, 36);
            this.btnToggleVidCapDvc.TabIndex = 23;
            this.btnToggleVidCapDvc.Text = "Toggle VidCapDvc";
            this.btnToggleVidCapDvc.UseVisualStyleBackColor = true;
            this.btnToggleVidCapDvc.Click += new System.EventHandler(this.btnToggleVidCapDvc_Click);
            // 
            // btnTracks
            // 
            this.btnTracks.Location = new System.Drawing.Point(2, 141);
            this.btnTracks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnTracks.Name = "btnTracks";
            this.btnTracks.Size = new System.Drawing.Size(149, 36);
            this.btnTracks.TabIndex = 22;
            this.btnTracks.Text = "Tracks";
            this.btnTracks.UseVisualStyleBackColor = true;
            this.btnTracks.Click += new System.EventHandler(this.btnTracks_Click);
            // 
            // btnCreateScene
            // 
            this.btnCreateScene.Location = new System.Drawing.Point(2, 7);
            this.btnCreateScene.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCreateScene.Name = "btnCreateScene";
            this.btnCreateScene.Size = new System.Drawing.Size(149, 36);
            this.btnCreateScene.TabIndex = 21;
            this.btnCreateScene.Text = "Create Scene";
            this.btnCreateScene.UseVisualStyleBackColor = true;
            this.btnCreateScene.Click += new System.EventHandler(this.btnCreateScene_Click);
            // 
            // btnDoNothing
            // 
            this.btnDoNothing.Location = new System.Drawing.Point(2, 545);
            this.btnDoNothing.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnDoNothing.Name = "btnDoNothing";
            this.btnDoNothing.Size = new System.Drawing.Size(149, 36);
            this.btnDoNothing.TabIndex = 22;
            this.btnDoNothing.Text = "Do Nothing";
            this.btnDoNothing.UseVisualStyleBackColor = true;
            this.btnDoNothing.Click += new System.EventHandler(this.btnDoNothing_Click);
            // 
            // AdvancedWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 692);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnEvents;
        private System.Windows.Forms.Button btnSourceFilters;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbSceneName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSourceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInputInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTransitions;
        private System.Windows.Forms.Button btnGetGroupList;
        private System.Windows.Forms.Button btnGetInputList;
        private System.Windows.Forms.Button btnSourcesList;
        private System.Windows.Forms.Button btnSourceInfo;
        private System.Windows.Forms.Button btnGetMonitorList;
        private System.Windows.Forms.Button btnToggleVidCapDvc;
        private System.Windows.Forms.Button btnTracks;
        private System.Windows.Forms.Button btnCreateScene;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.Button btnDoNothing;
    }
}