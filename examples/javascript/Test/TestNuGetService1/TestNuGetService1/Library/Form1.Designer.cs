namespace TestNuGetService1.Library
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plasmaControl1 = new PlasmaFormsControl.Library.PlasmaControl();
            this.plasmaControl2 = new PlasmaFormsControl.Library.PlasmaControl();
            this.SuspendLayout();
            // 
            // plasmaControl1
            // 
            this.plasmaControl1.Location = new System.Drawing.Point(66, 66);
            this.plasmaControl1.Name = "plasmaControl1";
            this.plasmaControl1.Size = new System.Drawing.Size(128, 128);
            this.plasmaControl1.TabIndex = 0;
            // 
            // plasmaControl2
            // 
            this.plasmaControl2.Location = new System.Drawing.Point(229, 66);
            this.plasmaControl2.Name = "plasmaControl2";
            this.plasmaControl2.Size = new System.Drawing.Size(128, 128);
            this.plasmaControl2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 367);
            this.Controls.Add(this.plasmaControl2);
            this.Controls.Add(this.plasmaControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PlasmaFormsControl.Library.PlasmaControl plasmaControl1;
        private PlasmaFormsControl.Library.PlasmaControl plasmaControl2;
    }
}