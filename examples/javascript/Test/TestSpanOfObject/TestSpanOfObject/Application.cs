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
using TestSpanOfObject;
using TestSpanOfObject.Design;
using TestSpanOfObject.HTML.Pages;

namespace TestSpanOfObject
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
            //Error CS0029  Cannot implicitly convert type '<anonymous type: int foo>' to 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLSpan' TestSpanOfObject Application.cs  33

            //perhaps in C# 7?

            page.foo = new { foo = 1 };

        }

    }
}
