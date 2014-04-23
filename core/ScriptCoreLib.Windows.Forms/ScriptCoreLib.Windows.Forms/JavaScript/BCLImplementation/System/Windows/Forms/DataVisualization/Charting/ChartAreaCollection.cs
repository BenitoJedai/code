using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.ChartAreaCollection))]
    internal class __ChartAreaCollection : __ChartNamedElementCollection<ChartArea>
    {

        public static implicit operator ChartAreaCollection(__ChartAreaCollection x)
        {
            return (ChartAreaCollection)(object)x;
        }
    }
}
