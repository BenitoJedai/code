using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestNestedUserControlLoadEvent
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
            this.class11 = new TestNestedUserControlLoadEvent.Class1();
            this.class12 = new TestNestedUserControlLoadEvent.Class1();
            this.class13 = new TestNestedUserControlLoadEvent.Class1();
            this.SuspendLayout();
            // 
            // class11
            // 
            this.class11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.class11.Location = new System.Drawing.Point(26, 12);
            this.class11.Name = "class11";
            this.class11.Size = new System.Drawing.Size(150, 150);
            this.class11.TabIndex = 0;
            // 
            // class12
            // 
            this.class12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.class12.Location = new System.Drawing.Point(191, 12);
            this.class12.Name = "class12";
            this.class12.Size = new System.Drawing.Size(150, 150);
            this.class12.TabIndex = 0;
            // 
            // class13
            // 
            this.class13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.class13.Location = new System.Drawing.Point(360, 12);
            this.class13.Name = "class13";
            this.class13.Size = new System.Drawing.Size(150, 150);
            this.class13.TabIndex = 0;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.class13);
            this.Controls.Add(this.class12);
            this.Controls.Add(this.class11);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(568, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }

        private Class1 class11;
        private Class1 class12;
        private Class1 class13;

    }
}
