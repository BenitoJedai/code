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


        public static async Task<object> xflash(IHTMLButton g, int ms, JSColor c)
        {
            Console.WriteLine("flash 0");
            g.style.backgroundColor = c;
            await Task.Delay(ms);
            Console.WriteLine("flash 100");
            g.style.backgroundColor = JSColor.None;
            await Task.Delay(ms);
            Console.WriteLine("flash 200");

            // script: error JSC1000: No implementation found for this native method, please implement 
            // [System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1.SetException(System.Exception)]
            return new object();
        }


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

            Func<IHTMLButton, int, JSColor, Task<object>> flash =
                 async (g, ms, c) =>
                 {
                     Console.WriteLine("flash 0");
                     g.style.backgroundColor = c;
                     await Task.Delay(ms);
                     Console.WriteLine("flash 100");
                     g.style.backgroundColor = JSColor.None;
                     await Task.Delay(ms);
                     Console.WriteLine("flash 200");

                     // script: error JSC1000: No implementation found for this native method, please implement 
                     // [System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1.SetException(System.Exception)]
                     return new object();
                 };


            page.Special.onclick +=
                delegate
                {
                    XState.Create(
                        //title: btn.innerText,
                        //url: "#" + btn.innerText,
                             value: new { button = page.Special.id, position = 0, seed = 0 },
                             invoke:
                                 async state =>
                                 {
                                     // how can we share the scope?
                                     // because this might be called from a .cctor

                                     Native.document.title = new { state.value.button }.ToString();
                                     var xbtn = (IHTMLButton)Native.document.getElementById(state.value.button);

                                     if (state)
                                     {
                                         System.Media.SystemSounds.Beep.Play();
                                         await xflash(xbtn, 250, JSColor.Cyan);
                                         //Console.Beep();
                                         await xflash(xbtn, 250, JSColor.Cyan);
                                     }
                                     var xpage = new App.FromDocument();



                                     xbtn.style.color = JSColor.Blue;
                                     xbtn.disabled = true;

                                     // how long are we in this state?

                                     //var value = state.value;

                                     //Console.WriteLine("lets start our streaming!");
                                     var seed = state.value.position;

                                     while (state)
                                     {
                                         // lets make a new state 
                                         //var position = value.position + 4;

                                         //Console.WriteLine("lets start our streaming! next!");

                                         state.value = new { state.value.button, position = state.value.position + 4, seed };
                                         xbtn.innerText = new { state.value.position }.ToString();
                                         await Task.Delay(1000);
                                     }
                                     // no way to get back to the state, without restarting

                                     //while (!state.GetAwaiter().IsCompleted)

                                     // paused!
                                     System.Media.SystemSounds.Beep.Play();
                                     //await flash(xbtn, 250, JSColor.Yellow);
                                     await xflash(xbtn, 250, JSColor.Yellow);
                                     //Console.Beep();
                                     await xflash(xbtn, 250, JSColor.Yellow);
                                     // should be a nop, yet wot work yets
                                     await state;

                                     xbtn.disabled = false;

                                 }
                         );
                };

            page.G.onclick +=
                async delegate
                {
                    page.G.disabled = true;


                    if (actions.All(x => x.btn.disabled))
                    {


                        System.Media.SystemSounds.Beep.Play();
                        await flash(page.G, 300, JSColor.Yellow);
                        //Console.Beep();
                        await flash(page.G, 300, JSColor.Yellow);


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
                        await flash(page.G, 300, JSColor.Yellow);
                        //Console.Beep();
                        await flash(page.G, 300, JSColor.Yellow);

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

    public class XState
    {
        public sealed class XVariation
        {
            public string id;

            public object value;

            public string invoke;

            public XVariation stack;

            public string title;
            public string url;
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

                var s = new XState.XVariation<object>(data,
                     delegate
                     {
                         Native.window.history.replaceState(
                             data,
                             data.title,
                             data.url
                         );
                     }
                );

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

        // Error	1	Cannot derive from 'T' because it is a type parameter	X:\jsc.svn\examples\javascript\AsyncHistoryExperiment\AsyncHistoryExperiment\Application.cs	255	38	AsyncHistoryExperiment
        public class XVariation<T>
        {
            //public int index;

            //public Action<XVariation<T>> invoke;

            readonly Action changed;


            public T value
            {
                get
                {
                    return (T)data.value;
                }
                set
                {
                    if (this)
                    {
                        // we can only make a change if we are the state

                        data.value = value;

                        if (changed != null)
                            changed();
                    }




                }
            }



            readonly XVariation data;
            readonly TaskCompletionSource<object> s;
            public string title { get { return this.data.title; } }

            public string url { get { return this.data.url; } }

            public static implicit operator bool(XVariation<T> x)
            {
                var state = ((XState.XVariation)Native.window.history.state);

                if (state == null)
                {
                    if (x == null)
                        return true;

                }
                else
                {
                    if (state.id == x.data.id)
                        return true;
                }

                return false;
            }

            public XVariation(XVariation data, Action changed)
            {
                this.data = data;

                //Native.window.history.replaceState(
                //              value,
                //              state.title,
                //              state.url
                //          );


                //this.url = url;
                //this.title = title;


                this.changed = changed;


                s = new TaskCompletionSource<object>();

                //Console.WriteLine("GetAwaiter");

                // GetAwaiter done { that = { index = 1 }, Previous = { index = -1 }, Current = { index = -1 } }
                // GetAwaiter done { that = { index = 1 }, Previous = { index = -1 }, Current = { index = -1 } }


                Console.WriteLine("await onpopstate " + data.id);

                var done = false;

                Action yield = delegate
                {
                    Console.WriteLine("GetAwaiter done  " + data.id);

                    s.SetResult(
                        new object()
                    );

                    done = true;
                };

                Native.window.onpopstate +=
                   e =>
                   {
                       if (done)
                           return;

                       var state = ((XState.XVariation)Native.window.history.state);

                       // GetAwaiter done { that = { index = 2 }, Previous = { index = 1 }, Current = { index = 1 } }

                       if (data.stack == null)
                       {
                           if (state == null)
                               yield();

                           return;
                       }

                       if (data.id == state.id)
                       {
                           // reloading ongoing state?
                           // let the old version die off while a new version is created
                           //yield();
                       }

                       if (data.stack.id == state.id)
                           yield();

                   };



            }

            public TaskAwaiter<object> GetAwaiter()
            {
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

                    stack = state,

                    title = title,
                    url = url
                };


            var s = new XVariation<T>(data,
                delegate
                {
                    Native.window.history.replaceState(
                        data,
                        data.title,
                        data.url
                    );
                }
            );

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
