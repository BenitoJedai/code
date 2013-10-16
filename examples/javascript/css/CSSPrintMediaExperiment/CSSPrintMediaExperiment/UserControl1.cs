using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSSPrintMediaExperiment
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private async void UserControl1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("before DoEnterData");




            if (this.applicationWebService1 == null)
                return;

            var x = await this.applicationWebService1.DoEnterData();
            // at CSSPrintMediaExperiment.UserControl1.<UserControl1_Load>d__0.MoveNext() in x:\jsc.svn\examples\javascript\css\CSSPrintMediaExperiment\CSSPrintMediaExperiment\UserControl1.cs:line 28
            this.dataGridView1.DataSource = x;
            Console.WriteLine("after DataSource");
        }
    }
}
