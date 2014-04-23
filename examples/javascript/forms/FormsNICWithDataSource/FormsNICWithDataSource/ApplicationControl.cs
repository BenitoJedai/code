using FormsNICWithDataSource;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsNICWithDataSource
{
    public partial class ApplicationControl : UserControl
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140423
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.set_AllowNew(System.Boolean)]

        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void nICDataGetInterfacesBindingSourceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
