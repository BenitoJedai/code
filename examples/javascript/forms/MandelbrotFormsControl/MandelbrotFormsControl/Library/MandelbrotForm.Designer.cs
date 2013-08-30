namespace MandelbrotFormsControl.Library
{
    partial class MandelbrotForm
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
            this.mandelbrotComponent1 = new MandelbrotFormsControl.Library.MandelbrotComponent();
            this.SuspendLayout();
            // 
            // mandelbrotComponent1
            // 
            this.mandelbrotComponent1.BackColor = System.Drawing.Color.Black;
            this.mandelbrotComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mandelbrotComponent1.Location = new System.Drawing.Point(0, 0);
            this.mandelbrotComponent1.Name = "mandelbrotComponent1";
            this.mandelbrotComponent1.Size = new System.Drawing.Size(284, 262);
            this.mandelbrotComponent1.TabIndex = 0;
            // 
            // MandelbrotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.mandelbrotComponent1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MandelbrotForm";
            this.Text = "MandelbrotForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MandelbrotComponent mandelbrotComponent1;
    }
}