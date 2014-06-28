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
using TestDelayInsideWorker;
using TestDelayInsideWorker.Design;
using TestDelayInsideWorker.HTML.Pages;
using System.IO;
using System.Threading;

namespace TestDelayInsideWorker
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
            // X:\jsc.svn\examples\javascript\async\AsyncNestedTaskDelay\AsyncNestedTaskDelay\Application.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140628

            // should we autobind it to an element of output?
            // we should color automated messages in a different color than the ones from Application
            Console.SetOut(new xConsole());

            // Starting with the .NET Framework 4.5, you can use the Run method with an Action object as a quick way to call StartNew with default parameters. 
            // For more information and code examples, see Task.Run vs Task.Factory.StartNew in the Parallel Programming with .NET blog.
            // http://blogs.msdn.com/b/pfxteam/archive/2011/10/24/10229468.aspx

            new IHTMLButton { "Task Run await" }.AttachToDocument().onclick +=
                async e =>
                {
                    // namespace System.Threading.Tasks
                    // public static Task<TResult> Run<TResult>(Func<Task<TResult>> function);
                    // vscript: error JSC1000: No implementation found for this native method, please implement [static System.Threading.Tasks.Task.Run(System.Func`1[[System.Threading.Tasks.Task`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]
                    // there does not seem to be a way to send in the state?
                    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.cs
                    // If you really wanted the Task<int>, you could use Task.Factory.StartNew, which doesn't do the automatic unwrapping. Or you could force the compiler not to see the result as a task, e.g. instead of doing:
                    // http://en.wikipedia.org/wiki/DWIM

                    var t = Task.Run(async () =>
                    {
                        Console.WriteLine("task: " + new { Thread.CurrentThread.ManagedThreadId });

                        await Task.Delay(1000);
                        return 42;
                    });

                    int result = await t;

                    Console.WriteLine(new { result });
                };


            new IHTMLButton { "go" }.AttachToDocument().onclick +=
            async e =>
            {

                // where is it defined?
                // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\TaskExtensions.cs
                var z = await Task.Factory.StartNew(
                     new { data = "whats the hash for this?" },
                        //async  
                        scope =>
                        {
                            // will this get signaled to UI thread? only string are to be synchronized for now?
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("hi " + new { Thread.CurrentThread.ManagedThreadId });

                            //await Task.Delay(2000);

                            return new { result = "ok" };
                        }
                    );

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UI " + new { z.result, Thread.CurrentThread.ManagedThreadId });
            };


        }
    }


    #region xConsole
    //class xConsole : StringWriter
    [Obsolete("jsc:js does not allow to overrider an override? we need it for SpecialFieldInfo to work!")]
    class xConsole : TextWriter
    {
        // http://www.danielmiessler.com/study/encoding_encryption_hashing/
        [Obsolete("can we have encrypted encoding?")]
        public override Encoding Encoding
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Write(string value)
        {
            var p = new IHTMLCode { innerText = value }.AttachToDocument();
            var s = p.style;

            // jsc, enum tostring?
            if (Console.ForegroundColor == ConsoleColor.Red)
                s.color = "red";

            if (Console.ForegroundColor == ConsoleColor.Blue)
                s.color = "blue";

            if (Console.ForegroundColor == ConsoleColor.Gray)
                s.color = "gray";

            if (Console.ForegroundColor == ConsoleColor.Yellow)
                s.color = "yellow";

            if (Console.ForegroundColor == ConsoleColor.Magenta)
                s.color = "magneta";
        }

        public override void WriteLine(string value)
        {
            //Console.WriteLine(new { value });


            Write(value);

            new IHTMLBreak { }.AttachToDocument();
        }
    }
    #endregion
}
