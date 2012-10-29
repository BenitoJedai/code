using TestComboboxDatabinding;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestComboboxDatabinding
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

        private void fillByToolStripButton_Click(object sender, System.EventArgs e)
        {
        

        }

        private void fillBy1ToolStripButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.table1TableAdapter.FillBy1(this.database1DataSet.Table1);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy2ToolStripButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.table1TableAdapter.FillBy2(this.database1DataSet.Table1);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

    }
}
