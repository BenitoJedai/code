using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SVGNavigationTiming;
using SVGNavigationTiming.Design;
using SVGNavigationTiming.HTML.Pages;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using Abstractatech.JavaScript.ApplicationPerformance;

namespace SVGNavigationTiming
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // allow onload to complete

            //var c = 0;
            IXMLHttpRequestActivity.onopen +=
                e =>
                {
                    //c++;

                    //// http://foo1:foo2@192.168.43.252:28406/xml?WebMethod=06000003&n=AtApplicationPerformance&c=1
                    //e.request.withCredentials = true;

                    //e.url += "&c=" + c;
                    //e.user = "foo1";
                    //e.password = "foo2";

                    Console.WriteLine("IXMLHttpRequestActivity.onopen " + new
                    {
                        Native.document.location.href,
                        e.url,
                        e.async
                    });
                };

            new IHTMLButton { "Reset" }.AttachToDocument().WhenClicked(this.Reset);

            new IHTMLButton { "WebMethod2500" }.AttachToDocument().WhenClicked(this.WebMethod2500);
            new IHTMLButton { "WebMethod500" }.AttachToDocument().WhenClicked(this.WebMethod500);
            new IHTMLButton { "x22" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    var a = Enumerable.Range(0, 22).Select(i => this.WebMethod22());

                    // script: error JSC1000: method was found, but too late: [WhenAll]
                    await Task.WhenAll(a);
                }
                );

            #region GetApplicationPerformance
            new IHTMLButton { "GetApplicationPerformance" }.AttachToDocument().WhenClicked(
                async button =>
                {


                    var f = new ApplicationPerformanceForm(this);



                    //Native.document.title = "f";
                    await f.ShowAsync();

                }
            );
            #endregion



            // package user
            Native.window.ReportUpstreamApplicationPerformance(this, page);

        }

    }
}

namespace Abstractatech.JavaScript.ApplicationPerformance
{
    public class ApplicationPerformanceForm : Form
    {
        public ApplicationPerformanceForm(IExploreApplicationPerformance service)
        {
            var f = this;



            f.Text = "GetApplicationPerformance";

            var g = new DataGridView
            {
                ScrollBars = System.Windows.Forms.ScrollBars.Vertical,

                AllowUserToAddRows = false,

                Dock = DockStyle.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
            }.AttachTo(f);

            #region CellFormatting
            g.CellFormatting +=
                (sender, e) =>
                {
                    var SourceCell = g[e.ColumnIndex, e.RowIndex];
                    var IsSelected = SourceCell.Selected;


                    if (SourceCell.OwningColumn.Name == "Timestamp" && !SourceCell.IsInEditMode)
                    {
                        e.FormattingApplied = true;

                        var t = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(SourceCell.Value);

                        e.Value = t.ToString();
                        //e.CellStyle.BackColor = g.RowHeadersDefaultCellStyle.BackColor;
                        e.CellStyle.ForeColor = Color.Gray;

                        return;
                    }
                };
            #endregion

            CSSDataGridView.__ContentTable_css_tr.hover.style.textDecoration = "underline";
            CSSDataGridView.__ContentTable_css_tr.hover.style.cursor = IStyle.CursorEnum.pointer;
            CSSDataGridView.__ContentTable_css_tr.hover.style.color = "blue";

            #region Load
            f.Load +=
                async delegate
                {
                    //await Native.window.requestAnimationFrameAsync;
                    //await Native.window.requestAnimationFrameAsync;
                    var s = Stopwatch.StartNew();
                    var data0 = await service.GetApplicationPerformance();
                    f.Text += " " + s.ElapsedMilliseconds + "ms";

                    g.DataSource = data0;



                    var p = g.PreferredSize;

                    if (p.Height > 400)
                        p.Height = 400;

                    f.ClientSize = p;


                };
            #endregion



            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.add_CellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventHandler)]
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewCellMouseEventArgs.get_RowIndex()]

            //g.CellContentClick +=
            //g.CellContentDoubleClick +=
            //g.CellMouseClick +=

            #region CellClick
            g.CellClick +=
                async delegate
                {

                    PerformanceResourceTimingData2ApplicationPerformanceRow x = g.SelectedRows.AsEnumerable().Select(
                                row => (PerformanceResourceTimingData2ApplicationPerformanceRow)(DataRow)row.DataBoundItem
                            ).FirstOrDefault();




                    var kf = new Form { Owner = f, Text = "GetApplicationResourcePerformance" };
                    var kg = new DataGridView
                    {
                        ScrollBars = System.Windows.Forms.ScrollBars.None,
                        AllowUserToAddRows = false,

                        Dock = DockStyle.Fill,
                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                        ReadOnly = true
                    }.AttachTo(kf);


                    #region CellFormatting
                    kg.CellFormatting +=
                        (sender, e) =>
                        {
                            var SourceCell = kg[e.ColumnIndex, e.RowIndex];
                            var IsSelected = SourceCell.Selected;


                            if (SourceCell.OwningColumn.Name == "Timestamp" && !SourceCell.IsInEditMode)
                            {
                                e.FormattingApplied = true;

                                var t = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(SourceCell.Value);

                                e.Value = t.ToString();
                                //e.CellStyle.BackColor = kkg.RowHeadersDefaultCellStyle.BackColor;
                                e.CellStyle.ForeColor = Color.Gray;

                                return;
                            }

                            #region duration
                            if (SourceCell.OwningColumn.Name == "duration" && !SourceCell.IsInEditMode)
                            {
                                e.FormattingApplied = true;

                                var crow = (PerformanceResourceTimingData2ApplicationResourcePerformanceRow)(DataRow)SourceCell.OwningRow.DataBoundItem;


                                var t = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(SourceCell.Value);


                                e.Value = crow.duration + "ms";

                                var rows =
                                    kg.Rows.AsEnumerable().Where(xx => xx.DataBoundItem != null).Select(
                                         xrow => ((PerformanceResourceTimingData2ApplicationResourcePerformanceRow)(DataRow)xrow.DataBoundItem)
                                    );

                                var min = rows.Min(
                                    xx => xx.duration
                                    );
                                var max = rows.Max(
                                    xx => xx.duration
                                    );

                                var red = (int)(((crow.duration - min) * 255) / (max - min));

                                e.CellStyle.ForeColor = Color.FromArgb(
                                    red: red,
                                    green: 0,
                                    blue: 255 - red
                                );
                                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                                return;
                            }
                            #endregion
                        };
                    #endregion


                    kf.Load +=
                      async delegate
                      {
                          var s = Stopwatch.StartNew();
                          var kdata = await service.GetApplicationResourcePerformance(x);
                          kf.Text += " " + s.ElapsedMilliseconds + "ms";

                          kg.DataSource = kdata;

                          kf.ClientSize = kg.PreferredSize;

                      };


                    kg.CellClick +=
                        delegate
                        {
                            kg.SelectedRows.AsEnumerable().Select(
                                row => (PerformanceResourceTimingData2ApplicationResourcePerformanceRow)(DataRow)row.DataBoundItem
                            ).FirstOrDefault().With(
                                async row =>
                                {
                                    var kkf = new Form { Owner = kf, Text = "GetSimilarApplicationResourcePerformance" };

                                    var kkg = new DataGridView
                                    {
                                        ScrollBars = System.Windows.Forms.ScrollBars.Vertical,
                                        AllowUserToAddRows = false,

                                        //DataSource = kdata,
                                        Dock = DockStyle.Fill,
                                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                                        ReadOnly = true
                                    }.AttachTo(kkf);


                                    #region CellFormatting
                                    kkg.CellFormatting +=
                                        (sender, e) =>
                                        {
                                            var SourceCell = kkg[e.ColumnIndex, e.RowIndex];
                                            var IsSelected = SourceCell.Selected;


                                            if (SourceCell.OwningColumn.Name == "Timestamp" && !SourceCell.IsInEditMode)
                                            {
                                                e.FormattingApplied = true;

                                                var t = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(SourceCell.Value);

                                                e.Value = t.ToString();
                                                //e.CellStyle.BackColor = kkg.RowHeadersDefaultCellStyle.BackColor;
                                                e.CellStyle.ForeColor = Color.Gray;

                                                return;
                                            }

                                            #region duration
                                            if (SourceCell.OwningColumn.Name == "duration" && !SourceCell.IsInEditMode)
                                            {
                                                e.FormattingApplied = true;

                                                var crow = (PerformanceResourceTimingData2ApplicationResourcePerformanceRow)(DataRow)SourceCell.OwningRow.DataBoundItem;


                                                var t = ScriptCoreLib.Library.StringConversionsForStopwatch.DateTimeConvertFromObject(SourceCell.Value);


                                                e.Value = crow.duration + "ms";

                                                var rows =
                                                    kkg.Rows.AsEnumerable().Where(xx => xx.DataBoundItem != null).Select(
                                                         xrow => ((PerformanceResourceTimingData2ApplicationResourcePerformanceRow)(DataRow)xrow.DataBoundItem)
                                                    );

                                                var min = rows.Min(
                                                    xx => xx.duration
                                                    );
                                                var max = rows.Max(
                                                    xx => xx.duration
                                                    );

                                                var red = (int)(((crow.duration - min) * 255) / (max - min));

                                                e.CellStyle.ForeColor = Color.FromArgb(
                                                    red: red,
                                                    green: 0,
                                                    blue: 255 - red
                                                );
                                                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                                                return;
                                            }
                                            #endregion
                                        };
                                    #endregion

                                    #region Load
                                    kkf.Load +=
                                        async delegate
                                        {
                                            //kkg.DataSource = await this.GetSimilarApplicationResourcePerformance(row);

                                            var s = Stopwatch.StartNew();

                                            var xdata = await service.GetSimilarApplicationResourcePerformance(row);
                                            kkf.Text += " " + s.ElapsedMilliseconds + "ms";

                                            kkg.DataSource = xdata;

                                            var p = kkg.PreferredSize;
                                            p.Height = p.Height.Min(400);

                                            kkf.ClientSize = p;
                                        };
                                    #endregion

                                    //kf.Hide();

                                    //Native.document.title = "kkf";
                                    await kkf.ShowAsync(hideOwner: true);

                                    //kf.Show();

                                    //Native.document.title = "kf";

                                }
                            );
                        };

                    //Native.document.title = "kf";
                    await kf.ShowAsync(hideOwner: true);
                    //Native.document.title = "f";


                };
            #endregion
        }
    }
}

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class UpstreamApplicationPerformanceExtensions
    {
        public static void ReportUpstreamApplicationPerformance(this IWindow w, IUpstreamApplicationPerformance service,


            IApp page = null
            )
        {
            Native.window.requestAnimationFrame +=
               async delegate
               {
                   var timing = Native.window.performance.timing;

                   #region page
                   if (page != null)
                   {
                       // SVGTSpanElement
                       page.connectEnd = new { timing.connectEnd }.ToString();
                       page.connectStart = new { timing.connectStart }.ToString();
                       page.TCP = "TCP " + (timing.connectEnd - timing.connectStart);

                       page.RequestStart = new { timing.requestStart }.ToString();
                       page.ResponseStart = new { timing.responseStart }.ToString();
                       page.ResponseEnd = new { timing.responseEnd }.ToString();
                       page.Request = "Request " + (timing.responseStart - timing.requestStart);
                       page.Response = "Response " + (timing.responseEnd - timing.responseStart);

                       page.DomLoading = new { timing.domLoading }.ToString();
                       page.DomComplete = new { timing.domComplete }.ToString();
                       page.Processing = "Processing " + (timing.domComplete - timing.domLoading);

                       page.LoadEventEnd = new { timing.loadEventEnd }.ToString();
                       page.LoadEventStart = new { timing.loadEventStart }.ToString();
                       page.Load = "Load " + (timing.loadEventEnd - timing.loadEventStart);
                   }
                   #endregion


                   // IXMLHttpRequestActivity.onopen { url = /xml?WebMethod=06000003&c=1 }

                   #region CurrentApplicationPerformance
                   var CurrentApplicationPerformance0 =
                                   await service.AtApplicationPerformance(
                                       new PerformanceResourceTimingData2ApplicationPerformanceRow
                                       {
                                           Timestamp = DateTime.Now,

                                           connectStart = (long)timing.connectStart,
                                           connectEnd = (long)timing.connectEnd,
                                           requestStart = (long)timing.requestStart,
                                           responseStart = (long)timing.responseStart,
                                           responseEnd = (long)timing.responseEnd,
                                           domLoading = (long)timing.domLoading,
                                           domComplete = (long)timing.domComplete,
                                           loadEventStart = (long)timing.loadEventStart,
                                           loadEventEnd = (long)timing.loadEventEnd,
                                       }
                                    );
                   var CurrentApplicationPerformance = CurrentApplicationPerformance0;
                   #endregion





                   Native.window.performance.With(
                       async p =>
                       {
                           p.onresourcetimingbufferfull =
                               IFunction.OfDelegate(
                                    new Action(
                                    delegate
                                    {
                                        Console.WriteLine("onresourcetimingbufferfull " + p.getEntries().Length);
                                    }
                                ));

                           // http://docs.webplatform.org/wiki/apis/resource_timing/Performance/setResourceTimingBufferSize
                           // Object #<Performance> has no method 'setResourceTimingBufferSize
                           //p.setResourceTimingBufferSize(96);

                           for (int i = 0; ; i++)
                           {
                               Native.document.title = new { i, p.getEntries().Length }.ToString();

                               // wait for more
                               while (!(i < p.getEntries().Length))
                                   await Native.window.requestAnimationFrameAsync;


                               var t = p.getEntries()[i];

                               // http://www.w3.org/TR/resource-timing/#performanceresourcetiming



                               // Error	3	The call is ambiguous between the following methods or properties: 
                               // 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonExtensions.WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Action<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton>)' and 
                               // 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonAsyncExtensions.WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Func<System.Threading.Tasks.Task>)'	X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs	108	33	SVGNavigationTiming


                               #region PerformanceResourceTimingElement
                               var e = default(PerformanceResourceTimingElement);

                               if (page != null)
                               {

                                   e = new PerformanceResourceTimingElement
                                  {
                                      text = new IHTMLCode { new { t.name, t.entryType, t.duration } },

                                      StartTime = new { t.startTime }.ToString(),

                                      connectEnd = new { t.connectEnd }.ToString(),
                                      connectStart = new { t.connectStart }.ToString(),
                                      TCP = "TCP " + (long)(t.connectEnd - t.connectStart),

                                      RequestStart = new { t.requestStart }.ToString(),
                                      ResponseStart = new { t.responseStart }.ToString(),
                                      ResponseEnd = new { t.responseEnd }.ToString(),

                                      Request = "Request " + (long)(t.responseStart - t.requestStart),
                                      Response = "Response " + (long)(t.responseEnd - t.responseStart)

                                  };


                                   // are we supposed to show it?
                                   e.AttachToDocument();
                               }
                               #endregion



                               if (t.name.Contains("AtApplicationResourcePerformance"))
                               {
                                   if (page != null)
                                       e.text.style.color = "gray";
                               }
                               else
                               {
                                   //e.text.style.color = "blue";

                                   // cookie conflict?
                                   await Task.Delay(200);

                                   //e.text.style.color = "black";

                                   // how not to report on report?
                                   await service.AtApplicationResourcePerformance(
                                     new PerformanceResourceTimingData2ApplicationResourcePerformanceRow
                                     {
                                         // a signed key, can we check it?
                                         ApplicationPerformance = CurrentApplicationPerformance,

                                         Timestamp = DateTime.Now,

                                         startTime = (long)t.startTime,

                                         duration = (long)t.duration,

                                         entryType = t.entryType,
                                         name = t.name,

                                         connectStart = (long)t.connectStart,
                                         connectEnd = (long)t.connectEnd,
                                         requestStart = (long)t.requestStart,
                                         responseStart = (long)t.responseStart,
                                         responseEnd = (long)t.responseEnd,
                                     }
                                  );
                               }



                           }


                       }
                   );
               };
        }
    }
}