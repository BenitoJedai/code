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
using ChromeEarth;
using ChromeEarth.Design;
using ChromeEarth.HTML.Pages;
using System.Windows.Forms;
using ChromeEarth.HTML.Images.FromAssets;

namespace ChromeEarth
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
        public Application(WebGLEarthByBjorn.HTML.Pages.IApp page)
        {
            // https://chrome.google.com/webstore/developer/dashboard/g16921973856226221075?hl=en&gl=EE#
            // https://chrome.google.com/webstore/detail/odccmjodfgabfaolpgbepgneikcblman

            // https://code.google.com/p/chromium/issues/detail?id=260810

            // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Forms.FloatStyler\Abstractatech.JavaScript.Forms.FloatStyler\Application.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140801


            //There were warnings when trying to install this extension:
            //'background' is only allowed for extensions, hosted apps, and legacy packaged apps, but this is a packaged app.

            // window.localStorage is not available in packaged apps. Use chrome.storage.local instead. 
            // https://groups.google.com/a/chromium.org/forum/#!topic/chromium-apps/zmOXJxZEzsQ


            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "Earth Visualization";
                chrome.Notification.DefaultIconUrl = new x128().src;

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


            // https://chrome.google.com/webstore/detail/odccmjodfgabfaolpgbepgneikcblman/publish-delayed
            new WebGLEarthByBjorn.Application(page);

        }

    }
}
