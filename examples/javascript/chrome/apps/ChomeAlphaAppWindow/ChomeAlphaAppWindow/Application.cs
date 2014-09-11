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
using ChomeAlphaAppWindow;
using ChomeAlphaAppWindow.Design;
using ChomeAlphaAppWindow.HTML.Pages;
using chrome;

namespace ChomeAlphaAppWindow
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
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeGalaxyS\ChromeGalaxyS\Application.cs

            // ! https://code.google.com/p/chromium/issues/detail?id=413165

            // https://code.google.com/p/chromium/issues/detail?id=260810
            // https://code.google.com/p/chromium/issues/detail?id=125295
            // https://code.google.com/p/chromium/issues/detail?id=244892
            // https://code.google.com/p/chromium/issues/detail?id=260819

            // damn chrome. stop changing your experimental api. thanks
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140815

            // http://www.omgchrome.com/chrome-os-transparent-window-theme/
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowFrameNoneExperiment\ChromeAppWindowFrameNoneExperiment\Application.cs


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");
                    return;
                }

                // chrome://flags/
                // Enable experimental canvas features
                // chrome://version/

                //chrome.app.window.on

                #region Launched
                chrome.app.runtime.Launched +=
                    async delegate
                    {
                        // https://plus.google.com/+FrancoisBeaufort/posts/7vcqSGYgiqC

                        // http://src.chromium.org/viewvc/chrome/branches/2062/src/apps/app_window.cc?r1=285044&r2=285043&pathrev=285044


                        // jsc, when will we backe the generics?
                        //type$AejzKfYgdT_a9VVOdZOmeGA.alwaysOnTop = null;
                        //type$AejzKfYgdT_a9VVOdZOmeGA.transparentBackground = null;


                        //var transparentBackground = true;
                        // Error: Invalid value for argument 2. Property 'alpha_enabled': Unexpected property.

                        // Unchecked runtime.lastError while running app.window.create: The alphaEnabled option requires app.window.alpha permission.

                        //var alphaEnabled = false;
                        var alphaEnabled = true;

                        var alwaysOnTop = true;

                        // it allows to maximize, but disables alpha then.
                        var resizable = false;
                        //var resizable = true;

                        var options = new
                        {
                            frame = "none",
                            alwaysOnTop,
                            //transparentBackground,
                            alphaEnabled,
                            resizable
                        };

                        var xappwindow = await chrome.app.window.create(
                            Native.document.location.pathname,

                            // not allowed
                            //"about:blank",

                            // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                            options
                        );


                        //xappwindow.ons

                        //xappwindow.on



                        //var alwaysOnTop = true;
                        //xappwindow.setAlwaysOnTop(alwaysOnTop);


                        // Invocation of form app.currentWindowInternal.setAlwaysOnTop(integer) doesn't match definition app.currentWindowInternal.setAlwaysOnTop(boolean always_on_top)

                        // 0:1377ms {{ alphaEnabled = false }}

                        // exception>: TypeError: Cannot read property 'alphaEnabled' of undefined
                        var xalphaEnabled = xappwindow.alphaEnabled();

                        Console.WriteLine(
                             new { xalphaEnabled }
                         );


                        xappwindow.show();


                        await xappwindow.contentWindow.async.onload;

                        //appwindow.contentWindow.onload +=

                        // -webkit-app-region: drag 
                        // https://developer.chrome.com/apps/app_window

                        (xappwindow.contentWindow.document.documentElement.style as dynamic).webkitAppRegion = "drag";

                        xappwindow.contentWindow.document.body.style.backgroundColor = "rgba(255, 255, 0, 0.7)";

                        xappwindow.contentWindow.document.body.innerText = "it works when 'Enable experimental canvas features'";

                        xappwindow.contentWindow.document.body.style.position = IStyle.PositionEnum.absolute;

                        xappwindow.contentWindow.document.body.style.height = "100%";

                        // Refused to frame 'http://example.com/?' because it violates the following Content Security Policy directive: "frame-src 'self' data: chrome-extension-resource:"

                        //new IHTMLIFrame { allowTransparency = true, src = "http://example.com" }.AttachTo(xappwindow.contentWindow.document.body);


                        // are we on the wrong thread?
                        //var webview = Native.document.createElement("webview");

                        // !!! need to be on the same document!
                        var webview = xappwindow.contentWindow.document.createElement("webview");

                        // ? needed? does not help either way?
                        webview.setAttribute("partition", "p1");

                        // will it show up?
                        webview.setAttribute("src", "http://example.com");
                        webview.AttachTo(xappwindow.contentWindow.document.body);

                        new Notification { Message = "it works when 'Enable experimental canvas features'" };
                        // showed

                    };
                #endregion


                return;
            }



        }


    }
}
