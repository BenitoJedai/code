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
using TestEmptyForLoop;
using TestEmptyForLoop.Design;
using TestEmptyForLoop.HTML.Pages;

namespace TestEmptyForLoop
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

            //            02000002 TestEmptyForLoop.Application
            //            script: error JSC1000: unknown while condition at Void.ctor(TestEmptyForLoop.HTML.Pages.IApp).Maybe you did not turn off c# compiler 'optimize code' feature?
            //script: error JSC1000: Method: .ctor, Type: TestEmptyForLoop.Application; emmiting failed : System.InvalidOperationException: unknown while condition at Void.ctor(Te

            //for (; ;)
            while (true)
            {


                new IHTMLPre { "enter for" }.AttachToDocument();

                if (page != null)
                    break;

            }

        }
    }
}