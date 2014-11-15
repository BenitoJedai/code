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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestHopToThreadPoolAwaitable;
using TestHopToThreadPoolAwaitable.Design;
using TestHopToThreadPoolAwaitable.HTML.Pages;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;

namespace TestHopToThreadPoolAwaitable
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToThreadPoolAwaitable : INotifyCompletion
    {
        public HopToThreadPoolAwaitable GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }

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
#if x
            new IHTMLButton { "invoke" }.AttachToDocument().onclick += async delegate
            {
                await this.WebMethod2(
                    @"hi " + new { Thread.CurrentThread.ManagedThreadId }
                );
            };

            new IHTMLButton { "invoke Task.Run" }.AttachToDocument().onclick += async delegate
            {
                await Task.Run(
                    async delegate
                {
                    await this.WebMethod2(
                        @"hi " + new { Thread.CurrentThread.ManagedThreadId }
                    );
                }
                );
            };

            new IHTMLButton { "invoke Task.Run Action" }.AttachToDocument().onclick += async delegate
            {
                // script: error JSC1000: No implementation found for this native method, please implement [static System.Threading.Tasks.Task.Run(System.Action)]

                await Task.Run(
                     delegate
                {
                    this.WebMethod2(
                       @"hi " + new { Thread.CurrentThread.ManagedThreadId }
                   );
                }
                );
            };

            //Stopwatch

            new IHTMLButton { "invoke HopToThreadPoolAwaitable" }.AttachToDocument().onclick += async delegate
            {

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141112


                // script: error JSC1000: No implementation found for this native method, please implement 
                // [System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitOnCompleted(
                // TestHopToThreadPoolAwaitable.HopToThreadPoolAwaitable&, TestHopToThreadPoolAwaitable.Application+<<_ctor>b__5>d__0&)]

                // Uncaught Error: InvalidOperationException: { MethodToken = nCcABsAxbTiq5EFPbq1SVA } function is not available at { href = https://192.168.43.252:18810/view-source#worker }

                var fromUI = "hello world" + new { Thread.CurrentThread.ManagedThreadId };

                // what types can we transmit from UI to worker?
                await default(HopToThreadPoolAwaitable);
                // the ultimate scope sharing
                // isnt this the same exact problem we already solved with client transport helper?

                // xml api is missing over here!
                // yet we have XElement sync on fields with the service?

                // can we create sub tasks? and wait for them?
                var sw = Stopwatch.StartNew();

                await this.WebMethod2(
                       @"hi " + new { Thread.CurrentThread.ManagedThreadId, fromUI }
                );

                // how can we hop back to UI?
                Console.WriteLine("done! " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });

                //  0:9585ms done! {{ ManagedThreadId = 10, ElapsedMilliseconds = 547 }}
            };
            #endif


            new IHTMLButton { "invoke HopToThreadPoolAwaitable" }.AttachToDocument().onclick += async delegate
            {
                var fromUI = "hello world" + new { Thread.CurrentThread.ManagedThreadId };

                await default(HopToThreadPoolAwaitable);

                var sw = Stopwatch.StartNew();

                await this.WebMethod2(
                       @"hi " + new { Thread.CurrentThread.ManagedThreadId, fromUI }
                );

                Console.WriteLine("done! " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });
            };
        }

    }
}
