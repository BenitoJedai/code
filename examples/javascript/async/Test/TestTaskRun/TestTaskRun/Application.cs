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
using TestTaskRun;
using TestTaskRun.Design;
using TestTaskRun.HTML.Pages;
using System.Threading;

namespace TestTaskRun
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
            // 'window.webkitStorageInfo' is deprecated. Please use 'navigator.webkitTemporaryStorage' or 'navigator.webkitPersistentStorage' instead.


            var foo = 500;


            new IHTMLButton { "invoke" }.AttachToDocument().onclick +=
                //async 
                e =>
                {
                    // 0:4413ms e: <Namespace>. 
                    //Console.WriteLine("e: " + e.GetType());

                    e.Element.innerText = "working... ";

                    //0:5147ms __Task will share scope, field scope1 view - source:40679
                    //0:5148ms __Task will share scope, field e

                    var bar = "xxx";
                    var row = new xRow();



                    //var t = Task.Run(delegate
                    var t = Task.Factory.StartNew(delegate
                    {
                        return new
                        {
                            Thread.CurrentThread.ManagedThreadId,
                            //foo = 55
                            foo,
                            bar,
                            row,

                            this.Field1


                            //foo = scope1.foo + 1
                        };
                    }
                    );

                    e.Element.innerText = "working...";

                    t.ContinueWithResult(
                        r =>
                        {
                            e.Element.innerText = "done " + new { r.ManagedThreadId, r.foo, r.bar, r.Field1 };
                        }
                    );
                };


        }

    }

    public class xRow
    {
    }
}
