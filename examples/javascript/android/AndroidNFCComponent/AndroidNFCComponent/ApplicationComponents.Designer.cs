namespace AndroidNFCComponent
{
    partial class ApplicationComponents
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.xnfcComponent1 = new AndroidNFCComponent.XNFCComponent();
            this.xApplicationWebService1 = new AndroidNFCComponent.XApplicationWebService();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = "This control will actually not be shown in the web app,\r\nyet will be hosting our " +
    "client side components.\r\n\r\nNotice it has the components down under. Can we edit " +
    "this \r\nat runtime? Property grid?\r\n\r\n";
            // 
            // xnfcComponent1
            // 
            this.xnfcComponent1.Source = this.xApplicationWebService1;
            this.xnfcComponent1.AtTagDiscovered += new System.Action<string>(this.xnfcComponent1_AtTagDiscovered);
            // 
            // ApplicationComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Name = "ApplicationComponents";
            this.Size = new System.Drawing.Size(421, 377);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private XApplicationWebService xApplicationWebService1;
        public XNFCComponent xnfcComponent1;
    }
}
