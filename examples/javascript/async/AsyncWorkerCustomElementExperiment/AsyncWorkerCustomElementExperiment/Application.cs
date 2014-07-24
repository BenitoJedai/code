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
using AsyncWorkerCustomElementExperiment;
using AsyncWorkerCustomElementExperiment.Design;
using AsyncWorkerCustomElementExperiment.HTML.Pages;
using System.Threading;

namespace AsyncWorkerCustomElementExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        static Application()
        {
            // inside worker?
            if (Native.document == null)
                return;



            // http://dejanglozic.com/tag/shadow-dom/

            Native.document.registerElement("x-work",
                async e =>
                {
                    var s = e.createShadowRoot();


                    new IHTMLPre { "working... " + new { Thread.CurrentThread.ManagedThreadId } }.AttachTo(s);


                    new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachTo(s);

                    // first insertion point
                    new IHTMLContent { }.AttachTo(s);


                    new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachTo(s);



                    IProgress<string> worker_new_IHTMLPre = new Progress<string>(value => new IHTMLPre { value }.AttachTo(s));

                    var z = await Task.Run(
                        async delegate
                    {
                        // inside a worker thread now.

                        // can a worker talk to a shadow dom? :)
                        // perhaps if we expend our scope sharing?
                        // yet
                        // how would it look like to create a node in a worker?


                        //new IHTMLPre { "working... " + new { Thread.CurrentThread.ManagedThreadId } }.AttachTo(s);

                        for (int i = 0; i < 10; i++)
                        {
                            // look we are scope sharing
                            worker_new_IHTMLPre.Report(
                               "working... " + new { i, Thread.CurrentThread.ManagedThreadId }
                          );


                            await Task.Delay(500);
                            // did we resync scope fields?
                            // we got new data from attributes?
                        }




                        return 42;
                    }
                    );



                    new IHTMLPre { "working... done " + new { z } }.AttachTo(s);

                }
            );
        }

        public Application(IApp page)
        {
            new IHTMLButton { "create work" }.AttachToDocument().onclick +=
                delegate
            {
                new IHTMLElement("x-work") { "hello from client" }.AttachToDocument();
            };

            new IHTMLButton { "service work" }.AttachToDocument().onclick +=
      async delegate
            {
                await WebMethod2();
            };
        }

    }
}
