using ScriptCoreLib.Shared.BCLImplementation.System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(ChartElementCollection<>))]
    internal class __ChartElementCollection<T> : __Collection<T>, IDisposable where T : ChartElement
    {
        public void Dispose()
        {
        }
    }
}
