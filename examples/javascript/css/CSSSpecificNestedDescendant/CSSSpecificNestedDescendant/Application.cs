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
using CSSSpecificNestedDescendant;
using CSSSpecificNestedDescendant.Design;
using CSSSpecificNestedDescendant.HTML.Pages;

namespace CSSSpecificNestedDescendant
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
            page.foo.css[page.fragmentx, page.siblingx].style.backgroundColor = "red";

            // 28ms css.style { selectorText = button[style-id="0"] ~ span:nth-of-type(1) } 
            page.foo.css[page.nop].style.backgroundColor = "red";

            // Uncaught TypeError: Cannot call method 'EQEABmASXzSNfpl_bHJLUOA' of undefined 
            //page.foo.css[page.nop2].style.backgroundColor = "red";


            //page.foo.css[page.fragment, page.sibling].after.content = "***";

        }

    }
}
