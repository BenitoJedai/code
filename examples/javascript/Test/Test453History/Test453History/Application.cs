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
using Test453History;
using Test453History.Design;
using Test453History.HTML.Pages;

namespace Test453History
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150110/history

            //02000556 ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier+<>c__DisplayClass17
            //{ SourceMethod = Void <op_GreaterThan>b__18(System.Threading.Tasks.Task) }
            //script: error JSC1000: Method: <op_GreaterThan>b__18, Type: ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier+<>c__DisplayClass17; emmiting failed : System.ArgumentNullException: Value cannot be null.
            //   at jsc.ILFlowStackItem.InlineLogic(Prestatement p) in x:\jsc.internal.git\compiler\jsc\CodeModel\ILFlow.cs:line 68
            //   at jsc.IL2ScriptGenerator.OpCodeHandlerArgument(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem s) in x:\jsc.internal.git\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 212

            var u = "201501";

            new IHTMLButton { "next" }.AttachToDocument().onclick +=
                 delegate
                 {
                     Native.window.history.pushState("data", "title", "/\{u}");

                 };
        }

    }
}
