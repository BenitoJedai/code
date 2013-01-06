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
using HeatZeeker.Units.Hind.Tester.Design;
using HeatZeeker.Units.Hind.Tester.HTML.Pages;

namespace HeatZeeker.Units.Hind.Tester
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
            Native.Document.body.onclick +=
                e =>
                {
                    var layout = new global::HeatZeeker.Units.Hind.HTML.Pages.App();

                    var c = new global::HeatZeeker.Units.Hind.Application(layout);

                    layout.Container.AttachToDocument();
                    layout.Container.style.SetLocation(e.CursorX - 200, e.CursorY - 200);
                };

        }

    }
}
