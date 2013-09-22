using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Lambda;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AsyncNestedTaskDelay;
using AsyncNestedTaskDelay.Design;
using AsyncNestedTaskDelay.HTML.Pages;
using System.Threading.Tasks;

namespace AsyncNestedTaskDelay
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

            Func<Task> go = async delegate
            {
                new IHTMLPre { innerText = "enter" }.AttachToDocument();

                Func<Task> f = async delegate
                {
                    new IHTMLPre { innerText = "f" }.AttachToDocument();

                    //await Task.Delay(1000);
                    await Int32AsyncExtensions.Delay(1000);

                    //script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x002a] beq        +0 -2{[0x0023] ldfld      +1 -1{[0x0022] ldarg.0    +1 -0} } {[0x0028] ldc.i4.s   +1 -0} , Location =
                    // assembly: V:\AsyncNestedTaskDelay.Application.exe
                    // type: ScriptCoreLib.Lambda.Int32AsyncExtensions+<Delay>d__0+<>MoveNext, AsyncNestedTaskDelay.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                    // offset: 0x002a
                    //  method:Int32 <0000> ldc.i4.1.try(<>MoveNext, <Delay>d__0 ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef, System.Runtime.CompilerServices.TaskAwaiter ByRef) }

                };
                new IHTMLPre { innerText = "before f" }.AttachToDocument();
                await f();
                new IHTMLPre { innerText = "after f" }.AttachToDocument();

                //await Task.Delay(300);
                await 300;

                //await Task.Delay(300);

                new IHTMLPre { innerText = "exit" }.AttachToDocument();

                Func<Task<string>> a = async delegate
                {
                    new IHTMLPre { innerText = "get a" }.AttachToDocument();

                    await Task.Delay(100);

                    return "a";
                };

                Func<Task<string>> b = async delegate
                {
                    new IHTMLPre { innerText = "get b" }.AttachToDocument();

                    await 120;

                    return "b";
                };

                new IHTMLPre { innerText = "complex" }.AttachToDocument();

                //             18e4:01:01 RewriteToAssembly error: System.NotSupportedException: multiple stack entries instead of one
                //at jsc.ILFlowStackItem.get_SingleStackInstruction() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILFlow.cs:line 131
                //at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass93.<>c__DisplayClassa3.<WriteSwitchRewrite>b__4c(ILGenerator flow_il) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs:line 1135

                //new IHTMLPre { innerText = "complex: " + await a() + " and " + await b() }.AttachToDocument();

                var _a = await a();
                var _b = await b();
                var x = new { a = _a, b = _b };

                new IHTMLPre { innerText = "complex: " + x.a + " and " + x.b }.AttachToDocument();

            };


            go().GetAwaiter().OnCompleted(
                delegate
                {
                    new IHTMLPre { innerText = "done" }.AttachToDocument();
                }
            );

        }

    }
}
