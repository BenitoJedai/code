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
using ChromeNexus7;
using ChromeNexus7.Design;
using ChromeNexus7.HTML.Pages;
using System.Windows.Forms;
using ChromeNexus7.HTML.Images.FromAssets;

namespace ChromeNexus7
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
        public Application(global::WebGLNexus7.HTML.Pages.IApp page)
        {
            // http://www.asus.com/Tablets_Mobile/Nexus_7/HelpDesk_Download/

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140811

            // https://chrome.google.com/webstore/developer/edit/hdebmjbiddbadmkcnjepnefgmlolgjdd
            // https://chrome.google.com/webstore/detail/hdebmjbiddbadmkcnjepnefgmlolgjdd/publish-delayed

            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeGalaxyS\ChromeGalaxyS\Application.cs



            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "Nexus7";
                chrome.Notification.DefaultIconUrl = new x128().src;

                ChromeTCPServer.TheServerWithAppWindow.Invoke(AppSource.Text);

                return;
            }
            #endregion


            // 
            new WebGLNexus7.Application(page);
        }

    }
}
