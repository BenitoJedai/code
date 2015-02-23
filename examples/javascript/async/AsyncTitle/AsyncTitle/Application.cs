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
using AsyncTitle;
using AsyncTitle.Design;
using AsyncTitle.HTML.Pages;

namespace AsyncTitle
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
    

            new IHTMLButton { "get new title " }.AttachToDocument().WhenClicked(
                async button =>
                {
                    // Uncaught URIError: URI malformed
                    var Title = await service.Title;

                    Title.ToDocumentTitle();

                }
            );
            new IHTMLButton { "get other title " }.AttachToDocument().WhenClicked(
                async button =>
                {
                    (await service.OtherTitle).ToDocumentTitle();


                    //Title.ToDocumentTitle();

                }
            );

        }

    }
}
