namespace FlashHeatZeekerWithStarlingT04.Library
{
    partial class GameUnitDiagnostics
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hueControl1 = new FormsHueControl.Library.HueControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current unit:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "0";
            // 
            // hueControl1
            // 
            this.hueControl1.AutoScroll = true;
            this.hueControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hueControl1.Location = new System.Drawing.Point(18, 41);
            this.hueControl1.Name = "hueControl1";
            this.hueControl1.Size = new System.Drawing.Size(222, 59);
            this.hueControl1.TabIndex = 2;
            this.hueControl1.AdjustHue += new System.Action<int>(this.hueControl1_AdjustHue);
            // 
            // GameUnitDiagnostics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hueControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GameUnitDiagnostics";
            this.Size = new System.Drawing.Size(507, 289);
            this.Load += new System.EventHandler(this.GameUnitDiagnostics_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FormsHueControl.Library.HueControl hueControl1;
    }
}
