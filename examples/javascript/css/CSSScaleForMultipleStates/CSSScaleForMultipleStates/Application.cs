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
using CSSScaleForMultipleStates;
using CSSScaleForMultipleStates.Design;
using CSSScaleForMultipleStates.HTML.Pages;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Experimental;

namespace CSSScaleForMultipleStates
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        static void f(double x)
        {
            Native.document.body.style.transform = "scale(" + x + ")";
            (Native.document.body.style as dynamic).webkitFilter = "blur(" + (1.0 - x) * 20 + "px) opacity(" + x + ")";

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            (Native.document.body.style as dynamic).webkitTransition = "all 0.5s linear";
            //(bar.style as dynamic).webkitTransitionProperty = "top, width, background-color";
            (Native.document.body.style as dynamic).webkitTransitionProperty = "webkitFilter webkitTransform";

            Native.window.onfocus +=
             delegate
             {
                 f(1);
             };

            Native.window.onblur +=
                delegate
                {
                    f(0.98);
                };

            Native.document.onmouseover +=
                 delegate
                 {
                     f(1);
                 };

            f(1);

            page.GoToLevel2.onclick +=
                delegate
                {
                    Native.window.history.pushState(
                        new { pushState = "world1" },
                        async scope2 =>
                        {

                            var layout2 = new App2();
                            layout2.body.style.zIndex = 1000;
                            f(0.9);

                            Native.document.body.parentNode.insertBefore(
                                    layout2.body,
                                    Native.document.body
                            );


                            (Native.document.body.style as dynamic).webkitTransition = "all 0.5s linear";
                            //(bar.style as dynamic).webkitTransitionProperty = "top, width, background-color";
                            (Native.document.body.style as dynamic).webkitTransitionProperty = "webkitFilter webkitTransform";


                            f(1);





                            #region go
                            Action<HistoryScope<InternalScriptApplicationSource>> go =
                                async
                                  scope =>
                                {
                                    var source = scope.state;

                                    var layout3 = new App3();
                                    layout3.body.style.zIndex = 2000;

                                    f(0.95);

                                    Native.document.body.parentNode.insertBefore(
                                            layout3.body,
                                            Native.document.body
                                    );


                                    (Native.document.body.style as dynamic).webkitTransition = "all 0.5s linear";
                                    //(bar.style as dynamic).webkitTransitionProperty = "top, width, background-color";
                                    (Native.document.body.style as dynamic).webkitTransitionProperty = "webkitFilter webkitTransform";


                                    f(1);

                                    //new zz(layout3);
                                    source.eval();

                                    await scope;

                                    f(0.95);


                                    await Task.Delay(400);

                                    Native.document.body.Orphanize();

                                    f(1);

                                };
                            #endregion

                            #region Level3
                            layout2.GoToLevel3.onclick +=
                                delegate
                                {

                                    Native.window.history.pushState(
                                        new { pushState = "world1" },
                                        async scope =>
                                        {
                                            //Uncaught Error: InvalidOperationException: we can only continue with global methods for now... { Target = [object Object] } 

                                            var layout3 = new App3();
                                            layout3.body.style.zIndex = 2000;

                                            f(0.95);

                                            Native.document.body.parentNode.insertBefore(
                                                    layout3.body,
                                                    Native.document.body
                                            );


                                            (Native.document.body.style as dynamic).webkitTransition = "all 0.5s linear";
                                            //(bar.style as dynamic).webkitTransitionProperty = "top, width, background-color";
                                            (Native.document.body.style as dynamic).webkitTransitionProperty = "webkitFilter webkitTransform";


                                            f(1);

                                            //new zz(layout3);

                                            await scope;

                                            f(0.95);


                                            await Task.Delay(400);

                                            Native.document.body.Orphanize();

                                            f(1);
                                        }
                                    );


                                };
                            #endregion


                            #region onclick z

                            new IHTMLButton { innerText = layout2.GoToLevel3.innerText + " [encrypted]" }.AttachToDocument().WhenClicked(
                                async button =>
                                {
                                    button.disabled = true;

                                    var source = await typeof(zz);

                                    Native.window.history.pushState(source, go);

                                    button.disabled = false;
                                }
                            );

                            #endregion

                            await scope2;

                            f(0.95);


                            await Task.Delay(400);

                            Native.document.body.Orphanize();

                            f(1);
                        }
                    );

                };
        }



        sealed class zz
        {
            public zz(IApp3 page)
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
