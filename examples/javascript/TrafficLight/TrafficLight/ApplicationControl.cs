using TrafficLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TrafficLight
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void GrayRed_Paint(object sender, System.EventArgs e)
        {
            Red_ON();
            Green_OFF();
            Yellow_OFF();
        }

        private void GrayYellow_Paint(object sender, System.EventArgs e)
        {
            Yellow_ON();
            Red_OFF();
            Green_OFF();
        }

        private void GrayGreen_Paint(object sender, System.EventArgs e)
        {
            Green_ON();
            Yellow_OFF();
            Red_OFF();
        }

        private void Red_Paint(object sender, System.EventArgs e)
        {
            Red_OFF();
        }

        private void Yellow_Paint(object sender, System.EventArgs e)
        {
            Yellow_OFF();
        }

        private void Green_Paint(object sender, System.EventArgs e)
        {
            Green_OFF();
        }
        public void Red_ON()
        {
            this.GrayRed.Visible = false;
            this.Red.Visible = true;
        }
        public void Red_OFF()
        {
            this.Red.Visible = false;
            this.GrayRed.Visible = true;
        }
        public void Yellow_ON()
        {
            this.GrayYellow.Visible = false;
            this.Yellow.Visible = true;
        }
        public void Yellow_OFF()
        {
            this.Yellow.Visible = false;
            this.GrayYellow.Visible = true;
        }
        public void Green_ON()
        {
            this.GrayGreen.Visible = false;
            this.Green.Visible = true;
        }
        public void Green_OFF()
        {
            this.Green.Visible = false;
            this.GrayGreen.Visible = true;
        }
    }
}
