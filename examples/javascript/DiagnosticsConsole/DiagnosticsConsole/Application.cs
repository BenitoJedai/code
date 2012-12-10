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
using DiagnosticsConsole.Design;
using DiagnosticsConsole.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace DiagnosticsConsole
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
            var diagnostics = new IHTMLIFrame { src = "/jsc", frameBorder = "0" };

            diagnostics.style.backgroundColor = "rgba(255, 255, 255, 0)";
            diagnostics.style.position = IStyle.PositionEnum.absolute;
            diagnostics.style.left = "0px";
            diagnostics.style.top = "-100%";
            diagnostics.style.width = "100%";
            diagnostics.style.height = "100%";
            diagnostics.AttachToDocument();

            // http://www.w3schools.com/css3/css3_transitions.asp
            diagnostics.style.With(
                       (dynamic s) => s.webkitTransition = "all 0.2s ease-in-out"
                 );
            diagnostics.style.With(
              (dynamic s) => s.transition = "all 0.3s ease-in-out"
        );


            Action Hide =
                delegate
                {
                    diagnostics.style.top = "-100%";
                    diagnostics.style.backgroundColor = "rgba(255, 255, 255, 0)";
                };

            Action Show =
                delegate
                {
                    diagnostics.style.top = "0%";
                    diagnostics.style.backgroundColor = "rgba(255, 255, 255, 0.9)";
                };

            Action Toggle =
                delegate
                {
                    if (diagnostics.style.top == "0%")
                    {
                        Hide();

                    }
                    else
                    {
                        Show();

                    }
                };
            Action<int> AtKeyCode =
                KeyCode =>
                {
                    new { KeyCode }.ToString().ToDocumentTitle();

                    if (KeyCode == 27)
                    {
                        Hide();

                    }

                    if (KeyCode == 222)
                    {
                        Toggle();
                    }
                };

            diagnostics.onload +=
                delegate
                {
                    diagnostics.contentWindow.document.onkeyup +=
                  e =>
                  {
                      AtKeyCode(e.KeyCode);
                  };

                    diagnostics.contentWindow.document.oncontextmenu +=
                        e =>
                        {
                            e.preventDefault();
                            Hide();

                        };
                };

            page.Tilde.onclick +=
                delegate
                {
                    Toggle();
                };

            Native.Document.onkeyup +=
                e =>
                {
                    AtKeyCode(e.KeyCode);
                };

            Native.Window.onorientationchange +=
                delegate
                {
                    if (Native.Window.orientation == 90)
                        Show();
                    else if (Native.Window.orientation == -90)
                        Show();
                    else
                        Hide();

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
