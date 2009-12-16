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
			this.findname_handler1 = new SimpleChat2.Buffer.Server.findname_handler();
			this.requestDispatcher1 = new SimpleChat2.ServerProvider.RequestDispatcher(this.components);
			this.asknames_handler1 = new SimpleChat2.Buffer.Server.asknames_handler();
			this.sendmessage_handler1 = new SimpleChat2.Buffer.Server.sendmessage_handler();
			this.sendname_handler1 = new SimpleChat2.Buffer.Server.sendname_handler();
			this.sendnames_handler1 = new SimpleChat2.Buffer.Server.sendnames_handler();
			this.mySync1 = new SimpleChat2.ServerProvider.MySync(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button5 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button6 = new System.Windows.Forms.Button();
			this.Poller = new System.Windows.Forms.Timer(this.components);
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.RegistrationTimeout = new System.Windows.Forms.Timer(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
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
			this.textBox1.Size = new System.Drawing.Size(539, 168);
			this.textBox1.TabIndex = 3;
			// 
			// textBox2
			// 
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(12, 345);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(399, 20);
			this.textBox2.TabIndex = 4;
			// 
			// button5
			// 
			this.button5.Enabled = false;
			this.button5.Location = new System.Drawing.Point(417, 345);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(135, 23);
			this.button5.TabIndex = 5;
			this.button5.Text = "Send";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Name:";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(81, 10);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(176, 20);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = "ken";
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(263, 8);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(107, 23);
			this.button6.TabIndex = 8;
			this.button6.Text = "Register";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// Poller
			// 
			this.Poller.Interval = 400;
			this.Poller.Tick += new System.EventHandler(this.Poller_Tick);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(553, 34);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(122, 23);
			this.button7.TabIndex = 9;
			this.button7.Text = "Spawn Cartman";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(553, 63);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(122, 23);
			this.button8.TabIndex = 10;
			this.button8.Text = "Spawn Tom";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(81, 39);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(176, 54);
			this.textBox4.TabIndex = 11;
			this.textBox4.Text = "ken\r\nCartman\r\nTom\r\n";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 23);
			this.label2.TabIndex = 12;
			this.label2.Text = "Friends:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 142);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(540, 23);
			this.label3.TabIndex = 13;
			this.label3.Text = "...";
			// 
			// RegistrationTimeout
			// 
			this.RegistrationTimeout.Interval = 5000;
			this.RegistrationTimeout.Tick += new System.EventHandler(this.RegistrationTimeout_Tick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(553, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(122, 23);
			this.button1.TabIndex = 14;
			this.button1.Text = "Spawn ken";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(687, 380);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SimpleChat2.Buffer.Server.findname_handler findname_handler1;
		private SimpleChat2.ServerProvider.RequestDispatcher requestDispatcher1;
		private SimpleChat2.Buffer.Server.asknames_handler asknames_handler1;
		private SimpleChat2.Buffer.Server.sendmessage_handler sendmessage_handler1;
		private SimpleChat2.Buffer.Server.sendname_handler sendname_handler1;
		private SimpleChat2.Buffer.Server.sendnames_handler sendnames_handler1;
		private SimpleChat2.ServerProvider.MySync mySync1;
		public System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button6;
		public System.Windows.Forms.Timer Poller;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label label3;
		public System.Windows.Forms.Timer RegistrationTimeout;
		private System.Windows.Forms.Button button1;
	}
}