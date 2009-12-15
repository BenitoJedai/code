namespace SimpleChat2
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.findname_handler1 = new SimpleChat2.Buffer.Server.findname_handler();
			this.requestDispatcher1 = new SimpleChat2.ServerProvider.RequestDispatcher(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.asknames_handler1 = new SimpleChat2.Buffer.Server.asknames_handler();
			this.sendmessage_handler1 = new SimpleChat2.Buffer.Server.sendmessage_handler();
			this.sendname_handler1 = new SimpleChat2.Buffer.Server.sendname_handler();
			this.sendnames_handler1 = new SimpleChat2.Buffer.Server.sendnames_handler();
			this.mySync1 = new SimpleChat2.ServerProvider.MySync(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(190, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Enable Server";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 41);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(190, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Disable Server";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// findname_handler1
			// 
			this.findname_handler1.Dispatcher = this.requestDispatcher1;
			this.findname_handler1.Request += new SimpleChat2.Buffer.Server.findname_handler.findname_delegate(this.findname_handler1_Request_1);
			// 
			// requestDispatcher1
			// 
			this.requestDispatcher1.DispatcherEnabled = false;
			this.requestDispatcher1.Port = 8081;
			this.requestDispatcher1.Tick += new System.Action(this.requestDispatcher1_Tick);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(266, 137);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "A";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(266, 166);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 2;
			this.button4.Text = "B";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// asknames_handler1
			// 
			this.asknames_handler1.Dispatcher = this.requestDispatcher1;
			// 
			// sendmessage_handler1
			// 
			this.sendmessage_handler1.Dispatcher = this.requestDispatcher1;
			this.sendmessage_handler1.Request += new SimpleChat2.Buffer.Server.sendmessage_handler.sendmessage_delegate(this.sendmessage_handler1_Request);
			// 
			// sendname_handler1
			// 
			this.sendname_handler1.Dispatcher = this.requestDispatcher1;
			// 
			// sendnames_handler1
			// 
			this.sendnames_handler1.Dispatcher = this.requestDispatcher1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(13, 168);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(247, 168);
			this.textBox1.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(511, 395);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SimpleChat2.Buffer.Server.findname_handler findname_handler1;
		private SimpleChat2.ServerProvider.RequestDispatcher requestDispatcher1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private SimpleChat2.Buffer.Server.asknames_handler asknames_handler1;
		private SimpleChat2.Buffer.Server.sendmessage_handler sendmessage_handler1;
		private SimpleChat2.Buffer.Server.sendname_handler sendname_handler1;
		private SimpleChat2.Buffer.Server.sendnames_handler sendnames_handler1;
		private SimpleChat2.ServerProvider.MySync mySync1;
		public System.Windows.Forms.TextBox textBox1;
	}
}