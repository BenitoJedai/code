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
using ChromeAudi;
using ChromeAudi.Design;
using ChromeAudi.HTML.Pages;
using ChromeAudi.HTML.Images.FromAssets;

namespace ChromeAudi
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
        public Application(
            global::WebGLAudi.HTML.Pages.IApp page)
        {


            // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeAudi\bin\Debug\staging\ChromeAudi.Application\web

            //"128": "assets/ChromeNexus7/x128.png"

            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "Audi Visualization";
                chrome.Notification.DefaultIconUrl = new x128().src;

                ChromeTCPServer.TheServerWithAppWindow.Invoke(
                    global::WebGLAudi.HTML.Pages.AppSource.Text
                    );

                return;
            }
            #endregion

            new global::WebGLAudi.Application(page);
        }

    }
}
