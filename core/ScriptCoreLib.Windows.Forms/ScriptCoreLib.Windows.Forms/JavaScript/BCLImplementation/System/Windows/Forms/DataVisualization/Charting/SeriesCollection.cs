using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.SeriesCollection))]
    internal class __SeriesCollection : __ChartNamedElementCollection<Series>
    {

        public static implicit operator SeriesCollection(__SeriesCollection x)
        {
            return (SeriesCollection)(object)x;
        }

    }
}
