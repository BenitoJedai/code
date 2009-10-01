namespace SimpleForms2
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.localMyPanel1 = new SimpleForms2.MyContext.LocalMyPanel();
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
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 215);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(97, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Message 1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(24, 186);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(97, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Message 2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(234, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "This project does know about jsc.meta compiler.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 125);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(281, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "In a pre build event jsc.meta is used to generate .net code";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(308, 12);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(260, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "Show default information";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// localMyPanel1
			// 
			this.localMyPanel1.Comment = "I like this!";
			this.localMyPanel1.Location = new System.Drawing.Point(308, 41);
			this.localMyPanel1.Name = "localMyPanel1";
			this.localMyPanel1.Size = new System.Drawing.Size(391, 216);
			this.localMyPanel1.TabIndex = 7;
			this.localMyPanel1.MoreDetails += new System.Action(this.localMyPanel1_MoreDetails);
			this.localMyPanel1.OK += new System.Action(this.localMyPanel1_OK);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(702, 265);
			this.Controls.Add(this.localMyPanel1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "SimpleForms2. Your C# will be converted javascript and java.";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button3;
		private SimpleForms2.MyContext.LocalMyPanel localMyPanel1;
	}
}

