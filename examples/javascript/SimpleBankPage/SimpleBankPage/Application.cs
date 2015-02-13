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
using SimpleBankPage.Design;
using SimpleBankPage.HTML.Pages;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript.Runtime;

namespace SimpleBankPage
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
            IHTMLDiv Control = new IHTMLDiv();


            Control.AttachToDocument();

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "This page will ask you to confirm in order to unload the page"));

            var check = new IHTMLInput(HTMLInputTypeEnum.checkbox).AttachToDocument();
            var label = new IHTMLLabel("Bypass check", check).AttachToDocument();



            Native.window.onbeforeunload +=
                delegate (IWindow.Confirmation ev)
                {

                    Timer.DoAsync(
                        delegate
                        {
                            Native.document.body.style.backgroundColor = JSColor.Red;


                            new Timer((t) => Native.document.body.style.backgroundColor = JSColor.White, 500, 0);
                        }
                    );

                    if (check.@checked)
                        return;

                    ev.Text = "This is a secure website, do you want to leave?";
                };

            var anchor = new IHTMLAnchor("http://example.com", "example.com");

            anchor.target = "_self";

            Control.appendChild(anchor);


        }

    }
}
