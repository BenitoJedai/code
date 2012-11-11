using TestAssetAsComponent.Design;
using TestAssetAsComponent.HTML.Pages;
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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestAssetAsComponent
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var controls = new[] { 
                content.vjLogoComponent4,
                content.vjLogoComponent5
            };
            foreach (Component item in controls)
            {
                (item as ImageLinkComponent).With(
                    c =>
                    {
                        var a = new IHTMLAnchor { href = c.href }.AttachToDocument();

                        c.img.AttachTo(a);

                        //a.style.SetLocation(c.Left, c.Top);
                    }
                );
            }

            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
