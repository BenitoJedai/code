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
using AsyncComputeAndThenCallServer;
using AsyncComputeAndThenCallServer.Design;
using AsyncComputeAndThenCallServer.HTML.Pages;
using System.Threading;

namespace AsyncComputeAndThenCallServer
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
                    this.Field1 = "working... " + new { UI = Thread.CurrentThread.ManagedThreadId };
                    e.Element.innerText = this.Field1;

                    var z = await Task.Factory.StartNew(
                        this,
                        scope =>
                        {
                            // what if this already were running on the server, based on what we tried to do?



                            scope.Field1 += new { Worker = Thread.CurrentThread.ManagedThreadId }.ToString();

                            // can we send it in? no xml in worker!
                            // Uncaught Error: InvalidOperationException: { MethodToken = AgAABs2SLzS_a1H0cJOBo7A } function is not available at { hre
                            //this.WebMethod2();


                            // UploadValuesAsync { status = 204, responseType = arraybuffer }0:2277ms 
                            scope.WebMethod2().ContinueWith(
                                delegate
                            {
                                // worker got async anwser0:7991ms  
                                Console.WriteLine("worker got async anwser");
                            }
                            );



                            return "ok " + new { scope.Field1 };
                        }
                    );

                    // was the state reflected back into UI?

                    e.Element.innerText = "done " + this.Field1 + new { z };
                };
        }

    }
}
