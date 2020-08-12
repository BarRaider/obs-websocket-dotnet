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
            this.SuspendLayout();
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(13, 13);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(671, 425);
            this.tbLog.TabIndex = 0;
            // 
            // btnEvents
            // 
            this.btnEvents.Location = new System.Drawing.Point(691, 13);
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(97, 23);
            this.btnEvents.TabIndex = 1;
            this.btnEvents.Text = "Events";
            this.btnEvents.UseVisualStyleBackColor = true;
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnProjector
            // 
            this.btnProjector.Location = new System.Drawing.Point(691, 42);
            this.btnProjector.Name = "btnProjector";
            this.btnProjector.Size = new System.Drawing.Size(97, 23);
            this.btnProjector.TabIndex = 2;
            this.btnProjector.Text = "Projector";
            this.btnProjector.UseVisualStyleBackColor = true;
            this.btnProjector.Click += new System.EventHandler(this.btnProjector_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(691, 71);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(97, 23);
            this.btnRename.TabIndex = 3;
            this.btnRename.Text = "Source Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // AdvancedWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnProjector);
            this.Controls.Add(this.btnEvents);
            this.Controls.Add(this.tbLog);
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
    }
}