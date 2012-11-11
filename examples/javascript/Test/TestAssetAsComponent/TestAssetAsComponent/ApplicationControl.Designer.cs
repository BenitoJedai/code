using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestAssetAsComponent
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
            this.vjLogoComponent4 = new TestAssetAsComponent.Design.VJLogoComponent();
            this.vjLogoComponent5 = new TestAssetAsComponent.Design.VJLogoComponent();
            this.SuspendLayout();
            // 
            // vjLogoComponent4
            // 
            this.vjLogoComponent4.href = "http://demo.andrewgreig.com/webgl/vjlogo.html";
            // 
            // vjLogoComponent5
            // 
            this.vjLogoComponent5.href = "http://demo.andrewgreig.com/webgl/vjlogo.html";
            // 
            // ApplicationControl
            // 
            this.Name = "ApplicationControl";
            this.ResumeLayout(false);

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

        private Design.VJLogoComponent vjLogoComponent1;
        private Design.VJLogoComponent vjLogoComponent2;
        private Design.VJLogoComponent vjLogoComponent3;
        public Design.VJLogoComponent vjLogoComponent4;
        public Design.VJLogoComponent vjLogoComponent5;


    }
}
