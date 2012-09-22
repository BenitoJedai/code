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

        private void button1_Click(object sender, System.EventArgs e)
        {
            new GridForm().Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
        }

        private void button2_Click_1(object sender, System.EventArgs e)
        {
            new GridForm {  TableName = button2.Text }.Show();

        }

    }
}
