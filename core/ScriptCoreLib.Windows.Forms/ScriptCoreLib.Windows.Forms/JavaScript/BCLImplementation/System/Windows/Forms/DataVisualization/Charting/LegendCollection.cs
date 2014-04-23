using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.LegendCollection))]
    internal class __LegendCollection : __ChartNamedElementCollection<Legend>
    {


        public static implicit operator LegendCollection(__LegendCollection x)
        {
            return (LegendCollection)(object)x;
        }
    }
}
