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
using WebApplicationWindowMessaging.Design;
using WebApplicationWindowMessaging.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace WebApplicationWindowMessaging
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
        public Application(IDefaultPage page)
        {
            if (Native.Document.location.hash == "")
            {
                page.Content.innerText = "this page shall have child frames!";

                var a = new IHTMLIFrame
                {
                }.AttachToDocument();

                a.contentWindow.document.location.replace("#/child1");

                var b = new IHTMLIFrame
                {
                }.AttachToDocument();

                b.contentWindow.document.location.replace("#/child2");

                new IHTMLBreak().AttachToDocument();
                
                var i = 3;

                new IHTMLButton { innerText = "new child popup window" }.With(
                    btn =>
                    {
                        btn.onclick +=
                            delegate
                            {
                                var w = Native.Window.open(
                                    "#/child" + i,
                                    "_blank",
                                    400,
                                    300,
                                    false
                                );

                                i++;
                                w.onload +=
                                    delegate
                                    {
                                        w.postMessage("hi from " + Native.Document.location.hash);
                                    };
                            };
                    }
                ).AttachToDocument();


                new IHTMLButton { innerText = "new child tab window" }.With(
                    btn =>
                    {
                        btn.onclick +=
                            delegate
                            {
                                var w = Native.Window.open(
                                    "#/child" + i,
                                    "_blank"
                                );

                                i++;
                                w.onload +=
                                    delegate
                                    {
                                        w.postMessage("hi from " + Native.Document.location.hash);
                                    };
                            };
                    }
                ).AttachToDocument();


                new IHTMLBreak().AttachToDocument();
            }
            else
            {
                page.Content.innerText = Native.Document.location.hash;

                var target = Native.Window.opener;
 
                if (target == null)
                    target = Native.Window.parent;

                target.With(
                    parent =>
                    {

                        new IHTMLButton { innerText = "send parent a message" }.With(
                            btn =>
                            {
                                btn.onclick +=
                                    delegate
                                    {

                                        parent.postMessage("hi from " + Native.Document.location.hash);
                                    };
                            }
                        ).AttachToDocument();
                    }
                );
            }

            Native.Window.onmessage +=
                e =>
                {
                    new IHTMLButton { innerText = e.data + " (click to reply)" }.With(
                        btn =>
                        {
                            if (Native.Document.location.hash == "")
                                btn.style.color = JSColor.Blue;
                            else
                                btn.style.color = JSColor.Red;



                            btn.onclick +=
                                delegate
                                {
                                    btn.Orphanize();


                                    var WhoAmI = Native.Document.location.hash;

                                    if (WhoAmI == "")
                                        WhoAmI = "parent";

                                    e.source.postMessage("this is a reply from " + WhoAmI);
                                };
                        }
                    ).AttachToDocument();

                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
