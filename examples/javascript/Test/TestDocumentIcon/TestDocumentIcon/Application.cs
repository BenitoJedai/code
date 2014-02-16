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
using TestDocumentIcon;
using TestDocumentIcon.Design;
using TestDocumentIcon.HTML.Images.FromAssets;
using TestDocumentIcon.HTML.Pages;

namespace TestDocumentIcon
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
            // what about gif, svg, canvas and webgl?
            Native.document.icon = new fullbox();

            new IHTMLButton { "?" }.AttachToDocument().WhenClicked(
                 button =>
                 {
                     var div = new IHTMLDiv { "?" };

                     // 7x20
                     div.style.color = "red";
                     div.style.width = "16px";
                     div.style.height = "16px";

                     IHTMLImage i = div;

                     var c = new CanvasRenderingContext2D(16, 16);

                     c.drawImage(i, 0, 0, 16, 16);


                     Native.css.style.cursorImage = i;

                     // Uncaught SecurityError: Failed to execute 'toDataURL' on 'HTMLCanvasElement': Tainted canvases may not be exported.
                     // why wont this work?
                     Native.document.icon = c.canvas.toDataURL();


                 }
            );
        }

    }
}
