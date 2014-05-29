#define FTRACE

using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class HistoryExtensions
    {
        static void location_reload()
        {
            //Native.document.title = "manual reload";
            //Native.document.body.style.backgroundColor = "gray";

            //since we do not know how to unwind, restart the engines
            // unless that stat tells us that it can unwind!

            // a slow down may also cause this?
            Native.document.location.reload();
        }

        class __entry
        {
            public Func<bool> unwind;
            public HistoryDetails unwind_data;

        }

        static Action later;
        public static Action<Action> yield;




        //02000038 ScriptCoreLib.JavaScript.DOM.HistoryExtensions+<>c__DisplayClass6
        //script: error JSC1000: unsupported flow detected, try to simplify.
        // Assembly V:\AsyncImageTask.Application.exe
        // DeclaringType ScriptCoreLib.JavaScript.DOM.HistoryExtensions+<>c__DisplayClass6, AsyncImageTask.Application, Version= 1.0.0.0, Culture= neutral, PublicKeyToken= null
        // OwnerMethod<onpopstate> b__8
        // Offset 00be
        // .Try ommiting the return, break or continue instruction.
        //script: error JSC1000: Method: <onpopstate>b__8, Type: ScriptCoreLib.JavaScript.DOM.HistoryExtensions+<>c__DisplayClass6; emmiting failed : System.InvalidOperationException: unsupported flow detected, try to simplify.

        static void onpopstate(PopStateEvent e)
        {
            // not roslyn friendly yet!

            e.preventDefault();
            e.stopPropagation();
            e.stopImmediatePropagation();


            // why 2 of them?
            var x_history_state = new Stack<Func<Task<__entry>>>();
            var x_e_state = new Stack<Func<Task<__entry>>>();

            #region y
            Action<string, Stack<Func<Task<__entry>>>, HistoryDetails> y = null;

            y = (sourcehint, x, state) =>
            {
                if (state == null)
                    return;

                if (state.state == null)
                {
                    Console.WriteLine("(state.state == null)");
                }


                //dynamic state = xstate;

                // if there is parent, we have to restore that first?

                string hint = state.hint;
                string url = state.url;
                bool exclusive = state.exclusive;

                // 0:226852ms onpopstate { sourcehint = e.state , hint = ScriptCoreLib.JavaScript.DOM.HistoryExtensions.pushState, url = /, exclusive = 0, 
                // state =  } 
                Console.WriteLine("onpopstate.y " + new
                {
                    sourcehint,
                    hint,
                    url,
                    exclusive,

                    // 0:25692ms onpopstate { sourcehint = e.state , MethodToken = , invoke = [object Object] } 
                    state.invoke,
                    state.state
                });


                //0:45199ms HistoryExtensions at onpopstate view-source:38792
                //0:45200ms onpopstate { sourcehint = e.state , hint = ScriptCoreLib.JavaScript.DOM.HistoryExtensions.pushState, url = /, exclusive = 0, state =  } view-source:38792
                //0:45202ms onpopstate { sourcehint = history.state , hint = ScriptCoreLib.JavaScript.DOM.HistoryExtensions.pushState, url = /, exclusive = 0, state =  } view-source:38792
                //0:45202ms onpopstate did we just move forward? { Count = 1 } view-source:38792
                //0:45203ms preparing the task for { MethodToken =  } view-source:38792
                //0:45204ms NextStyle added to InternalHistoryAwareSheets { Count = 2 } view-source:38792
                //0:45204ms enter IFunction.ByName { name =  } view-source:38792
                //0:45207ms returning the task view-source:38792

                // who is setting the invoke? how can it be empty?
                // see :687
                dynamic invoke = state.invoke;

                // what if our dynamic is broken ???
                // roslyn $function ?
                // script: error JSC1000: No implementation found for this native method, please implement [static Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, System.Type, System.Collections.Generic.IEnumerable`1[[Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]])]
                //string MethodToken0 = Expando.Of(invoke)["function"];
                //string MethodToken0 = Expando.Of(state.invoke)["function"].GetValue();
                //Debugger.Break();

                // wtf??
                // 0:40145ms onpopstate.y { sourcehint = e.state , MethodToken = , MethodToken0 = undefined, invoke = [object Object] } 

                //string MethodToken = invoke.function;
                string MethodToken = invoke.MethodToken;

                // :226853ms { MethodToken = , invoke = [object Object] } 
                Console.WriteLine("onpopstate.y " + new { sourcehint, MethodToken, invoke });

                object arguments = invoke.arguments;
                object arg0 = ((object[])arguments)[0];


                #region __unwind
                TaskCompletionSource<HistoryScope<object>> __unwind = null;

                Func<TaskCompletionSource<HistoryScope<object>>> __get_unwind =
                    delegate
                {
                    // ok, something is listening to inline unwind.
                    // lets wait for the event then and not reload

                    Console.WriteLine("__get_unwind");

                    if (__unwind == null)
                        __unwind = new TaskCompletionSource<HistoryScope<object>>();

                    return __unwind;
                };
                #endregion



                var scope = new HistoryScope<object>
                {
                    __state = arg0,
                    __TaskCompletionSource = __get_unwind
                };


                // whats the x?
                x.Push(
                    delegate
                {
                    Console.WriteLine("preparing the task for " + new { MethodToken });

                    var z = new TaskCompletionSource<__entry>();

                    #region missing ?
                    var sw = Stopwatch.StartNew();

                    if (!Expando.Of(Native.self).Contains(MethodToken))
                    {
                        //{ hint = typeof(y) } missing { MethodToken = CAAABoz2jD6nJMVTcci_a_bQ }


                        Console.WriteLine(new { hint } + " missing " + new { MethodToken });

                        //Could not load type 'ctor>b__6>d__17' from assembly 'WorkerInsideSecondaryApplicationWithStateReplace.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
                        // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.Task.ContinueWith(System.Action`1[[System.Threading.Tasks.Task, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]
                    }
                    #endregion

                    // test the refresh!
                    var NextStyle = new IStyleSheet();
                    NextStyle.Owner.setAttribute("historic-url", IStyleSheet.all.Owner.getAttribute("historic-url") + " -> " + url);
                    IStyleSheet.InternalHistoryAwareSheets.Push(NextStyle);

                    Console.WriteLine("NextStyle added to InternalHistoryAwareSheets " + new { IStyleSheet.InternalHistoryAwareSheets.Count });

                    IFunction.ByName(MethodToken).ContinueWithResult(
                        f =>
                            {
                                // { hint = typeof(y) } ok { MethodToken = CQAABoz2jD6nJMVTcci_a_bQ, ElapsedMilliseconds = 171 }
                                Console.WriteLine(new { hint } + " ok " + new { MethodToken, sw.ElapsedMilliseconds });



                                f.apply(null, scope);
                                //go(source);

                                z.SetResult(
                                    new __entry
                                {
                                    unwind_data = state,
                                    unwind =
                                           delegate
                                    {
                                        if (__unwind == null)
                                            return true;

                                        // time to do inline unwind.
                                        __unwind.SetResult(scope);

                                        return false;
                                    }
                                }
                                );
                            }
                    );

                    Console.WriteLine("returning the task");

                    return z.Task;
                }
                );

                var parent = (HistoryDetails)state.state;
                y(sourcehint + " parent", x, parent);
            };
            #endregion

            // crude cast. 
            // why are we calling both?
            y("e.state ", x_e_state, (HistoryDetails)e.state);
            y("history.state ", x_history_state, (HistoryDetails)Native.window.history.state);

            //Application onpopstate { e = { state = [object Object] }, history = { state = [object Object] } }

            // Application onpopstate { e = { state = 2 }, history = { state = 2 } }

            //Console.WriteLine(
            //    "HistoryExtensions onpopstate " + new
            //    {
            //        e.state,

            //        e = new { state = x_e_state.Count },
            //        history = new { state = x_history_state.Count },

            //        //previous_Count,
            //        HistoryScope.inline_unwind.Count,

            //        // X:\jsc.svn\examples\javascript\Test\TestNavigationTiming\TestNavigationTiming\Application.cs
            //        // not available for safari!
            //        //Native.window.performance.navigation.redirectCount,
            //        //Native.window.performance.navigation.type
            //    }
            //);

            //HistoryExtensions onpopstate { state = , e = { state = 0 }, history = { state = 0 }, Count = 1 }

            #region restoreall
            Action restoreall = null;

            restoreall = delegate
            {
                Console.WriteLine("at restoreall " + new { x_history_state.Count });

                if (x_history_state.Count > 0)
                {

                    var yield = x_history_state.Pop();

                    yield().ContinueWithResult(
                        z =>
                        {
                            //Application onpopstate { e = { state = 2 }, history = { state = 2 }, Count = 1 }

                            #region exclusive
                            if (z.unwind_data.exclusive)
                            {
                                foreach (var unwind in HistoryScope.inline_unwind.AsEnumerable())
                                {
                                    var reload = unwind();

                                    if (reload)
                                    {
                                        throw new NotImplementedException("exclusve scope needs to unwind prior states, yet one of them asks for full reload. what to do?");
                                    }
                                }
                            }
                            #endregion

                            Console.WriteLine("restored: " + new { z.unwind_data });

                            HistoryScope.inline_unwind_data.Push(z.unwind_data);
                            HistoryScope.inline_unwind.Push(z.unwind);


                            restoreall();
                        }
                    );

                    return;
                }


                Console.WriteLine("at restoreall done! " + new { HistoryScope.inline_unwind.Count });
            };

            #endregion

            #region did we just move backward
            if ((HistoryScope.inline_unwind.Count - 1) == x_history_state.Count)
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517

                // Application onpopstate { e = { state = 0 }, history = { state = 1 }, Count = 0 }

                //  Native.window.performance.navigation.type,
                Console.WriteLine(" did we just move backward? " + new
                {
                    unwind_data = HistoryScope.inline_unwind_data.Count,
                    unwind = HistoryScope.inline_unwind.Count,
                });

                var unwind_data = (HistoryDetails)HistoryScope.inline_unwind_data.Pop();
                var unwind = HistoryScope.inline_unwind.Pop();

                // if exclusive we have to reactivete the remaining as if full reload?
                Console.WriteLine(new
                {
                    unwind,
                    unwind_data.url,
                    unwind_data.exclusive,
                    HistoryScope.inline_unwind.Count
                });

                var reload = unwind();

                Console.WriteLine(new { reload, HistoryScope.inline_unwind.Count });

                if (reload)
                {
                    location_reload();
                    return;
                }

                //2:3ms  did we just move backward? view-source:35996
                //2:3ms { unwind = [object Object], url = http://192.168.1.91:14617/#/bar, exclusive = 1, Count = 0 } view-source:35955
                //2:5ms { reload = false, Count = 0 } 

                // X:\jsc.svn\examples\javascript\CSS\Test\CSSHistoric\CSSHistoric\Application.cs
                var css = IStyleSheet.InternalHistoryAwareSheets.Pop();
                if (css != null)
                {
                    Console.WriteLine("remove css " + css.Owner.getAttribute("historic-url")
                        );

                    css.disabled = true;
                    css.Owner.Orphanize();
                }

                //previous_Count--;

                if (unwind_data.exclusive)
                {
                    HistoryScope.inline_unwind_data.Clear();
                    HistoryScope.inline_unwind.Clear();

                    restoreall();
                }

                return;
            }
            #endregion

            // HistoryExtensions onpopstate { state = [object Object], e = { state = 1 }, history = { state = 1 }, Count = 0 }


            #region did we just move forward?
            if ((HistoryScope.inline_unwind.Count + 1) == x_history_state.Count)
            {
                // X:\jsc.svn\examples\javascript\test\TestHistoryForwardEvent\TestHistoryForwardEvent\Application.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517

                Console.WriteLine("onpopstate did we just move forward? " + new { x_history_state.Count });

                // where is it added to that list so we can invoke it?
                x_history_state.First()().ContinueWithResult(
                    z =>
                    {
                        Console.WriteLine("onpopstate go forward " + new { z.unwind_data.url, z.unwind_data.exclusive });

                        #region exclusive
                        if (z.unwind_data.exclusive)
                        {
                            foreach (var unwind in HistoryScope.inline_unwind.AsEnumerable())
                            {
                                var reload = unwind();

                                if (reload)
                                {
                                    throw new NotImplementedException("exclusve scope needs to unwind prior states, yet one of them asks for full reload. what to do?");
                                }
                            }
                        }
                        #endregion

                        HistoryScope.inline_unwind_data.Push(z.unwind_data);
                        HistoryScope.inline_unwind.Push(z.unwind);

                        Console.WriteLine(new { HistoryScope.inline_unwind.Count });

                    }
                );


                return;
            }
            #endregion

            //HistoryExtensions onpopstate { state = [object Object], e = { state = 2 }, history = { state = 2 }, Count = 2 }

            #region ? reload
            if (e.state != null)
            {
                // back: { e = { state = 2 }, history = { state = 2 }, previous_Count = 3 }
                // forward: { e = { state = 3 }, history = { state = 3 }, previous_Count = 2 }




                location_reload();

                return;
            }
            #endregion


            //previous_Count = x_history_state.Count;




            restoreall();
        }

        static HistoryExtensions()
        {
            Console.WriteLine("HistoryExtensions ..ctor enter");

            later = delegate { };
            //yield = y => later += y;
            //var previous_Count = -1;

            // each entry an try to prevent full reload and do inline unwind


            if (Native.window == null)
            {
                Console.WriteLine("HistoryExtensions exit. no window");
                return;
            }


            var keepordering = IStyleSheet.all;


            //0:11ms HistoryExtensions ..ctor enter view-source:36565
            //0:11ms HistoryExtensions before onpopstate 

            Console.WriteLine("HistoryExtensions before onpopstate");

            // http://stackoverflow.com/questions/6421769/popstate-on-pages-load-in-chrome
            // chrome devs, what the duck are you doing over there?
            yield = y => y();

            Native.window.onpopstate += e =>
            {
                Console.WriteLine("HistoryExtensions at onpopstate");

                onpopstate(e);


                //var z = later;
                //later = delegate { };

                //// whenn will it be null?
                //if (z != null)
                //    z();
            };
        }

        public static void replaceState<T>(this History h, T state, Action<HistoryScope<T>> yield)
        {

            replaceState(h, state,

                Native.document.location.pathname,

                exclusive: false,
                yield: yield
                );
        }

        public static void replaceState<T>(this History h,
            T state,
            string url,
            bool exclusive,
            Action<HistoryScope<T>> yield
            )
        {
            // tested by
            // X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\AsyncHistoricActivities\Application.cs

            HistoryExtensions.yield(
                delegate
            {

                //Console.WriteLine("enter replaceState");

                if (yield.Target != null)
                    if (yield.Target != Native.self)
                        throw new InvalidOperationException("we can only continue with global methods for now... " + new { yield.Target });

                var MethodToken = ((__MethodInfo)yield.Method).MethodToken;


                var data_state = (HistoryDetails)Native.window.history.state;

                // HistoryExtensions onpopstate { state = [object Object], e = { state = 7 }, history = { state = 7 }, Count = 2 }

                //        m.state = h.state;
                //m.hint = 'ScriptCoreLib.JavaScript.DOM.HistoryExtensions.replaceState';

                if (data_state != null)
                    data_state = (HistoryDetails)data_state.state;


                var data = new HistoryDetails
                {
                    // parent
                    state = data_state,

                    hint = "ScriptCoreLib.JavaScript.DOM.HistoryExtensions.replaceState",

                    exclusive = exclusive,
                    url = url,

                    // arguments:

                    //invoke = new { function = MethodToken, arguments = new object[] { state } }
                    invoke = new { MethodToken, arguments = new object[] { state } }
                };

                Console.WriteLine("before history.replaceState");
                // IE throws __exc	Argument not optional
                // X:\jsc.svn\examples\javascript\CSS\Test\CSSHistoric\CSSHistoric\Application.cs


                Native.window.history.replaceState(data, "", url);
                Console.WriteLine("after history.replaceState");

                #region __unwind
                TaskCompletionSource<HistoryScope<T>> __unwind = null;

                Func<TaskCompletionSource<HistoryScope<T>>> __get_unwind =
                    delegate
                {
                    // ok, something is listening to inline unwind.
                    // lets wait for the event then and not reload

                    Console.WriteLine("__get_unwind [inline]");

                    if (__unwind == null)
                        __unwind = new TaskCompletionSource<HistoryScope<T>>();

                    return __unwind;
                };
                #endregion

                var scope = new HistoryScope<T> { __state = state, __TaskCompletionSource = __get_unwind };

                if (HistoryScope.inline_unwind.Count > 0)
                {
                    HistoryScope.inline_unwind_data.Pop();
                    var unwind = HistoryScope.inline_unwind.Pop();
                    var reload = unwind();

                    if (reload)
                    {
                        throw new NotImplementedException("cant reload here can we");
                    }
                }

                #region exclusive
                if (exclusive)
                {
                    // this state is market exlusive.
                    // as such. we shall destroy any other state before we continue.
                    // if we go back in time
                    // we have to reactivate them ofcourse.

                    Console.WriteLine("HistoryExtensions pushState exlusive scope " + new { url });

                    foreach (var unwind in HistoryScope.inline_unwind.AsEnumerable())
                    {
                        var reload = unwind();

                        if (reload)
                        {
                            throw new NotImplementedException("exclusve scope needs to unwind prior states, yet one of them asks for full reload. what to do?");
                        }
                    }

                    Console.WriteLine("disable all history aware styles, who is reenabling them?");

                    IStyleSheet.InternalHistoryAwareSheets.AsEnumerable()
                            // the application .ctor is not rerun? so we cannot even keep the primary style can we?
                            //.Skip(1)
                            .WithEachIndex(

                        (style, i) =>
                            {

                                Console.WriteLine("new exclusive state, will disable historic css " + new { i } + style.Owner.getAttribute("historic-url"));

                                if (i == 0)
                                    return;

                                style.disabled = true;
                            }
                    );
                }
                #endregion

                HistoryScope.inline_unwind_data.Push(data);
                HistoryScope.inline_unwind.Push(
                     delegate
                {
                    if (__unwind == null)
                        return true;

                    // time to do inline unwind.
                    __unwind.SetResult(scope);

                    return false;
                }
                );

                //var css = IStyleSheet.InternalHistoryAwareSheets.Pop();
                //if (css != null)
                //    css.disabled = true;

                var css = IStyleSheet.InternalHistoryAwareSheets.Pop();
                if (css != null)
                {
                    Console.WriteLine("replace: remove css " + css.Owner.getAttribute("historic-url")
                        );

                    css.disabled = true;
                    css.Owner.Orphanize();
                }

                //IStyleSheet.InternalHistoryAwareSheets.Push(new IStyleSheet());

                var NextStyle = new IStyleSheet();
                NextStyle.Owner.setAttribute("historic-url", IStyleSheet.all.Owner.getAttribute("historic-url") + " => " + url);
                IStyleSheet.InternalHistoryAwareSheets.Push(NextStyle);

                Console.WriteLine("before yield");
                yield(scope);

                Console.WriteLine("replaceState: " + new { HistoryScope.inline_unwind.Count });
            }
             );

        }



        // testd by
        // X:\jsc.svn\examples\javascript\future\HistoricSnapshotMashup\HistoricSnapshotMashup\Application.cs

        public static void pushState<T>(this History h, T state, Action<HistoryScope<T>> yield)
        {

            pushState(h, state, Native.document.location.pathname, exclusive: false, yield: yield);
        }


        
        public static void pushState<T>(
            this History h,
            T state,
            string url,
            bool exclusive,
            Action<HistoryScope<T>> yield
            )
        {
            // exclusive parent means a sub state will undo parent, so they wont exist at the same time
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131222-form
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517
            // X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs
            // X:\jsc.svn\examples\javascript\CSS\Test\CSSHistoric\CSSHistoric\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestHistoryForwardEvent\TestHistoryForwardEvent\Application.cs
            // does the forward button work?

            var delay = Stopwatch.StartNew();

            Console.WriteLine("HistoryExtensions pushState before yield");

            // when is the yield called?
            HistoryExtensions.yield(
                 delegate
            {
                //0:3083ms HistoryExtensions pushState before yield
                //0:3086ms HistoryExtensions pushState.yield { ElapsedMilliseconds = 3, state =  }
                //0:3087ms HistoryExtensions pushState.yield before: { exclusive = 0, length = 1, MethodToken = AgAABgY2dze_awFR4sqxY4A, data_invoke = { function = AgAABgY2dze_awFR4sqxY4A, arguments = { foo = foo } }, data = [object Object] }

                Console.WriteLine("HistoryExtensions pushState.yield " + new
                {
                    delay.ElapsedMilliseconds,
                    // whats the current state?
                    Native.window.history.state
                }
                );

                if (yield.Target != null)
                    if (yield.Target != Native.self)
                        throw new InvalidOperationException(
                            "we can only continue with global methods for now... " + new { yield.Target }
                        );



                var MethodToken = ((__MethodInfo)yield.Method).MethodToken;

                //var data_invoke = new { function = MethodToken, arguments = new object[] { state } };
                var data_invoke = new { MethodToken, arguments = new object[] { state } };
                var data = new HistoryDetails
                {
                    // is this the previous state?
                    state = Native.window.history.state,

                    hint = "ScriptCoreLib.JavaScript.DOM.HistoryExtensions.pushState",

                    exclusive = exclusive,
                    url = url,

                    // arguments:

                    //invoke = new { function = MethodToken, arguments = new object[] { state } }
                    invoke = data_invoke
                };


                //0:22850ms HistoryExtensions pushState before: { exclusive = 0, length = 1 } view-source:38792
                //0:22851ms HistoryExtensions pushState after: { length = 2 } 

                // X:\jsc.svn\examples\javascript\Test\TestHistoryForwardEvent\TestHistoryForwardEvent\Application.cs
                // http://stackoverflow.com/questions/6460377/html5-history-api-what-is-the-max-size-the-state-object-can-be
                Console.WriteLine("HistoryExtensions pushState.yield before: " + new
                {
                    MethodToken,

                    exclusive,
                    Native.window.history.length,
                    data_invoke,
                    data
                });


                var current = new { Native.document.location.href };

                // fck ie
                //   y("history.state ", x_history_state, (HistoryDetails)Native.window.history.state);
                Native.window.history.pushState(data, "", url);

                //0:137143ms HistoryExtensions pushState before yield view-source:38792
                //0:137145ms HistoryExtensions pushState.yield view-source:38792
                //0:137147ms HistoryExtensions pushState.yield before: { exclusive = 0, length = 1, MethodToken = AgAABgY2dze_awFR4sqxY4A, data_invoke = { function = AgAABgY2dze_awFR4sqxY4A, arguments = { foo = foo } }, data = [object Object] } view-source:38792
                //0:137149ms HistoryExtensions pushState after: { length = 2, state = [object Object] } 

                //Console.WriteLine("HistoryExtensions pushState.yield after: " + new
                //{
                //    Native.window.history.length,
                //    Native.window.history.state
                //});

                //var data1 = (HistoryDetails)Native.window.history.state;

                //Console.WriteLine("HistoryExtensions pushState.yield after: " + new
                //{
                //    data1.invoke
                //});


                #region __unwind
                TaskCompletionSource<HistoryScope<T>> __unwind = null;

                Func<TaskCompletionSource<HistoryScope<T>>> __get_unwind =
                    delegate
                {
                    // ok, something is listening to inline unwind.
                    // lets wait for the event then and not reload

                    Console.WriteLine("HistoryExtensions pushState __get_unwind [inline]");

                    if (__unwind == null)
                        __unwind = new TaskCompletionSource<HistoryScope<T>>();

                    return __unwind;
                };
                #endregion

                var scope = new HistoryScope<T>
                {
                    __state = state,
                    __TaskCompletionSource = __get_unwind
                };


                #region exclusive
                if (exclusive)
                {
                    // this state is market exlusive.
                    // as such. we shall destroy any other state before we continue.
                    // if we go back in time
                    // we have to reactivate them ofcourse.

                    Console.WriteLine("HistoryExtensions pushState exlusive scope " + new { url });

                    foreach (var unwind in HistoryScope.inline_unwind.AsEnumerable())
                    {
                        var reload = unwind();

                        if (reload)
                        {
                            throw new NotImplementedException("exclusve scope needs to unwind prior states, yet one of them asks for full reload. what to do?");
                        }
                    }

                    //0:1ms HistoryExtensions pushState exlusive scope { url = http://192.168.1.91:13905/#/foo } view-source:35994
                    //0:1ms HistoryExtensions pushState before enter scope { url = http://192.168.1.91:13905/#/foo } 

                    Console.WriteLine("disable all history aware styles, who is reenabling them?");

                    IStyleSheet.InternalHistoryAwareSheets.AsEnumerable()
                             // the application .ctor is not rerun? so we cannot even keep the primary style can we?
                             //.Skip(1)
                             .WithEachIndex(

                        (style, i) =>
                             {

                                 Console.WriteLine("new exclusive state, will disable historic css " + new { i } + style.Owner.getAttribute("historic-url"));

                                 if (i == 0)
                                     return;

                                 style.disabled = true;
                             }
                    );

                }
                #endregion

                HistoryScope.inline_unwind_data.Push(data);
                HistoryScope.inline_unwind.Push(
                     delegate
                {
                    if (__unwind == null)
                        return true;

                    // time to do inline unwind.
                    __unwind.SetResult(scope);

                    return false;
                }
                );


                // X:\jsc.svn\examples\javascript\CSS\Test\CSSHistoric\CSSHistoric\Application.cs

                ;

                var NextStyle = new IStyleSheet();
                NextStyle.Owner.setAttribute("historic-url", IStyleSheet.all.Owner.getAttribute("historic-url") + " -> " + url);
                IStyleSheet.InternalHistoryAwareSheets.Push(NextStyle);

                // activate the scope
                Console.WriteLine("HistoryExtensions pushState before enter scope " + new { url });
                yield(scope);
                Console.WriteLine("HistoryExtensions pushState " + new { HistoryScope.inline_unwind.Count });
            }
             );

        }


        sealed class HistoryDetails
        {
            public object state;

            public string hint;

            public bool exclusive;
            public string url;

            public object invoke;

        }
    }


}
