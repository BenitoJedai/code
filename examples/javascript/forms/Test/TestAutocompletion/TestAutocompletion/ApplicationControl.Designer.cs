using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestAutocompletion
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
            this.phoneBox = new System.Windows.Forms.TextBox();
            this.phonebox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.emailbox2 = new System.Windows.Forms.TextBox();
            this.emailbox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // phoneBox
            // 
            this.phoneBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.phoneBox.Location = new System.Drawing.Point(52, 35);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(144, 20);
            this.phoneBox.TabIndex = 0;
            // 
            // phonebox2
            // 
            this.phonebox2.Location = new System.Drawing.Point(52, 101);
            this.phonebox2.Name = "phonebox2";
            this.phonebox2.Size = new System.Drawing.Size(144, 20);
            this.phonebox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // emailbox2
            // 
            this.emailbox2.Location = new System.Drawing.Point(234, 101);
            this.emailbox2.Name = "emailbox2";
            this.emailbox2.Size = new System.Drawing.Size(144, 20);
            this.emailbox2.TabIndex = 4;
            // 
            // emailbox1
            // 
            this.emailbox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.emailbox1.Location = new System.Drawing.Point(234, 35);
            this.emailbox1.Name = "emailbox1";
            this.emailbox1.Size = new System.Drawing.Size(144, 20);
            this.emailbox1.TabIndex = 3;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.emailbox2);
            this.Controls.Add(this.emailbox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.phonebox2);
            this.Controls.Add(this.phoneBox);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
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

        private TextBox phoneBox;
        private TextBox phonebox2;
        public Button button1;
        private TextBox emailbox2;
        private TextBox emailbox1;

    }
}
