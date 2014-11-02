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
using TaskAsyncTaskRun;
using TaskAsyncTaskRun.Design;
using TaskAsyncTaskRun.HTML.Pages;
using System.Threading;

namespace TaskAsyncTaskRun
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
            // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs
            // X:\jsc.svn\examples\javascript\async\Test\TestTaskRun\TestTaskRun\Application.cs

            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                //async 
                e =>
                {
                    e.Element.innerText = "working... ";

                    var bar = "xxx";


                    var t_Unwrap = Task.Run(async delegate
                    //var t = Task.Factory.StartNew(async delegate
                    {
                        // async worker0:45239ms  

                        Console.WriteLine("enter");
                        //Thread.SpinWait(
                        //Thread.Sleep(1000);
                        await Task.Delay(1000);

                        Console.WriteLine("exit");

                        return new
                        {
                            Thread.CurrentThread.ManagedThreadId,
                            //foo = 55
                            bar,

                        };
                    }
                    );

                    e.Element.innerText = "after StartNew...";

                    //t.Unwrap().ContinueWithResult(
                    t_Unwrap.ContinueWithResult(
                        r =>
                        {
                            e.Element.innerText = "done " + new { r.ManagedThreadId, r.bar };
                        }
                    );
                };

        }

    }
}
