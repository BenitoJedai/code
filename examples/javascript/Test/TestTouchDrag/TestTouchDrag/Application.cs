using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestTouchDrag.Design;
using TestTouchDrag.HTML.Pages;

namespace TestTouchDrag
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
            var caption_foreground = new IHTMLDiv();

            caption_foreground.style.backgroundColor = JSColor.Red;
            caption_foreground.style.SetLocation(100, 100, 100, 100);
            caption_foreground.AttachToDocument();

            var drag = new ScriptCoreLib.JavaScript.Controls.DragHelper(caption_foreground);

            drag.Position = new Point(100, 100);
            // http://forum.mootools.net/topic.php?id=534
            // disable text selection
            // look at http://forkjavascript.com/

            drag.Enabled = true;

            drag.DragStart +=
                delegate
                {
                    caption_foreground.style.backgroundColor = JSColor.Yellow;
                };
            drag.DragMove +=
                delegate
                {
                    caption_foreground.style.backgroundColor = JSColor.Green;
                    caption_foreground.style.SetLocation(drag.Position.X, drag.Position.Y);
                };
            drag.DragStop +=
            delegate
            {
                caption_foreground.style.backgroundColor = JSColor.Blue;
            };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
