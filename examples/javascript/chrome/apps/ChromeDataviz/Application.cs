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
using ChromeDataviz;
using ChromeDataviz.Design;
using ChromeDataviz.HTML.Pages;

namespace ChromeDataviz
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
        public Application(WebGLDatavizLayout.HTML.Pages.IApp page)
        {
            // https://chrome.google.com/webstore/developer/edit/ijhhfcakchepjhcaliedaifmafofmdnm

            //Icon image is missing.
            //At least one new-style screenshot or video is required.
            //Small tile image is missing.
            //Please select a Primary Category for your item.
            //Language is not selected.


            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {

                chrome.Notification.DefaultTitle = "Data Visualization";
                //chrome.Notification.DefaultIconUrl = new x128().src;

                ChromeTCPServer.TheServerWithAppWindow.Invoke(

                    WebGLDatavizLayout.HTML.Pages.AppSource.Text);

                return;
            }
            #endregion

            new WebGLDatavizLayout.Application(page);

        }

    }
}
