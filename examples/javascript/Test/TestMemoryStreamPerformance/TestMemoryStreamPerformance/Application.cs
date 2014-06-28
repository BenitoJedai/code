using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMemoryStreamPerformance;
using TestMemoryStreamPerformance.Design;
using TestMemoryStreamPerformance.HTML.Pages;

namespace TestMemoryStreamPerformance
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //  public static implicit operator IHTMLOutput(Task<Xml.Linq.XElement> x);
            //page.output =
            // script: error JSC1000: No implementation found for this native method, please implement [System.Threading.Tasks.TaskFactory.StartNew(System.Action)]

            // public static implicit operator IHTMLSpan(Task innerText);


            //
            page.output.css.empty.before.contentText = "working...";
            page.output.css.hover.style.color = "blue";
            // will this style get erased once worker resumes?


            //Native.document.onerr

            // could we send XAttributes to server?
            // could we send XAttributes to worker?
            // and enable two way binding

            // { ElapsedMilliseconds = 46, world = hello, Length = 524288 } 
            // { ElapsedMilliseconds = 436, world = hello, Length = 8388608 }
            // X:\jsc.svn\examples\javascript\test\TestTaskStartToString\TestTaskStartToString\Application.cs

            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                delegate
                {
                    page.output = Task.Factory.StartNew(
                        new { foo = "goo" },
                        scope =>
                        {
                            // Uncaught Error: InvalidOperationException: inline scope sharing not yet implemented 

                            Console.WriteLine(
                                //new { Thread.CurrentThread.ManagedThreadId, scope.foo }
                                new { Thread.CurrentThread.ManagedThreadId, scope }
                                );

                            // { ManagedThreadId = 10, foo = goo } 

                            var s = Stopwatch.StartNew();



                            // { ElapsedMilliseconds = 9, world = hello, Length = 131072 }
                            var length = 0x800000;
                            //var a = new byte[length];

                            // { ElapsedMilliseconds = 93, world = hello, Length = 32000 }
                            // { ElapsedMilliseconds = 1327, world = hello, Length = 131072 }

                            // { ElapsedMilliseconds = 5969, world = hello, Length = 262144 }
                            // { ElapsedMilliseconds = 5804, world = hello, Length = 262144 }
                            // { ElapsedMilliseconds = 16717, world = hello, Length = 262144 }
                            var m = new MemoryStream { Capacity = length * 2 };
                            // { ElapsedMilliseconds = 5, world = hello, Length = 262144 }
                            // { ElapsedMilliseconds = 377, world = hello, Length = 262144 }
                            // jsc, when will you learn to inline code?
                            // we need an inline rewriter?

                            // { ElapsedMilliseconds = 31, world = hello, Length = 262144 }
                            // { ElapsedMilliseconds = 27, world = hello, Length = 262144 }
                            // { ElapsedMilliseconds = 71, world = hello, Length = 524288 }

                            //var ii = 0;
                            //Action<byte> WriteByte = value =>
                            //{
                            //    a[ii] = value;
                            //    ii++;
                            //};

                            for (int i = 0; i < length; i++)
                            {
                                //a[i] = ((byte)(i % 32));
                                //WriteByte((byte)(i % 32));
                                m.WriteByte((byte)(i % 32));
                            }

                            // [object Object]
                            // since, jsc is not adding type serialization info here yet
                            // it will be serialized via JSON and will lose information now.

                            // als what if we wanted to return XML here?
                            // for DOM?
                            // or listen for DOM events?
                            // or talk to this.WebMethod?
                            return new
                            {
                                s.ElapsedMilliseconds,
                                world = "hello",

                                m.Length
                                //a.Length

                            }.ToString();
                        }
                    );
                };

        }

    }
}
