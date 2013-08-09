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
using OpenSearchExperiment.Design;
using OpenSearchExperiment.HTML.Pages;

namespace OpenSearchExperiment
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
        public Application(IApp page)
        {
            // https://code.google.com/p/chromium/issues/detail?id=245988
            // http://productforums.google.com/forum/#!topic/chrome/Po1hHSjkaZA
            // http://maisonbisson.com/post/11197/all-about-opensearch-and-autodiscover-from-davey-p/
            // ?s=goo
            var search = Native.document.location["s"];

            page.s.value = search;

            new { search }.ToString().ToDocumentTitle();
            // Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);

            page.AddSearchProvider.onclick +=
                delegate
                {
                    // window.external.AddSearchProvider('http://www.dailymotion.com/opensearch.xml');
                    dynamic window = Native.window;
                    dynamic external = window.external;

                    // Uncaught Error: NotImplementedException: __CallSite.Create 

                    //external.AddSearchProvider("http://www.dailymotion.com/opensearch.xml");

                    var href = page.searchlink.href;

                    Console.WriteLine(new { href });

                    // Firefox could not install the search plugin from "http://192.168.1.100:24950/opensearchdescription" because an engine with the same name already exists.

                    new IFunction("href", "window.external.AddSearchProvider(href)").apply(null, href);

                };
        }

    }
}
