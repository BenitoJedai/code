using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MandelbrotFormsControl.Library
{
    public partial class MandelbrotComponent
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.DoTask = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.ForeColor = System.Drawing.Color.Yellow;
            this.checkBox1.Location = new System.Drawing.Point(24, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "[Design]";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DoTask
            // 
            this.DoTask.AutoSize = true;
            this.DoTask.BackColor = System.Drawing.Color.Transparent;
            this.DoTask.ForeColor = System.Drawing.Color.Yellow;
            this.DoTask.Location = new System.Drawing.Point(3, 3);
            this.DoTask.Name = "DoTask";
            this.DoTask.Size = new System.Drawing.Size(15, 14);
            this.DoTask.TabIndex = 2;
            this.DoTask.UseVisualStyleBackColor = false;
            // 
            // MandelbrotComponent
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.DoTask);
            this.Controls.Add(this.checkBox1);
            this.DoubleBuffered = true;
            this.Name = "MandelbrotComponent";
            this.Size = new System.Drawing.Size(128, 128);
            this.Load += new System.EventHandler(this.MandelbrotComponent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox DoTask;
    }
}
