using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficLight
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Yellow = new System.Windows.Forms.Panel();
            this.Green = new System.Windows.Forms.Panel();
            this.GrayGreen = new System.Windows.Forms.Panel();
            this.GrayYellow = new System.Windows.Forms.Panel();
            this.GrayRed = new System.Windows.Forms.Panel();
            this.Red = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.Red);
            this.panel1.Controls.Add(this.Yellow);
            this.panel1.Controls.Add(this.Green);
            this.panel1.Controls.Add(this.GrayGreen);
            this.panel1.Controls.Add(this.GrayYellow);
            this.panel1.Controls.Add(this.GrayRed);
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(76, 194);
            this.panel1.TabIndex = 0;
            // 
            // Yellow
            // 
            this.Yellow.BackColor = System.Drawing.Color.Yellow;
            this.Yellow.Location = new System.Drawing.Point(13, 70);
            this.Yellow.Name = "Yellow";
            this.Yellow.Size = new System.Drawing.Size(52, 52);
            this.Yellow.TabIndex = 2;
            this.Yellow.Tag = "Yellow";
            this.Yellow.Visible = false;
            this.Yellow.Click += new System.EventHandler(this.Yellow_Paint);
            // 
            // Green
            // 
            this.Green.BackColor = System.Drawing.Color.Green;
            this.Green.Location = new System.Drawing.Point(13, 128);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(52, 52);
            this.Green.TabIndex = 3;
            this.Green.Tag = "Green";
            this.Green.Visible = false;
            this.Green.Click += new System.EventHandler(this.Green_Paint);
            // 
            // GrayGreen
            // 
            this.GrayGreen.BackColor = System.Drawing.Color.Silver;
            this.GrayGreen.Location = new System.Drawing.Point(13, 128);
            this.GrayGreen.Name = "GrayGreen";
            this.GrayGreen.Size = new System.Drawing.Size(52, 52);
            this.GrayGreen.TabIndex = 2;
            this.GrayGreen.Tag = "GreyGreen";
            this.GrayGreen.Click += new System.EventHandler(this.GrayGreen_Paint);
            // 
            // GrayYellow
            // 
            this.GrayYellow.BackColor = System.Drawing.Color.Silver;
            this.GrayYellow.Location = new System.Drawing.Point(13, 70);
            this.GrayYellow.Name = "GrayYellow";
            this.GrayYellow.Size = new System.Drawing.Size(52, 52);
            this.GrayYellow.TabIndex = 1;
            this.GrayYellow.Tag = "GreyYellow";
            this.GrayYellow.Click += new System.EventHandler(this.GrayYellow_Paint);
            // 
            // GrayRed
            // 
            this.GrayRed.BackColor = System.Drawing.Color.Silver;
            this.GrayRed.Location = new System.Drawing.Point(13, 12);
            this.GrayRed.Name = "GrayRed";
            this.GrayRed.Size = new System.Drawing.Size(52, 52);
            this.GrayRed.TabIndex = 0;
            this.GrayRed.Tag = "GreyRed";
            this.GrayRed.Click += new System.EventHandler(this.GrayRed_Paint);
            // 
            // Red
            // 
            this.Red.BackColor = System.Drawing.Color.Red;
            this.Red.Location = new System.Drawing.Point(13, 12);
            this.Red.Name = "Red";
            this.Red.Size = new System.Drawing.Size(52, 52);
            this.Red.TabIndex = 3;
            this.Red.Tag = "Red";
            this.Red.Visible = false;
            this.Red.Click += new System.EventHandler(this.Red_Paint);
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(83, 198);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

        private Panel panel1;
        private Panel GrayGreen;
        private Panel GrayYellow;
        private Panel GrayRed;
        private Panel Green;
        private Panel Yellow;
        private Panel Red;

    }
}
