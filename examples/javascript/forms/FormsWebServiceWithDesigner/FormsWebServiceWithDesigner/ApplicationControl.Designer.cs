using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsWebServiceWithDesigner
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
            this.foo1 = new FormsWebServiceWithDesigner.Foo();
            this.xFoo1 = new FormsWebServiceWithDesigner.XFoo();
            this.SuspendLayout();
            // 
            // xFoo1
            // 
            this.xFoo1.Location = new System.Drawing.Point(32, 105);
            this.xFoo1.Name = "xFoo1";
            this.xFoo1.Size = new System.Drawing.Size(265, 161);
            this.xFoo1.TabIndex = 0;
            this.xFoo1.Text = "xFoo1";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.xFoo1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
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

        private Foo foo1;
        private XFoo xFoo1;

    }
}
