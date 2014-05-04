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

        // thats the labels in the bottom
        public string XValueMember { get; set; }


        public string Legend { get; set; }




        #region ChartType

        public event Action InternalChartTypeChanged;

        SeriesChartType InternalChartType = SeriesChartType.Column;

        // what is this supposed to do?       [Bindable(true)]
        public SeriesChartType ChartType
        {
            get { return InternalChartType; }
            set
            {

                InternalChartType = value;

                if (InternalChartTypeChanged != null)
                    InternalChartTypeChanged();
            }
        }
        #endregion




        public string ChartArea { get; set; }


        public static implicit operator __Series(Series s)
        {
            return (__Series)(object)s;

        }
    }
}
