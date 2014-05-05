using ChartTypeExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChartTypeExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, System.EventArgs e)
        {
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;

        }

        private void toolStripButton2_Click_1(object sender, System.EventArgs e)
        {
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
            this.splitContainer1.Orientation = Orientation.Vertical;
        }

        private void toolStripButton4_Click(object sender, System.EventArgs e)
        {
            this.splitContainer1.Orientation = Orientation.Horizontal;

        }

    }
}
