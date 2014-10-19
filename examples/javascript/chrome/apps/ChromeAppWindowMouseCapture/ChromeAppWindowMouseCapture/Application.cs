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
            // "X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow.sln"

            // since now jsc shows ssl support
            // how about packaging the view-source for chrome too?

            // nuget, add chrome.

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

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: null
                        );

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion


            // can we also test the shadow DOM ?
            // how does it work again?


            // now we have to update our alpha window/server window
            // to be in the correct context.

            // what about property window
            // back in the vb days we made one.
            // time to do one?

            new MyShadow { }.AttachTo(Native.shadow);

            // shadow will select div from chldren
            var div = new IHTMLDiv { }.AttachTo(Native.document.documentElement);


            new IHTMLPre { "drag me" }.AttachTo(div);
            var xy = new IHTMLPre { "{}" }.AttachTo(div);

            div.css.style.backgroundColor = "transparent";
            div.css.style.transition = "background 500ms linear";

            div.css.active.style.backgroundColor = "yellow";

            Native.document.documentElement.style.cursor = IStyle.CursorEnum.move;

            div.onmousemove +=
                e =>
                {
                    // we could tilt the svg cursor
                    // like we do on heat zeeker:D


                    //Native.document.title = new { e.CursorX, e.CursorY }.ToString();
                    xy.innerText = new { e.CursorX, e.CursorY }.ToString();

                };

            div.onmousedown +=
                async e =>
                {
                    e.CaptureMouse();

                    await div.async.onmouseup;
                };
        }

    }
}
