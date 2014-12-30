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
using TestNotification;
using TestNotification.Design;
using TestNotification.HTML.Pages;

namespace TestNotification
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

            // {{ permission = default }}
            new IHTMLPre { new { Notification.permission } }.AttachToDocument();

            // https://developer.mozilla.org/en-US/docs/Web/API/Notification/Using_Web_Notifications
            // default: the user didn't give any permission yet (and therefore no notification will be displayed to the user).

            new IHTMLButton { "requestPermission!" }.AttachToDocument().onclick +=
                delegate
            {
                // Uncaught TypeError: Failed to execute 'requestPermission' on 'Notification': The callback provided as parameter 1 is not a function.
                Notification.requestPermission(
                    new Action<string>(
                        p =>
                        {

                            new IHTMLPre { new { p } }.AttachToDocument();
                            // {{ p = granted }}
                        }
                    )
                );


            };

            new IHTMLButton { "do!" }.AttachToDocument().onclick +=
                delegate
            {
                var n = new Notification("hello world", options: new { sticky = true, icon = new HTML.Images.FromAssets.jsc().src });

                n.onclick += delegate
                {
                    new IHTMLPre { "onclick" }.AttachToDocument();
                };

                n.onclose += delegate
                {
                    new IHTMLPre { "onclose" }.AttachToDocument();
                };

            };
        }

    }
}
