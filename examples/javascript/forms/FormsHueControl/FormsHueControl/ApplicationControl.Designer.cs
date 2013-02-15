using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsHueControl
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
            this.hueControl1 = new FormsHueControl.Library.HueControl();
            this.SuspendLayout();
            // 
            // hueControl1
            // 
            this.hueControl1.AutoScroll = true;
            this.hueControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hueControl1.Location = new System.Drawing.Point(3, 3);
            this.hueControl1.Name = "hueControl1";
            this.hueControl1.Size = new System.Drawing.Size(218, 46);
            this.hueControl1.TabIndex = 0;
            this.hueControl1.AdjustHue += new System.Action<int>(this.hueControl1_AdjustHue);
            this.hueControl1.Load += new System.EventHandler(this.hueControl1_Load);
            this.hueControl1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hueControl1_Scroll);
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.hueControl1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
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

        private Library.HueControl hueControl1;

    }
}
