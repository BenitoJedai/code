using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MichaelVincentProgramManager
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 66);
            this.panel1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(354, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "http://www.michaelv.org/ with fullscreen button";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Panel panel1;
        private Button button2;

    }
}
