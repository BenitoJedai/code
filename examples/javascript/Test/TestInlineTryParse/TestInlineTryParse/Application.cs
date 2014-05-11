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
using TestInlineTryParse;
using TestInlineTryParse.Design;
using TestInlineTryParse.HTML.Pages;

namespace TestInlineTryParse
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

            //            02000381 ScriptCoreLib.JavaScript.BCLImplementation.System.__Double
            //script: error JSC1000: unknown opcode stind.r8 at TryParse + 0x0021
            //script: error JSC1000: Method: TryParse, Type: ScriptCoreLib.JavaScript.BCLImplementation.System.__Double; emmiting failed : System.InvalidOperationException: unknown opcode stind.r8 at TryParse + 0x0021

            // script: error JSC1000: No implementation found for this native method, please implement [static System.Double.TryParse(System.String, System.Double&)]
            double.TryParse("1.3", out var goo);
            //int.TryParse("13", out var goo);

            new IHTMLPre { goo }.AttachToDocument();

        }

    }
}
