using FormsSplitter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsSplitter
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_SizeChanged(object sender, System.EventArgs e)
        {
            // why aint this called for child controls?
            Console.WriteLine("ApplicationControl_SizeChanged");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Orientation = Orientation.Vertical;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Orientation = Orientation.Horizontal;
        }

    }
}
