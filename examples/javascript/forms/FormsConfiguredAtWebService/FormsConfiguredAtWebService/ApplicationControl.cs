using FormsConfiguredAtWebService;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsConfiguredAtWebService
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var x = await applicationWebService1.CreateServerGoo();


            x.Show();
        }

    }
}
