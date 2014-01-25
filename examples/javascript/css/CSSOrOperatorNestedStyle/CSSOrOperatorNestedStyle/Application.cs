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
using CSSOrOperatorNestedStyle;
using CSSOrOperatorNestedStyle.Design;
using CSSOrOperatorNestedStyle.HTML.Pages;

namespace CSSOrOperatorNestedStyle
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
            //24ms css regroup view-source:34764

            // view-source:34764
            //25ms css.style { selectorText = button[style-id="0"] > span:nth-of-type(1)::after, button[style-id="0"] ~ span:nth-of-type(1)::after } 

            page.foo.css[page.fragment, page.sibling].after.content = "***";

            //  f.OgEABmASXzSNfpl_bHJLUOA(g).HwEABmASXzSNfpl_bHJLUOA().EQEABmASXzSNfpl_bHJLUOA().color = 'red';
            page.foo.css.hover[page.fragment, page.sibling].after.style.color = "red";


        }

    }
}
