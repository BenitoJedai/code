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
using TestDynamicCall;
using TestDynamicCall.Design;
using TestDynamicCall.HTML.Pages;

namespace TestDynamicCall
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
            //Native.window.confirm(""
            dynamic w = Native.window;

            // 0:59ms { Name = alert, ReturnType = , IsReturnVoid = true, Count = 2 }
            // 0:42ms { target = [object Window], Name = alert, arg1 = hello world, arg2 = goo }
            //w.alert("hello world");
            bool goo = w.confirm("hello world");

            new IHTMLPre { new { goo } }.AttachToDocument();


            //0:28ms { binder = InvokeMemberBinder }
            //view - source:27206 Uncaught Error: NotImplementedException:
            //__CallSite.Create

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
        }

    }
}
