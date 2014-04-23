using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(ChartNamedElementCollection<>))]
    internal class __ChartNamedElementCollection<T> : __ChartElementCollection<T> where T : ChartNamedElement
    {
        public __ChartNamedElementCollection()
        {

        }
    }
}
