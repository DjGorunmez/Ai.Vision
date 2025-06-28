namespace Ai.Cam.WindowsApp
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			gpStatus = new GroupBox();
			lblStatus = new Label();
			gpControls = new GroupBox();
			btnStop = new Button();
			btnStart = new Button();
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			camerasToolStripMenuItem = new ToolStripMenuItem();
			startToolStripMenuItem = new ToolStripMenuItem();
			stopToolStripMenuItem = new ToolStripMenuItem();
			exitToolStripMenuItem = new ToolStripMenuItem();
			faceBox = new PictureBox();
			bodyPic = new PictureBox();
			idBox = new PictureBox();
			gpStatus.SuspendLayout();
			gpControls.SuspendLayout();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)faceBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)bodyPic).BeginInit();
			((System.ComponentModel.ISupportInitialize)idBox).BeginInit();
			SuspendLayout();
			// 
			// gpStatus
			// 
			gpStatus.Controls.Add(lblStatus);
			gpStatus.Location = new Point(12, 39);
			gpStatus.Name = "gpStatus";
			gpStatus.Size = new Size(257, 136);
			gpStatus.TabIndex = 0;
			gpStatus.TabStop = false;
			gpStatus.Text = "Status";
			// 
			// lblStatus
			// 
			lblStatus.AutoSize = true;
			lblStatus.Location = new Point(21, 28);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(48, 15);
			lblStatus.TabIndex = 0;
			lblStatus.Text = "Status...";
			// 
			// gpControls
			// 
			gpControls.Controls.Add(btnStop);
			gpControls.Controls.Add(btnStart);
			gpControls.Location = new Point(12, 194);
			gpControls.Name = "gpControls";
			gpControls.Size = new Size(257, 400);
			gpControls.TabIndex = 1;
			gpControls.TabStop = false;
			gpControls.Text = "Controls";
			// 
			// btnStop
			// 
			btnStop.Location = new Point(110, 32);
			btnStop.Name = "btnStop";
			btnStop.Size = new Size(85, 42);
			btnStop.TabIndex = 1;
			btnStop.Text = "Stop";
			btnStop.UseVisualStyleBackColor = true;
			btnStop.Click += btnStop_Click;
			// 
			// btnStart
			// 
			btnStart.Location = new Point(19, 32);
			btnStart.Name = "btnStart";
			btnStart.Size = new Size(85, 42);
			btnStart.TabIndex = 0;
			btnStart.Text = "Start";
			btnStart.UseVisualStyleBackColor = true;
			btnStart.Click += btnStart_Click;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(946, 24);
			menuStrip1.TabIndex = 3;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { camerasToolStripMenuItem, exitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			// 
			// camerasToolStripMenuItem
			// 
			camerasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { startToolStripMenuItem, stopToolStripMenuItem });
			camerasToolStripMenuItem.Name = "camerasToolStripMenuItem";
			camerasToolStripMenuItem.Size = new Size(123, 22);
			camerasToolStripMenuItem.Text = "Camera's";
			// 
			// startToolStripMenuItem
			// 
			startToolStripMenuItem.Name = "startToolStripMenuItem";
			startToolStripMenuItem.Size = new Size(98, 22);
			startToolStripMenuItem.Text = "Start";
			// 
			// stopToolStripMenuItem
			// 
			stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			stopToolStripMenuItem.Size = new Size(98, 22);
			stopToolStripMenuItem.Text = "Stop";
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new Size(123, 22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			// 
			// faceBox
			// 
			faceBox.Location = new Point(453, 283);
			faceBox.Name = "faceBox";
			faceBox.Size = new Size(481, 300);
			faceBox.SizeMode = PictureBoxSizeMode.StretchImage;
			faceBox.TabIndex = 4;
			faceBox.TabStop = false;
			// 
			// bodyPic
			// 
			bodyPic.Location = new Point(275, 283);
			bodyPic.Name = "bodyPic";
			bodyPic.Size = new Size(172, 300);
			bodyPic.SizeMode = PictureBoxSizeMode.StretchImage;
			bodyPic.TabIndex = 5;
			bodyPic.TabStop = false;
			// 
			// idBox
			// 
			idBox.Location = new Point(362, 33);
			idBox.Name = "idBox";
			idBox.Size = new Size(454, 241);
			idBox.SizeMode = PictureBoxSizeMode.StretchImage;
			idBox.TabIndex = 6;
			idBox.TabStop = false;
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(946, 606);
			Controls.Add(idBox);
			Controls.Add(bodyPic);
			Controls.Add(faceBox);
			Controls.Add(gpControls);
			Controls.Add(gpStatus);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "Main";
			Text = "im";
			gpStatus.ResumeLayout(false);
			gpStatus.PerformLayout();
			gpControls.ResumeLayout(false);
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)faceBox).EndInit();
			((System.ComponentModel.ISupportInitialize)bodyPic).EndInit();
			((System.ComponentModel.ISupportInitialize)idBox).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private GroupBox gpStatus;
        private GroupBox gpControls;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private Label lblStatus;
        private Button btnStop;
        private Button btnStart;
        private ToolStripMenuItem camerasToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
		private PictureBox faceBox;
		private PictureBox bodyPic;
		private PictureBox idBox;
	}
}
