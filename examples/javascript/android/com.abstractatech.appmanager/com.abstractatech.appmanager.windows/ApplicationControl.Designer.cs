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
            this.Icon = new System.Windows.Forms.Panel();
            this.Label = new System.Windows.Forms.Label();
            this.Package = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Launch = new System.Windows.Forms.Button();
            this.Uninstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Icon
            // 
            this.Icon.Location = new System.Drawing.Point(14, 13);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(146, 99);
            this.Icon.TabIndex = 0;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.Location = new System.Drawing.Point(180, 13);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(195, 26);
            this.Label.TabIndex = 1;
            this.Label.Text = "Remote Web Shell";
            // 
            // Package
            // 
            this.Package.AutoSize = true;
            this.Package.Location = new System.Drawing.Point(182, 73);
            this.Package.Name = "Package";
            this.Package.Size = new System.Drawing.Size(96, 13);
            this.Package.TabIndex = 2;
            this.Package.Text = "Remote Web Shell";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Remote Web Shell";
            this.label3.Visible = false;
            // 
            // Launch
            // 
            this.Launch.Location = new System.Drawing.Point(375, 218);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(75, 23);
            this.Launch.TabIndex = 4;
            this.Launch.Text = "Launch";
            this.Launch.UseVisualStyleBackColor = true;
            this.Launch.Click += new System.EventHandler(this.button1_Click);
            // 
            // Uninstall
            // 
            this.Uninstall.ForeColor = System.Drawing.Color.Red;
            this.Uninstall.Location = new System.Drawing.Point(14, 218);
            this.Uninstall.Name = "Uninstall";
            this.Uninstall.Size = new System.Drawing.Size(75, 23);
            this.Uninstall.TabIndex = 5;
            this.Uninstall.Text = "Uninstall";
            this.Uninstall.UseVisualStyleBackColor = true;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.Uninstall);
            this.Controls.Add(this.Launch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Package);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.Icon);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(464, 254);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Label label3;
        public Panel Icon;
        public Label Label;
        public Label Package;
        public Button Launch;
        public Button Uninstall;

    }
}
