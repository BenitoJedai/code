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
using CSSNotOperator;
using CSSNotOperator.Design;
using CSSNotOperator.HTML.Pages;

namespace CSSNotOperator
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
            var css = page.body.css[0].not;



            // 0:18ms { css = { selectorElement = , rule = { selectorText = body>:not(:nth-child(1)), rule = null } } } 
            Console.WriteLine(new { css });

            css.style.color = "red";

            page.body.css.children.style.transition = "border-left 300ms linear";

            (!page.body.css.children.focus).style.borderLeft = "1em solid gray";

            // http://www.w3schools.com/cssref/css_selectors.asp

            var xreadonly = new XAttribute("readonly", "readonly");

            //page.body.css.children[xreadonly].not.style.backgroundColor = "yellow";
            (!page.body.css.children[xreadonly]).style.backgroundColor = "yellow";
        }

    }
}
