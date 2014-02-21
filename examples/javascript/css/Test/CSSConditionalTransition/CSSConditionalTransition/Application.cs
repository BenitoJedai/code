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
using CSSConditionalTransition;
using CSSConditionalTransition.Design;
using CSSConditionalTransition.HTML.Pages;

namespace CSSConditionalTransition
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
            // { selectorText = html>*>:not(body) }
            //var css = !(Native.css.children + IHTMLElement.HTMLElementEnum.body);
            //var css = !(Native.css + IHTMLElement.HTMLElementEnum.body);
            //var css = (Native.document.documentElement.css + IHTMLElement.HTMLElementEnum.input);

            //new IHTMLPre { new { css.rule.selectorText } }.AttachToDocument();

            new IStyle(Native.document.documentElement.css + IHTMLElement.HTMLElementEnum.input).display = IStyle.DisplayEnum.none;

            // move up.

            Native.document.head.insertNextSibling(
                page.a.Orphanize()
            );

            //page.a.AttachTo(Native.document.documentElement);

            //page



            new IStyle(page.alabel)
            {
                transition = "color 300ms linear, border-left 300ms linear"
            };


            new IStyle(page.a.css.@checked[page.alabel])
            {
                color = "red",
                borderLeft = "1em solid red"
            };

        }

    }
}
