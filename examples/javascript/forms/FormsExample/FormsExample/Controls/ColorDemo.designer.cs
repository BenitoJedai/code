namespace FormsExample.js
{
    partial class ColorDemo
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
            this.ColorPanelControl = new System.Windows.Forms.Panel();
            this.ColorNameControl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ColorPanelControl
            // 
            this.ColorPanelControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ColorPanelControl.Location = new System.Drawing.Point(3, 3);
            this.ColorPanelControl.Name = "ColorPanelControl";
            this.ColorPanelControl.Size = new System.Drawing.Size(42, 14);
            this.ColorPanelControl.TabIndex = 0;
            // 
            // ColorNameControl
            // 
            this.ColorNameControl.Location = new System.Drawing.Point(51, 3);
            this.ColorNameControl.Name = "ColorNameControl";
            this.ColorNameControl.Size = new System.Drawing.Size(293, 14);
            this.ColorNameControl.TabIndex = 1;
            this.ColorNameControl.Text = "label1";
            // 
            // ColorDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColorNameControl);
            this.Controls.Add(this.ColorPanelControl);
            this.Name = "ColorDemo";
            this.Size = new System.Drawing.Size(347, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ColorPanelControl;
        private System.Windows.Forms.Label ColorNameControl;


    }
}
