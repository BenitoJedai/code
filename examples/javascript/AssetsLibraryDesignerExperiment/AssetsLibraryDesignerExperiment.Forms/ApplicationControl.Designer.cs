using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AssetsLibraryDesignerExperiment.Forms
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationWebServiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.plasma1 = new AssetsLibraryDesignerExperiment.Forms.Library.Toolbox.Plasma();
            this.applicationWebService1 = new AssetsLibraryDesignerExperiment.Forms.ApplicationWebService();
            this.class11 = new AssetsLibraryDesignerExperiment.Forms.Library.Components.Class1();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.applicationWebServiceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ContextMenuStrip = this.contextMenuStrip1;
            this.button1.Location = new System.Drawing.Point(22, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fooToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // fooToolStripMenuItem
            // 
            this.fooToolStripMenuItem.Name = "fooToolStripMenuItem";
            this.fooToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.fooToolStripMenuItem.Text = "foo";
            this.fooToolStripMenuItem.Click += new System.EventHandler(this.fooToolStripMenuItem_Click);
            // 
            // plasma1
            // 
            this.plasma1.Location = new System.Drawing.Point(12, 0);
            this.plasma1.Name = "plasma1";
            this.plasma1.Size = new System.Drawing.Size(356, 231);
            this.plasma1.TabIndex = 1;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.plasma1);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 448);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.applicationWebServiceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        private ApplicationWebService applicationWebService1;
        private Button button1;
        private Library.Toolbox.Plasma plasma1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem fooToolStripMenuItem;
        private Library.Components.Class1 class11;
        private BindingSource applicationWebServiceBindingSource;

    }
}
