using ChartExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChartExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void chart1_Click(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/questions/13350036/c-sharp-charts-add-multiple-series-from-datatable
            // http://msdn.microsoft.com/en-us/library/dd456766(v=vs.110).aspx
            //this.book1Sheet1BindingSourceBindingSource.AddNew();
            this.chart1.DataBind();

        }

        private void book1Sheet1BindingSourceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
