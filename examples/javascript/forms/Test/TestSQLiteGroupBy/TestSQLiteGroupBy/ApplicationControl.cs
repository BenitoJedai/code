using TestSQLiteGroupBy;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestSQLiteGroupBy
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var w = await this.applicationWebService1.WebMethod2();

            this.ParentForm.Text = new { Groups = w.Count() }.ToString();

        }

    }
}
