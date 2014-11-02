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
using WebMessagingExample.Design;
using WebMessagingExample.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace WebMessagingExample
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
        public Application(IDefault page)
        {
            if (Native.document.location.hash == "")
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
            }
            else
            {
                page.Content.innerText = Native.document.location.hash;

                Native.window.parent.With(
                    parent =>
                    {

                        new IHTMLButton { innerText = "send parent a message" }.With(
                            btn =>
                            {
                                btn.onclick +=
                                    delegate
                                    {

                                        parent.postMessage("hi from " + Native.document.location.hash);
                                    };
                            }
                        ).AttachToDocument();
                    }
                );
            }

            Native.window.onmessage +=
                e =>
                {
                    new IHTMLButton { innerText = e.data + " (click to reply)" }.With(
                        btn =>
                        {
                            if (Native.document.location.hash == "")
                                btn.style.color = JSColor.Blue;
                            else
                                btn.style.color = JSColor.Red;



                            btn.onclick +=
                                delegate
                                {
                                    btn.Orphanize();


                                    var WhoAmI = Native.document.location.hash;

                                    if (WhoAmI == "")
                                        WhoAmI = "parent";

                                    e.source.postMessage("this is a reply from " + WhoAmI);
                                };
                        }
                    ).AttachToDocument();

                };

        }

    }
}
