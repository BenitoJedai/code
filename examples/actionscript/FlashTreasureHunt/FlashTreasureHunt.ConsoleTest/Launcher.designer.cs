namespace FlashTreasureHunt.ConsoleTest
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.button1 = new System.Windows.Forms.Button();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.ShowServerWindow = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ShowServerWindow);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(4, 176);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(324, 27);
			this.panel1.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(4, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.button1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.checkedListBox1);
			this.splitContainer1.Size = new System.Drawing.Size(324, 172);
			this.splitContainer1.SplitterDistance = 107;
			this.splitContainer1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(107, 172);
			this.button1.TabIndex = 1;
			this.button1.Text = "Go!";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(213, 169);
			this.checkedListBox1.TabIndex = 0;
			// 
			// ShowServerWindow
			// 
			this.ShowServerWindow.AutoSize = true;
			this.ShowServerWindow.Location = new System.Drawing.Point(32, 4);
			this.ShowServerWindow.Name = "ShowServerWindow";
			this.ShowServerWindow.Size = new System.Drawing.Size(129, 17);
			this.ShowServerWindow.TabIndex = 0;
			this.ShowServerWindow.Text = "Show Server Window";
			this.ShowServerWindow.UseVisualStyleBackColor = true;
			// 
			// Launcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 207);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Name = "Launcher";
			this.Opacity = 0.8;
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "Launcher";
			this.TopMost = true;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.CheckBox ShowServerWindow;
		private System.Windows.Forms.SplitContainer splitContainer1;
		public System.Windows.Forms.Button button1;
		public System.Windows.Forms.CheckedListBox checkedListBox1;

	}
}