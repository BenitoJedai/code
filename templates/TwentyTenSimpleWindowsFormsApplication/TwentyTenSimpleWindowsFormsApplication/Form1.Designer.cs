namespace TwentyTenSimpleWindowsFormsApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.userControl21 = new TwentyTenSimpleWindowsFormsApplication.UserControl2();
            this.userControl22 = new TwentyTenSimpleWindowsFormsApplication.UserControl2();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel5.Location = new System.Drawing.Point(44, 148);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(140, 17);
            this.linkLabel5.TabIndex = 15;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "● Support for flash or java?";
            this.linkLabel5.UseCompatibleTextRendering = true;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.LinkArea = new System.Windows.Forms.LinkArea(0, 59);
            this.linkLabel4.Location = new System.Drawing.Point(34, 131);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(224, 17);
            this.linkLabel4.TabIndex = 16;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "What about WPF interop and WebBrowser?";
            this.linkLabel4.UseCompatibleTextRendering = true;
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel6.Location = new System.Drawing.Point(45, 199);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(176, 17);
            this.linkLabel6.TabIndex = 14;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "● Windows Forms with ASP.NET?";
            this.linkLabel6.UseCompatibleTextRendering = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel1.Location = new System.Drawing.Point(44, 165);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 17);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "● Rich text editor?";
            this.linkLabel1.UseCompatibleTextRendering = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(0, 76);
            this.linkLabel2.Location = new System.Drawing.Point(45, 182);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(113, 17);
            this.linkLabel2.TabIndex = 12;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "● Vista look and feel?";
            this.linkLabel2.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "This project does not know about jsc compiler";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "This form was designed with Visual Studio 2010";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // userControl21
            // 
            this.userControl21.AutoScroll = true;
            this.userControl21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl21.Location = new System.Drawing.Point(296, 22);
            this.userControl21.Name = "userControl21";
            this.userControl21.Size = new System.Drawing.Size(412, 134);
            this.userControl21.TabIndex = 19;
            // 
            // userControl22
            // 
            this.userControl22.AutoScroll = true;
            this.userControl22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.userControl22.Location = new System.Drawing.Point(296, 162);
            this.userControl22.Name = "userControl22";
            this.userControl22.Size = new System.Drawing.Size(412, 134);
            this.userControl22.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(525, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(606, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Forward";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 379);
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
            this.Name = "Form1";
            this.Text = "TwentyTenSimpleWindowsFormsApplication. Your C# will be converted javascript.";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UserControl2 userControl21;
        private UserControl2 userControl22;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

