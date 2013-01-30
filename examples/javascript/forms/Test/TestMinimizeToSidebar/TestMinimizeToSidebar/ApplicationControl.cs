using TestMinimizeToSidebar;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestMinimizeToSidebar
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public ApplicationForm Content = new ApplicationForm();

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            Content.Show();
        }

    }
}
