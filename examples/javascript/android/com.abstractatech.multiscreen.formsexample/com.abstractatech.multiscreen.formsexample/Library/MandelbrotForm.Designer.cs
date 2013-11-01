using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace com.abstractatech.multiscreen.formsexample.Library
{
    public partial class MandelbrotForm
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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mandelbrotComponent2 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.mandelbrotComponent1 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.SuspendLayout();
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(344, 281);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(40, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Go";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(188, 281);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(40, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Go";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // mandelbrotComponent2
            // 
            this.mandelbrotComponent2.Location = new System.Drawing.Point(344, 124);
            this.mandelbrotComponent2.Name = "mandelbrotComponent2";
            this.mandelbrotComponent2.Size = new System.Drawing.Size(132, 132);
            this.mandelbrotComponent2.TabIndex = 2;
            // 
            // mandelbrotComponent1
            // 
            this.mandelbrotComponent1.Location = new System.Drawing.Point(188, 124);
            this.mandelbrotComponent1.Name = "mandelbrotComponent1";
            this.mandelbrotComponent1.Size = new System.Drawing.Size(131, 132);
            this.mandelbrotComponent1.TabIndex = 3;
            // 
            // MandelbrotForm
            // 
            this.ClientSize = new System.Drawing.Size(665, 422);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.mandelbrotComponent2);
            this.Controls.Add(this.mandelbrotComponent1);
            this.DoubleBuffered = true;
            this.Name = "MandelbrotForm";
            this.Text = "Plasma";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private MandelbrotFormsControl.Library.MandelbrotComponent mandelbrotComponent2;
        private MandelbrotFormsControl.Library.MandelbrotComponent mandelbrotComponent1;
    }
}
