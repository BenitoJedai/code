using TestServiceInterfacings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestServiceInterfacings
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public iFoo foo;

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var t = await foo.ReturnString();
            button1.Text = t;
        }
    }
}
