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
using AsyncHistoryExperiment;
using AsyncHistoryExperiment.Design;
using AsyncHistoryExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using System.Collections.Generic;

namespace AsyncHistoryExperiment
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
            // X:\jsc.svn\examples\javascript\ButtonsWithHistory\ButtonsWithHistory\Application.cs


            page.GoBackwards.onclick +=
                delegate
                {
                    Native.window.history.back();
                };

            page.GoForwards.onclick +=
                delegate
                {
                    Native.window.history.forward();
                };
            page.Reload.onclick +=
                delegate
                {
                    Native.document.location.reload();
                };






            // which way to go?

            var actions =

                from btn in new[] {
                    page.A,
                    page.B,
                    page.C,
                    page.D,
                    page.E,
                    page.F,
                }

                let invoke = new Action(
                    delegate
                    {
                        XState.Create(
                            //title: btn.innerText,
                            //url: "#" + btn.innerText,
                            value: new { button = btn.id },
                            invoke:
                                async state =>
                                {
                                    // how can we share the scope?
                                    // because this might be called from a .cctor

                                    Native.document.title = new { state.value.button }.ToString();


                                    var xpage = new App.FromDocument();


                                    var xbtn = (IHTMLButton)Native.document.getElementById(state.value.button);

                                    xbtn.style.color = JSColor.Blue;
                                    xbtn.disabled = true;

                                    await state;

                                    xbtn.disabled = false;

                                }
                        );
                    }
                )

                select new { btn, invoke };



            actions.WithEach(x => x.btn.onclick += delegate { x.invoke(); });


            page.G.style.color = JSColor.Red;

            //Error	3	Cannot convert async anonymous method to delegate type 'System.Func<object>'. 
            // An async anonymous method may return void, Task or Task<T>, none of which are convertible to 'System.Func<object>'.	X:\jsc.svn\examples\javascript\AsyncHistoryExperiment\AsyncHistoryExperiment\Application.cs	110	18	AsyncHistoryExperiment

            //script: error JSC1000: No implementation found for this native method, please implement 
            // [System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1.SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)]

            Func<int, Task<object>> flash =
                 async ms =>
                 {
                     Console.WriteLine("flash 0");
                     page.G.style.backgroundColor = JSColor.Yellow;
                     await Task.Delay(ms);
                     Console.WriteLine("flash 100");
                     page.G.style.backgroundColor = JSColor.None;
                     await Task.Delay(ms);
                     Console.WriteLine("flash 200");

                     // script: error JSC1000: No implementation found for this native method, please implement 
                     // [System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1.SetException(System.Exception)]
                     return new object();
                 };


            page.G.onclick +=
                async delegate
                {
                    page.G.disabled = true;


                    if (actions.All(x => x.btn.disabled))
                    {


                        System.Media.SystemSounds.Beep.Play();
                        await flash(300);
                        //Console.Beep();
                        await flash(300);


                        while (actions.Where(k => k.btn.disabled).Any())
                        {
                            Native.window.history.back();

                            await Task.Delay(100);
                        }

                        page.G.disabled = false;
                    }
                    else
                    {

                        do
                        {
                            actions.FirstOrDefault(k => !k.btn.disabled).With(
                                x =>
                                {
                                    x.invoke();
                                }
                            );

                            await Task.Delay(100);
                        }
                        while (actions.Where(k => !k.btn.disabled).Any());

                        System.Media.SystemSounds.Beep.Play();
                        await flash(300);
                        //Console.Beep();
                        await flash(300);

                    }

                    page.G.disabled = false;
                };




            Native.window.onpopstate +=
                delegate
                {
                    // we just dont know which way we can go
                    page.GoForwards.disabled = !(Native.window.history.length > 1);
                    page.GoBackwards.disabled = !(Native.window.history.length > 1);
                };



        }

    }

    class XState
    {
        public sealed class XVariation
        {
            public string id;

            public object value;

            public string invoke;

            public XVariation stack;
        }



        static XState()
        {
            Action<XState.XVariation> invoke = null;

            invoke = data =>
            {
                data.stack.With(invoke);

                var data_invoke = data.invoke;

                // lets resume! { data_invoke = BwAABoL2TT6iqfiSNWZMtg }
                Console.WriteLine("lets resume! " + new { data_invoke });

                var s = new XState.XVariation<object>
                {
                    value = data.value,
                    data = data
                };

                IFunction.Of(data_invoke).apply(null, s);
            };

            Native.window.onpopstate +=
               e =>
               {
                   Console.WriteLine("onpopstate!");

                   var state = ((XState.XVariation)Native.window.history.state);

                   state.With(invoke);
               };
        }

        public class XVariation<T>
        {
            //public int index;

            //public Action<XVariation<T>> invoke;

            public T value;



            public XVariation data;

            public TaskAwaiter<object> GetAwaiter()
            {
                var s = new TaskCompletionSource<object>();

                //Console.WriteLine("GetAwaiter");

                // GetAwaiter done { that = { index = 1 }, Previous = { index = -1 }, Current = { index = -1 } }
                // GetAwaiter done { that = { index = 1 }, Previous = { index = -1 }, Current = { index = -1 } }


                Console.WriteLine("await onpopstate " + data.id);

                Action yield = delegate
                {
                    Console.WriteLine("GetAwaiter done  " + data.id);

                    s.SetResult(
                        new object()
                    );

                    s = null;
                };

                Native.window.onpopstate +=
                   e =>
                   {
                       if (s == null)
                           return;

                       var state = ((XState.XVariation)Native.window.history.state);

                       // GetAwaiter done { that = { index = 2 }, Previous = { index = 1 }, Current = { index = 1 } }

                       if (data.stack == null)
                       {
                           if (state == null)
                               yield();

                           return;
                       }

                       if (data.stack.id == state.id)
                           yield();

                   };



                return s.Task.GetAwaiter();

            }
        }

        public static XVariation<T> Create<T>(T value, Action<XVariation<T>> invoke)
        {
            return Create(
                default(string),
                default(string),
                value,
                invoke
            );
        }

        public static XVariation<T> Create<T>(
            string title, string url, T value, Action<XVariation<T>> invoke)
        {
            var state = ((XState.XVariation)Native.window.history.state);


            //var data_index = Native.window.history.length;

            var data_invoke = ((__MethodInfo)invoke.Method).MethodToken;


            var id = "";

            if (state != null)
                id = state.id;

            id += ":" + new Random().Next().ToString("x8");

            var data =
                new XVariation
                {
                    id = id,


                    value = value,
                    invoke = data_invoke,
                    //index = data_index,

                    stack = state
                };


            var s = new XVariation<T>
            {
                //index = data_index,
                value = value,
                //invoke = invoke,


                data = data
            };




            Console.WriteLine("pushState " + id);

            Native.window.history.pushState(
                data: data


                , title: title, url: url
                );

            invoke(s);

            return s;
        }
    }


}
