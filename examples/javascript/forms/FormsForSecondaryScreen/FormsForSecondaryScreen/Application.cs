using FormsForSecondaryScreen.Design;
using FormsForSecondaryScreen.HTML.Pages;
using FormsForSecondaryScreen.Library;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormsForSecondaryScreen
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // what about webrtc?

            content.BackColor = Color.Transparent;

            if (Native.window.opener != null)
            {
                content.button1.Enabled = false;
                content.button1.Text = "we are the secondary screen!";
                return;
            }

            var flocal = new Form1();

            flocal.Show();
            flocal.webBrowser1.Navigate("/jsc");

            content.button1.Click +=
                delegate
                {
                    content.button1.Enabled = false;

                    int c = -1;

                    Native.window.open("/", "_blank").With(
                        w =>
                        {
                            w.onload +=
                                delegate
                                {
                                    c++;

                                    if (c == 0)
                                    {

                                    }
                                    else if (c == 1)
                                    {
                                        // we need the secondary load?
                                        w.document.title = "secondary screen";


                                        w.document.body.style.backgroundColor = JSColor.Yellow;



                                        var fremote = new Form1 { Text = "(Remote)" };

                                        fremote.webBrowser1.Navigate("/jsc");

                                        fremote.Show();


                                        fremote.GetHTMLTarget().AttachTo(
                                            w.document.body
                                        );

                                        fremote.Opacity = 0.5;

                                        var shadow = new IHTMLDiv();

                                        shadow.style.SetLocation(
                                            32, 32, 200,
                                            200
                                        );

                                        shadow.style.backgroundColor = JSColor.Yellow;

                                        shadow.style.Opacity = 0.5;

                                        shadow.AttachToDocument();

                                        #region update
                                        Action update =
                                            delegate
                                            {
                                                dynamic xwlocal = Native.window;

                                                int xwlocal_left = xwlocal.screenLeft;
                                                int xwlocal_top = xwlocal.screenTop;

                                                int xwlocal_innerHeight = xwlocal.innerHeight;
                                                int xwlocal_outerHeight = xwlocal.outerHeight;
                                                int xwlocal_innerWidth = xwlocal.innerWidth;
                                                int xwlocal_outerWidth = xwlocal.outerWidth;

                                                dynamic xw = w;

                                                int xw_left = xw.screenLeft;
                                                int xw_top = xw.screenTop;

                                                int xw_innerHeight = xw.innerHeight;
                                                int xw_outerHeight = xw.outerHeight;
                                                int xw_innerWidth = xw.innerWidth;
                                                int xw_outerWidth = xw.outerWidth;

                                                Console.WriteLine(
                                                    new
                                                    {
                                                        flocal.Left,
                                                        xwlocal_left,
                                                        xw_left,
                                                        xwlocal_innerWidth,
                                                        xwlocal_outerWidth,
                                                        xw_innerWidth,
                                                        xw_outerWidth
                                                    }
                                                    );

                                                fremote.MoveTo(
                                                    flocal.Left + xwlocal_left + (xwlocal_outerWidth - xwlocal_innerWidth) - xw_left - (xw_outerWidth - xw_innerWidth),
                                                    flocal.Top + xwlocal_top + (xwlocal_outerHeight - xwlocal_innerHeight) - xw_top - (xw_outerHeight - xw_innerHeight)
                                                );

                                                fremote.SizeTo(
                                                    flocal.Width,
                                                    flocal.Height
                                                );

                                                shadow.style.SetLocation(
                                                   -xwlocal_left - (xwlocal_outerWidth - xwlocal_innerWidth) + xw_left + (xw_outerWidth - xw_innerWidth),
                                                   -xwlocal_top - (xwlocal_outerHeight - xwlocal_innerHeight) + xw_top + (xw_outerHeight - xw_innerHeight),

                                                   xw_innerWidth,
                                                   xw_innerHeight
                                               );

                                            };
                                        flocal.LocationChanged +=
                                            delegate
                                            {
                                                update();
                                            };

                                        flocal.SizeChanged +=
                                            delegate
                                            {
                                                update();
                                            };

                                        update();

                                        Action loop = null;
                                        loop = delegate
                                            {
                                                update();
                                                Native.window.requestAnimationFrame += loop;
                                            };
                                        loop();
                                        #endregion

                                        content.button1.Enabled = true;

                                    }

                                };
                        }
                    );

                    @"primary screen".ToDocumentTitle();
                };

            //content.AttachControlTo(page.Content);

            content.AttachControlToDocument();

            //content.AutoSizeControlTo(page.ContentSize);

        }

    }
}
