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
using TestBase64;
using TestBase64.Design;
using TestBase64.HTML.Pages;

namespace TestBase64
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
            //            02000002 TestBase64.Application
            //script: error JSC1000: Method: .ctor, Type: TestBase64.Application; emmiting failed : System.NotImplementedException: Byte
            //   at jsc.IL2ScriptGenerator.OpCode_newarr(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 931
            //   at jsc.IL2ScriptGenerator.OpCodeHandler(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem s) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 250


            // https://sites.google.com/a/jsc-solutions.net/backlog/system/app/pages/createPage?source=/knowledge-base/2014/201401
            Console.WriteLine("findme");

            var a = new byte[] { 1, 2, 3 };

            new IHTMLPre {
                new {
                    base64 = Convert.ToBase64String(a)
                }
            }.AttachToDocument();

        }

    }
}
