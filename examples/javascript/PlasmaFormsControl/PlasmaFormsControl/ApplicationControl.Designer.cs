using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlasmaFormsControl
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
            this.plasmaControl2 = new PlasmaFormsControl.Library.PlasmaControl();
            this.plasmaControl1 = new PlasmaFormsControl.Library.PlasmaControl();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(24, 184);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(40, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Go";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(180, 184);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(40, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Go";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // plasmaControl2
            // 
            this.plasmaControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plasmaControl2.Location = new System.Drawing.Point(180, 26);
            this.plasmaControl2.Name = "plasmaControl2";
            this.plasmaControl2.Size = new System.Drawing.Size(364, 128);
            this.plasmaControl2.TabIndex = 0;
            // 
            // plasmaControl1
            // 
            this.plasmaControl1.Location = new System.Drawing.Point(24, 26);
            this.plasmaControl1.Name = "plasmaControl1";
            this.plasmaControl1.Size = new System.Drawing.Size(128, 128);
            this.plasmaControl1.TabIndex = 0;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.plasmaControl2);
            this.Controls.Add(this.plasmaControl1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(579, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Library.PlasmaControl plasmaControl1;
        private CheckBox checkBox1;
        private Library.PlasmaControl plasmaControl2;
        private CheckBox checkBox2;

    }
}
