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
using LoadExternalFlashComponent.Components;
using LoadExternalFlashComponent.HTML.Pages;

namespace LoadExternalFlashComponent
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
        public Application(IDefaultPage page)
        {
            // Initialize MySprite1
            var s = new MySprite1();

            s.Inspecting +=
                doc =>
                {
                    new IHTMLPre { innerText = doc.ToString() }.AttachTo(page.Content);
                };

            var Inspect = new IHTMLButton("Inspect").AttachTo(page.Content);
            
            Inspect.onclick +=
                delegate
                {
                    s.Inspect();
                };

            s.AttachSpriteTo(page.Content);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
