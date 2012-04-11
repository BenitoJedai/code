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
using jsPDF.Design.Styles;
using jsPDF.HTML.Pages;
using jsPDF.Library;

namespace jsPDF
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // based on http://snapshotmedia.co.uk/blog/jspdf

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new Design.jspdf().Content.When(
                 source =>
                 {
                     source.onload +=
                       delegate
                       {
                           InitializeContent(page);
                       };

                     source.AttachToDocument();
                 }
              );


            InitializeContent(page);

            style.Content.AttachToHead();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        private static void InitializeContent(IDefaultPage page)
        {
            page.twopage.onclick +=
                delegate
                {
                    // the api was neither auto generated
                    // nor was it auto loaded on demand
                    // maybe in the future :)

                    var doc = new Design.jsPDF();

                    doc.text(20, 20, "Hello world!");
                    doc.text(20, 30, "This is client-side Javascript, pumping out a PDF.");
                    doc.addPage();
                    doc.text(20, 20, "Do you like that?");

                    // Output as Data URI
                    doc.output("datauri");
                };
        }

    }
}
