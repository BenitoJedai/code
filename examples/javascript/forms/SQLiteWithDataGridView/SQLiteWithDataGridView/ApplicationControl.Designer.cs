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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.applicationWebService1 = new SQLiteWithDataGridView.ApplicationWebService();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(257, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "SQLiteWithDataGridView_0_Table001";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(257, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "SQLiteWithDataGridView_0_Table002";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(30, 83);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(257, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "SQLiteWithDataGridView_0_Table003";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }

        private Button button1;
        private Button button2;
        private Button button3;
        private ApplicationWebService applicationWebService1;

    }
}
