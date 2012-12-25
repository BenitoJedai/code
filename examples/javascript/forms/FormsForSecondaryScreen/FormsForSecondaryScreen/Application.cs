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
            content.BackColor = Color.Transparent;

            if (Native.Window.opener != null)
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
                    content.button1.Text = "loading";

                    Native.Window.open("/", "_blank").With(
                        w =>
                        {
                            w.onload +=
                                delegate
                                {
                                    if (content.button1.Text == "loading")
                                    {
                                        content.button1.Text = "we are the primary screen!";


                                    }
                                    else if (content.button1.Text == "we are the primary screen!")
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

                                        Action update =
                                            delegate
                                            {
                                                dynamic xwlocal = Native.Window;

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


                                        new ScriptCoreLib.JavaScript.Runtime.Timer(
                                            delegate
                                            {
                                                update();
                                            }
                                        ).StartInterval(1000 / 60);
                                    }

                                };
                        }
                    );

                    @"primary screen".ToDocumentTitle();
                };

            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);

        }

    }
}
