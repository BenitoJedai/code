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
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeGalaxyS\ChromeGalaxyS\Application.cs

            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "Nexus7";
                //chrome.Notification.DefaultIconUrl = new x128().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    //AtFormCreated: FormStyler.AtFormCreated
                    AtFormCreated: FormStylerLikeFloat.LikeFloat,

                    transparentBackground: true,
                    resizable: false
                );

                return;
            }
            #endregion
            new WebGLNexus7.Application(page);
        }

    }
}
