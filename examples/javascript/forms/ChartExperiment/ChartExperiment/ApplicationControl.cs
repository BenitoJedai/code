using ChartExperiment;
using System;
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
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripContainer.get_ContentPanel()]

        }

        private void ApplicationControl_SizeChanged(object sender, System.EventArgs e)
        {
            // 41:26920ms { Name = , Siblin
            // no. not in the browser. why?


            // dod docked controls get the event? do we?
            //Console.WriteLine(
            //    new { this.ParentForm.Name } +
            //    " ApplicationControl_SizeChanged");
        }

    }
}
