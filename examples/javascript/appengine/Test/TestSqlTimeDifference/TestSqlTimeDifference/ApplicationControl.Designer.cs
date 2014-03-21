using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestSqlTimeDifference
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
            this.label1 = new System.Windows.Forms.Label();
            this.sTime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server time";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // sTime
            // 
            this.sTime.AutoSize = true;
            this.sTime.Location = new System.Drawing.Point(119, 41);
            this.sTime.Name = "sTime";
            this.sTime.Size = new System.Drawing.Size(35, 13);
            this.sTime.TabIndex = 2;
            this.sTime.Text = "label3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lTime
            // 
            this.lTime.AutoSize = true;
            this.lTime.Location = new System.Drawing.Point(119, 66);
            this.lTime.Name = "lTime";
            this.lTime.Size = new System.Drawing.Size(35, 13);
            this.lTime.TabIndex = 5;
            this.lTime.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Local time";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lTime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sTime);
            this.Controls.Add(this.label1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(491, 360);
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private Label label1;
        public Label sTime;
        public Button button1;
        public Label lTime;
        private Label label2;

    }
}
