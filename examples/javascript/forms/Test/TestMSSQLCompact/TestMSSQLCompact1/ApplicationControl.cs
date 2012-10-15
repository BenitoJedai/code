using TestMSSQLCompact1;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestMSSQLCompact1
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
            try
            {
                this.fooTableTableAdapter.FillBy(this.database1DataSet.FooTable);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            this.fooTableBindingSource.EndEdit();
            this.fooTableTableAdapter.Update(this.database1DataSet.FooTable);
            MessageBox.Show("Update successful");
        }

    }
}
