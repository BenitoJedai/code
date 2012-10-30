namespace AssetsLibraryDesignerExperiment.Components
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.DropZone = new AssetsLibraryDesignerExperiment.Components.UserControl2();
            this.button1 = new System.Windows.Forms.Button();
            this.DropZone.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(35, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // DropZone
            // 
            this.DropZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DropZone.Controls.Add(this.button1);
            // 
            // 
            // 
            this.DropZone.DropZone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DropZone.DropZone.BackColor = System.Drawing.Color.Red;
            this.DropZone.DropZone.Location = new System.Drawing.Point(7, 33);
            this.DropZone.DropZone.Name = "DropZone";
            this.DropZone.DropZone.Size = new System.Drawing.Size(339, 224);
            this.DropZone.DropZone.TabIndex = 2;
            this.DropZone.Location = new System.Drawing.Point(27, 45);
            this.DropZone.Name = "DropZone";
            this.DropZone.Size = new System.Drawing.Size(481, 396);
            this.DropZone.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 510);
            this.Controls.Add(this.DropZone);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DropZone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl2 userControl21;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private UserControl2 DropZone;
        private System.Windows.Forms.Button button1;




    }
}