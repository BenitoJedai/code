using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using YieldFromTaskResultExperiment;
using YieldFromTaskResultExperiment.Design;
using YieldFromTaskResultExperiment.HTML.Pages;

namespace YieldFromTaskResultExperiment
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
            // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.TaskFactory.ContinueWhenAll(System.Threading.Tasks.Task`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]][], System.Action`1[[System.Threading.Tasks.Task`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]][], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]


            Console.WriteLine("starting...");

            var xtasks = new Random().Next(2, 4).GetTasks().ToArray();

            Console.WriteLine("got tasks...");

            Task.Factory.ContinueWhenAll(
                xtasks,
                tasks =>
                {
                    tasks.GetOutput().AttachToDocument();

                    return "ok";
                }
                ,
                cancellationToken: default(CancellationToken),
                continuationOptions: default(TaskContinuationOptions),

                // GUI
                scheduler: TaskScheduler.FromCurrentSynchronizationContext()
            );
        }


    }

    static class X
    {
        //        script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0008]
        //        bne.un.s + 0 - 2{[0x0001]
        //        ldfld      +1 -1{[0x0000]
        //        ldarg.0    +1 -0}
        //} {[0x0006]
        //ldc.i4.s   +1 -0} , Location =

        // assembly: V:\YieldFromTaskResultExperiment.Application.exe
        // type: YieldFromTaskResultExperiment.X+<GetOutput>d__0, YieldFromTaskResultExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x0008
        //  method:System.Collections.Generic.IEnumerator`1[YieldFromTaskResultExperiment.HTML.Pages.YieldElement]
        //System.Collections.Generic.IEnumerable<YieldFromTaskResultExperiment.HTML.Pages.YieldElement>.G
        //etEnumerator() }

        public static IEnumerable<YieldElement> GetOutput(this IEnumerable<Task<string>> source)
        {
            //            Closure: YieldFromTaskResultExperiment.X+<GetOutput>d__1+<MoveNext>
            //RewriteToAssembly error: System.NotImplementedException: The finally clause is not yet implemented! Try to refactor!

            var a = source.ToArray();

            for (int i = 0; i < a.Length; i++)
            {
                var y = new YieldElement
                {
                    output = a[i].Result,
                    ThreadId = new { Thread.CurrentThread.ManagedThreadId }.ToString()
                };

                yield return y;
            }

        }


        // length will confuse ScriptCoreLib and makes it think its an array
        public static IEnumerable<Task<string>> GetTasks(this int xlength)
        {
            Console.WriteLine("enter GetTasks");

            //var function = new { hint = default(string) }.Create(
            //    state =>
            //    {
            //        var z = new { state.hint, Thread.CurrentThread.ManagedThreadId }.ToString();

            //        Console.WriteLine(z);

            //        return z;
            //    }
            //);


            yield return Task.Factory.StartNew(
                new { hint = "first" },
                state =>
                {
                    var z = new { state.hint, Thread.CurrentThread.ManagedThreadId }.ToString();

                    Console.WriteLine(z);

                    return z;
                }
            );

            //yield return y;

            for (int i = 0; i < xlength; i++)
                yield return Task.Factory.StartNew(
                    new { hint = "work " + new { i } },
                       state =>
                       {
                           var z = new { state.hint, Thread.CurrentThread.ManagedThreadId }.ToString();

                           Console.WriteLine(z);

                           return z;
                       }
                );



            yield return Task.Factory.StartNew(
              new { hint = "last" },
                 state =>
                 {
                     var z = new { state.hint, Thread.CurrentThread.ManagedThreadId }.ToString();

                     Console.WriteLine(z);

                     return z;
                 }
           );

            Console.WriteLine("exit GetTasks");
        }
    }

    public static class XX
    {
        public static Func<T, TResult> Create<T, TResult>(this T state, Func<T, TResult> yield)
        {
            return yield;
        }
    }
}
