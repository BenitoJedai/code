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
using TestTaskDelay2;
using TestTaskDelay2.Design;
using TestTaskDelay2.HTML.Pages;
using System.Threading;
using System.Diagnostics;

namespace TestTaskDelay2
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
            new { }.With(
                async delegate
            {
                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

                var sw = Stopwatch.StartNew();

                // access UI controls
                await Task.Delay(100);
                // 0:220ms {{ ManagedThreadId = 1, ElapsedMilliseconds = 115 }} 

                // DO NOT access UI controls here, as you're very likely on a ThreadPool thread
                Console.WriteLine("Back!");
                Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });
            }
            );
        }

    }
}
