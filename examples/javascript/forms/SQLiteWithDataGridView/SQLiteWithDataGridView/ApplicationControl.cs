using SQLiteWithDataGridView;
using SQLiteWithDataGridView.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLiteWithDataGridView
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

    

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }


        private void Table001_Click(object sender, System.EventArgs e)
        {
            new GridForm { service = this.applicationWebService1 }.Show();
        }

  
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

    }
}
