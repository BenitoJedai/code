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
using TestWebMessagingAPI.Design;
using TestWebMessagingAPI.HTML.Pages;
using ScriptCoreLib.JavaScript.MessagingAPI;
using ScriptCoreLib.JavaScript.Runtime;

namespace TestWebMessagingAPI
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
                    src = "#/child1"
                }.AttachToDocument();

                var b = new IHTMLIFrame
                {
                    src = "#/child2"
                }.AttachToDocument();

                new IHTMLBreak().AttachToDocument();
            }
            else
            {
                page.Content.innerText = Native.Document.location.hash;

                window.parent.With(
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

            window.onmessage +=
                e =>
                {
                    new IHTMLButton { innerText = e.data + " (click to reply)" }.With(
                        btn =>
                        {
                            btn.style.color = JSColor.Blue;



                            btn.onclick +=
                                delegate
                                {
                                    btn.Orphanize();

                                    var source = (XWindow)(object)e.source;

                                    var WhoAmI = Native.Document.location.hash;

                                    if (WhoAmI == "")
                                        WhoAmI = "parent";

                                    source.postMessage("this is a reply from " + WhoAmI);
                                };
                        }
                    ).AttachToDocument();

                    //Native.Window.alert("window.onmessage: " + e.data);
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }


        [Script(ExternalTarget = "window")]
        static XWindow window;
    }


    [Script(HasNoPrototype = true)]
    class XWindow : IWindow
    {

        public XWindow parent;

        public void postMessage(object message, string targetOrigin = "*")
        {
            // http://www.whatwg.org/specs/web-apps/current-work/#the-window-object
        }

        #region event
        public event System.Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion
    }
}
