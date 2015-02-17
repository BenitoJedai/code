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
using ConsoleWorm.Design;
using ConsoleWorm.HTML.Pages;
using ScriptCoreLib.JavaScript.Windows.Forms;

namespace ConsoleWorm
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();


        //[multicast]
        // chrome apps get udp broadcast here
        public event Action<XElement> AtFoo;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // has it ever been a chrome app?
            // we do have a version for android
            // X:\jsc.svn\examples\javascript\android\com.abstractatech.consoleworm\com.abstractatech.consoleworm\Application.cs
            // code duplicates!
            // "X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.craft\com.abstractatech.gamification.craft.sln"

#if FCHROME
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
                chrome.Notification.DefaultTitle = "ConsoleWorm";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    DefaultSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion
#endif



            new ConsoleWorm.js.Game();

            //@"Console Worm".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"Console Multi Worm",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
