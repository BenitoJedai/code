using Abstractatech.JavaScript.FormAsPopup;
using Abstractatech.JavaScript.FormAsPopup.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Abstractatech.JavaScript.FormAsPopup
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public Form f = new ExampleForm();

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

            f.Show();
        }

    }
}
