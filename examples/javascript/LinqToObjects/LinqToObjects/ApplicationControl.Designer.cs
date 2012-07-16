using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinqToObjects
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
            this.label1 = new System.Windows.Forms.Label();
            this.users = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filter = new System.Windows.Forms.TextBox();
            this.filter2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.result = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter a list of names separated by commas";
            // 
            // users
            // 
            this.users.Location = new System.Drawing.Point(15, 24);
            this.users.Multiline = true;
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(355, 48);
            this.users.TabIndex = 1;
            this.users.Text = "_martin, mike, mac, ken, neo, zen, jay, morpheous, trinity, Agent Smith, _psycho";
            this.users.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter a partial name to be found from the list above.";
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(15, 115);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(355, 20);
            this.filter.TabIndex = 3;
            this.filter.Text = "psy";
            this.filter.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // filter2
            // 
            this.filter2.Location = new System.Drawing.Point(15, 158);
            this.filter2.Name = "filter2";
            this.filter2.Size = new System.Drawing.Size(355, 20);
            this.filter2.TabIndex = 5;
            this.filter2.Text = "a";
            this.filter2.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter a partial name to make the entry special";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(222, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Enter a partial name to make the entry special";
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(15, 218);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.Size = new System.Drawing.Size(355, 67);
            this.result.TabIndex = 1;
            this.result.Text = "?";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.label4);
            this.Controls.Add(this.filter2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.filter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.result);
            this.Controls.Add(this.users);
            this.Controls.Add(this.label1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label label1;
        private TextBox users;
        private Label label2;
        private TextBox filter;
        private TextBox filter2;
        private Label label3;
        private Label label4;
        private TextBox result;

    }
}
