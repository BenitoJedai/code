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

using ScriptCoreLib.JavaScript.Experimental;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;

using WorkerInsideSecondaryApplicationWithBackButton;
using WorkerInsideSecondaryApplicationWithBackButton.Design;
using WorkerInsideSecondaryApplicationWithBackButton.HTML.Pages;
using System.Runtime.CompilerServices;

namespace WorkerInsideSecondaryApplicationWithBackButton
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
            Action<HistoryScope<InternalScriptApplicationSource>> go =
                //async 
                     scope =>
                     {
                         var source = scope.state;

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
                         source.eval();

                         //var src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource();
                         ////await new IHTMLScript { src = src };
                         //Native.window.eval(source);

                         // now what?
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

                    //go.ToHistoricAction()(source);
                    //go.Invoke(source, XHistory.HistoricEvent.pushState);



                    Native.window.history.replaceState(source, go);

                    //go.ToHistoricAction()
                    // or reload?
                    //Native.document.location.reload();
                };
            #endregion


            //           HistoryExtensions onpopstate { state = [object Object], e = { state = 2 }, history = { state = 2 }, Count = 1 }
            //did we just move forward?



            new IHTMLButton { innerText = "replaceState", id = "replaceState" }.AttachToDocument().WhenClicked(
                          btn =>
                          {
                              Native.window.history.replaceState(
                                  new { replaceState = "world", btn.id },
                                  scope =>
                                  {
                                      ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;

                                      Native.document.title = "zz " + new { scope.state.replaceState };

                                  }
                              );
                          }
                      );

            #region options
            new IHTMLButton { innerText = "pushState1", id = "pushState1" }.AttachToDocument().WhenClicked(
                btn =>
                {

                    Native.window.history.pushState(
                        new { pushState = "world1", btn.id },
                        async scope =>
                        {
                            ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                            Native.document.title = "zz " + new { scope.state.pushState };
                            var x = new { Native.document.body.style.backgroundColor };
                            Native.document.body.style.backgroundColor = "yellow";

                            await scope;
                            //scope.TaskCompletionSource.Task.ContinueWithResult(
                            //    delegate
                            //    {

                            Native.document.body.style.backgroundColor = x.backgroundColor;

                            ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                            Native.document.title = "zz!";
                            //    }
                            //);
                        }
                    );
                }
            );


            new IHTMLButton { innerText = "pushState2", id = "pushState2" }.AttachToDocument().WhenClicked(
                btn =>
                {

                    Native.window.history.pushState(
                        new { pushState = "world2", btn.id },
                        async scope =>
                        {
                            ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                            Native.document.title = "zz " + new { scope.state.pushState };

                            var x = new { Native.document.body.style.backgroundColor };
                            Native.document.body.style.backgroundColor = "green";

                            await scope;


                            Native.document.body.style.backgroundColor = x.backgroundColor;

                            ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                            Native.document.title = "zz!";
                        }
                    );
                }
            );
            #endregion



            // first time:
            // Application onpopstate { e = { state =  }, history = { state =  } }


            // after replaceState and reload
            // Application onpopstate { e = { state =  }, history = { state = [object Object] } }


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
                Native.document.title = "x";


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
                Action<HistoryScope<InternalScriptApplicationSource>> go =
                    //async 
                      scope =>
                      {
                          var source = scope.state;

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

                          //var src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource();
                          ////await new IHTMLScript { src = src };
                          //Native.window.eval(source);

                          source.eval();


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

                        //Native.window.history.replaceState(
                        //     new
                        //     {
                        //         Native.window.history.state,

                        //         hint = "typeof(y)",

                        //         invoke = new { function = ((__MethodInfo)go.Method).MethodToken, arguments = new[] { source } }
                        //     }
                        //);

                        //// invoke as state?
                        ////go(source);

                        //Native.document.location.reload();

                        Native.window.history.replaceState(source, go);

                    };
                #endregion


                #region options
                new IHTMLButton { innerText = "pushState1", id = "pushState1" }.AttachToDocument().WhenClicked(
                    btn =>
                    {

                        Native.window.history.pushState(
                            new { pushState = "world1", btn.id },
                            async scope =>
                            {
                                ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                                Native.document.title = "zz " + new { scope.state.pushState };
                                var x = new { Native.document.body.style.backgroundColor };
                                Native.document.body.style.backgroundColor = "yellow";

                                await scope;
                                //scope.TaskCompletionSource.Task.ContinueWithResult(
                                //    delegate
                                //    {

                                Native.document.body.style.backgroundColor = x.backgroundColor;

                                ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                                Native.document.title = "zz!";
                                //    }
                                //);
                            }
                        );
                    }
                );


                new IHTMLButton { innerText = "pushState2", id = "pushState2" }.AttachToDocument().WhenClicked(
                    btn =>
                    {

                        Native.window.history.pushState(
                            new { pushState = "world2", btn.id },
                            async scope =>
                            {
                                ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                                Native.document.title = "zz " + new { scope.state.pushState };

                                var x = new { Native.document.body.style.backgroundColor };
                                Native.document.body.style.backgroundColor = "green";

                                await scope;


                                Native.document.body.style.backgroundColor = x.backgroundColor;

                                ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                                Native.document.title = "zz!";
                            }
                        );
                    }
                );
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
                    Native.document.title = "y";

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

                    #region go
                    Action<HistoryScope<InternalScriptApplicationSource>> go =
                        async
                          scope =>
                        {
                            var source = scope.state;

                            // show login

                            var layout = new AppOther();

                            var newbody = layout.body;
                            var oldbody = Native.document.body;

                            // switch layouts

                            Native.document.body.parentNode.replaceChild(
                                newbody,
                                oldbody
                            );

                            // how did we get the source?
                            //var app = new y(layout);

                            //var src = new Blob(source).ToObjectURL().SetInternalScriptApplicationSource();
                            ////await new IHTMLScript { src = src };
                            //Native.window.eval(source);

                            var restore = source.eval();


#if FUTURE
                      var yy = await new y(default(IProgress<string>), data: default(string));
#endif

                            await scope;

                            restore();

                            Native.document.body.parentNode.replaceChild(
                                oldbody,
                                newbody
                            );

                            Native.document.title = "y";

                        };
                    #endregion


                    #region onclick z

                    new IHTMLButton { innerText = "go to the other page z" }.AttachToDocument().WhenClicked(
                        async button =>
                        {
                            button.disabled = true;

                            var source = await typeof(z);

                            Native.window.history.pushState(source, go);

                            button.disabled = false;
                        }
                    );

                    #endregion

                    #region onclick zz

                    new IHTMLButton { innerText = "go to the other page zz" }.AttachToDocument().WhenClicked(
                        async button =>
                        {
                            button.disabled = true;

                            var source = await typeof(zz);

                            // we can now Activate it?
                            //Activator.CreateInstance(typeof(x));

                            //Native.window.alert("pushState");
                            Native.window.history.pushState(
                                 new
                                 {
                                     Native.window.history.state,

                                     hint = "typeof(zz)",

                                     invoke = new { function = ((__MethodInfo)go.Method).MethodToken, arguments = new[] { source } }
                                 }
                            );

                            // invoke as state?
                            //go(source);

                            //Native.window.alert("reload");
                            Native.document.location.reload();
                        }
                    );

                    #endregion

                    #region options
                    new IHTMLButton { innerText = "pushState1", id = "pushState1" }.AttachToDocument().WhenClicked(
                        btn =>
                        {

                            Native.window.history.pushState(
                                new { pushState = "world1", btn.id },
                                async scope =>
                                {
                                    ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                                    Native.document.title = "zz " + new { scope.state.pushState };
                                    var x = new { Native.document.body.style.backgroundColor };
                                    Native.document.body.style.backgroundColor = "yellow";

                                    await scope;
                                    //scope.TaskCompletionSource.Task.ContinueWithResult(
                                    //    delegate
                                    //    {

                                    Native.document.body.style.backgroundColor = x.backgroundColor;

                                    ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                                    Native.document.title = "zz!";
                                    //    }
                                    //);
                                }
                            );
                        }
                    );


                    new IHTMLButton { innerText = "pushState2", id = "pushState2" }.AttachToDocument().WhenClicked(
                        btn =>
                        {

                            Native.window.history.pushState(
                                new { pushState = "world2", btn.id },
                                async scope =>
                                {
                                    ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                                    Native.document.title = "zz " + new { scope.state.pushState };

                                    var x = new { Native.document.body.style.backgroundColor };
                                    Native.document.body.style.backgroundColor = "green";

                                    await scope;


                                    Native.document.body.style.backgroundColor = x.backgroundColor;

                                    ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                                    Native.document.title = "zz!";
                                }
                            );
                        }
                    );
                    #endregion


                }


                sealed class z
                {
                    public z(IAppOther page)
                    {
                        Native.document.title = "z";

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

                sealed class zz
                {
                    public zz(IAppOther page)
                    {
                        Native.document.title = "zz";
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


                        new IHTMLButton { innerText = "replaceState", id = "replaceState" }.AttachToDocument().WhenClicked(
                            btn =>
                            {
                                Native.window.history.replaceState(
                                    new { replaceState = "world", btn.id },
                                    scope =>
                                    {
                                        ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;

                                        Native.document.title = "zz " + new { scope.state.replaceState };

                                    }
                                );
                            }
                        );

                        #region options
                        new IHTMLButton { innerText = "pushState1", id = "pushState1" }.AttachToDocument().WhenClicked(
                            btn =>
                            {

                                Native.window.history.pushState(
                                    new { pushState = "world1", btn.id },
                                    async scope =>
                                    {
                                        ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                                        Native.document.title = "zz " + new { scope.state.pushState };
                                        var x = new { Native.document.body.style.backgroundColor };
                                        Native.document.body.style.backgroundColor = "yellow";

                                        await scope;
                                        //scope.TaskCompletionSource.Task.ContinueWithResult(
                                        //    delegate
                                        //    {

                                        Native.document.body.style.backgroundColor = x.backgroundColor;

                                        ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                                        Native.document.title = "zz!";
                                        //    }
                                        //);
                                    }
                                );
                            }
                        );


                        new IHTMLButton { innerText = "pushState2", id = "pushState2" }.AttachToDocument().WhenClicked(
                            btn =>
                            {

                                Native.window.history.pushState(
                                    new { pushState = "world2", btn.id },
                                    async scope =>
                                    {
                                        ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = true;
                                        Native.document.title = "zz " + new { scope.state.pushState };

                                        var x = new { Native.document.body.style.backgroundColor };
                                        Native.document.body.style.backgroundColor = "green";

                                        await scope;


                                        Native.document.body.style.backgroundColor = x.backgroundColor;

                                        ((IHTMLButton)Native.document.getElementById(scope.state.id)).disabled = false;
                                        Native.document.title = "zz!";
                                    }
                                );
                            }
                        );
                        #endregion


                    }
                }
            }



        }
        #endregion

    }
}
