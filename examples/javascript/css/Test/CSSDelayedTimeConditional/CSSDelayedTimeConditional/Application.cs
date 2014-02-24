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
using CSSDelayedTimeConditional;
using CSSDelayedTimeConditional.Design;
using CSSDelayedTimeConditional.HTML.Pages;

namespace CSSDelayedTimeConditional
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

            (Native.document.body.css < 300).style.backgroundColor = "cyan";

            (Native.document.body.css > 1800).style.backgroundColor = "yellow";



            //(Native.document.body.css + Native.document.documentElement.async.onclick).complete.style.color = "blue";


            //var css = ((Native.document.body.css + Native.document.documentElement.async.onclick).incomplete + 1000).complete;
            //var css = (Native.document.body.css + Native.document.documentElement.async.onclick).incomplete;

            //// Uncaught TypeError: Cannot call method 'UgcABp40VzyuSEPbO80fJQ' of null 
            //css.style.color = "red";

            Native.document.body.css.before.contentText = "click me";

            var css = (!(Native.document.body.css + Native.document.documentElement.async.onclick) > 300);

            //var css = (!(Native.document.body.css + 1800 + Native.document.documentElement.async.onclick));


            //(Native.document.body.css + Native.document.documentElement.async.onclick > 1800).style.color = "red";

            css.style.color = "red";

            Native.document.body.css.before.contentText = "click me " + new { css.rule.selectorText };

            var onclick = Native.document.documentElement.async.onclick;

            (Native.document.body.css + onclick).style.color = "blue";
            (Native.document.body.css + onclick > 500).style.color = "cyan";

            onclick.With(
                async o =>
                {
                    await o;

                    Native.document.body.css.style.borderLeft = "1em solid red";

                }
            );


            onclick.ContinueWith(
                task =>
                {
                    Native.document.body.css.before.contentText = "click done!";
                }
            );


        }

    }
}
