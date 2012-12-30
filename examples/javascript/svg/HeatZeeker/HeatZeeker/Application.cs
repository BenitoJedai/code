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
using HeatZeeker.Design;
using HeatZeeker.HTML.Pages;

namespace HeatZeeker
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
            var h = new Hind();

            h.Container.AttachToDocument();

            h.Container.style.SetLocation(200, 200);

            var r = 0.0;
            var t = new IDate().getTime();

            Action loop = null;

            loop = delegate
            {
                var tt = new IDate().getTime();
                var dt = tt - t;
                t = tt;

                r += double.Parse(page.range.value) * 0.01 * dt;

                //page.wings.style.transform = " rotate(" + r + "deg)";
                h.wings.style.transform = " rotate(" + r + "deg)";

                Native.Window.requestAnimationFrame += loop;
            };

            Native.Window.requestAnimationFrame += loop;

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
