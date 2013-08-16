using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EventSourceForWebServiceYield.Design;
using EventSourceForWebServiceYield.HTML.Pages;

namespace EventSourceForWebServiceYield
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
            new Abstractatech.ConsoleFormPackage.Library.ConsoleForm { }.InitializeConsoleFormWriter().Show();



            page.Invoke.onclick +=
                delegate
                {
                    var now = DateTime.Now;

                    var n = new XElement("client",
                        new XAttribute("value", "" + now)
                    );

                    service.Invoke(
                        n,
                        xml =>
                        {
                            Console.WriteLine(xml.ToString());
                            //new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.right);
                        }
                    );
                };

            page.InvokeSpecal.onclick +=
                delegate
                {
                    var now = DateTime.Now;

                    var n = new XElement("client",
                        new XAttribute("value", "" + now)
                    );

                    service.InvokeSpecial(
                        n,
                        xml =>
                        {
                            Console.WriteLine(xml.ToString());
                            //new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.left);
                        }
                    );
                };

            page.InvokeSpecalInsideWorker.onclick +=
                delegate
                {
                    Console.WriteLine("InvokeSpecalInsideWorker");

                    var ww = new Worker(
                       worker =>
                       {
                           // running in worker context. cannot talk to outer scope yet.

                           worker.RedirectConsoleOutput();
                           Console.WriteLine("at worker");


                           var now = DateTime.Now;

                           var n = "<client  value='now' />";


                           var xservice = new ApplicationWebService();

                           Console.WriteLine("before InvokeSpecialString");
                           xservice.InvokeSpecialString(
                               n,
                               xml =>
                               {
                                   Console.WriteLine("at InvokeSpecialString");

                                   Console.WriteLine(xml);
                                   //new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.left);
                               }
                           );
                       }
                   );


                    ww.onmessage +=
                        e =>
                        {
                            Console.Write("" + e.data);
                        };
                };



            page.Clear.onclick +=
                delegate
                {
                    // Error	1	The call is ambiguous between the following methods or properties: 
                    // 'ScriptCoreLib.JavaScript.Extensions.INodeExtensions.Clear(ScriptCoreLib.JavaScript.DOM.INode)' and
                    // 'ScriptCoreLib.JavaScript.Extensions.INodeExtensions.Clear(ScriptCoreLib.JavaScript.DOM.INode)'	X:\jsc.svn\examples\javascript\EventSourceForWebServiceYield\EventSourceForWebServiceYield\Application.cs	71	21	EventSourceForWebServiceYield


                    //page.left.Clear();
                    //page.right.Clear();
                };
        }

    }
}
