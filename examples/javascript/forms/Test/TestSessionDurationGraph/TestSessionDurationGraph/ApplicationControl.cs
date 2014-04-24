using TestSessionDurationGraph;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestSessionDurationGraph
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            applicationWebService1.Closing();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var f = new GraphForm();
            
            Action getTable = async delegate
            {
                var dt = await applicationWebService1.getSessionTable();
                f.chart1.DataSource = dt;
                f.chart1.DataBind();
            };
            getTable();
            

            f.Show();
        }

    }
}
