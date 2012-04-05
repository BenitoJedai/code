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
using TestAsyncFeatures.Design.Styles;
using TestAsyncFeatures.HTML.Pages;

namespace AsyncResearch
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
         public Application(IDefaultPage page)
        {
            style.Content.AttachToHead();
            @"before".ToDocumentTitle();

            Initialize();

            @"after".ToDocumentTitle();

        }

        async void Initialize()
        {
            // Send data from JavaScript to the server tier
            var value = await service.WebMethod0("in");

            value.ToDocumentTitle();

        }
    }
}
