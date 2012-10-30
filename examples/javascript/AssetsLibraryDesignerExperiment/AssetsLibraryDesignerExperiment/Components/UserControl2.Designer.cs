namespace AssetsLibraryDesignerExperiment.Components
{
    partial class UserControl2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.userControl12 = new AssetsLibraryDesignerExperiment.Components.UserControl1();
            this.class11 = new AssetsLibraryDesignerExperiment.Components.Class1();
            this.userControl12.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // userControl12
            // 
            this.userControl12.BackColor = System.Drawing.Color.Blue;
            // 
            // userControl12.?DropZone
            // 
            this.userControl12.DropZone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userControl12.DropZone.BackColor = System.Drawing.Color.Red;
            this.userControl12.DropZone.Location = new System.Drawing.Point(4, 33);
            this.userControl12.DropZone.Name = "?DropZone";
            this.userControl12.DropZone.Size = new System.Drawing.Size(347, 265);
            this.userControl12.DropZone.TabIndex = 2;
            this.userControl12.Location = new System.Drawing.Point(31, 41);
            this.userControl12.Name = "userControl12";
            this.userControl12.Size = new System.Drawing.Size(354, 301);
            this.userControl12.TabIndex = 0;
            // 
            // class11
            // 
            this.class11.BackColor = System.Drawing.Color.Empty;
            this.class11.Foo = null;
            this.class11.ForeColor = System.Drawing.Color.Empty;
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.userControl12);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(481, 396);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            this.userControl12.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private UserControl1 userControl12;
        private Class1 class11;
    }
}
