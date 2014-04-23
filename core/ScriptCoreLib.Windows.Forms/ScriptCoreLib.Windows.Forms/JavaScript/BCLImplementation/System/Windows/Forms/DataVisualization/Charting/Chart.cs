using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.Chart))]
    internal class __Chart : __Control, ISupportInitialize
    {
        public IHTMLDiv InternalElement = new IHTMLDiv { };


        // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs

        // html, svg, flash or webgl?

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public __Chart()
        {
            this.ChartAreas = new __ChartAreaCollection();
            this.Series = new __SeriesCollection();
            this.Legends = new __LegendCollection();
        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.DataBind()]

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.ChartNamedElement.set_Name(System.String)]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.ChartNamedElement.set_Name(System.String)]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.get_ChartAreas()]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.set_Palette(System.Windows.Forms.DataVisualization.Charting.ChartColorPalette)]
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Series.set_ChartArea(System.String)]
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.get_Series()]

        public ChartColorPalette Palette { get; set; }

        public void DataBind()
        {
            // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs

        }

        public ChartAreaCollection ChartAreas { get; set; }
        public object DataSource { get; set; }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.get_Legends()]

        public LegendCollection Legends { get; set; }

        public SeriesCollection Series { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}
