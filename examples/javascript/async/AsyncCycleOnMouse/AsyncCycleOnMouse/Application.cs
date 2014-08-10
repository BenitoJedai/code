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
using AsyncCycleOnMouse;
using AsyncCycleOnMouse.Design;
using AsyncCycleOnMouse.HTML.Pages;

namespace AsyncCycleOnMouse
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
            //page.Header.css.hover.adjacentSibling.style.borderTop = "1px solid red";


            page.Header.css.hover.siblings.style.borderRight = "2px solid red";

            //page.Header.css.hover.siblings["*"].nthChild[3].style.borderRight = "3px solid red";

            //page.Header.parentNode.css.first.child.style.borderLeft = "1px solid blue";

            //(page.Header.parentNode.firstChild as IHTMLElement).css.siblings["h1"].style.borderRight = "1px solid blue";




            page.Header.css.style.transition = "color 300ms linear";
            page.Header.css.adjacentSibling.style.transition = "border-left 300ms linear";

            page.With(
                async delegate
                {
                    // using transition 300ms ?
                    while (true)
                    {
                        await page.Header.async.onmouseover;


                        // would jsc be smart enough to infer transitions?

                        page.Header.css.style.color = "red";
                        page.Header.css.adjacentSibling.style.borderLeft = "4px solid red";

                        await page.Header.async.onmouseout;

                        page.Header.css.style.color = "yellow";
                        page.Header.css.adjacentSibling.style.borderLeft = "1px solid yellow";


                    }
                }
            );
        }

    }
}
