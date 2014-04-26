using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

            // X:\jsc.svn\examples\javascript\svg\SVGChartExperiment\SVGChartExperiment\Application.cs

            #region build something visible

            {
                var svg = new ISVGSVGElement
                {
                    //width = 200,
                    //height = 200
                };


                var polygon = new ISVGPolygonElement
                {
                    //points = "200,10 250,190 160,210"
                    //points = w.ToString()

                }.AttachTo(svg);


                Action update = delegate
                {
                    svg.width = this.clientWidth;
                    svg.height = this.clientHeight;

                    // width: 780px;

                    //svg.setAttribute("width", "200");

                    var w = new StringBuilder();

                    Action<double, double> add = (x, y) =>
                    {
                        // for print!
                        var svghardmargin = 16;

                        var xx = (x * (this.clientWidth - 2 * svghardmargin)) + svghardmargin;
                        var yy = (y * (this.clientHeight - 2 * svghardmargin)) + svghardmargin;

                        w.Append(xx + "," + yy + " ");

                    };

                    add(1, 1);
                    add(0, 1);

                    // upside down

                    // databind?

                    //StackTrace
                    // jsc shall fill in the StackTrace info?

                    //Console.WriteLine(new { this.DataSource });

                    // 35:33486ms { DataSource = <Namespace>.BindingSource } 

                    var asBindingSource = this.DataSource as __BindingSource;
                    if (asBindingSource != null)
                    {
                        foreach (Series s in this.Series)
                        {
                            // 35:11741ms { asBindingSource = <Namespace>.BindingSource, Count = 5, XValueMember = Xvalues, YValueMembers = Series2 }

                            //item.XValueType
                            Console.WriteLine(new
                            {
                                asBindingSource,
                                asBindingSource.Count,


                                s.XValueMember,
                                s.YValueMembers
                            });


                            for (int i = 0; i < asBindingSource.Count; i++)
                            {
                                add(
                                    i / (asBindingSource.Count - 1),
                                    i / (asBindingSource.Count - 1)
                                    );

                            }

                            // we can only talk to 1 for now
                            break;
                        }

                        polygon.points = w.ToString();
                        return;
                    }


                    add(0, 0.8);
                    add(0.2, 0.2);
                    add(0.4, 0.8);
                    add(0.6, 0.6);
                    add(0.8, 0.8);
                    add(1.0, 0.0);

                    polygon.points = w.ToString();
                };

                InternalAtDataBind += update;

                update();

                this.ClientSizeChanged +=
                    delegate
                    {
                        update();
                    };

                polygon.setAttribute("style", "fill:purple;stroke:darkpurple;stroke-width:1;");


                svg.AttachTo(InternalElement);
            }

            #endregion
        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.DataBind()]

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.ChartNamedElement.set_Name(System.String)]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.ChartNamedElement.set_Name(System.String)]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.get_ChartAreas()]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.set_Palette(System.Windows.Forms.DataVisualization.Charting.ChartColorPalette)]
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Series.set_ChartArea(System.String)]
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataVisualization.Charting.Chart.get_Series()]

        public ChartColorPalette Palette { get; set; }

        public event Action InternalAtDataBind;

        public void DataBind()
        {
            // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs

            if (InternalAtDataBind != null)
                InternalAtDataBind();
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
