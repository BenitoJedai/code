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
using System.Diagnostics;

namespace TestTaskRunReturnToString
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        string bar = "xxx";

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

                    //                    0:2171ms Task scope { MemberName = bar, IsString = true, IsNumber = false, TypeIndex =  }
                    //0:2216ms Task scope { MemberName = __4__this, IsString = false, IsNumber = false, TypeIndex = type$C0HYdVszjTu8aXYpHQQHEA }
                    //var bar = "xxx";

                    var r = await Task.Run(async delegate
                    {
                        // given we are switching to backround thread here, what if we mirrored the state to server too,
                        // would be able to look at async stack?

                        var s = Stopwatch.StartNew();

                        this.data = "enter " + new { Thread.CurrentThread.ManagedThreadId, bar };
                        Console.WriteLine(new { this.data });

                        await this.WebMethod2();

                        //await Task.Delay(1000);

                        Console.WriteLine("exit " + new { Thread.CurrentThread.ManagedThreadId, bar });


                        return new
                        {
                            Thread.CurrentThread.ManagedThreadId,
                            //foo = 55
                            bar,

                            s.ElapsedMilliseconds
                        };
                    }
                    );


                    // can we have level1 typeinfo for us?
                    e.Element.innerText = "done " + new { r };
                    // yes it is there now.
                    // done {{ r = {{ ManagedThreadId = 10, bar = xxx }} }}
                };
        }

    }
}
