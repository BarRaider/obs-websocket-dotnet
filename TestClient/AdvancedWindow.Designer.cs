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
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(15, 15);
            this.tbLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(782, 490);
            this.tbLog.TabIndex = 0;
            // 
            // btnEvents
            // 
            this.btnEvents.Location = new System.Drawing.Point(806, 15);
            this.btnEvents.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(113, 27);
            this.btnEvents.TabIndex = 1;
            this.btnEvents.Text = "Events";
            this.btnEvents.UseVisualStyleBackColor = true;
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnProjector
            // 
            this.btnProjector.Location = new System.Drawing.Point(806, 48);
            this.btnProjector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnProjector.Name = "btnProjector";
            this.btnProjector.Size = new System.Drawing.Size(113, 27);
            this.btnProjector.TabIndex = 2;
            this.btnProjector.Text = "Projector";
            this.btnProjector.UseVisualStyleBackColor = true;
            this.btnProjector.Click += new System.EventHandler(this.btnProjector_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(806, 82);
            this.btnRename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(113, 27);
            this.btnRename.TabIndex = 3;
            this.btnRename.Text = "Source Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnSourceFilters
            // 
            this.btnSourceFilters.Location = new System.Drawing.Point(806, 115);
            this.btnSourceFilters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSourceFilters.Name = "btnSourceFilters";
            this.btnSourceFilters.Size = new System.Drawing.Size(113, 27);
            this.btnSourceFilters.TabIndex = 4;
            this.btnSourceFilters.Text = "Source Filters";
            this.btnSourceFilters.UseVisualStyleBackColor = true;
            this.btnSourceFilters.Click += new System.EventHandler(this.btnSourceFilters_Click);
            // 
            // btnCreateScene
            // 
            this.btnCreateScene.Location = new System.Drawing.Point(806, 149);
            this.btnCreateScene.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateScene.Name = "btnCreateScene";
            this.btnCreateScene.Size = new System.Drawing.Size(113, 27);
            this.btnCreateScene.TabIndex = 5;
            this.btnCreateScene.Text = "Create Scene";
            this.btnCreateScene.UseVisualStyleBackColor = true;
            this.btnCreateScene.Click += new System.EventHandler(this.btnCreateScene_Click);
            // 
            // btnOutputs
            // 
            this.btnOutputs.Location = new System.Drawing.Point(805, 182);
            this.btnOutputs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOutputs.Name = "btnOutputs";
            this.btnOutputs.Size = new System.Drawing.Size(113, 27);
            this.btnOutputs.TabIndex = 6;
            this.btnOutputs.Text = "Outputs";
            this.btnOutputs.UseVisualStyleBackColor = true;
            this.btnOutputs.Click += new System.EventHandler(this.btnOutputs_Click);
            // 
            // btnTransition
            // 
            this.btnTransition.Location = new System.Drawing.Point(805, 215);
            this.btnTransition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTransition.Name = "btnTransition";
            this.btnTransition.Size = new System.Drawing.Size(113, 27);
            this.btnTransition.TabIndex = 7;
            this.btnTransition.Text = "Transition";
            this.btnTransition.UseVisualStyleBackColor = true;
            this.btnTransition.Click += new System.EventHandler(this.btnTransition_Click);
            // 
            // AdvancedWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.btnTransition);
            this.Controls.Add(this.btnOutputs);
            this.Controls.Add(this.btnCreateScene);
            this.Controls.Add(this.btnSourceFilters);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnProjector);
            this.Controls.Add(this.btnEvents);
            this.Controls.Add(this.tbLog);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AdvancedWindow";
            this.Load += new System.EventHandler(this.AdvancedWindow_Load);
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
    }
}