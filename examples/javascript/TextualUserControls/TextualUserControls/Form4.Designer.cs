namespace TextualUserControls
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
			this.userControl11 = new TextualUserControls.UserControl1();
			this.SuspendLayout();
			// 
			// userControl11
			// 
			this.userControl11.Location = new System.Drawing.Point(50, 47);
			this.userControl11.Name = "userControl11";
			this.userControl11.Size = new System.Drawing.Size(386, 273);
			this.userControl11.TabIndex = 0;
			this.userControl11.Textbox1 = "Do you like it?\r\nYes?\r\n\r\npowered by jsc";
			this.userControl11.Textbox2 = "textbox2";
			this.userControl11.OK += new System.Action(this.userControl11_OK);
			// 
			// Form4
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 381);
			this.Controls.Add(this.userControl11);
			this.Name = "Form4";
			this.Text = "Form4";
			this.ResumeLayout(false);

		}

		#endregion

		private UserControl1 userControl11;


	}
}