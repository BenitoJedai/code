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
using ChromeGalaxyS;
using ChromeGalaxyS.Design;
using ChromeGalaxyS.HTML.Pages;
using System.Windows.Forms;

namespace ChromeGalaxyS
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140911

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(WebGLGalaxyS.HTML.Pages.IApp page)
        {
            // 20140911

            //An error occurred: Failed to process your item.

            //Invalid version number in manifest: 40. Please make sure the newly uploaded package has a larger version in file manifest.json than the published package: 40.

            //<webview tabindex="-1" partition="p1" allowtransparency="false" allowfullscreen="true" class=" WebBrowser" src="http://192.168.1.196:8077" style="opacity: 0; position: absolute; left: 0px; top: 0px; width: 664px; height: 509px;"></webview>

            // https://chrome.google.com/webstore/detail/dionniaojcmomejjhemchjmdadbnhhaj/preview

            #region ChromeTCPServer

            //<package id="Abstractatech.JavaScript.Forms.FloatStyler" version="1.0.0.0" targetFramework="net451" />
            //<package id="Chrome.Web.Server" version="1.0.0.0" targetFramework="net451" />
            //<package id="Chrome.Web.Server.StyledForm" version="1.0.0.0" targetFramework="net451" />
            //<package id="Chrome.Web.Store" version="1.0.0.0" targetFramework="net451" />

            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                chrome.Notification.DefaultTitle = "I9000";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.x128().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    //AtFormCreated: FormStyler.AtFormCreated
                    //AtFormCreated: FormStylerLikeFloat.LikeFloat,
                    AtFormCreated: FormStyler.LikeVisualStudioMetro,

                    transparentBackground: true,
                    resizable: false
                );

                return;
            }
            #endregion

            new WebGLGalaxyS.Application(page);
        }

    }
}
