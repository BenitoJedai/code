using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestThreadStart;
using TestThreadStart.Design;
using TestThreadStart.HTML.Pages;

namespace TestThreadStart
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
            // http://qscribble.blogspot.com/2012/07/asyncawait-vs-stack-switching.html
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130825

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Delegate.cs

            Action yield = delegate
            {
                Console.WriteLine("more work");
            };

            // script: error JSC1000: No implementation found for this native method, please implement [System.Delegate.get_Target()]
            Console.WriteLine(
                new { yield.Target, yield.Method }
            );

            new Worker(
                 worker =>
                 {
                     // running in worker context. cannot talk to outer scope yet.

                     Console.WriteLine("working ...");
                     var s = new Stopwatch();
                     s.Start();

                     // spin the cpu 
                     // how long do we have to, in order for task manager to notice?
                     // this should keep one cpu utilized atleast at 70%
                     while (s.ElapsedMilliseconds < 5000) ;

                     Console.WriteLine("working ... done " + new { s.Elapsed });
                 }
             );
        }

    }
}
