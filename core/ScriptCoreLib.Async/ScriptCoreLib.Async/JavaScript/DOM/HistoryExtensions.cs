using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class HistoryExtensions
    {
        static HistoryExtensions()
        {
            #region onpopstate

            //var previous_Count = -1;

            // each entry an try to prevent full reload and do inline unwind

            Console.WriteLine("HistoryExtensions");

            if (Native.window != null)
                Native.window.onpopstate +=
                     e =>
                     {
                         e.preventDefault();
                         e.stopPropagation();
                         e.stopImmediatePropagation();



                         var x_history_state = new Stack<Func<Task<Func<bool>>>>();
                         var x_e_state = new Stack<Func<Task<Func<bool>>>>();

                         #region y
                         Action<Stack<Func<Task<Func<bool>>>>, object> y = null;

                         y = (x, xstate) =>
                         {
                             if (xstate == null)
                                 return;

                             dynamic state = xstate;

                             // if there is parent, we have to restore that first?

                             string hint = state.hint;

                             Console.WriteLine(new { hint });

                             dynamic invoke = state.invoke;

                             string MethodToken = invoke.function;
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



                             var scope = new HistoryScope<object> { __state = arg0, __TaskCompletionSource = __get_unwind };


                             x.Push(
                                 delegate
                                 {
                                     var z = new TaskCompletionSource<Func<bool>>();
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



                                             f.apply(null, scope);
                                             //go(source);

                                             z.SetResult(
                                                delegate
                                                {
                                                    if (__unwind == null)
                                                        return true;

                                                    // time to do inline unwind.
                                                    __unwind.SetResult(scope);

                                                    return false;
                                                }
                                             );
                                         }
                                     );

                                     return z.Task;
                                 }
                             );

                             object parent = state.state;
                             y(x, parent);
                         };
                         #endregion


                         y(x_e_state, e.state);
                         y(x_history_state, Native.window.history.state);

                         //Application onpopstate { e = { state = [object Object] }, history = { state = [object Object] } }

                         // Application onpopstate { e = { state = 2 }, history = { state = 2 } }

                         Console.WriteLine(
                             "HistoryExtensions onpopstate " + new
                             {
                                 e.state,

                                 e = new { state = x_e_state.Count },
                                 history = new { state = x_history_state.Count },

                                 //previous_Count,
                                 HistoryScope.inline_unwind.Count
                             }
                         );

                         //HistoryExtensions onpopstate { state = , e = { state = 0 }, history = { state = 0 }, Count = 1 }


                         #region did we just move backward
                         if ((HistoryScope.inline_unwind.Count - 1) == x_history_state.Count)
                         {
                             // Application onpopstate { e = { state = 0 }, history = { state = 1 }, Count = 0 }


                             Console.WriteLine(" did we just move backward?");

                             var unwind = HistoryScope.inline_unwind.Pop();

                             var reload = unwind();

                             Console.WriteLine(new { reload, HistoryScope.inline_unwind.Count });


                             if (reload)
                             {
                                 Native.document.location.reload();
                                 return;
                             }

                             //previous_Count--;

                             return;
                         }
                         #endregion

                         // HistoryExtensions onpopstate { state = [object Object], e = { state = 1 }, history = { state = 1 }, Count = 0 }


                         #region did we just move forward?
                         if ((HistoryScope.inline_unwind.Count + 1) == x_history_state.Count)
                         {
                             Console.WriteLine(" did we just move forward?");

                             //var unwind = await x_history_state.First()();
                             x_history_state.First()().ContinueWithResult(
                                 unwind =>
                                 {
                                     HistoryScope.inline_unwind.Push(unwind);
                                     Console.WriteLine(new { HistoryScope.inline_unwind.Count });

                                 }
                             );


                             return;
                         }
                         #endregion

                         //HistoryExtensions onpopstate { state = [object Object], e = { state = 2 }, history = { state = 2 }, Count = 2 }

                         if (e.state != null)
                         {
                             // back: { e = { state = 2 }, history = { state = 2 }, previous_Count = 3 }
                             // forward: { e = { state = 3 }, history = { state = 3 }, previous_Count = 2 }



                             //Native.document.title = "manual reload";

                             //Native.document.body.style.backgroundColor = "gray";


                             //since we do not know how to unwind, restart the engines
                             // unless that stat tells us that it can unwind!
                             Native.document.location.reload();

                             return;
                         }

                         //previous_Count = x_history_state.Count;


                         {
                             Action all = null;

                             all = delegate
                             {
                                 if (x_history_state.Count > 0)
                                 {
                                     x_history_state.Pop()().ContinueWithResult(
                                         unwind =>
                                         {
                                             //Application onpopstate { e = { state = 2 }, history = { state = 2 }, Count = 1 }

                                             HistoryScope.inline_unwind.Push(unwind);

                                             all();
                                         }
                                     );
                                 }
                                 else
                                 {
                                     Console.WriteLine("done! " + new { HistoryScope.inline_unwind.Count });

                                 }
                             };

                             //Action all = async delegate
                             //{
                             //    while (x_history_state.Count > 0)
                             //    {
                             //        var unwind = await x_history_state.Pop()();

                             //        HistoryScope.inline_unwind.Push(unwind);
                             //    }
                             //};

                             all();
                         }

                     };
            #endregion

        }

        public static void replaceState<T>(this History h, T state, Action<HistoryScope<T>> yield)
        {
            if (yield.Target != null)
                if (yield.Target != Native.self)
                    throw new InvalidOperationException("we can only continue with global methods for now... " + new { yield.Target });

            var MethodToken = ((__MethodInfo)yield.Method).MethodToken;

            var data = new
            {
                Native.window.history.state,

                hint = "ScriptCoreLib.JavaScript.DOM.HistoryExtensions.replaceState",

                // arguments:

                invoke = new { function = MethodToken, arguments = new object[] { state } }
            };

            Native.window.history.replaceState(data);

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

            yield(scope);

            Console.WriteLine("replaceState: " + new { HistoryScope.inline_unwind.Count });
        }

        public static void pushState(this History h, string url, Action<HistoryScope<object>> yield)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs

            h.pushState(null, null, url);

            h.replaceState(
                state: new { },
                yield: yield
            );
        }

        public static void pushState<T>(this History h, T state, Action<HistoryScope<T>> yield)
        {
            if (yield.Target != null)
                if (yield.Target != Native.self)
                    throw new InvalidOperationException("we can only continue with global methods for now... " + new { yield.Target });

            var MethodToken = ((__MethodInfo)yield.Method).MethodToken;

            var data = new
            {
                Native.window.history.state,

                hint = "ScriptCoreLib.JavaScript.DOM.HistoryExtensions.pushState",

                // arguments:

                invoke = new { function = MethodToken, arguments = new object[] { state } }
            };

            // http://stackoverflow.com/questions/6460377/html5-history-api-what-is-the-max-size-the-state-object-can-be
            Console.WriteLine("pushState before: " + new { Native.window.history.length });

            Native.window.history.pushState(data);

            Console.WriteLine("pushState after: " + new { Native.window.history.length });


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

            yield(scope);

            Console.WriteLine("pushState: " + new { HistoryScope.inline_unwind.Count });
        }
    }
}
