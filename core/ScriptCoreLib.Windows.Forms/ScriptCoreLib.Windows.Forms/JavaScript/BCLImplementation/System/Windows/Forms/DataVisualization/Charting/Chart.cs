using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.Chart))]
    public class __Chart : __Control, ISupportInitialize
    {
        public IHTMLDiv InternalElement = typeof(__Chart);



        // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs

        // html, svg, flash or webgl?

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }




        public double BindingSourcePositionAlpha = 0.1;

        //public static Action<WebUserControl, Chart, double> InternalSetBindingSourcePositionAlpha;
        // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\Experimental\WebUserControl.cs
        [Obsolete("how to do this automatically?")]
        public static void InternalSetBindingSourcePositionAlpha(object context, Chart c, double value)
        {
            __Chart cc = c;

            cc.BindingSourcePositionAlpha = Math.Max(0.0, Math.Min(1.0, value));
            cc.DataBind();
        }

        public static implicit operator __Chart(Chart e)
        {
            return (__Chart)(object)e;
        }

        public __Chart()
        {
            this.ChartAreas = new __ChartAreaCollection();
            this.Series = new __SeriesCollection();
            this.Legends = new __LegendCollection();

            // X:\jsc.svn\examples\javascript\svg\SVGChartExperiment\SVGChartExperiment\Application.cs

            this.InternalElement.style.overflow = DOM.IStyle.OverflowEnum.hidden;


            #region build something visible

            {
                var svg = new ISVGSVGElement
                {
                    //width = 200,
                    //height = 200
                };


                var polygon0 = new ISVGPolygonElement { }.AttachTo(svg);
                var polygon1 = new ISVGPolygonElement { }.AttachTo(svg);


                Action update = delegate
                {
                    svg.width = this.clientWidth;
                    svg.height = this.clientHeight;

                    // width: 780px;

                    //svg.setAttribute("width", "200");

                    var w0 = new StringBuilder();
                    var w1 = new StringBuilder();

                    Action<double, double> add0 = (x, y) =>
                    {
                        // for print!
                        var svghardmargin = 16;

                        var xx = (x * (this.clientWidth - 2 * svghardmargin)) + svghardmargin;
                        var yy = (y * (this.clientHeight - 2 * svghardmargin)) + svghardmargin;

                        w0.Append(xx + "," + yy + " ");

                    };

                    Action<double, double> add1 = (x, y) =>
                    {
                        // for print!
                        var svghardmargin = 16;

                        var xx = (x * (this.clientWidth - 2 * svghardmargin)) + svghardmargin;
                        var yy = (y * (this.clientHeight - 2 * svghardmargin)) + svghardmargin;

                        w1.Append(xx + "," + yy + " ");

                    };

                    //add0(1, 1);
                    add0(0, 1);

                    add1(0, 1);

                    // upside down

                    // databind?

                    //StackTrace
                    // jsc shall fill in the StackTrace info?

                    //Console.WriteLine(new { this.DataSource });

                    // 35:33486ms { DataSource = <Namespace>.BindingSource } 

                    // as IList instead?
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

                            var datas =
                                from rowIndex in Enumerable.Range(0, asBindingSource.Count)

                                let asDataRowView = (DataRowView)asBindingSource[rowIndex]

                                //let asDataRowView = asBindingSource[rowIndex] as DataRowView
                                //where asDataRowView != null

                                let XValueMember = asDataRowView[s.XValueMember]
                                let YValueMembers = asDataRowView[s.YValueMembers]

                                // try?
                                //let x = Convert.ToDouble(XValueMember)
                                let y = Convert.ToDouble(YValueMembers)


                                select new { rowIndex, XValueMember, y };


                            var data0 = datas.ToArray();

                            var data1 = datas.Take(asBindingSource.Position + 1).ToArray();

                            // what about neg values?
                            //var xmax = data.Max(z => z.x);
                            var ymax = data0.Max(z => z.y);

                            Console.WriteLine(new { data0.Length, max = ymax });

                            // script: error JSC1000: No implementation found for this native method, please implement [static System.Convert.ToDouble(System.Object)]

                            //35:524ms { item = { rowIndex = 0, x = 1, y = 44 }, xx = 0.25, yy = 0.19999999999999996 }
                            //35:525ms { item = { rowIndex = 1, x = 2, y = 55 }, xx = 0.5, yy = 0 }
                            //35:525ms { item = { rowIndex = 2, x = 3, y = 33 }, xx = 0.75, yy = 0.4 }
                            //35:526ms { item = { rowIndex = 3, x = 4, y = 9 }, xx = 1, yy = 0.8363636363636364 }
                            //35:526ms { item = { rowIndex = 4, x = 5, y = 44 }, xx = 1.25, yy = 0.19999999999999996 }

                            //foreach (var item in data.OrderBy(z => z.x))


                            // .net does not seem to auto reorder x axis values per default
                            foreach (var item in data0)
                            {
                                //var xx = item.x / Math.Max(xmax, 1);
                                //var xx = item.rowIndex / Math.Max(data.Length, 1);
                                // fk int math.
                                var xx = (double)item.rowIndex / Math.Max(data0.Length - 1, 1);
                                var yy = 1 - (item.y / Math.Max(ymax, 1));

                                //Console.WriteLine(new { item, xx, yy });

                                add0(
                                    xx,
                                    yy
                                  );

                            }

                            foreach (var item1 in data1)
                            {
                                var xx = (double)item1.rowIndex / Math.Max(data0.Length - 1, 1);
                                var yy = 1 - (item1.y / Math.Max(ymax, 1));

                                //Console.WriteLine(new { item1, xx, yy });

                                add1(
                                    xx,
                                    yy
                                  );

                                if (item1.rowIndex == asBindingSource.Position)
                                {
                                    // this is the last selection item.


                                    this.InternalElement.title =
                                            item1.XValueMember + " " + item1.y;




                                    add1(
                                     xx,
                                     1.0
                                   );
                                }
                            }

                            //35:704ms { rowIndex = 0, XValueMember = 1, YValueMembers = 44 }
                            //35:704ms { rowIndex = 1, XValueMember = 2, YValueMembers = 55 }
                            //35:705ms { rowIndex = 2, XValueMember = 3, YValueMembers = 33 }
                            //35:705ms { rowIndex = 3, XValueMember = 4, YValueMembers = 9 }
                            //35:705ms { rowIndex = 4, XValueMember = 5, YValueMembers = 44 }



                            //for (int i = 0; i < asBindingSource.Count; i++)
                            //{
                            //    add(
                            //        i / (asBindingSource.Count - 1),
                            //        i / (asBindingSource.Count - 1)
                            //        );

                            //}

                            // we can only talk to 1 for now
                            break;
                        }


                        add0(1.0, 1.0);

                        polygon0.setAttribute("style", "fill:purple;stroke:darkpurple;stroke-width:1;");
                        polygon0.points = w0.ToString();

                        //polygon1.setAttribute("style", "fill:rgba(255,255,255,0.07);stroke:rgba(255,255,255,0.0);stroke-width:8;");
                        polygon1.setAttribute("style", "fill:rgba(255,255,255," + this.BindingSourcePositionAlpha + ");stroke:rgba(255,255,255,0.0);stroke-width:8;");
                        polygon1.points = w1.ToString();
                        return;
                    }


                    add0(0, 0.8);
                    add0(0.2, 0.2);
                    add0(0.4, 0.8);
                    add0(0.6, 0.6);
                    add0(0.8, 0.8);
                    add0(1.0, 0.0);

                    polygon0.setAttribute("style", "fill:red;stroke:darkpurple;stroke-width:1;");
                    polygon0.points = w0.ToString();
                };


                InternalDataSourceChanged += delegate
                {
                    #region jsc experience, not available for CLR
                    {
                        var asBindingSource = this.DataSource as __BindingSource;
                        if (asBindingSource != null)
                        {
                            asBindingSource.PositionChanged +=
                                delegate
                                {
                                    update();
                                };
                        }
                    }
                    #endregion
                };

                InternalAtDataBind += delegate
                {
                    update();
                };

                update();

                //this.ClientSizeChanged +=
                this.SizeChanged +=
                    delegate
                    {
                        update();
                    };




                svg.AttachTo(InternalElement);
            }

            #endregion
        }

        public ChartColorPalette Palette { get; set; }

        public event Action InternalAtDataBind;

        public void DataBind()
        {
            // X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs

            if (InternalAtDataBind != null)
                InternalAtDataBind();
        }

        public ChartAreaCollection ChartAreas { get; set; }




        #region DataSource
        public object InternalDataSource;
        public event Action InternalDataSourceChanged;
        public object DataSource
        {
            get { return InternalDataSource; }
            set
            {
                InternalDataSource = value;

                if (InternalDataSourceChanged != null)
                    InternalDataSourceChanged();

                DataBind();
            }
        }
        #endregion



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
