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
using AndroidHopToActivityThread;
using AndroidHopToActivityThread.Design;
using AndroidHopToActivityThread.HTML.Pages;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AndroidHopToActivityThread
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

            new IHTMLButton { "invoke HopToThreadPoolAwaitable" }.AttachToDocument().onclick += async delegate
            {
                var fromUI = "hello world" + new { Thread.CurrentThread.ManagedThreadId };

                // on android this seems very slow
                await default(HopToThreadPoolAwaitable);

                var sw = Stopwatch.StartNew();

                await this.WebMethod2(
                       @"hi " + new { Thread.CurrentThread.ManagedThreadId, fromUI }
                );

                Console.WriteLine("done! " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });

                // could we jump back to ui?

            };
        }

    }


    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    struct HopToThreadPoolAwaitable : INotifyCompletion
    {
        public HopToThreadPoolAwaitable GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }
}
