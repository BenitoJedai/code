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
using TestByRefTaskAwaiter;
using TestByRefTaskAwaiter.Design;
using TestByRefTaskAwaiter.HTML.Pages;

namespace TestByRefTaskAwaiter
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
            var x = new IHTMLButton { "other button" }.AttachToDocument();

            //02000014 TestByRefTaskAwaiter.Application+ctor>b__2>d__8+<MoveNext>06000009
            //{ Location =
            // assembly: S:\TestByRefTaskAwaiter.Application.exe
            // type: TestByRefTaskAwaiter.Application+ctor>b__2>d__8+<MoveNext>06000009, TestByRefTaskAwaiter.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x000a
            //  method:Int32 <001d> nop.try(<MoveNext>06000009, ctor>b__2>d__8 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.Object] ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[System.Object] ByRef) }
            //script: error JSC1000: Method: <001d> nop.try, Type: TestByRefTaskAwaiter.Application+ctor>b__2>d__8+<MoveNext>06000009; emmiting failed : System.NotImplementedException: { ParameterType = TestByRefTaskAwaiter.Application+ctor>b__2>d__8&, p = [0x000f] brtrue.s   +0 -1{[0x000a] ca
            //   at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\IdentWriter.cs:line 833
            //   at jsc.IL2ScriptGenerator.OpCode_call_override(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s, MethodBase m) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.cs:line 379


            new IHTMLButton { "go" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201402/201402

                    Func<Task> GetFBUserProperties = delegate { return "".AsResult(); };

                    await GetFBUserProperties();
                }
             );
        }

    }
}
