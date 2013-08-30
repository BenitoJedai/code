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
            this.mandelbrotComponent2 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.mandelbrotComponent1 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.mandelbrotComponent3 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mandelbrotComponent2
            // 
            this.mandelbrotComponent2.BackColor = System.Drawing.Color.Black;
            this.mandelbrotComponent2.Location = new System.Drawing.Point(176, 19);
            this.mandelbrotComponent2.Name = "mandelbrotComponent2";
            this.mandelbrotComponent2.Size = new System.Drawing.Size(132, 132);
            this.mandelbrotComponent2.TabIndex = 0;
            // 
            // mandelbrotComponent1
            // 
            this.mandelbrotComponent1.BackColor = System.Drawing.Color.Black;
            this.mandelbrotComponent1.Location = new System.Drawing.Point(20, 19);
            this.mandelbrotComponent1.Name = "mandelbrotComponent1";
            this.mandelbrotComponent1.Size = new System.Drawing.Size(131, 132);
            this.mandelbrotComponent1.TabIndex = 0;
            // 
            // mandelbrotComponent3
            // 
            this.mandelbrotComponent3.BackColor = System.Drawing.Color.Black;
            this.mandelbrotComponent3.Location = new System.Drawing.Point(20, 179);
            this.mandelbrotComponent3.Name = "mandelbrotComponent3";
            this.mandelbrotComponent3.Size = new System.Drawing.Size(744, 491);
            this.mandelbrotComponent3.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(375, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mandelbrotComponent3);
            this.Controls.Add(this.mandelbrotComponent2);
            this.Controls.Add(this.mandelbrotComponent1);
            this.DoubleBuffered = true;
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(798, 690);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }

        private Library.MandelbrotComponent mandelbrotComponent2;
        private Library.MandelbrotComponent mandelbrotComponent1;
        private Library.MandelbrotComponent mandelbrotComponent3;
        private Button button1;

    }
}
