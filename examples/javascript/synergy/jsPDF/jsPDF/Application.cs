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
        // X:\jsc.svn\examples\javascript\synergy\WebServicePDFGenerator\WebServicePDFGenerator\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\synergy\jsPDF\jsPDF\Application.cs
        // based on http://snapshotmedia.co.uk/blog/jspdf

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new Design.base64().Content.WhenAvailable(
                 delegate
                 {

                     new Design.sprintf().Content.WhenAvailable(
                          delegate
                          {

                              new Design.jspdf().Content.WhenAvailable(
                                 delegate
                                 {

                                     InitializeContent(page);
                                 }
                              );
                          }
                       );
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
            // http://mrrio.github.io/jsPDF/

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


            page.fonts.onclick +=
               delegate
               {
                  var doc = new Design.jsPDF();
                    doc.setFontSize(22);
                    doc.text(20, 20, "This is a title");

                    doc.setFontSize(16);
                    doc.text(20, 30, "This is some normal sized text underneath.");	

                    // Output as Data URI
                    doc.output("datauri");
               };

            // https://github.com/MrRio/jsPDF/issues/339

            page.metadata.onclick +=
                delegate
                {
                    var doc = new Design.jsPDF();

                    // addImage: function(imageData, format, x, y, w, h) {

                    doc.text(20, 20, "This PDF has a title, subject, author, keywords and a creator.");

                    // Optional - set properties on the document
                    doc.setProperties(new jsPDFProperties {
	                    title= "Title",
	                    subject= "This is the subject",
	                    author= "James Hall",
	                    keywords= "generated, javascript, web 2.0, ajax",
	                    creator= "MEEE"
                    });

                    // Output as Data URI
                    doc.output("datauri");
                };
        }

    }

    sealed class jsPDFProperties
    {
        public string
            title,
            subject,
            author,
            keywords,
            creator;
    }
}
