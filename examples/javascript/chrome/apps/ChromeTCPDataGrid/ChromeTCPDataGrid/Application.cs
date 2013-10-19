using ChromeTCPDataGrid;
using ChromeTCPDataGrid.Design;
using ChromeTCPDataGrid.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeTCPDataGrid
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                //chrome.Notification.DefaultTitle = "FlashTowerDefense for Galaxy Note";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text
                    //,
                    //ApplicationSprite.DefaultWidth,
                    //ApplicationSprite.DefaultHeight
                );

                return;
            }
            #endregion

            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);
        }

    }
}
