using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SQLiteWithDataGridView
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
            this.applicationWebService1 = new SQLiteWithDataGridView.ApplicationWebService();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Table003 = new System.Windows.Forms.Button();
            this.Table002 = new System.Windows.Forms.Button();
            this.Table001 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Table003);
            this.panel1.Controls.Add(this.Table002);
            this.panel1.Controls.Add(this.Table001);
            this.panel1.Location = new System.Drawing.Point(13, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 100);
            this.panel1.TabIndex = 3;
            // 
            // Table003
            // 
            this.Table003.Location = new System.Drawing.Point(13, 68);
            this.Table003.Name = "Table003";
            this.Table003.Size = new System.Drawing.Size(257, 23);
            this.Table003.TabIndex = 5;
            this.Table003.Text = "SQLiteWithDataGridView_0_Table003";
            this.Table003.UseVisualStyleBackColor = true;
            this.Table003.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Table002
            // 
            this.Table002.Location = new System.Drawing.Point(13, 39);
            this.Table002.Name = "Table002";
            this.Table002.Size = new System.Drawing.Size(257, 23);
            this.Table002.TabIndex = 4;
            this.Table002.Text = "SQLiteWithDataGridView_0_Table002";
            this.Table002.UseVisualStyleBackColor = true;
            this.Table002.Click += new System.EventHandler(this.Table002_Click);
            // 
            // Table001
            // 
            this.Table001.Location = new System.Drawing.Point(13, 10);
            this.Table001.Name = "Table001";
            this.Table001.Size = new System.Drawing.Size(257, 23);
            this.Table001.TabIndex = 3;
            this.Table001.Text = "SQLiteWithDataGridView_0_Table001";
            this.Table001.UseVisualStyleBackColor = true;
            this.Table001.Click += new System.EventHandler(this.Table001_Click);
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private ApplicationWebService applicationWebService1;
        private Panel panel1;
        private Button Table003;
        private Button Table002;
        private Button Table001;

    }
}
