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
using QRCodeTemplate.HTML.Pages;
using QRCodeTemplate.Design;
using QRCodeTemplate.Library;

namespace QRCodeTemplate
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new __qr().Content.With(
                source =>
                {
                    source.onload +=
                        delegate
                        {
                            InitializeContent(page);
                        };

                }
            ).AttachToDocument();

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page = null)
        {
            // see also: http://neocotic.com/qr.js/

            Action<string> Add =
                e =>
                {
                    var p = new IHTMLDiv().AttachToDocument();

                    p.style.margin = "4em";

                    new IHTMLAnchor
                    {
                        href = e,
                        innerText = e
                    }.AttachTo(p);

                    new IHTMLBreak().AttachTo(p);

                    e.ToQRCode().AttachTo(p);
                };


            Add("" + Native.Document.location);
            Add("http://www.jsc-solutions.net");


        }
    }
}
