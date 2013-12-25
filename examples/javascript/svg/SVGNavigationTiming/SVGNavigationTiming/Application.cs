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

            Native.window.requestAnimationFrame +=
                delegate
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
                    }

                    new IHTMLButton { "service" }.AttachToDocument().WhenClicked(this.WebMethod2).With(
                        button =>
                        {
                            button.css.style.position = IStyle.PositionEnum.@fixed;
                            button.css.style.right = "1em";
                            button.css.style.top = "1em";
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


                                new IHTMLPre { new { t.name, t.entryType, t.duration } }.AttachToDocument();

                                new PerformanceResourceTimingElement
                                {
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

                            }


                        }
                    );
                };




        }

    }
}
