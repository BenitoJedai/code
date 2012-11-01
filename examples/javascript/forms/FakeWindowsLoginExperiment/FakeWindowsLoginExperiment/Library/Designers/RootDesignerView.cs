using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeWindowsLoginExperiment.Library.Designers
{


    // RootDesignerView is a simple control that will be displayed  
    // in the designer window. 
    public class RootDesignerView : Control
    {
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private SampleRootDesigner m_designer;

        public RootDesignerView(SampleRootDesigner designer)
        {
            m_designer = designer;
            BackColor = Color.Blue;
            Font = new Font(Font.FontFamily.Name, 24.0f);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Draws the name of the component in large letters.
            pe.Graphics.DrawString(m_designer.Component.Site.Name, Font, Brushes.Yellow, ClientRectangle);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

}
