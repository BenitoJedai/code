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
using TestHistoryForwardEvent;
using TestHistoryForwardEvent.Design;
using TestHistoryForwardEvent.HTML.Pages;

namespace TestHistoryForwardEvent
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
            //  Native.window.history.pushState(
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140517

            page.State2.onclick +=
                e =>
                {
                    e.preventDefault();

                    //Native.window.history.pushState(

                    HistoryExtensions.pushState(
                        Native.window.history,
                        new { foo = "foo" },
                        scope =>
                        {
                            Console.WriteLine("enter State2");

                            Native.css.style.backgroundColor = "yellow";

                            scope.TaskCompletionSource.Task.ContinueWith(
                                delegate
                                {
                                    Console.WriteLine("exit State2");
                                    Native.css.style.backgroundColor = "cyan";
                                }
                            );

                        }
                    );
                };

            page.State1.Historic(
                async scope =>
                {
                    Native.css.style.backgroundColor = "yellow";

                    await scope;

                    Native.css.style.backgroundColor = "cyan";
                }
            );

        }

    }
}
