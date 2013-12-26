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

            Native.window.requestAnimationFrame +=
                async delegate
                {
                    {
                        var t = Native.window.performance.timing;


                        // SVGTSpanElement
                        page.connectEnd = new { t.connectEnd }.ToString();
                        page.connectStart = new { t.connectStart }.ToString();
                        page.TCP = "TCP " + (t.connectEnd - t.connectStart);

                        page.RequestStart = new { t.requestStart }.ToString();
                        page.ResponseStart = new { t.responseStart }.ToString();
                        page.ResponseEnd = new { t.responseEnd }.ToString();
                        page.Request = "Request " + (t.responseStart - t.requestStart);
                        page.Response = "Response " + (t.responseEnd - t.responseStart);

                        page.DomLoading = new { t.domLoading }.ToString();
                        page.DomComplete = new { t.domComplete }.ToString();
                        page.Processing = "Processing " + (t.domComplete - t.domLoading);

                        page.LoadEventEnd = new { t.loadEventEnd }.ToString();
                        page.LoadEventStart = new { t.loadEventStart }.ToString();
                        page.Load = "Load " + (t.loadEventEnd - t.loadEventStart);

                        // IXMLHttpRequestActivity.onopen { url = /xml?WebMethod=06000003&c=1 }

                        await this.AtApplicationPerformance(
                            new PerformanceResourceTimingDataApplicationPerformanceRow
                            {
                                connectStart = (long)t.connectStart,
                                connectEnd = (long)t.connectEnd,
                                requestStart = (long)t.requestStart,
                                responseStart = (long)t.responseStart,
                                responseEnd = (long)t.responseEnd,
                                domLoading = (long)t.domLoading,
                                domComplete = (long)t.domComplete,
                                loadEventStart = (long)t.loadEventStart,
                                loadEventEnd = (long)t.loadEventEnd,
                            }
                         );
                    }

                    new IHTMLButton { "WebMethod2500" }.AttachToDocument().WhenClicked(this.WebMethod2500);
                    new IHTMLButton { "WebMethod500" }.AttachToDocument().WhenClicked(this.WebMethod500);


                    new IHTMLButton { "GetApplicationPerformance" }.AttachToDocument().WhenClicked(
                        async button =>
                        {
                            var data0 = await this.GetApplicationPerformance();
                            var data = data0;

                            var g = new DataGridView
                            {
                                DataSource = data,
                                Dock = DockStyle.Fill,
                                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                                ReadOnly = true,
                                AllowUserToAddRows = false
                            };


                            var f = new Form();

                            g.AttachTo(f);


                            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.add_CellContentDoubleClick(System.Windows.Forms.DataGridViewCellEventHandler)]
                            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridViewCellMouseEventArgs.get_RowIndex()]

                            //g.CellContentClick +=
                            //g.CellContentDoubleClick +=
                            //g.CellMouseClick +=
                            g.CellClick +=
                                async (sender, args) =>
                                {
                                    var RowIndex = args.RowIndex;

                                    Console.WriteLine(
                                        "CellContentClick" + new { RowIndex }
                                        );

                                    PerformanceResourceTimingDataApplicationPerformanceRow x = data.Rows[RowIndex];

                                    f.Hide();

                                    var kdata = await this.GetApplicationResourcePerformance(x);

                                    var kg = new DataGridView
                                    {
                                        DataSource = kdata,
                                        Dock = DockStyle.Fill,
                                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                                        ReadOnly = true
                                    };


                                    var kf = new Form();

                                    kg.AttachTo(kf);


                                    await kf.ShowAsync();

                                    f.Show();

                                };

                            await f.ShowAsync();

                        }
                    );

                    Native.window.performance.With(
                        async p =>
                        {

                            for (int i = 0; ; i++)
                            {
                                Native.document.title = new { i }.ToString();

                                // wait for more
                                while (!(i < p.getEntries().Length))
                                    await Native.window.requestAnimationFrameAsync;


                                var t = p.getEntries()[i];

                                // http://www.w3.org/TR/resource-timing/#performanceresourcetiming



                                // Error	3	The call is ambiguous between the following methods or properties: 
                                // 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonExtensions.WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Action<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton>)' and 
                                // 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButtonAsyncExtensions.WhenClicked(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton, System.Func<System.Threading.Tasks.Task>)'	X:\jsc.svn\examples\javascript\svg\SVGNavigationTiming\SVGNavigationTiming\Application.cs	108	33	SVGNavigationTiming


                                var e = new PerformanceResourceTimingElement
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

                                 }.AttachToDocument();

                                if (t.name.Contains("AtApplicationResourcePerformance"))
                                {
                                    e.text.style.color = "gray";
                                }
                                else
                                {
                                    e.text.style.color = "blue";

                                    // cookie conflict?
                                    await Task.Delay(200);

                                    e.text.style.color = "black";

                                    // how not to report on report?
                                    await this.AtApplicationResourcePerformance(
                                      new PerformanceResourceTimingDataApplicationResourcePerformanceRow
                                      {
                                          startTime = (long)t.startTime,

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
