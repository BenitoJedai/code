using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestNavigationTiming.Design;
using TestNavigationTiming.HTML.Pages;
//using ScriptCoreLib.JavaScript.TimingAPI;

namespace TestNavigationTiming
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            var timing = Native.window.performance.timing;
            new IHTMLPre
            {

                innerText = new
                {
                    Native.window.performance.navigation.redirectCount,
                    Native.window.performance.navigation.type,

                    timing.connectEnd,
                    timing.connectStart,
                    timing.domainLookupEnd,
                    timing.domainLookupStart,
                    timing.domComplete,
                    timing.domContentLoadedEventEnd,
                    timing.domContentLoadedEventStart,
                    timing.domInteractive,
                    timing.domLoading,
                    timing.fetchStart,
                    timing.loadEventEnd,
                    timing.loadEventStart,
                    timing.navigationStart,
                    timing.redirectEnd,
                    timing.redirectStart,
                    timing.requestStart,
                    timing.responseEnd,
                    timing.responseStart,
                    timing.secureConnectionStart,
                    timing.unloadEventEnd,
                    timing.unloadEventStart,

                }.ToString().Replace(",", "\n")
            }.AttachToDocument();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        //[Script(ExternalTarget = "window")]
        //static IWindow window;
        //static XWindow window;
    }

    //[Script(HasNoPrototype = true)]
    //class XWindow : IWindow
    //{
    //    public Performance performance;
    //}
}
