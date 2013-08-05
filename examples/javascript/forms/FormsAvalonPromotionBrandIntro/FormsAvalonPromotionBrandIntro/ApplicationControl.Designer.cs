using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsAvalonPromotionBrandIntro
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
            this.animationControlHost2 = new FormsAvalonAnimation.AvalonPromotionBrandIntroHost();
            this.animationControlHost1 = new FormsAvalonAnimation.AvalonPromotionBrandIntroHost();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(273, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(200, 100);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Child = null;
            // 
            // animationControlHost2
            // 
            this.animationControlHost2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.animationControlHost2.BackColor = System.Drawing.Color.Transparent;
            this.animationControlHost2.Location = new System.Drawing.Point(17, 14);
            this.animationControlHost2.Name = "animationControlHost2";
            this.animationControlHost2.Size = new System.Drawing.Size(282, 263);
            this.animationControlHost2.TabIndex = 1;
            // 
            // animationControlHost1
            // 
            this.animationControlHost1.BackColor = System.Drawing.Color.Transparent;
            this.animationControlHost1.Child = null;
            this.animationControlHost1.Location = new System.Drawing.Point(0, 0);
            this.animationControlHost1.Name = "animationControlHost1";
            this.animationControlHost1.Size = new System.Drawing.Size(150, 150);
            this.animationControlHost1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(305, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.animationControlHost2);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }

        private Button button1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private FormsAvalonAnimation.AvalonPromotionBrandIntroHost animationControlHost1;
        private FormsAvalonAnimation.AvalonPromotionBrandIntroHost animationControlHost2;
        private Button button2;

    }
}
