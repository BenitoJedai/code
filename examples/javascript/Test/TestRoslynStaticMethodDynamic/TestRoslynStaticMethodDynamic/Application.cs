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
using TestRoslynStaticMethodDynamic;
using TestRoslynStaticMethodDynamic.Design;
using TestRoslynStaticMethodDynamic.HTML.Pages;

namespace TestRoslynStaticMethodDynamic
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
   

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140526/dynamic


        static void foo(dynamic foo)
        {
            foo.backgroundColor = "red";
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            foo(Native.document.body.style);

        }

    }
}
