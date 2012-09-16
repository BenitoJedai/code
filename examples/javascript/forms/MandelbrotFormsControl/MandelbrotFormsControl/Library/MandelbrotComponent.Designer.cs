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
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // MandelbrotComponent
            // 
            this.DoubleBuffered = true;
            this.Name = "MandelbrotComponent";
            this.Size = new System.Drawing.Size(128, 128);
            this.Load += new System.EventHandler(this.MandelbrotComponent_Load);
            this.ResumeLayout(false);

        }

        public System.Windows.Forms.Timer timer1;
    }
}
