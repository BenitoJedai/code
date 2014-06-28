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
using TestTaskRunReturnToString;
using TestTaskRunReturnToString.Design;
using TestTaskRunReturnToString.HTML.Pages;
using System.Threading;

namespace TestTaskRunReturnToString
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
            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                async e =>
                {
                    e.Element.innerText = "working... " + new { Thread.CurrentThread.ManagedThreadId };


                    var bar = "xxx";

                    var r = await Task.Run(async delegate
                    {
                        Console.WriteLine("enter " + new { bar });

                        await Task.Delay(1000);

                        Console.WriteLine("exit " + new { bar });

                        return new
                        {
                            Thread.CurrentThread.ManagedThreadId,
                            //foo = 55
                            bar,
                        };
                    }
                    );


                    // can we have level1 typeinfo for us?
                    e.Element.innerText = "done " + new { r };
                };
        }

    }
}
