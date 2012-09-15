using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SimpleMazeGenerator
{
    public partial class Form2
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
            this.SuspendLayout();
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(597, 370);
            this.DoubleBuffered = true;
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
    }
}
