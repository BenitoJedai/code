using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.Series))]
    internal class __Series : __DataPointCustomProperties
    {
        public string YValueMembers { get; set; }

        public string XValueMember { get; set; }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Series.set_XValueMember(System.String)]

        public string Legend { get; set; }

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Series.set_ChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType)]

        public SeriesChartType ChartType { get; set; }

        public string ChartArea { get; set; }

    }
}
