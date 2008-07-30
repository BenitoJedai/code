namespace FlashConsoleWorm.Test
{
	partial class Launcher
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(200, 64);
			this.button1.TabIndex = 0;
			this.button1.Text = "Go!";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// Launcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(216, 80);
			this.Controls.Add(this.button1);
			this.Name = "Launcher";
			this.Opacity = 0.8;
			this.Padding = new System.Windows.Forms.Padding(8);
			this.Text = "Launcher";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button button1;
	}
}