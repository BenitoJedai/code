using AvalonCloseWindowExperiment;
using AvalonCloseWindowExperiment.Design;
using AvalonCloseWindowExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace AvalonCloseWindowExperiment
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
            #region AtFormCreated
            FormStyler.AtFormCreated =
                 s =>
                 {
                     s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                     var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                 };
            #endregion



            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "AvalonCloseWindowExperiment";
                //chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion

            ApplicationSprite sprite = new ApplicationSprite();

            sprite.AutoSizeSpriteTo(page.ContentSize);
            sprite.AttachSpriteTo(page.Content);

            sprite.DoClose +=
                delegate
                {
                    // if we are running as a chrome AppWindow
                    // then the webview might want to 
                    // listen to the close event?
                    // for now we get a blank screen instead.
                    // waiting for the fix.

                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs
                    Native.window.close();
                };

        }
    }
}
