using AppZoomAnimation.HTML.Pages;
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
using ZoomAnimation;

namespace ZoomAnimation
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
            // http://stackoverflow.com/questions/10464038/imitate-browser-zoom-with-javascript
            // http://stackoverflow.com/questions/1055336/changing-the-browser-zoom-level

            // http://www.css3.com/css-zoom/
            // http://stackoverflow.com/questions/6521524/fix-chrome-zoom-issues

            new IHTMLButton { "80%" }.AttachToDocument().WhenClicked(
                button =>
                {
                    // no this is not it
                    (Native.document.body.style as dynamic).zoom = 0.8;

                }
            );
        }

    }
}
