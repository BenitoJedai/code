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
using TestByRefField;
using TestByRefField.Design;
using TestByRefField.HTML.Pages;

namespace TestByRefField
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\rewrite\test\TestSwitchRewritePassAsByPref\TestSwitchRewritePassAsByPref\Program.cs

        // if we re using ref[] = new [] { arg0 }
        // then we should also add the index lateron for ldflda opcode?
        // it would mean ref = new { index = 0, data = new [] { arg0 } }
        // it would mean ref = new { index = 'foo', data = new { foo: arg0 } }
        // how would it work for as and jvm?
        // what would it do to performance?

        public void InternalMoveNext(ref string e)
        {
            e = "new value";
        }

        string Field1;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //         Unhandled Exception: System.InvalidOperationException: Method: .ctor, Type: TestByRefField.Application; emmiting failed : System.NotImplementedException: { ParameterType = System.String &, p = [0x000f] call + 0 - 2{[0x0008]
            //     ldarg.0    +1 -0} {[0x000a]
            // ldflda     +1 -1{[0x0009]
            // ldarg.0    +1 -0} } , m = Void InternalMoveNext(System.String ByRef) }
            //at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\IdentWriter.cs:line 833
            //at jsc.IL2ScriptGenerator.OpCode_call_override(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 379
            //at jsc.IL2ScriptGenerator.<CreateInstructionHandlers>b__f(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.OpCodes.cs:line 701

            // how would this work for js?
            InternalMoveNext(ref this.Field1);

        }

    }
}
