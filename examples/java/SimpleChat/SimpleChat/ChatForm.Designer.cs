namespace SimpleChat
{
	partial class ChatForm
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button2 = new System.Windows.Forms.Button();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.MessageEffectTimer = new System.Windows.Forms.Timer(this.components);
			this.MessageHider = new System.Windows.Forms.Timer(this.components);
			this.remoteUsers1 = new SimpleChat.RemoteUsers();
			this.localInformation1 = new SimpleChat.MyContext.LocalInformation();
			this.localPopular1 = new SimpleChat.MyContext.LocalPopular();
			this.webServerComponent1 = new SimpleChat.WebServerComponent();
			this.outgoingMessages1 = new SimpleChat.OutgoingMessages(this.components);
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 143);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(509, 145);
			this.textBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(433, 294);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 34);
			this.button1.TabIndex = 1;
			this.button1.Text = "Send";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Message From:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Message To:";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(99, 13);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(328, 32);
			this.textBox2.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(96, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(331, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "registering your name... 3%";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(99, 75);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(328, 32);
			this.textBox3.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(96, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(331, 18);
			this.label4.TabIndex = 6;
			this.label4.Text = "looking for users... 3%";
			// 
			// textBox4
			// 
			this.textBox4.Enabled = false;
			this.textBox4.Location = new System.Drawing.Point(99, 296);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(328, 32);
			this.textBox4.TabIndex = 8;
			this.textBox4.Text = "howdy!";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 296);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Message:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(13, 334);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(508, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "Chat Form";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 3000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(433, 13);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 32);
			this.button2.TabIndex = 10;
			this.button2.Text = "Foo";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(433, 75);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(88, 32);
			this.button3.TabIndex = 11;
			this.button3.Text = "Bar";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(643, 13);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(101, 32);
			this.button4.TabIndex = 12;
			this.button4.Text = "Spawn";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(667, 294);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(77, 34);
			this.button5.TabIndex = 14;
			this.button5.Text = "Tween";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(583, 296);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 32);
			this.button6.TabIndex = 15;
			this.button6.Text = "Message";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// MessageEffectTimer
			// 
			this.MessageEffectTimer.Enabled = true;
			this.MessageEffectTimer.Interval = 50;
			this.MessageEffectTimer.Tick += new System.EventHandler(this.MessageEffectTimer_Tick);
			// 
			// MessageHider
			// 
			this.MessageHider.Interval = 5000;
			this.MessageHider.Tick += new System.EventHandler(this.MessageHider_Tick);
			// 
			// remoteUsers1
			// 
			this.remoteUsers1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.remoteUsers1.Location = new System.Drawing.Point(527, 75);
			this.remoteUsers1.Message = "Currently online chat buddies:";
			this.remoteUsers1.MessageHeight = 32;
			this.remoteUsers1.Name = "remoteUsers1";
			this.remoteUsers1.Size = new System.Drawing.Size(217, 213);
			this.remoteUsers1.TabIndex = 13;
			// 
			// webServerComponent1
			// 
			this.webServerComponent1.Configuration = new SimpleChat.WebServerProvider[0];
			this.webServerComponent1.Start += new SimpleChat.WebServerProviderAction(this.webServerComponent1_Start);
			this.webServerComponent1.IncomingData += new SimpleChat.WebServerProvider.IncomingDataDelegate(this.webServerComponent1_IncomingData);
			this.webServerComponent1.Shutdown += new SimpleChat.WebServerProviderAction(this.webServerComponent1_Shutdown);
			// 
			// outgoingMessages1
			// 
			this.outgoingMessages1.LocalConfigurationFile = "Configuration/servers.txt";
			this.outgoingMessages1.PathPrefix = "/chat";
			this.outgoingMessages1.NotFound += new SimpleChat.MessageEndpointAction(this.outgoingMessages1_NotFound);
			// 
			// ChatForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(756, 356);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.remoteUsers1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Name = "ChatForm";
			this.Text = "ChatForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Timer timer1;
		private SimpleChat.MyContext.LocalInformation localInformation1;
		private System.Windows.Forms.Button button2;
		private SimpleChat.MyContext.LocalPopular localPopular1;
		private System.Windows.Forms.Timer timer2;
		private WebServerComponent webServerComponent1;
		private OutgoingMessages outgoingMessages1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private RemoteUsers remoteUsers1;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Timer MessageEffectTimer;
		private System.Windows.Forms.Timer MessageHider;
	}
}