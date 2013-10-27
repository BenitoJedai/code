using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestCSSPrint;
using TestCSSPrint.Design;
using TestCSSPrint.HTML.Pages;

namespace TestCSSPrint
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {




            //   IStyleSheetRule.AddRule error { text = @media print{/**/} }
            // at http://192.168.43.7:9711/view-source:28086
            //creating a disabled style rule as android webview does not know any better?
            // at http://192.168.43.7:9711/view-source:28086

            page.body.css.style.backgroundColor = "yellow";

          
            page.body.css.print.style.backgroundColor = "cyan";
        }

    }
}
