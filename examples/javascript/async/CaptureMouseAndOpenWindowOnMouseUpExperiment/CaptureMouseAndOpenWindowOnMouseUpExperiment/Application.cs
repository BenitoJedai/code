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
using CaptureMouseAndOpenWindowOnMouseUpExperiment.Design;
using CaptureMouseAndOpenWindowOnMouseUpExperiment.HTML.Pages;

namespace CaptureMouseAndOpenWindowOnMouseUpExperiment
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

            page.DragMe.onmousedown +=
                async e =>
                {
                    e.CaptureMouse();
                    page.DragMe.style.color = "red";

                    //new IHTMLPre { () => await page.DragMe.async.onmousemove }.AttachToDocument();

                    var up = await page.DragMe.async.onmouseup;

                    page.DragMe.style.color = "";

                    var w = new IWindow();

                    await w.async.onload;

                    w.document.title = "new IWindow";

                    w.moveTo(
                        up.CursorX,
                        up.CursorY
                    );


                };


        }

    }
}
