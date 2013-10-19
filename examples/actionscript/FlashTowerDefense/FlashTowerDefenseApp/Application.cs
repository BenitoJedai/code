using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FlashTowerDefenseApp.Components;
using FlashTowerDefenseApp.HTML.Pages;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;
using chrome;
using System.Windows.Forms;

namespace FlashTowerDefenseApp
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

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
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "FlashTowerDefenseApp";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text, 
                    ApplicationSprite.DefaultWidth ,
                    ApplicationSprite.DefaultHeight
                );

                return;
            }
            #endregion


            // Initialize MySprite1
            new ApplicationSprite().AttachSpriteToDocument();

        }

    }
}
