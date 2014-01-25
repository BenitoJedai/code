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
using CSSSpecificDescendant;
using CSSSpecificDescendant.Design;
using CSSSpecificDescendant.HTML.Pages;

namespace CSSSpecificDescendant
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
            // X:\jsc.svn\examples\javascript\CSS\CSSOrOperatorNestedStyle\CSSOrOperatorNestedStyle\Application.cs


            page.foo.css[page.fragment].style.backgroundColor = "red";
            page.foo.css[page.fragment].first.style.backgroundColor = "yellow";
            page.foo.css[page.fragment].after.content = "*";


            //page.foo.css[page.fragment].first.letter.style.textTransform = IStyle.TextTransformEnum.uppercase;

            // 18ms css.style { selectorText = button[style-id="0"] ~ span > :nth-child(1) }
            // 18ms css.style { selectorText = button[style-id="0"] ~ span:nth-of-type(1) } 


            // this wont work?
            //page.foo.css[page.sibling].first.letter.style.color = "blue";

            page.foo.css[page.sibling].style.backgroundColor = "red";
            page.foo.css[page.sibling].first.style.backgroundColor = "yellow";
            page.foo.css[page.sibling].after.content = "*";


            //page.foo.css.siblings[IHTMLElement.HTMLElementEnum.span][0].style.color = "red";


            // Could not load type 'ScriptCoreLib.JavaScript.DOM.PopStateEvent' from assembly 'ScriptCoreLib, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null'

            page.foo.With(
                async button =>
                {
                    await button.async.onclick;

                    // replace that inner element, and observe
                    // css still being applicable
                    page.fragment = "hello!";
                    page.sibling = "hello!";
                    //page.sibling += "next span?";

                    var m =
                        page.foo.css[page.fragment]
                        | page.foo.css[page.sibling];



                    m.style.backgroundColor = "cyan";

                    await button.async.onclick;

                    page.foo.css[page.fragment, page.sibling].after.content = "***";

                }
            );

        }

    }
}
