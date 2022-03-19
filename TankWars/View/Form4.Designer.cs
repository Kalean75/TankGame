
namespace View
{
	partial class Form4
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
			this.aboutTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// aboutTextBox
			// 
			this.aboutTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.aboutTextBox.Enabled = false;
			this.aboutTextBox.Location = new System.Drawing.Point(0, 0);
			this.aboutTextBox.Name = "aboutTextBox";
			this.aboutTextBox.ReadOnly = true;
			this.aboutTextBox.Size = new System.Drawing.Size(668, 602);
			this.aboutTextBox.TabIndex = 0;
			this.aboutTextBox.Text = "Tank Wars\nProgrammed by:\nDevin White\nXuyen Nguyen\n2021\n";
			// 
			// Form4
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(668, 602);
			this.Controls.Add(this.aboutTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form4";
			this.Text = "Form4";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox aboutTextBox;
	}
}