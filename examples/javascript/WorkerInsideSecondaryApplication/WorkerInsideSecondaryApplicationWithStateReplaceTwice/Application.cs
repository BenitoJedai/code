using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Threading;
using WorkerInsideSecondaryApplicationWithStateReplaceTwice;
using WorkerInsideSecondaryApplicationWithStateReplaceTwice.Design;
using WorkerInsideSecondaryApplicationWithStateReplaceTwice.HTML.Pages;
using ScriptCoreLib.JavaScript.Experimental;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;

namespace WorkerInsideSecondaryApplicationWithStateReplaceTwice
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
        public Application(IDisclaimer page)
        {

            #region go
            Action<string> go =
                //async 
                     source =>
                     {
                         // show login

                         #region layout
                         var layout = new Login();

                         // loaded app will expect id's, restore em
                         layout.Login.id = "Login";

                         var newbody = layout.body;
                         var oldbody = Native.document.body;

                         // switch layouts

                         Native.document.body.parentNode.replaceChild(
                             newbody,
                             oldbody
                         );
                         #endregion


                         // how did we get the source?
                         //var app = new x(layout);

                         //await 

                         //new IHTMLScript { src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource() }.AttachToHead();

                         var src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource();
                         //await new IHTMLScript { src = src };
                         Native.window.eval(source);

                         // now what?
                     };
            #endregion

            #region onclick
            page.Continue.onclick +=
                async delegate
                {
                    page.Continue.disabled = true;

                    if (go.Target != null)
                        if (go.Target != Native.self)
                            throw new InvalidOperationException("we can only continue with global methods for now... " + new { go.Target });

                    var MethodToken = ((__MethodInfo)go.Method).MethodToken;


                    var source = await typeof(x);

                    // we can now Activate it?
                    //Activator.CreateInstance(typeof(x));


                    Native.window.history.replaceState(
                        new
                        {
                            Native.window.history.state,

                            hint = "typeof(x)",

                            // arguments:

                            invoke = new { function = MethodToken, arguments = new[] { source } }
                        }
                    );

                    // invoke as state?
                    //go(source);

                    // or reload?
                    Native.document.location.reload();
                };
            #endregion


            // first time:
            // Application onpopstate { e = { state =  }, history = { state =  } }


            // after replaceState and reload
            // Application onpopstate { e = { state =  }, history = { state = [object Object] } }

            #region onpopstate
            Native.window.onpopstate +=
                 e =>
                 {

                     #region x
                     var x = new Stack<Func<Task<object>>>();

                     Action<object> y = null;

                     y = xstate =>
                     {
                         if (xstate == null)
                             return;

                         dynamic state = xstate;

                         // if there is parent, we have to restore that first?

                         string hint = state.hint;

                         dynamic invoke = state.invoke;

                         string MethodToken = invoke.function;
                         object arguments = invoke.arguments;

                         x.Push(
                             delegate
                             {
                                 var z = new TaskCompletionSource<object>();
                                 var sw = new Stopwatch();
                                 sw.Start();

                                 if (!Expando.Of(Native.self).Contains(MethodToken))
                                 {
                                     //{ hint = typeof(y) } missing { MethodToken = CAAABoz2jD6nJMVTcci_a_bQ }


                                     Console.WriteLine(new { hint } + " missing " + new { MethodToken });

                                     //Could not load type 'ctor>b__6>d__17' from assembly 'WorkerInsideSecondaryApplicationWithStateReplace.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
                                     // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]
                                 }


                                 IFunction.ByName(MethodToken).ContinueWithResult(
                                     f =>
                                     {
                                         // { hint = typeof(y) } ok { MethodToken = CQAABoz2jD6nJMVTcci_a_bQ, ElapsedMilliseconds = 171 }
                                         Console.WriteLine(new { hint } + " ok " + new { MethodToken, sw.ElapsedMilliseconds });

                                         f.apply(null, args: (object[])arguments);
                                         //go(source);

                                         z.SetResult(null);
                                     }
                                 );

                                 return z.Task;
                             }
                         );

                         object parent = state.state;
                         y(parent);
                     };


                     y(Native.window.history.state);
                     #endregion


                     Console.WriteLine(
                         "Application onpopstate " + new
                         {
                             e = new { e.state },
                             history = new { Native.window.history.state }
                         }
                        + "\n steps to resume this state: " + new { x.Count }
                     );

                     //steps to resume this state: { Count = 1 }


                     Action all = async delegate
                     {
                         while (x.Count > 0)
                         {
                             await x.Pop()();
                         }

                     };

                     all();
                 };
            #endregion

        }



        #region x
        sealed class x
        {
            static x()
            {
                Console.WriteLine("x has now been defined");
            }

            public x(ILogin page)
            {

                // Uncaught Error: InvalidOperationException: { MethodToken = BgAABqe4Nzm_bBv4spuPWGg } function is not available at { href = http://192.168.43.252:24113/view-source#worker } 


                #region go 2
                new IHTMLButton { innerText = "go 2" }.AttachToDocument().WhenClicked(
                    //async
                 delegate
                 {
                     // hsc, why wont lambda declaration work within async scope?

                     // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx

                     Console.WriteLine("before");

                     // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                     //var x =
                     //await 
                     Task.Factory.StartNewWithProgress(

                     new { value = "world!" },

                     progress:
                        xx =>
                        {
                            Console.WriteLine("DOM Progress: " + new { xx.value, Thread.CurrentThread.ManagedThreadId });

                            Native.document.title = new { xx.value, Thread.CurrentThread.ManagedThreadId }.ToString();
                        },

                     function:
                         scope =>
                         {
                             #region scope is null
                             if (scope == null)
                             {
                                 // { x = scope_progress is null { state = [object Object], BackgroundThread = 10 }, ManagedThreadId = 1 }
                                 return new { value = "scope is null " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                             }
                             #endregion



                             var state = scope.Item2;

                             #region scope_progress
                             var scope_progress = scope.Item1;
                             if (scope_progress == null)
                             {
                                 // { x = null { BackgroundThread = 10 }, ManagedThreadId = 1 }
                                 return new { value = "scope_progress is null " + new { state, BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                             }
                             #endregion

                             var e = new Stopwatch();
                             e.Start();

                             scope_progress.Report(
                                  new
                                  {
                                      value = "hi " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId }
                                  }
                             );


                             for (int i = 0; i < 20; i++)
                             {
                                 scope_progress.Report(
                                    new
                                    {
                                        value = ". " + new { i, state.value, e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId }
                                    }
                                 );

                                 Thread.Sleep(100);
                             }


                             return new { value = "done " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                         }
                 ).ContinueWithResult(
                     x =>
                     {
                         Console.WriteLine("after");
                         Native.document.title = new { x.value, Thread.CurrentThread.ManagedThreadId }.ToString();
                     }
                 );

                 }
             );
                #endregion

                //Uncaught Error: InvalidOperationException: { MethodToken = BgAABqe4Nzm_bBv4spuPWGg } function is not available at { href = http://192.168.43.252:18192/view-source#worker } 

                #region go
                Action<string> go =
                    //async 
                      source =>
                      {
                          // show login

                          var layout = new App();

                          var newbody = layout.body;
                          var oldbody = Native.document.body;

                          // switch layouts

                          Native.document.body.parentNode.replaceChild(
                              newbody,
                              oldbody
                          );

                          // how did we get the source?
                          //var app = new y(layout);

                          var src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource();
                          //await new IHTMLScript { src = src };
                          Native.window.eval(source);


#if FUTURE
                      var yy = await new y(default(IProgress<string>), data: default(string));
#endif


                      };
                #endregion


                #region onclick
                page.Login.onclick +=
                    async delegate
                    {
                        page.Login.disabled = true;

                        var source = await typeof(y);

                        // we can now Activate it?
                        //Activator.CreateInstance(typeof(x));

                        Native.window.history.replaceState(
                             new
                             {
                                 Native.window.history.state,

                                 hint = "typeof(y)",

                                 invoke = new { function = ((__MethodInfo)go.Method).MethodToken, arguments = new[] { source } }
                             }
                        );

                        // invoke as state?
                        //go(source);

                        Native.document.location.reload();
                    };
                #endregion

            }

            sealed class y
            {

                static y()
                {
                    Console.WriteLine("y has now been defined");
                }

#if FUTURE
                public readonly TaskCompletionSource<y> yield = new TaskCompletionSource<y>();

                public TaskAwaiter<y> GetAwaiter()
                {
                    return yield.Task.GetAwaiter();
                }

                public y(IProgress<string> progress, string data)
                {



                    yield.SetResult(this);
                }
#endif

                public y(IApp page)
                {
                    // state null
                    #region go 2
                    new IHTMLButton { innerText = "go 2" }.AttachToDocument().WhenClicked(
                        //async
                     delegate
                     {
                         // hsc, why wont lambda declaration work within async scope?

                         // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx

                         Console.WriteLine("before");

                         // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress
                         //var x =
                         //await 
                         Task.Factory.StartNewWithProgress(

                         new { value = "world!" },

                         progress:
                            xx =>
                            {
                                Console.WriteLine("DOM Progress: " + new { xx.value, Thread.CurrentThread.ManagedThreadId });

                                Native.document.title = new { xx.value, Thread.CurrentThread.ManagedThreadId }.ToString();
                            },

                         function:
                             scope =>
                             {
                                 #region scope is null
                                 if (scope == null)
                                 {
                                     // { x = scope_progress is null { state = [object Object], BackgroundThread = 10 }, ManagedThreadId = 1 }
                                     return new { value = "scope is null " + new { BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                                 }
                                 #endregion



                                 var state = scope.Item2;

                                 #region scope_progress
                                 var scope_progress = scope.Item1;
                                 if (scope_progress == null)
                                 {
                                     // { x = null { BackgroundThread = 10 }, ManagedThreadId = 1 }
                                     return new { value = "scope_progress is null " + new { state, BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                                 }
                                 #endregion

                                 var e = new Stopwatch();
                                 e.Start();

                                 scope_progress.Report(
                                      new
                                      {
                                          value = "hi " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId }
                                      }
                                 );


                                 for (int i = 0; i < 20; i++)
                                 {
                                     scope_progress.Report(
                                        new
                                        {
                                            value = ". " + new { i, state.value, e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId }
                                        }
                                     );

                                     Thread.Sleep(100);
                                 }


                                 return new { value = "done " + new { e.ElapsedMilliseconds, BackgroundThread = Thread.CurrentThread.ManagedThreadId } };
                             }
                     ).ContinueWithResult(
                         x =>
                         {
                             Console.WriteLine("after");
                             Native.document.title = new { x.value, Thread.CurrentThread.ManagedThreadId }.ToString();
                         }
                     );

                     }
                 );
                    #endregion


                }
            }
        }
        #endregion

    }

}
