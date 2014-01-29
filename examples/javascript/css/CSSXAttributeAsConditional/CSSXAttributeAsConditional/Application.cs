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
using CSSXAttributeAsConditional;
using CSSXAttributeAsConditional.Design;
using CSSXAttributeAsConditional.HTML.Pages;

namespace CSSXAttributeAsConditional
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

            var x = new XAttribute("foo", "bar");



            //Native.document.documentElement.css["[foo]"][page.Header].style.backgroundColor = "yellow";

            // Uncaught SyntaxError: An invalid or illegal string was specified. 
            //Native.document.documentElement.css["[foo]"]["h1"].style.backgroundColor = "yellow";

            Native.document.body.css["[foo]"][" h1"].style.backgroundColor = "yellow";
            Native.document.body.css["[foo='bar']"][" h1"].style.color = "red";
            Native.document.body.css["[foo='zoo']"][" h1"].style.color = "green";


            Native.document.body.css[x][page.Content].With(
                css =>
                {
                    css.style.borderLeft = "1em solid black";

                    page.Content.innerText = new { css.rule.selectorText }.ToString();

                }
            );



            Native.document.body.css[x].descendant[IHTMLElement.HTMLElementEnum.h1].style.borderLeft = "1em solid cyan";
            Native.document.body.css[new XAttribute("foo", "zoo")].descendant[IHTMLElement.HTMLElementEnum.h1].style.borderLeft = "1em solid cyan";

            //Native.document.body.css[e => e.getAttribute("foo") == "bar"]


            //Native.document.body.css.before.style.content = "attr(foo)";
            Native.document.body.css.before.contentXAttribute = x;

            new IHTMLButton { "foo" }.AttachToDocument().WhenClicked(
                 delegate
                 {
                     //x.AttachTo(Native.document.documentElement);
                     x.AttachTo(Native.document.body);


                 }
            );

            new IHTMLButton { "change foo" }.AttachToDocument().WhenClicked(
                delegate
                {
                    x.Value = "zoo";
                }
            );
        }

    }
}
