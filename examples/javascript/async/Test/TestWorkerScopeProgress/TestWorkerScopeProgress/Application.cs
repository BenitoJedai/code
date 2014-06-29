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
using TestWorkerScopeProgress;
using TestWorkerScopeProgress.Design;
using TestWorkerScopeProgress.HTML.Pages;
using System.Threading;

namespace TestWorkerScopeProgress
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140629
            // can we get support for any level1 scope progress elements?
            // after that we may want to allow Tasks being sent back to the worker.
            // what about sharing static bools too. we already share strings..
            // what about synchronizing scope objects once worker is running?
            // a Thread Signal or Task Yield may request the sync to take place.
            // this would allow data sharing during computation?
            // then we may also want to get delegate sharing..


            //0:6113ms Task scope { MemberName = progress, IsString = false, IsNumber = false, TypeIndex = type$Iuc7fw31uTShD4lFrT9xIg }
            //0:6161ms Task scope { MemberName = CS___9__CachedAnonymousMethodDelegate6, IsString = false, IsNumber = false, TypeIndex = type$P_aMwuiDRzTKOqkDQd7BGAw }

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
            IProgress<string> progress = new Progress<string>(
                x =>
                {
                    new IHTMLPre {
                        new { x, Thread.CurrentThread.ManagedThreadId }
                    }.AttachToDocument();
                }
            );



            Native.css.style.transition = "background-color 300ms linear";

            // future jsc will allow a background thread to directly talk to the DOM, while creating a callsite in the background
            IProgress<string> set_backgroundColor = new Progress<string>(
                x =>
                {
                    Native.css.style.backgroundColor = x;
                }
            );


            var foo = "foo";

            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                async e =>
                {
                    progress.Report("UI " + new { foo, Thread.CurrentThread.ManagedThreadId });

                    await Task.Run(async delegate
                    {
                        Console.WriteLine("inside worker " + new { foo, Thread.CurrentThread.ManagedThreadId });

                        // we could also support direct delegates?
                        progress.Report("inside worker " + new { foo, Thread.CurrentThread.ManagedThreadId });

                        set_backgroundColor.Report("yellow");

                        // this will confuse task to be Task<?> ?
                        await Task.Delay(1300);

                        set_backgroundColor.Report("cyan");

                        Console.WriteLine("exit worker " + new { foo, Thread.CurrentThread.ManagedThreadId });

                        // we could also support direct delegates?
                        progress.Report("exit worker " + new { foo, Thread.CurrentThread.ManagedThreadId });
                    });
                };

        }
    }
}
