using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeAppWindowMouseCapture;
using ChromeAppWindowMouseCapture.Design;
using ChromeAppWindowMouseCapture.HTML.Pages;

namespace ChromeAppWindowMouseCapture
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
        public Application(IApp page)
        {
            // how are we to make this into a chrome app?



            Native.body.css.style.backgroundColor = "transparent";
            Native.body.css.style.transition = "background 500ms linear";

            Native.body.css.active.style.backgroundColor = "yellow";

            Native.document.documentElement.style.cursor = IStyle.CursorEnum.move;

            Native.body.onmousemove +=
                e =>
                {
                    // we could tilt the svg cursor
                    // like we do on heat zeeker:D


                    Native.document.title = new { e.CursorX, e.CursorY }.ToString();

                };

            Native.body.onmousedown +=
                async e =>
                {
                    e.CaptureMouse();

                    await Native.body.async.onmouseup;
                };
        }

    }
}
