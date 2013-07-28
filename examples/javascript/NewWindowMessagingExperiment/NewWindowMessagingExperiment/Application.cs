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
using NewWindowMessagingExperiment.Design;
using NewWindowMessagingExperiment.HTML.Pages;

namespace NewWindowMessagingExperiment
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/2013

            Native.Window.onmessage +=
               m =>
               {
                   // { data = hi from newwindow handler, origin = chrome-extension://cbbhdbeoonaodjnocnepmnidolnhaepp }

                   // cannot look at m.source
                   //new IHTMLDiv { innerText = new { m.data, m.origin, m.source }.ToString() }.AttachToDocument();
                   new IHTMLDiv { innerText = new { m.data, m.origin, ports = m.ports.Length }.ToString() }.AttachToDocument();

                   //m.ports.WithEach(
                   //    port =>
                   //    {
                   //        //port.postMessage("fast reply1 to: " + new { m.data }, m.origin);
                   //        port.postMessage("fast reply2 to: " + new { m.data, m.origin }, null);
                   //    }
                   // );

                   //m.source.postMessage("fast reply1 to: " + new { m.data });



                   Action<XElement> postMessage =
                       x =>
                       {
                           Console.WriteLine(new { x });

                           var data = Native.Window.escape(x.ToString());

                           var w = Native.Window.open("http://hack-wtf-postmessage/" + data);
                       };


                   postMessage(new XElement("frame0", new XAttribute("xx", "yy")));
                   postMessage(new XElement("frame1", new XAttribute("xx", "yy")));
                   postMessage(new XElement("frame2", new XAttribute("xx", "yy")));
                   postMessage(new XElement("frame3", new XAttribute("xx", "yy")));
               };

            page.Go.onclick +=
                delegate
                {
                    var w = new IWindow();

                    w.onbeforeunload +=
                        delegate
                        {
                            new IHTMLDiv { innerText = "onbeforeunload" }.AttachToDocument();
                        };

                    w.onunload +=
                        delegate
                        {
                            new IHTMLDiv { innerText = "onunload" }.AttachToDocument();
                        };


                    w.onmessage +=
                        m =>
                        {
                            // { data = hi from newwindow handler, origin = chrome-extension://cbbhdbeoonaodjnocnepmnidolnhaepp }

                            // cannot look at m.source
                            //new IHTMLDiv { innerText = new { m.data, m.origin, m.source }.ToString() }.AttachToDocument();
                            new IHTMLDiv { innerText = new { m.data, m.origin }.ToString() }.AttachToDocument();
                            //w.status = new { m.data, m.origin }.ToString();

                            try
                            {
                                //new IHTMLDiv { innerText = new { m.data, m.origin, w.opener, w.parent, w.top }.ToString() }.AttachToDocument();
                            }
                            catch
                            {
                            }

                            var c = 0;

                            new IHTMLButton("reply").AttachTo(w.document.body).WhenClicked(
                                btn =>
                                {
                                    c++;

                                    m.source.postMessage("reply to: " + new { c, m.data }, m.origin);
                                    new IHTMLDiv { innerText = "reply sent! do you see it?" }.AttachToDocument();

                                    //btn.Orphanize();
                                }
                            );
                            m.source.postMessage("fast reply1 to: " + new { c, m.data }, m.origin);
                            m.source.postMessage("fast reply2 to: " + new { c, m.data });

                        };

                    w.onload +=
                        delegate
                        {
                            w.document.title = "new IWindow";

                            w.status = "new IWindow";
                            new IHTMLDiv { innerText = "onload" }.AttachToDocument();

                        };
                };
        }

    }
}
