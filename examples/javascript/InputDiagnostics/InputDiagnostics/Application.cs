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
using InputDiagnostics.Design;
using InputDiagnostics.HTML.Pages;
using ScriptCoreLib.Shared.Drawing;

namespace InputDiagnostics
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
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );



            var x = new IHTMLDiv();

            var size = new
            {
                w = Native.Window.Width,
                h = Native.Window.Height
            };

            x.style.SetLocation(0, 0, size.w, size.h);

            x.style.backgroundColor = Color.Blue;
            x.style.color = Color.Yellow;
            x.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            x.AttachToDocument();

            Action<string> Append =
                e =>
                {
                    x.appendChild(new IHTMLDiv(e));

                    if (x.childNodes.Length > 20) x.removeChild(x.firstChild);

                };

            Append("size: " + size);

            Native.Document.onkeydown +=
                ev => Append("keydown: " + ev.KeyCode);


            Native.Document.onkeyup +=
                ev => Append("keyup: " + ev.KeyCode);

            Native.Document.onkeypress +=
                ev => Append("keypress: " + ev.KeyCode);


            Native.Document.onclick +=
                ev => Append("click: " + ev.OffsetPosition);

            Native.Document.body.ontouchstart +=
            ev =>
            {
                ev.PreventDefault();

                Append("ontouchstart: " + ev.touches.length);
            };



            Native.Document.body.ontouchmove +=
            ev =>
            {
                ev.PreventDefault();

                Append("ontouchmove: " + ev.touches.length + new { ev.touches[0].clientX, ev.touches[0].clientY });
            };

        }

    }
}
