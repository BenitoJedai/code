using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestMouseCapturedMove.Design;
using TestMouseCapturedMove.HTML.Pages;

namespace TestMouseCapturedMove
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
            {

                Action<IHTMLElement> c =
                    i =>
                    {
                        IStyleSheetRule r = null;

                        i.onmousedown +=
                            e =>
                            {
                                // stop selection
                                e.CaptureMouse();

                                // this will be used by IE
                                page.Foo.style.cursor = IStyle.CursorEnum.crosshair;

                                // works in chrome, not IE
                                //r = IStyleSheet.Default.AddRule("html", rr => rr.style.cursor = IStyle.CursorEnum.move);
                                r = IStyleSheet.Default.AddRule("*", rr => rr.style.cursor = IStyle.CursorEnum.move);

                                //Native.Document.body.style.cursor = IStyle.CursorEnum.move;
                                page.Foo.style.backgroundColor = JSColor.Green;

                            };

                        i.onmouseup +=
                            e =>
                            {
                                //Native.Document.body.style.cursor = IStyle.CursorEnum.@default;

                                IStyleSheet.Default.RemoveRule(0);
                                page.Foo.style.cursor = IStyle.CursorEnum.@default;

                                page.Foo.style.backgroundColor = JSColor.Red;


                            };
                    };

                page.FooBubbler.onmousedown +=
                        e =>
                        {
                            page.FooBubbler.style.backgroundColor = JSColor.Green;

                        };

                page.FooBubbler.onmouseup +=
                    e =>
                    {
                        page.FooBubbler.style.backgroundColor = JSColor.Red;


                    };

                c(page.Foo);
                c(page.CaptureTest);

            }

            {

                Action<IHTMLElement> c =
                    i =>
                    {
                        IStyleSheetRule r = null;

                        i.ontouchstart +=
                            e =>
                            {
                                e.PreventDefault();

                                // stop selection
                                //e.CaptureMouse();

                                // this will be used by IE
                                page.Bar.style.cursor = IStyle.CursorEnum.crosshair;

                                // works in chrome, not IE
                                //r = IStyleSheet.Default.AddRule("html", rr => rr.style.cursor = IStyle.CursorEnum.move);
                                r = IStyleSheet.Default.AddRule("*", rr => rr.style.cursor = IStyle.CursorEnum.move);

                                //Native.Document.body.style.cursor = IStyle.CursorEnum.move;
                                page.Bar.style.backgroundColor = JSColor.Green;

                            };

                        i.ontouchend +=
                            e =>
                            {
                                //Native.Document.body.style.cursor = IStyle.CursorEnum.@default;

                                IStyleSheet.Default.RemoveRule(0);
                                page.Bar.style.cursor = IStyle.CursorEnum.@default;

                                page.Bar.style.backgroundColor = JSColor.Red;


                            };
                    };

                page.FooBubbler.ontouchstart +=
                        e =>
                        {
                            page.FooBubbler.style.backgroundColor = JSColor.Green;

                        };

                page.FooBubbler.ontouchend +=
                    e =>
                    {
                        page.FooBubbler.style.backgroundColor = JSColor.Red;


                    };

                c(page.Bar);
                c(page.BarButton);

            }


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
