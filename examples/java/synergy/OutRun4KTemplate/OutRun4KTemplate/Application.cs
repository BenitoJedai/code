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
//using OutRun4KTemplate.Components;
using OutRun4KTemplate.HTML.Pages;

namespace OutRun4KTemplate
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // Initialize MyApplet1
            var a = new OutRun4KTemplate_Components_MyApplet1();

            a.AttachAppletTo(page.Content);

            //var btn = new IHTMLButton("get the damn string");

            //btn.AttachToDocument();

            //btn.onclick +=
            //    delegate
            //    {
            //        Native.Window.alert(a.FooMethodX());
            //    };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
