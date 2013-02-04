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
    public class RootDesignerView : UserControl
    {
        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private Button button1;
        private SampleRootDesigner m_designer;

        public RootDesignerView()
        {
            InitializeComponent();
        }

        public RootDesignerView(SampleRootDesigner designer)
        {
            m_designer = designer;
            BackColor = Color.Blue;
            //Font = new Font(Font.FontFamily.Name, 24.0f);
            Font = new Font(Font.Name, 24.0f);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Draws the name of the component in large letters.
            pe.Graphics.DrawString(
                "jsc: " + m_designer.Component.Site.Name
                , Font, Brushes.Yellow, ClientRectangle);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // RootDesignerView
            // 
            this.Controls.Add(this.button1);
            this.Name = "RootDesignerView";
            this.Size = new System.Drawing.Size(444, 277);
            this.ResumeLayout(false);

        }
    }

}
