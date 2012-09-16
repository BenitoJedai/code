using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MandelbrotFormsControl
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.mandelbrotComponent2 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.mandelbrotComponent1 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 176);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(40, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Go";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(176, 176);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(40, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Go";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // mandelbrotComponent2
            // 
            this.mandelbrotComponent2.Location = new System.Drawing.Point(176, 19);
            this.mandelbrotComponent2.Name = "mandelbrotComponent2";
            this.mandelbrotComponent2.Size = new System.Drawing.Size(132, 132);
            this.mandelbrotComponent2.TabIndex = 0;
            // 
            // mandelbrotComponent1
            // 
            this.mandelbrotComponent1.Location = new System.Drawing.Point(20, 19);
            this.mandelbrotComponent1.Name = "mandelbrotComponent1";
            this.mandelbrotComponent1.Size = new System.Drawing.Size(131, 132);
            this.mandelbrotComponent1.TabIndex = 0;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.mandelbrotComponent2);
            this.Controls.Add(this.mandelbrotComponent1);
            this.DoubleBuffered = true;
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Library.MandelbrotComponent mandelbrotComponent2;
        private Library.MandelbrotComponent mandelbrotComponent1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;

    }
}
