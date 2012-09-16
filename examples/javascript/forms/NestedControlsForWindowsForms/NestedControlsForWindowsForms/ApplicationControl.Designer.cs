using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NestedControlsForWindowsForms
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.userControl22 = new TwentyTenSimpleWindowsFormsApplication.UserControl2();
            this.userControl21 = new TwentyTenSimpleWindowsFormsApplication.UserControl2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 326);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "Forward";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(522, 326);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // userControl22
            // 
            this.userControl22.AutoScroll = true;
            this.userControl22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl22.Location = new System.Drawing.Point(293, 186);
            this.userControl22.Name = "userControl22";
            this.userControl22.Size = new System.Drawing.Size(412, 134);
            this.userControl22.TabIndex = 28;
            // 
            // userControl21
            // 
            this.userControl21.AutoScroll = true;
            this.userControl21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl21.Location = new System.Drawing.Point(293, 46);
            this.userControl21.Name = "userControl21";
            this.userControl21.Size = new System.Drawing.Size(412, 134);
            this.userControl21.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "This project does not know about jsc compiler";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "This form was designed with Visual Studio 2010";
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel5.Location = new System.Drawing.Point(41, 172);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(140, 17);
            this.linkLabel5.TabIndex = 24;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "● Support for flash or java?";
            this.linkLabel5.UseCompatibleTextRendering = true;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel4.Location = new System.Drawing.Point(31, 155);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(224, 17);
            this.linkLabel4.TabIndex = 25;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "What about WPF interop and WebBrowser?";
            this.linkLabel4.UseCompatibleTextRendering = true;
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel6.Location = new System.Drawing.Point(42, 223);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(176, 17);
            this.linkLabel6.TabIndex = 23;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "● Windows Forms with ASP.NET?";
            this.linkLabel6.UseCompatibleTextRendering = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel1.Location = new System.Drawing.Point(41, 189);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 17);
            this.linkLabel1.TabIndex = 22;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "● Rich text editor?";
            this.linkLabel1.UseCompatibleTextRendering = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel2.Location = new System.Drawing.Point(42, 206);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(113, 17);
            this.linkLabel2.TabIndex = 21;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "● Vista look and feel?";
            this.linkLabel2.UseCompatibleTextRendering = true;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.userControl22);
            this.Controls.Add(this.userControl21);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel5);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.linkLabel6);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.linkLabel2);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(736, 395);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button button2;
        private Button button1;
        private TwentyTenSimpleWindowsFormsApplication.UserControl2 userControl22;
        private TwentyTenSimpleWindowsFormsApplication.UserControl2 userControl21;
        private Label label2;
        private Label label1;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel6;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;

    }
}
