using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkerInsideSecondaryApplicationWithStateReplace;
using WorkerInsideSecondaryApplicationWithStateReplace.Design;
using WorkerInsideSecondaryApplicationWithStateReplace.HTML.Pages;
using ScriptCoreLib.JavaScript.Experimental;


namespace WorkerInsideSecondaryApplicationWithStateReplace
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
                async source =>
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

                    await new IHTMLScript { src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource() };
                };
            #endregion

            #region onclick
            page.Continue.onclick +=
                async delegate
                {
                    page.Continue.disabled = true;

                    var source = await typeof(x);

                    // we can now Activate it?
                    //Activator.CreateInstance(typeof(x));


                    Native.window.history.replaceState(
                        data: new { source },
                        title: default(string),
                        url: default(string)
                    );

                    // invoke as state?
                    //go(source);

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
                     Console.WriteLine("Application onpopstate " + new
                     {

                         e = new { e.state },
                         history = new { Native.window.history.state }

                     });

                     if (Native.window.history.state == null)
                         return;

                     dynamic state = Native.window.history.state;

                     string source = state.source;

                     page.Continue.disabled = true;

                     //Could not load type 'ctor>b__6>d__17' from assembly 'WorkerInsideSecondaryApplicationWithStateReplace.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
                     // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                     Task.Delay(700).GetAwaiter().OnCompleted(
                         delegate
                         {
                             go(source);

                         }
                     );

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
                  async source =>
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

                      await new IHTMLScript { src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource() };

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

                        // invoke as state?
                        go(source);
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
