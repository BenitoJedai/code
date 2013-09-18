using SinePlasmaApplet;
using SinePlasmaApplet.Design;
using SinePlasmaApplet.HTML.Pages;
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

namespace SinePlasmaApplet
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        //public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            #region TheServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "SinePlasmaApplet";
                ChromeTCPServer.TheServer.Invoke(
                    DefaultSource.Text
                );


                return;
            }
            #endregion

            // Initialize ApplicationApplet
            new ApplicationApplet().AttachAppletToDocument();
            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
