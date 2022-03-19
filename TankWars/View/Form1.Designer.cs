
namespace Client
{
	partial class TankWars
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TankWars));
			this.PlayerNameTextBox = new System.Windows.Forms.TextBox();
			this.ConnectButton = new System.Windows.Forms.Button();
			this.ServerNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.MenuPanel = new System.Windows.Forms.Panel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.retroModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// PlayerNameTextBox
			// 
			this.PlayerNameTextBox.Location = new System.Drawing.Point(198, 27);
			this.PlayerNameTextBox.Name = "PlayerNameTextBox";
			this.PlayerNameTextBox.Size = new System.Drawing.Size(328, 20);
			this.PlayerNameTextBox.TabIndex = 0;
			// 
			// ConnectButton
			// 
			this.ConnectButton.Location = new System.Drawing.Point(532, 24);
			this.ConnectButton.Name = "ConnectButton";
			this.ConnectButton.Size = new System.Drawing.Size(75, 23);
			this.ConnectButton.TabIndex = 1;
			this.ConnectButton.Text = "Connect";
			this.ConnectButton.UseVisualStyleBackColor = true;
			this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
			// 
			// ServerNameTextBox
			// 
			this.ServerNameTextBox.Location = new System.Drawing.Point(37, 27);
			this.ServerNameTextBox.Name = "ServerNameTextBox";
			this.ServerNameTextBox.Size = new System.Drawing.Size(100, 20);
			this.ServerNameTextBox.TabIndex = 2;
			this.ServerNameTextBox.Text = "localhost";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(324, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Player Name";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(68, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Server";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// MenuPanel
			// 
			this.MenuPanel.AutoSize = true;
			this.MenuPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.MenuPanel.Controls.Add(this.toolStripContainer1);
			this.MenuPanel.Controls.Add(this.label2);
			this.MenuPanel.Controls.Add(this.label1);
			this.MenuPanel.Controls.Add(this.ServerNameTextBox);
			this.MenuPanel.Controls.Add(this.ConnectButton);
			this.MenuPanel.Controls.Add(this.PlayerNameTextBox);
			this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.MenuPanel.Location = new System.Drawing.Point(0, 0);
			this.MenuPanel.Margin = new System.Windows.Forms.Padding(2);
			this.MenuPanel.Name = "MenuPanel";
			this.MenuPanel.Size = new System.Drawing.Size(800, 52);
			this.MenuPanel.TabIndex = 6;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(185, 0);
			this.toolStripContainer1.Location = new System.Drawing.Point(614, 26);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(185, 23);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
			this.toolStrip1.Location = new System.Drawing.Point(6, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(63, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.retroModeToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(51, 22);
			this.toolStripDropDownButton1.Text = "Menu";
			// 
			// controlsToolStripMenuItem
			// 
			this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
			this.controlsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.controlsToolStripMenuItem.Text = "Controls";
			this.controlsToolStripMenuItem.Click += new System.EventHandler(this.controlsToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// retroModeToolStripMenuItem
			// 
			this.retroModeToolStripMenuItem.CheckOnClick = true;
			this.retroModeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.retroModeToolStripMenuItem.Name = "retroModeToolStripMenuItem";
			this.retroModeToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.retroModeToolStripMenuItem.Text = "Retro Mode";
			this.retroModeToolStripMenuItem.Click += new System.EventHandler(this.retroModeToolStripMenuItem_Click);
			// 
			// TankWars
			// 
			this.AcceptButton = this.ConnectButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.MenuPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "TankWars";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TankWars";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HandleKeyUp);
			this.MenuPanel.ResumeLayout(false);
			this.MenuPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox PlayerNameTextBox;
		private System.Windows.Forms.Button ConnectButton;
		private System.Windows.Forms.TextBox ServerNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel MenuPanel;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem retroModeToolStripMenuItem;
	}
}

