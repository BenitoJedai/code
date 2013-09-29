using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace com.abstractatech.adminshell
{
    partial class ApplicationControl
    {
        private IContainer components;

        private void InitializeComponent()
        {
            this.applicationWebService1 = new com.abstractatech.adminshell.ApplicationWebService();
            this.nfc = new AndroidNFCEvents.ApplicationControl_onnfc();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nfc
            // 
            this.nfc.service = this.applicationWebService1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This control is not to be shown to the user. HTML Layout will be shown instead.";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.label1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(484, 270);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ApplicationWebService applicationWebService1;
        private System.Windows.Forms.Label label1;
        public AndroidNFCEvents.ApplicationControl_onnfc nfc;
    }
}
