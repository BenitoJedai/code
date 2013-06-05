using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MulticastExperiment
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
            this.applicationControl1 = new MulticastExperimentCore.ApplicationControl();
            this.SuspendLayout();
            // 
            // applicationControl1
            // 
            this.applicationControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.applicationControl1.Location = new System.Drawing.Point(43, 54);
            this.applicationControl1.Name = "applicationControl1";
            this.applicationControl1.Size = new System.Drawing.Size(400, 300);
            this.applicationControl1.TabIndex = 0;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.applicationControl1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(538, 432);
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

        private MulticastExperimentCore.ApplicationControl applicationControl1;

    }
}
