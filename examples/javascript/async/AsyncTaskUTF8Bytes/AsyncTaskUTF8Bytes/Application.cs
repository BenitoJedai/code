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
using AsyncTaskUTF8Bytes;
using AsyncTaskUTF8Bytes.Design;
using AsyncTaskUTF8Bytes.HTML.Pages;
using System.Threading;

namespace AsyncTaskUTF8Bytes
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
            Task.Factory.StartNew(
                new { data = "£" },
                scope =>
                {
                    var bytes = Encoding.UTF8.GetBytes(scope.data);

                    var w = "";

                    foreach (var item in bytes)
                    {
                        w += "0x" + item.ToString("x2") + " ";
                    }

                    return w + new { Thread.CurrentThread.ManagedThreadId };
                }
            ).ContinueWithResult(
                r =>
                {
                    new IHTMLPre { r + new { Thread.CurrentThread.ManagedThreadId } }.AttachToDocument();
                }
            );

        }

    }
}
