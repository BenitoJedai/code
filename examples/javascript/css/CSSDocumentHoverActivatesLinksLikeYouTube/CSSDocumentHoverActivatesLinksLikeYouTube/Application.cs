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
using CSSDocumentHoverActivatesLinksLikeYouTube;
using CSSDocumentHoverActivatesLinksLikeYouTube.HTML.Pages;

namespace CSSDocumentHoverActivatesLinksLikeYouTube
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
            new IStyle(IHTMLElement.HTMLElementEnum.a)
            {
                transition = "color 300ms linear",
                color = "gray",
                textDecoration = "none"
            };

            (Native.document.documentElement.css.hover.descendant + IHTMLElement.HTMLElementEnum.a).style.color = "blue";



            new IHTMLAnchor { "drag me" }.AttachToDocument().With(
                dragme =>
                {
                    dragme.style.position = IStyle.PositionEnum.@fixed;
                    dragme.style.left = "1em";
                    dragme.style.bottom = "1em";

                    dragme.AllowToDragAsApplicationPackage();
                }
            );
        }

    }
}
