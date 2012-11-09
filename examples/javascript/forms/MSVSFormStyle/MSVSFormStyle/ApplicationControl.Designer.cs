using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSVSFormStyle
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "new default Form()";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(45, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 27);
            this.button2.TabIndex = 2;
            this.button2.Text = "new metro Form()";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(45, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(211, 27);
            this.button3.TabIndex = 2;
            this.button3.Text = "new windows 3 Form()";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(45, 190);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(211, 27);
            this.button4.TabIndex = 3;
            this.button4.Text = "new custom Form()";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(45, 42);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(211, 27);
            this.button5.TabIndex = 4;
            this.button5.Text = "new Form()";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
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
        public Button button1;
        public Button button2;
        public Button button3;
        public Button button4;
        public Button button5;

    }
}
