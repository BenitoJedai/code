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
using ChromeWebviewFullscreen;
using ChromeWebviewFullscreen.Design;
using ChromeWebviewFullscreen.HTML.Pages;

namespace ChromeWebviewFullscreen
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
            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                // could we change the color of the window?

                // https://developer.chrome.com/apps/manifest/icons

                ChromeTCPServer.TheServerWithAppWindow.Invoke(AppSource.Text);

                return;
            }
            #endregion
            Console.WriteLine("? awaiting to go fullscreen");

            Native.body.style.backgroundColor = "rgba(255,255,255,0.7)";

            Action requestFullscreen = Native.document.body.requestFullscreen;
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs

            //            % c11:55907ms awaiting to go fullscreen.go!
            //View details

            //Failed to execute 'requestFullScreen' on 'Element': API can only be initiated by a user gesture.
            //View details

            // webview virtual dispatch
            Native.window.onmessage +=
                e =>
                {
                    //await contentWindow.postMessageAsync("virtual webview.requestFullscreen");
                    new IHTMLPre { new { e.data } }.AttachToDocument();

                    if (e.data == "virtual webview.requestFullscreen")
                    {
                        requestFullscreen = delegate
                        {
                            new IHTMLPre { "requestFullscreen" }.AttachToDocument();

                            //e.ports.

                            //e.ports.WithEach
                            e.postMessage("requestFullscreen");
                        };

                        return;
                    }

                    // ???
                    Native.document.body.innerText = new { e.data }.ToString();

                    //await contentWindow.postMessageAsync("virtual webview.requestFullscreen");
                };


            new IHTMLButton { "requestFullscreen" }.AttachToDocument().onclick +=
                e =>
                {
                    requestFullscreen();

                };

            new IHTMLButton { "exitFullscreen" }.AttachToDocument().onclick +=
             e =>
                {
                    Native.document.exitFullscreen();

                };
        }

    }
}
