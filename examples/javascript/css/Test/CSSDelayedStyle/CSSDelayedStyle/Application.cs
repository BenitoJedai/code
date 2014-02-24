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
using CSSDelayedStyle;
using CSSDelayedStyle.Design;
using CSSDelayedStyle.HTML.Pages;

namespace CSSDelayedStyle
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
            Native.document.body.css.style.transition = "background-color 300ms linear";


            //var css = Native.document.body.css[Task.Delay(1000)].complete;
            var css = (Native.document.body.css + 500);

            css.style.backgroundColor = "yellow";

            Native.document.body.css.emptyText = css.rule.selectorText;

            (Native.document.body.css + 1700).style.backgroundColor = "cyan";

            (!(Native.document.body.css + Native.document.body.async.onclick)).after.contentImage = new IHTMLDiv { 
                "this div has been converted to svg, and set as a :after content image to tell you that once you click this, css will make it go away" 
            };

            (Native.document.body.css + Native.document.body.async.onclick).style.backgroundColor = "blue";

            //(Native.document.body.css + Native.document.body.async.onclick).complete.style.backgroundColor = "blue";

        }

    }
}
