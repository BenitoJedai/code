using FormsAvalonAnimation;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsWithAvalonExample
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
            this.button1 = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.animationControl1 = new FormsAvalonAnimation.AnimationControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.BackColor = System.Drawing.Color.Yellow;
            this.elementHost1.Location = new System.Drawing.Point(6, 3);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(522, 239);
            this.elementHost1.TabIndex = 12;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.animationControl1;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.button1);
            this.Controls.Add(this.elementHost1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(570, 273);
            this.ResumeLayout(false);

        }

        private Button button1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private AnimationControl animationControl1;

    }
}
