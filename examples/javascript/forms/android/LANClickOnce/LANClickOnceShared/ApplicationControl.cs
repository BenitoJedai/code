using LANClickOnce.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LANClickOnce.Core
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_MouseEnter(object sender, System.EventArgs e)
        {
            this.BackColor = Color.Yellow;
        }

        private void ApplicationControl_MouseLeave(object sender, System.EventArgs e)
        {
            this.BackColor = Color.Red;

        }

    }
}
