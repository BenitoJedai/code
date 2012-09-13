using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleWindowsFormsApplication
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel5.Location = new System.Drawing.Point(133, 178);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(140, 17);
            this.linkLabel5.TabIndex = 28;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "● Support for flash or java?";
            this.linkLabel5.UseCompatibleTextRendering = true;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel4.Location = new System.Drawing.Point(123, 161);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(224, 17);
            this.linkLabel4.TabIndex = 27;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "What about WPF interop and WebBrowser?";
            this.linkLabel4.UseCompatibleTextRendering = true;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel3.Location = new System.Drawing.Point(129, 289);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(108, 17);
            this.linkLabel3.TabIndex = 26;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Order a feature now!";
            this.linkLabel3.UseCompatibleTextRendering = true;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel6.Location = new System.Drawing.Point(134, 229);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(176, 17);
            this.linkLabel6.TabIndex = 25;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "● Windows Forms with ASP.NET?";
            this.linkLabel6.UseCompatibleTextRendering = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel2.Location = new System.Drawing.Point(134, 212);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(113, 17);
            this.linkLabel2.TabIndex = 24;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "● Vista look and feel?";
            this.linkLabel2.UseCompatibleTextRendering = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel1.Location = new System.Drawing.Point(133, 195);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 17);
            this.linkLabel1.TabIndex = 23;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "● Rich text editor?";
            this.linkLabel1.UseCompatibleTextRendering = true;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel6.Location = new System.Drawing.Point(560, 311);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(85, 4);
            this.panel6.TabIndex = 22;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Navy;
            this.panel5.Location = new System.Drawing.Point(469, 311);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(85, 4);
            this.panel5.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel4.Location = new System.Drawing.Point(378, 311);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(85, 4);
            this.panel4.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Blue;
            this.panel3.Location = new System.Drawing.Point(287, 311);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(85, 4);
            this.panel3.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel2.Location = new System.Drawing.Point(196, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(85, 4);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(105, 311);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 4);
            this.panel1.TabIndex = 17;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(405, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(219, 58);
            this.button3.TabIndex = 16;
            this.button3.Text = "Show what controls can be used currently";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(499, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Message 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Message 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "This project does not know about jsc compiler";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "This form was designed with Visual Studio";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.linkLabel5);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel6);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(750, 404);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel6;
        private LinkLabel linkLabel2;
        private Timer timer1;
        private LinkLabel linkLabel1;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel1;
        private Button button3;
        private Button button2;
        private Button button1;
        private Label label2;
        private Label label1;

    }
}
