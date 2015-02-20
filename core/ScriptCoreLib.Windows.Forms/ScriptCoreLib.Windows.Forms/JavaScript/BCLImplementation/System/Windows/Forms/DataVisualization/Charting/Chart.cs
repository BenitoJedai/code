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
using System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.DataVisualization.Charting
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataVisualization.Charting.Chart))]
    public class __Chart : __Control, ISupportInitialize
    {
        // will it show in vr/svg?
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




        #region BindingSourcePositionAlpha
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
        #endregion

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


                var SeriesOfInterest = new List<__Series>();

                var UpdateCounter = 0;

                Action update = delegate { };

                update = delegate
                {
                    UpdateCounter++;


                    Console.WriteLine("__Chart update " + new { UpdateCounter });



                    svg.width = this.clientWidth;
                    svg.height = this.clientHeight;

                    // width: 780px;

                    //svg.setAttribute("width", "200");

                    var w0 = new StringBuilder();
                    var w1 = new StringBuilder();



                    // for print!
                    var svghardmarginx = 16;
                    var svghardmarginy = 16;

                    #region add0
                    Action<double, double> add0 = (x, y) =>
                    {
                        var xx = (x * (this.clientWidth - 2 * svghardmarginx)) + svghardmarginx;
                        var yy = (y * (this.clientHeight - 2 * svghardmarginy)) + svghardmarginy;

                        w0.Append(xx + "," + yy + " ");

                    };

                    // the highlight
                    Action<double, double> add1 = (x, y) =>
                    {
                        var xx = (x * (this.clientWidth - 2 * svghardmarginx)) + svghardmarginx;
                        var yy = (y * (this.clientHeight - 2 * svghardmarginy)) + svghardmarginy;

                        w1.Append(xx + "," + yy + " ");

                    };
                    #endregion

                    //add0(1, 1);


                    // upside down

                    // databind?

                    //StackTrace
                    // jsc shall fill in the StackTrace info?

                    //Console.WriteLine(new { this.DataSource });

                    // 35:33486ms { DataSource = <Namespace>.BindingSource } 

                    // as IList instead?

                    // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\DataTable.cs
                    var __DataSource = this.DataSource;
                    if (__DataSource != null)
                    {
                        // jsc as opertor for jsc does not like null yet?


                        var asIList = __DataSource as IList;
                        if (asIList == null)
                        {
                            Console.WriteLine("not IList " + new { __DataSource });
                            // 44:105945ms not IList

                            var asIListSource = __DataSource as IListSource;
                            if (asIListSource != null)
                            {
                                asIList = asIListSource.GetList();
                                Console.WriteLine("IListSource " + new { asIList });
                            }

                        }

                        var asBindingSource_Position = 0;
                        var asBindingSource = __DataSource as __BindingSource;
                        if (asBindingSource != null)
                        {
                            asBindingSource_Position = asBindingSource.Position;
                        }

                        //var asBindingSource = asIListSource.GetList();
                        if (asIList != null)
                        {
                            #region Series
                            foreach (Series s in this.Series)
                            {
                                var ss = (__Series)s;

                                // when the chart type changes, we may want to redraw our diagram.
                                // X:\jsc.svn\examples\javascript\forms\ChartTypeExperiment\ChartTypeExperiment\ApplicationControl.cs

                                // 44:95855ms { ChartType = 10, a
                                // 44:142897ms { ChartType = 13,






                                // hey SeriesOfInterest have we seen this series yet?
                                if (!SeriesOfInterest.Contains(ss))
                                {
                                    // okay. first time we see it.
                                    // lets start monitoring it for any changes we need to know about.

                                    ss.InternalChartTypeChanged += delegate
                                    {

                                        update();
                                    };

                                    SeriesOfInterest.Add(ss);
                                }



                                // 35:11741ms { asBindingSource = <Namespace>.BindingSource, Count = 5, XValueMember = Xvalues, YValueMembers = Series2 }

                                //item.XValueType
                                Console.WriteLine(new
                                {
                                    s.ChartType,

                                    asIList,
                                    asIList.Count,


                                    s.XValueMember,
                                    s.YValueMembers
                                });


                                Func<string, double> ConvertToDoubleOrZero = zz =>
                                {
                                    // http://stackoverflow.com/questions/586436/double-tryparse-or-convert-todouble-which-is-faster-and-safer

                                    if (string.IsNullOrEmpty(zz))
                                        return 0;


                                    return double.Parse(zz);
                                };




                                var datas =
                                    from rowIndex in Enumerable.Range(0, asIList.Count)

                                    let asDataRowView = (DataRowView)asIList[rowIndex]

                                    //let asDataRowView = asBindingSource[rowIndex] as DataRowView
                                    //where asDataRowView != null

                                    // Error: Invalid value for <polygon> attribute points="16,155 16,43.8 150.4,16 284.8,71.6 419.2,132.25454545454545 553.6,43.8 688,NaN 688,155 " 

                                    let XValueMember = asDataRowView[s.XValueMember]
                                    let YValueMembers = asDataRowView[s.YValueMembers]

                                    // try?
                                    //let x = Convert.ToDouble(XValueMember)
                                    //let y = Convert.ToDouble(YValueMembers)

                                    // new rows added, may show up empty!
                                    let y = ConvertToDoubleOrZero((string)YValueMembers)


                                    select new { rowIndex, XValueMember, y };


                                var data0 = datas.ToArray();

                                var data1 = datas.Take(asBindingSource_Position + 1).ToArray();

                                // what about neg values?
                                //var xmax = data.Max(z => z.x);
                                var ymax = data0.Max(z => z.y);

                                //Console.WriteLine(new { data0.Length, max = ymax });

                                // script: error JSC1000: No implementation found for this native method, please implement [static System.Convert.ToDouble(System.Object)]

                                //35:524ms { item = { rowIndex = 0, x = 1, y = 44 }, xx = 0.25, yy = 0.19999999999999996 }
                                //35:525ms { item = { rowIndex = 1, x = 2, y = 55 }, xx = 0.5, yy = 0 }
                                //35:525ms { item = { rowIndex = 2, x = 3, y = 33 }, xx = 0.75, yy = 0.4 }
                                //35:526ms { item = { rowIndex = 3, x = 4, y = 9 }, xx = 1, yy = 0.8363636363636364 }
                                //35:526ms { item = { rowIndex = 4, x = 5, y = 44 }, xx = 1.25, yy = 0.19999999999999996 }

                                //foreach (var item in data.OrderBy(z => z.x))




                                //44:3380ms { ChartType = 0, asIList = <Namespace>.BindingSource, Count = 5, XValueMember = Xvalues, YValueMembers = Series2 }
                                //44:11642ms __Chart update { UpdateCounter = 18 } view-source:38201
                                //44:11643ms { ChartType = 10, asIList = <Namespace>.BindingSource, Count = 5, XValueMember = Xvalues, YValueMembers = Series2 } view-source:38160
                                //44:16057ms __Chart update { UpdateCounter = 19 } view-source:38201
                                //44:16057ms { ChartType = 13, asIList = <Namespace>.BindingSource, Count = 5, XValueMember = Xvalues, YValueMembers = Series2 } 


                                // .1?
                                // 44:619ms { ColumnWidth = 315 }
                                var ColumnWidth =
                                    (1.0 / data0.Length) * 0.5;


                                if (ss.ChartType == SeriesChartType.Column)
                                {
                                    // changing constants, jsc should autoupdate currently running code in the browser.
                                    // should we have an int constant pool for that as we have for static strings?
                                    svghardmarginx =
                                        16 +
                                        (this.clientWidth / data0.Length) / 2;
                                }


                                //Console.WriteLine(
                                //    new { ColumnWidth }
                                //    );


                                add0(0, 1);

                                add1(0, 1);


                                // .net does not seem to auto reorder x axis values per default
                                foreach (var item0 in data0)
                                {
                                    //var xx = item.x / Math.Max(xmax, 1);
                                    //var xx = item.rowIndex / Math.Max(data.Length, 1);
                                    // fk int math.

                                    var xx = (double)item0.rowIndex / Math.Max(data0.Length - 1, 1);
                                    var yy = 1 - (item0.y / Math.Max(ymax, 1));

                                    if (ss.ChartType == SeriesChartType.Area)
                                    {
                                        //Console.WriteLine(new { item, xx, yy });

                                        add0(
                                            xx,
                                            yy
                                          );

                                    }
                                    else
                                    {
                                        // how do we draw column?


                                        //Console.WriteLine(new { item, xx, yy });

                                        add0(
                                           xx - ColumnWidth,
                                           1
                                         );

                                        add0(
                                           xx - ColumnWidth,
                                           yy
                                         );


                                        add0(
                                              xx + ColumnWidth,
                                              yy
                                            );

                                        add0(
                                           xx + ColumnWidth,
                                           1
                                         );

                                    }
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

                                    if (item1.rowIndex == asBindingSource_Position)
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
                            #endregion


                            add0(1.0, 1.0);

                            polygon0.setAttribute("style", "fill:purple;stroke:darkpurple;stroke-width:1;");
                            polygon0.points = w0.ToString();

                            //polygon1.setAttribute("style", "fill:rgba(255,255,255,0.07);stroke:rgba(255,255,255,0.0);stroke-width:8;");
                            polygon1.setAttribute("style", "fill:rgba(255,255,255," + this.BindingSourcePositionAlpha + ");stroke:rgba(255,255,255,0.0);stroke-width:8;");
                            polygon1.points = w1.ToString();
                            return;
                        }
                    }






                    add0(0, 1);

                    add1(0, 1);


                    add0(0, 0.8);
                    add0(0.2, 0.2);
                    add0(0.4, 0.8);
                    add0(0.6, 0.6);
                    add0(0.8, 0.8);

                    add0(1.0, 1.0);

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
