using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestSQLJoin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.theView1 = new TestSQLJoin.Library.TheView();
            this.theDealerOtherText1 = new TestSQLJoin.Library.TheDealerOtherText();
            this.theDealer1 = new TestSQLJoin.Library.TheDealer();
            this.theDealerContact1 = new TestSQLJoin.Library.TheDealerContact();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.applicationWebService1 = new TestSQLJoin.ApplicationWebService();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(8, 8);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(505, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.CheckOnClick = true;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(70, 22);
            this.toolStripButton1.Text = "DealerContact";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.CheckOnClick = true;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton3.Text = "Dealer";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.CheckOnClick = true;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton2.Text = "DealerOtherText";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.CheckOnClick = true;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(45, 22);
            this.toolStripButton4.Text = "TheView";
            // 
            // theView1
            // 
            this.theView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theView1.Location = new System.Drawing.Point(8, 532);
            this.theView1.Name = "theView1";
            this.theView1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.theView1.Size = new System.Drawing.Size(505, 140);
            this.theView1.TabIndex = 4;
            this.theView1.AtRefresh += new System.Action(this.theView1_AtRefresh);
            // 
            // theDealerOtherText1
            // 
            this.theDealerOtherText1.Dock = System.Windows.Forms.DockStyle.Top;
            this.theDealerOtherText1.Location = new System.Drawing.Point(8, 366);
            this.theDealerOtherText1.Name = "theDealerOtherText1";
            this.theDealerOtherText1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.theDealerOtherText1.Size = new System.Drawing.Size(505, 166);
            this.theDealerOtherText1.TabIndex = 3;
            // 
            // theDealer1
            // 
            this.theDealer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.theDealer1.Location = new System.Drawing.Point(8, 198);
            this.theDealer1.Name = "theDealer1";
            this.theDealer1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.theDealer1.Size = new System.Drawing.Size(505, 168);
            this.theDealer1.TabIndex = 2;
            // 
            // theDealerContact1
            // 
            this.theDealerContact1.Dock = System.Windows.Forms.DockStyle.Top;
            this.theDealerContact1.Location = new System.Drawing.Point(8, 33);
            this.theDealerContact1.Margin = new System.Windows.Forms.Padding(8);
            this.theDealerContact1.Name = "theDealerContact1";
            this.theDealerContact1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.theDealerContact1.Size = new System.Drawing.Size(505, 165);
            this.theDealerContact1.TabIndex = 1;
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(38, 22);
            this.toolStripButton5.Text = "Server ";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // ApplicationControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Aqua;
            this.Controls.Add(this.theView1);
            this.Controls.Add(this.theDealerOtherText1);
            this.Controls.Add(this.theDealer1);
            this.Controls.Add(this.theDealerContact1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "ApplicationControl";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(521, 680);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton4;
        private Library.TheDealerContact theDealerContact1;
        private Library.TheDealer theDealer1;
        private Library.TheDealerOtherText theDealerOtherText1;
        private Library.TheView theView1;
        private ToolStripButton toolStripButton5;
        private ApplicationWebService applicationWebService1;

    }
}
