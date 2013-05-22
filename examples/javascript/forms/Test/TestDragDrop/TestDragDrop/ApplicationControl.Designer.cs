using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestDragDrop
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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // ApplicationControl
            // 
            this.AllowDrop = true;
            this.Controls.Add(this.label1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ApplicationControl_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ApplicationControl_DragOver);
            this.DragLeave += new System.EventHandler(this.ApplicationControl_DragLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ApplicationControl_MouseMove);
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

        private Label label1;

    }
}
