using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestMenu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fooToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.heyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helloToolStripMenuItem,
            this.worldToolStripMenuItem,
            this.heyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helloToolStripMenuItem
            // 
            this.helloToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fooToolStripMenuItem});
            this.helloToolStripMenuItem.Name = "helloToolStripMenuItem";
            this.helloToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helloToolStripMenuItem.Text = "Hello";
            // 
            // worldToolStripMenuItem
            // 
            this.worldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barToolStripMenuItem});
            this.worldToolStripMenuItem.Name = "worldToolStripMenuItem";
            this.worldToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.worldToolStripMenuItem.Text = "World";
            // 
            // fooToolStripMenuItem
            // 
            this.fooToolStripMenuItem.Name = "fooToolStripMenuItem";
            this.fooToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fooToolStripMenuItem.Text = "foo";
            // 
            // barToolStripMenuItem
            // 
            this.barToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fooToolStripMenuItem1});
            this.barToolStripMenuItem.Name = "barToolStripMenuItem";
            this.barToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.barToolStripMenuItem.Text = "bar";
            // 
            // fooToolStripMenuItem1
            // 
            this.fooToolStripMenuItem1.Name = "fooToolStripMenuItem1";
            this.fooToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.fooToolStripMenuItem1.Text = "foo";
            // 
            // heyToolStripMenuItem
            // 
            this.heyToolStripMenuItem.Name = "heyToolStripMenuItem";
            this.heyToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.heyToolStripMenuItem.Text = "hey";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.menuStrip1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem helloToolStripMenuItem;
        private ToolStripMenuItem worldToolStripMenuItem;
        private ToolStripMenuItem fooToolStripMenuItem;
        private ToolStripMenuItem barToolStripMenuItem;
        private ToolStripMenuItem fooToolStripMenuItem1;
        private ToolStripMenuItem heyToolStripMenuItem;

    }
}
