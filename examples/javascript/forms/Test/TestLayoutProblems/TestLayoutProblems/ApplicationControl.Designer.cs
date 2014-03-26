using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestLayoutProblems
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.testControl1 = new TestLayoutProblems.TestControl();
            this.testControl2 = new TestLayoutProblems.TestControl();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.textBox1);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.testControl2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(14, 35);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(608, 523);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 94);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.Controls.Add(this.testControl1);
            this.panel2.Location = new System.Drawing.Point(3, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(564, 94);
            this.panel2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 216);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Red;
            this.panel3.Location = new System.Drawing.Point(3, 242);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(564, 94);
            this.panel3.TabIndex = 4;
            // 
            // testControl1
            // 
            this.testControl1.Location = new System.Drawing.Point(106, 79);
            this.testControl1.Name = "testControl1";
            this.testControl1.Size = new System.Drawing.Size(150, 150);
            this.testControl1.TabIndex = 0;
            // 
            // testControl2
            // 
            this.testControl2.Location = new System.Drawing.Point(3, 342);
            this.testControl2.Name = "testControl2";
            this.testControl2.Size = new System.Drawing.Size(150, 150);
            this.testControl2.TabIndex = 5;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(665, 595);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
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
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private TextBox textBox1;
        private Panel panel3;
        public FlowLayoutPanel flowLayoutPanel1;
        private TestControl testControl1;
        private TestControl testControl2;

    }
}
