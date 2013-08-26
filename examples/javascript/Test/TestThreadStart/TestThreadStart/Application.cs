using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using TestThreadStart;
using TestThreadStart.Design;
using TestThreadStart.HTML.Pages;

namespace TestThreadStart
{
    static class yield_locals
    {
        public static string loc0;
    }


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // http://msdn.microsoft.com/en-us/library/dd642243.aspx
        //public static ThreadLocal<string> data1 = new ThreadLocal<string>();


        public readonly ApplicationWebService service = new ApplicationWebService();


        static object __string
        {
            get
            {
                return (new IFunction("return __string;").apply(Native.window));
            }
        }
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://qscribble.blogspot.com/2012/07/asyncawait-vs-stack-switching.html
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130825

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Delegate.cs

            yield_locals.loc0 = "foo";

            Action yield = delegate
            {
                Console.WriteLine("more work " + new { yield_locals.loc0, Thread.CurrentThread.ManagedThreadId });


                // this function can run in both DOM and WebWorker.

                // if we were to talk to DOM, would we need to detect and autoswitch the context?
                //page.Content.innerText = "goo";
            };



            // { Target = [object Window], Method = { MethodToken = AQAABrpGJTe973zWF5WUSg } } 
            Console.WriteLine(
                new { yield.Target, yield.Method }
            );

            //yield.Method.MetadataToken

            var Method = yield.Method;
            var MethodToken = (string)(object)((__MethodInfo)(object)Method).MethodToken;
            Console.WriteLine(
                new { MethodToken }
            );
            // { MethodToken = AQAABrpGJTe973zWF5WUSg } 

            // should be static and parameterless call
            // more work { loc0 = foo }
            Method.Invoke(null, new object[0]);



            var counter = 0;

            new IHTMLButton { innerText = "spawn new thread" }.AttachToDocument().WhenClicked(
                delegate
                {
                    counter++;

                    yield_locals.loc0 = "loc0 #" + new { counter };


                    var w = new Worker(
                         worker =>
                         {
                             Console.WriteLine("will wait for scope ...");

                             worker.onmessage +=
                                 e =>
                                 {
                                     // { data = ldstr:AQAABrpGJTe973zWF5WUSg } 

                                     Console.WriteLine(new { e.data });

                                     dynamic data = e.data;
                                     //var data___string = (object)data.__string;

                                     //var datax = Expando.Of(data___string);

                                     //dynamic target = __string;

                                     //datax.GetMembers().WithEach(
                                     //    nn =>
                                     //    {
                                     //        Console.WriteLine("worker reads " + nn.Name);

                                     //        target[nn.Name] = nn.Value;
                                     //    }
                                     //);

                                     string message = data.message;


                                     var scope_MethodToken = message.SkipUntilOrEmpty("ldstr:");

                                     if (!string.IsNullOrEmpty(scope_MethodToken))
                                     {
                                         Console.WriteLine(new { scope_MethodToken });

                                         //yield_locals.loc0 = "bar";

                                         // more work { loc0 = bar }
                                         var scope_yield_add = (MulticastDelegate)IFunction.Of(scope_MethodToken);

                                         // Uncaught TypeError: Object #<lxyFdjnUJTaDStMnfRqY_aQ> has no method 'bBoABiRtYD2yr4CzwPIbLw' 
                                         // will this fail? jsc might need to do a reconstructive cast here
                                         Action scope_yield = delegate { scope_yield_add.Method.Invoke(null, new object[0]); };

                                         // Uncaught TypeError: Cannot read property 'undefined' of null 
                                         //scope_yield += (Action)scope_yield_add;


                                         //scope_yield_add.Method.Invoke(null, new object[0]);
                                         scope_yield();
                                     }


                                     //f.apply(null);




                                     // running in worker context. cannot talk to outer scope yet.

                                     // fist thing to support should be to allow incoming local strings

                                     Console.WriteLine("working ...");
                                     var s = new Stopwatch();
                                     s.Start();

                                     // spin the cpu 
                                     // how long do we have to, in order for task manager to notice?
                                     // this should keep one cpu utilized atleast at 70%
                                     for (int i = 0; i < 5; i++)
                                     {


                                         var xs = new Stopwatch();
                                         xs.Start();
                                         while (xs.ElapsedMilliseconds < 1000) ;

                                         Console.WriteLine(".");

                                     }

                                     Console.WriteLine("working ... done " + new { s.Elapsed });


                                     // how can we yield back to DOM?



                                     // SynchronizationContext.Current.


                                     // special magic to break out of webworker context?

                                     // awaitable?
                                     worker.yield(
                                         delegate
                                         {
                                             // which context are we now in?
                                             // DOM? nested worker?

                                         }
                                     );


                                 };



                         }
                     );


                    // staticfields.string.data1
                    // can we send a copy of all static fields?
                    // send scope/context data
                    // we need domain memory kind of thing
                    // memory that can be swapped

                    dynamic xdata = new object();

                    xdata.message = "ldstr:" + MethodToken;

                    //dynamic xdata___string = new object();

                    //Expando.Of(__string).GetMembers().With(
                    //      xx =>
                    //      {
                    //          xx.WithEach(
                    //              nn =>
                    //              {
                    //                  if (nn.Value != null)
                    //                  {
                    //                      Console.WriteLine("will copy " + nn.Name);

                    //                      xdata___string[nn.Name] = nn.Value;
                    //                  }
                    //              }
                    //          );
                    //      }
                    // );

                    ////xdata___string.fake = "fake";

                    //xdata.__string = xdata___string;

                    w.postMessage((object)xdata);



                }
            );





            Expando.Of(__string).GetMembers().With(
                xx =>
                {
                    Console.WriteLine(new { xx.Length });

                    var bytes = 0;

                    xx.WithEach(
                        nn =>
                        {
                            if (nn.Value != null)
                            {
                                bytes += nn.Value.Length;
                                Console.WriteLine(nn.Name + " = " + nn.Value);
                            }
                            else
                            {
                                Console.WriteLine(nn.Name + " = null");
                            }
                        }
                    );

                    Console.WriteLine(new { bytes });
                }
            );
        }

    }

    public static class X
    {
        public static void SwitchToCaller(this WorkerGlobalScope w, Action yield)
        {
            // are we calling from web worker?

        }

        public static void yield(this WorkerGlobalScope w, Action yield)
        {
            // are we calling from web worker?

        }
    }
}
