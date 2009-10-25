﻿namespace SimpleChat2
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.mySync1 = new SimpleChat2.MySync(this.components);
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.outgoingMessages1 = new SimpleChat2.OutgoingMessages(this.components);
			this.button4 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.RegisteringTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(250, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Port configuration:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 39);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(145, 42);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "6666; 6667";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(281, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(250, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Route configuration:";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(283, 39);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(229, 131);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "[\r\n[\"Tanel Tammet\",\"22.33.44.55:6666\"],\r\n[\"Peeter Laud\",\"22.33.44.11:6666\"]\r\n]";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(17, 128);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(144, 26);
			this.textBox3.TabIndex = 4;
			this.textBox3.Text = "Kenny";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(250, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Your nickname:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(17, 188);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Connect";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(317, 188);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(195, 23);
			this.button2.TabIndex = 6;
			this.button2.Text = "Disconnect";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(17, 228);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(495, 152);
			this.textBox4.TabIndex = 7;
			this.textBox4.Text = "This is a chat application.\r\n";
			// 
			// textBox5
			// 
			this.textBox5.Enabled = false;
			this.textBox5.Location = new System.Drawing.Point(17, 424);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(144, 42);
			this.textBox5.TabIndex = 9;
			this.textBox5.Text = "Jumbo";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(13, 398);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(250, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Reciepient nickname:";
			// 
			// textBox6
			// 
			this.textBox6.Enabled = false;
			this.textBox6.Location = new System.Drawing.Point(170, 424);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(229, 42);
			this.textBox6.TabIndex = 11;
			this.textBox6.Text = "Hi!";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(166, 398);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(250, 23);
			this.label5.TabIndex = 10;
			this.label5.Text = "Message:";
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(406, 425);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(106, 41);
			this.button3.TabIndex = 12;
			this.button3.Text = "Send";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// outgoingMessages1
			// 
			this.outgoingMessages1.PathPrefix = "/chat";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(123, 188);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 13;
			this.button4.Text = "Spawn";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(23, 157);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Registering...";
			this.label6.Visible = false;
			// 
			// RegisteringTimer
			// 
			this.RegisteringTimer.Interval = 5000;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(532, 478);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private MySync mySync1;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button3;
		private OutgoingMessages outgoingMessages1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Timer RegisteringTimer;
	}
}