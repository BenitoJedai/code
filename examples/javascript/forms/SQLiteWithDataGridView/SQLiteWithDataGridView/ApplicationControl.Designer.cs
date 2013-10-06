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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Table001 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Table001);
            this.panel1.Location = new System.Drawing.Point(15, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 47);
            this.panel1.TabIndex = 3;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "http://jscdatagriddemo.sourceforge.net/";
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(108)))), ((int)(((byte)(160)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(503, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Panel panel1;
        private Button Table001;
        public Label label1;

    }
}
