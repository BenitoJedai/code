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
using AsyncInlineWorkerDocumentExperiment;
using AsyncInlineWorkerDocumentExperiment.Design;
using AsyncInlineWorkerDocumentExperiment.HTML.Pages;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;

namespace AsyncInlineWorkerDocumentExperiment
{
    public class async<T>
    {
        public T value;


        public Action<T> SetResult = delegate { };

        public TaskAwaiter<T> GetAwaiter()
        {
            var x = new TaskCompletionSource<T>();

            SetResult += x.SetResult;


            return x.Task.GetAwaiter();
        }

        public static implicit operator async<T>(T t)
        {
            return new async<T> { value = t };
        }
    }

    public class IHTMLElement
    {
        public string id;
    }

    public class IHTMLDocument
    {
        public MessagePort port;

        public async<IHTMLElement> createElement(string e)
        {
            var z = new async<IHTMLElement> { };

            Action<string, Action<string>> __createElement =
                (xvalue, yield) =>
                {
                    var x = Native.document.createElement(xvalue);

                    x.EnsureID();
                    x.innerText = new { x.id }.ToString();
                    x.AttachToDocument();

                    yield(x.id);
                };

            port.postMessage(
                new
                {
                    ((__MethodInfo)__createElement.Method).MethodToken,
                    value = e
                },
                new Action<string>(
                    yield =>
                    {
                        var n = new IHTMLElement { id = yield };

                        z.SetResult(n);
                    }
                )
            );

            return z;
        }

        public async<string> title
        {
            set
            {
                var x = value.value;

                Console.WriteLine("set title " + new { System.Threading.Thread.CurrentThread.ManagedThreadId });


                Action<string, Action<string>> set_title =
                    (xvalue, yield) =>
                    {
                        Console.WriteLine("set title " + new { System.Threading.Thread.CurrentThread.ManagedThreadId });

                        Native.document.title = xvalue;
                    };

                port.postMessage(
                    new
                    {
                        ((__MethodInfo)set_title.Method).MethodToken,
                        value = x
                    }
                );
            }
            get
            {
                Console.WriteLine("get title" + new { System.Threading.Thread.CurrentThread.ManagedThreadId });

                var z = new async<string> { };

                //new Timer(
                //    delegate
                //    {
                //        Console.WriteLine("get title done");
                //        z.SetResult("xxx");
                //    }
                //).StartTimeout(300);


                Action<string, Action<string>> get_title =
                    (xvalue, yield) =>
                    {
                        Console.WriteLine("get title " + new { System.Threading.Thread.CurrentThread.ManagedThreadId });

                        yield(Native.document.title);
                    };

                port.postMessage(
                    new
                    {
                        ((__MethodInfo)get_title.Method).MethodToken,
                        value = default(object)
                    },
                    new Action<string>(
                        yield =>
                        {
                            Console.WriteLine("get title done" + new { System.Threading.Thread.CurrentThread.ManagedThreadId });
                            z.SetResult(yield);
                        }
                    )
                );

                return z;
            }
        }
    }

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
            var w = new Worker(
                worker =>
                {
                    // running in worker context. cannot talk to outer scope yet.


                    worker.onmessage +=
                        async x =>
                        {
                            var slave = x.ports.First();

                            // see also
                            // X:\jsc.svn\core\ScriptCoreLib.Document\ScriptCoreLib.Document\ActionScript\DOM\ExternalContext.cs

                            var document = new IHTMLDocument { port = slave };


                            var title = await document.title;

                            document.title = "worker: " + new { title };

                            // could we hijack DOM api?
                            // could jsc switch to async on its own?
                            // what if this was the server?
                            //var span = new IHTMLSpan("goo");

                            var div1 = await document.createElement("p");

                            Console.WriteLine(new { div1 = new { div1.id } });

                            var div2 = await document.createElement("h1");

                            Console.WriteLine(new { div2 = new { div2.id } });

                            var div3 = await document.createElement("button");

                            Console.WriteLine(new { div3 = new { div3.id } });


                        };


                }
            );

            #region slave
            w.postMessage(
                "slave",
                x =>
                {
                    dynamic z = x.data;

                    string MethodToken = z.MethodToken;
                    object value = z.value;


                    Console.WriteLine("slave " + new { MethodToken, value, System.Threading.Thread.CurrentThread.ManagedThreadId });

                    IFunction.Of(MethodToken).apply(null,
                        args: new object[] { value, 


                            new Action<object>(
                                yield =>
                                {
                                    Console.WriteLine("slave yield " + new { MethodToken, value, System.Threading.Thread.CurrentThread.ManagedThreadId });

                                    x.ports.First().postMessage(yield);

                                }
                            )
                        }
                    );


                }
            );
            #endregion

        }

    }
}
