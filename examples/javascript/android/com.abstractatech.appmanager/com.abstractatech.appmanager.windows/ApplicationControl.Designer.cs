using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.abstractatech.appmanager.windows
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label = new System.Windows.Forms.Label();
            this.Launch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Package = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Icon = new System.Windows.Forms.Panel();
            this.Uninstall = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(93)))), ((int)(((byte)(123)))));
            this.panel1.Controls.Add(this.Uninstall);
            this.panel1.Controls.Add(this.Label);
            this.panel1.Controls.Add(this.Launch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Package);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 207);
            this.panel1.TabIndex = 7;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.BackColor = System.Drawing.Color.Transparent;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.ForeColor = System.Drawing.Color.White;
            this.Label.Location = new System.Drawing.Point(169, 16);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(195, 26);
            this.Label.TabIndex = 12;
            this.Label.Text = "Remote Web Shell";
            // 
            // Launch
            // 
            this.Launch.Location = new System.Drawing.Point(341, 127);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(108, 23);
            this.Launch.TabIndex = 10;
            this.Launch.Text = "Launch";
            this.Launch.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(188, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Remote Web Shell";
            this.label3.Visible = false;
            // 
            // Package
            // 
            this.Package.AutoSize = true;
            this.Package.ForeColor = System.Drawing.Color.White;
            this.Package.Location = new System.Drawing.Point(188, 68);
            this.Package.Name = "Package";
            this.Package.Size = new System.Drawing.Size(96, 13);
            this.Package.TabIndex = 8;
            this.Package.Text = "Remote Web Shell";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(80)))), ((int)(((byte)(112)))));
            this.panel2.Controls.Add(this.Icon);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 207);
            this.panel2.TabIndex = 13;
            // 
            // Icon
            // 
            this.Icon.BackColor = System.Drawing.Color.Transparent;
            this.Icon.Location = new System.Drawing.Point(40, 47);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(86, 85);
            this.Icon.TabIndex = 8;
            // 
            // Uninstall
            // 
            this.Uninstall.ForeColor = System.Drawing.Color.Red;
            this.Uninstall.Location = new System.Drawing.Point(341, 167);
            this.Uninstall.Name = "Uninstall";
            this.Uninstall.Size = new System.Drawing.Size(108, 23);
            this.Uninstall.TabIndex = 14;
            this.Uninstall.Text = "Uninstall";
            this.Uninstall.UseVisualStyleBackColor = true;
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(464, 207);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
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

        private Panel panel1;
        public Button Uninstall;
        public Label Label;
        public Button Launch;
        private Label label3;
        public Label Package;
        private Panel panel2;
        public Panel Icon;

    }
}
