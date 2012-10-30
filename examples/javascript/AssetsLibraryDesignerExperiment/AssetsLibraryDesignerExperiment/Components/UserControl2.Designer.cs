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
            this.DropZone = new AssetsLibraryDesignerExperiment.Components.UserControl1();
            this.button1 = new System.Windows.Forms.Button();
            this.DropZone.DropZone.SuspendLayout();
            this.SuspendLayout();
            // 
            // DropZone
            // 
            this.DropZone.BackColor = System.Drawing.Color.Blue;
            // 
            // 
            // 
            this.DropZone.DropZone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DropZone.DropZone.BackColor = System.Drawing.Color.Red;
            this.DropZone.DropZone.Controls.Add(this.button1);
            this.DropZone.DropZone.Location = new System.Drawing.Point(7, 33);
            this.DropZone.DropZone.Name = "DropZone";
            this.DropZone.DropZone.Size = new System.Drawing.Size(339, 224);
            this.DropZone.DropZone.TabIndex = 2;
            this.DropZone.DropZone.Paint += new System.Windows.Forms.PaintEventHandler(this.userControl11_DropZone_Paint);
            this.DropZone.Location = new System.Drawing.Point(64, 50);
            this.DropZone.Name = "DropZone";
            this.DropZone.Size = new System.Drawing.Size(349, 260);
            this.DropZone.TabIndex = 0;
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
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.DropZone);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(481, 396);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            this.DropZone.DropZone.ResumeLayout(false);
            this.DropZone.DropZone.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl1 userControl11;
        private System.Windows.Forms.Button button1;
        private UserControl1 DropZone;
    }
}
