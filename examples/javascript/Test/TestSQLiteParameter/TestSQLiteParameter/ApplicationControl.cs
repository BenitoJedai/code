using TestSQLiteParameter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestSQLiteParameter
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var now = DateTime.Now;

            this.applicationWebService1.WebMethod2("Load: " + now,
                y =>
                {
                    this.listBox1.Items.Add(y);
                }
            );
        }

    }
}
