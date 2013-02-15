using FormsHueControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsHueControl
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void hueControl1_Load(object sender, System.EventArgs e)
        {

        }

        private void hueControl1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void hueControl1_AdjustHue(int delta)
        {
            Console.WriteLine(new { delta });
        }

    }
}
