using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace com.abstractatech.multiscreen.formsexample.Library
{
    public partial class LINQForm
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
            this.linqControl1 = new com.abstractatech.multiscreen.formsexample.Library.LINQControl();
            this.SuspendLayout();
            // 
            // linqControl1
            // 
            this.linqControl1.Location = new System.Drawing.Point(32, 34);
            this.linqControl1.Name = "linqControl1";
            this.linqControl1.Size = new System.Drawing.Size(400, 300);
            this.linqControl1.TabIndex = 0;
            // 
            // LINQForm
            // 
            this.ClientSize = new System.Drawing.Size(665, 422);
            this.Controls.Add(this.linqControl1);
            this.DoubleBuffered = true;
            this.Name = "LINQForm";
            this.Text = "LINQ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private LINQControl linqControl1;
    }
}
