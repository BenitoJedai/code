namespace FormsAvalonAnimation
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel6 = new System.Windows.Forms.LinkLabel();
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.animationControl1 = new FormsAvalonAnimation.AnimationControl();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "This form was designed with Visual Studio";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(223, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "This project does not know about jsc compiler";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.panel1.Location = new System.Drawing.Point(6, 234);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(85, 4);
			this.panel1.TabIndex = 3;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.panel2.Location = new System.Drawing.Point(97, 234);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(85, 4);
			this.panel2.TabIndex = 4;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.Blue;
			this.panel3.Location = new System.Drawing.Point(188, 234);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(85, 4);
			this.panel3.TabIndex = 5;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.panel4.Location = new System.Drawing.Point(279, 234);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(85, 4);
			this.panel4.TabIndex = 6;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.Navy;
			this.panel5.Location = new System.Drawing.Point(370, 234);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(85, 4);
			this.panel5.TabIndex = 7;
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.panel6.Location = new System.Drawing.Point(461, 234);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(85, 4);
			this.panel6.TabIndex = 8;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
			this.linkLabel2.Location = new System.Drawing.Point(35, 135);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(113, 17);
			this.linkLabel2.TabIndex = 10;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "● Vista look and feel?";
			this.linkLabel2.UseCompatibleTextRendering = true;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
			this.linkLabel3.Location = new System.Drawing.Point(30, 212);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(108, 17);
			this.linkLabel3.TabIndex = 10;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Order a feature now!";
			this.linkLabel3.UseCompatibleTextRendering = true;
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel4
			// 
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
			this.linkLabel4.Location = new System.Drawing.Point(24, 84);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(224, 17);
			this.linkLabel4.TabIndex = 11;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "What about WPF interop and WebBrowser?";
			this.linkLabel4.UseCompatibleTextRendering = true;
			this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
			// 
			// linkLabel5
			// 
			this.linkLabel5.AutoSize = true;
			this.linkLabel5.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
			this.linkLabel5.Location = new System.Drawing.Point(34, 101);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(140, 17);
			this.linkLabel5.TabIndex = 11;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = "● Support for flash or java?";
			this.linkLabel5.UseCompatibleTextRendering = true;
			this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
			this.linkLabel1.Location = new System.Drawing.Point(34, 118);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(96, 17);
			this.linkLabel1.TabIndex = 10;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "● Rich text editor?";
			this.linkLabel1.UseCompatibleTextRendering = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel6
			// 
			this.linkLabel6.AutoSize = true;
			this.linkLabel6.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
			this.linkLabel6.Location = new System.Drawing.Point(35, 152);
			this.linkLabel6.Name = "linkLabel6";
			this.linkLabel6.Size = new System.Drawing.Size(176, 17);
			this.linkLabel6.TabIndex = 10;
			this.linkLabel6.TabStop = true;
			this.linkLabel6.Text = "● Windows Forms with ASP.NET?";
			this.linkLabel6.UseCompatibleTextRendering = true;
			this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// elementHost1
			// 
			this.elementHost1.Location = new System.Drawing.Point(6, 3);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(537, 246);
			this.elementHost1.TabIndex = 12;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Child = this.animationControl1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(545, 250);
			this.Controls.Add(this.linkLabel5);
			this.Controls.Add(this.linkLabel4);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.linkLabel6);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.panel6);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.elementHost1);
			this.Name = "Form1";
			this.Text = "FormsAvalonAnimation. Your C# will be converted javascript.";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel5;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel6;
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private AnimationControl animationControl1;
	}
}

