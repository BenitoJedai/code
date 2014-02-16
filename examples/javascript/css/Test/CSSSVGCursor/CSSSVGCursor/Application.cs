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
using CSSSVGCursor;
using CSSSVGCursor.Design;
using CSSSVGCursor.HTML.Pages;

namespace CSSSVGCursor
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
            //X:\jsc.svn\examples\javascript\css\Test\CSSCursorImage\CSSCursorImage\Application.cs

            new HTML.Images.FromAssets.MyCursor().AttachToDocument();

            Native.document.documentElement.style.cursorImage = new HTML.Images.FromAssets.MyCursor();


            var i = (IHTMLImage)new IHTMLDiv { "333" };

            i.AttachToDocument();

            page.Header.style.cursorImage = i;


            // wow, cursors can be bigger than 32px
            new IHTMLDiv {
                //new HTML.Images.FromAssets._3dgarro(),
                "222 111 000" }.With(
                div =>
                {
                    div.style.color = "blue";
                    div.AttachToDocument();

                    page.Content.style.cursorElement = div;
                }
            );


        }

    }
}
