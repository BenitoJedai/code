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
using ChromeAppWindowForm;
using ChromeAppWindowForm.Design;
using ChromeAppWindowForm.HTML.Pages;
using System.Windows.Forms;

namespace ChromeAppWindowForm
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
            // can we get the resize grip to work?
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowMouseCapture\ChromeAppWindowMouseCapture\Application.cs

            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?


                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "Launched2");

                        // https://code.google.com/p/chromium/issues/detail?id=177706

                        var o = new object();
                        var hidden = o == o;
                        var alphaEnabled = o == o;
                        var alwaysOnTop = o == o;


                        var options = new
                        {
                            //allow webkitAppRegion
                            frame = "none",
                            hidden,
                            alphaEnabled,
                            alwaysOnTop
                        };

                        // The URL used for window creation must be local for security reasons.

                        var xappwindow = await chrome.app.window.create(
                           Native.document.location.pathname,
                           //Native.document.location.pathname + "#http://example.com", 
                           options
                    );

                        //xappwindow.set

                        // can we prevent the white page from appearing?
                        await xappwindow.contentWindow.async.onload;

                        xappwindow.contentWindow.document.title = "http://example.com";

                        await Task.Delay(1);
                        //await Task.Delay(200);

                        xappwindow.show();

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion

            // subst b: X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowForm\ChromeAppWindowForm\bin\Debug\staging\ChromeAppWindowForm.Application\web

            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Form\Form..ctor.cs
            //CaptionForeground.className = "caption";

            // Could not load file or assembly 'ScriptCoreLib' or one of its dependencies. The parameter is incorrect. (Exception from HRESULT: 0x80070057 (E_INVALIDARG))

            var css = Native.css[typeof(Form)][" .caption"];
            //var css = IStyleSheet.all[" .caption"];


            //new IHTMLPre { new { css.rule.selectorText } }.AttachToDocument();


            // https://code.google.com/p/chromium/issues/detail?id=229330
            (css.style as dynamic).webkitAppRegion = "drag";

            //(ff.CaptionForeground.style as dynamic).webkitAppRegion = "drag";

            FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            var f = new Form
            {

                ShowIcon = false,

                Text = Native.document.title,

                //Text = Native.document.location.hash,
                StartPosition = FormStartPosition.Manual
            };


            f.MoveTo(0, 0).SizeTo(
                    Native.window.Width,
                    Native.window.Height
                );

            //f.Opacity = 0.5;

            f.Show();


            var t = new TrackBar
            {

                Maximum = 100,
                Minimum = 40,

                Dock = DockStyle.Top
            };
            t.AttachTo(f);
            t.ValueChanged += delegate
            {
                f.Opacity = (double)t.Value / (double)t.Maximum;

            };
            f.Opacity = 0.8;



            f.FormClosed +=
                delegate
            {
                // close the appwindow

                // DWM animates the close.
                Native.window.close();
            };

            f.SizeChanged +=
                delegate
            {
                Native.window.resizeTo(
                    f.Width,
                    f.Height
                );

            };

            Native.window.onresize +=
                delegate
            {
                // outer frame is resized
                f.SizeTo(
                    Native.window.Width,
                    Native.window.Height
                );

            };

            // activate resize grip to window width?
        }

    }
}
