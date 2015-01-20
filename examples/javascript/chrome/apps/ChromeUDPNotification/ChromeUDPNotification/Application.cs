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
using ChromeUDPNotification;
using ChromeUDPNotification.Design;
using ChromeUDPNotification.HTML.Pages;
using chrome;
using ChromeUDPNotification.HTML.Images.FromAssets;

namespace ChromeUDPNotification
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
            // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeAudi\Application.cs
            // X:\jsc.svn\examples\javascript\chrome\apps\MulticastListenExperiment\MulticastListenExperiment\Application.cs
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeNotificationExperiment\ChromeNotificationExperiment\Application.cs


            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //new logo1
                #region AtUDPString
                Action<string> AtUDPString =
                     async xmlstring =>
                     {
                         // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
                         var xml = XElement.Parse(xmlstring);

                         if (xml.Value.StartsWith("Visit me at "))
                         {
                             // what about android apps runnning on SSL?
                             // what about preview images?
                             // do we get localhost events too?

                             var n = xml.Attribute("n");

                             var uri = "http://" + xml.Value.SkipUntilOrEmpty("Visit me at ");

                             var cnn = new ScriptCoreLib.JavaScript.DOM.Notification(
                                 title: "" + n,

                                   options: new
                                   {
                                       icon = new logo1().src
                                   }
                              );


                             //var cn = new chrome.Notification(
                             //    message: uri,
                             //    title: "" + n
                             //);

                             Task.Delay(8000).ContinueWith(x => cnn.close());

                             await cnn.async.onclick;

                             Native.window.open(uri + "/results?search_query=" + n);

                         }

                     };
                #endregion


                #region Launched
                chrome.app.runtime.Launched +=
                     async delegate
                     {
                         var gport = 40804;
                         var cnn = new ScriptCoreLib.JavaScript.DOM.Notification(
                             title: "awaiting for TV.. " + gport,

                             options: new
                             {
                                 icon = new logo1().src
                             }
                          );

                         Task.Delay(2000).ContinueWith(x => cnn.close());

                         //  var n = new chrome.Notification(
                         //    title: "awaiting for TV..",
                         //    message: "TV could say hi about now..."

                         //);

                         var socket = await chrome.socket.create("udp", new object());
                         var socketId = socket.socketId;
                         var value_setMulticastTimeToLive = await socketId.setMulticastTimeToLive(30);
                         var value_bind = await socketId.bind("0.0.0.0", gport);
                         var value_joinGroup = await socketId.joinGroup("239.1.2.3");
                         var forever = true;

                         while (forever)
                         {
                             var result = await socketId.recvFrom(1048576);
                             if (result.resultCode < 0)
                                 return;

                             byte[] source = new ScriptCoreLib.JavaScript.WebGL.Uint8ClampedArray(result.data);

                             var xml = Encoding.UTF8.GetString(source);

                             AtUDPString(xml);
                             // 52 bytes
                         }
                     };
                #endregion


            }

            #endregion

        }
    }
}