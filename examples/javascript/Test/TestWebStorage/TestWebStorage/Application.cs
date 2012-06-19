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
using TestWebStorage.Design;
using TestWebStorage.HTML.Pages;
using ScriptCoreLib.JavaScript.StorageAPI;

namespace TestWebStorage
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
            Native.Window.alert(window.sessionStorage["foo"]);

            var now = DateTime.Now;
            window.sessionStorage["foo"] = now.ToString();

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
        public Storage sessionStorage;
        public Storage localStorage;
    }
}
