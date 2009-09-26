namespace TextualUserControls
{
	partial class UserControl1
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// ButtonOK
			// 
			this.ButtonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.ButtonOK.ForeColor = System.Drawing.Color.Red;
			this.ButtonOK.Location = new System.Drawing.Point(296, 15);
			this.ButtonOK.Size = new System.Drawing.Size(60, 42);
			// 
			// ButtonMoreDetails
			// 
			this.ButtonMoreDetails.Location = new System.Drawing.Point(80, 216);
			// 
			// ButtonHelpMe
			// 
			this.ButtonHelpMe.Location = new System.Drawing.Point(208, 172);
			this.ButtonHelpMe.Size = new System.Drawing.Size(148, 68);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(8, 0);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(196, 17);
			this.checkBox1.TabIndex = 8;
			this.checkBox1.Text = "Windows Forms designer is so easy!";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// UserControl1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBox1);
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(386, 273);
			//this.Controls.SetChildIndex(this.checkBox1, 0);
			//this.Controls.SetChildIndex(this.ButtonOK, 0);
			//this.Controls.SetChildIndex(this.ButtonHelpMe, 0);
			//this.Controls.SetChildIndex(this.ButtonMoreDetails, 0);
			//this.Controls.SetChildIndex(this.ButtonCancel, 0);
			//this.Controls.SetChildIndex(this.Label2, 0);
			//this.Controls.SetChildIndex(this.Label1, 0);
			//this.Controls.SetChildIndex(this.TextBoxTextbox2, 0);
			//this.Controls.SetChildIndex(this.TextBoxTextbox1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBox1;
	}
}
