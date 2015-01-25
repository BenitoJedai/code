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
//Error NuGet Package restore failed for project ChromeUDPNotification: Unable to find version '1.0.0.0' of package 'Chrome.Web.Store'..			0

using ChromeUDPNotification.HTML.Images.FromAssets;

using a.analysis;

namespace a
{
    static class analysis
    {
        public class i
        {
            public string name;
        }
        public static i caller
        {
            get
            {
                return null;
            }
        }
    }
}

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

            Console.WriteLine("hi!");


            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                Action respawn = delegate { };
                // https://code.google.com/p/chromium/issues/detail?id=142450


                //new logo1
                #region AtUDPString
                var AtUDPString = default(Action<string>);

                AtUDPString =
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

                             respawn = delegate
                             {
                                 AtUDPString(xmlstring);
                             };

                             //var cnn = new ScriptCoreLib.JavaScript.DOM.Notification(
                             //    title: "" + n,

                             //      options: new
                             //      {
                             //          icon = new logo1().src
                             //      }
                             // );
                             //Task.Delay(8000).ContinueWith(x => cnn.close());


                             var cn = new chrome.Notification(
                                 //message: uri,
                                 title: "" + n,
                                 iconUrl: new logo1().src
                             );

                             await (Task)cn;
                             //await cnn.async.onclick;

                             Native.window.open(uri + "/results?search_query=" + n);


                         }

                     };
                #endregion


                //delegate
                //{

                //};
                ////chrome.
                //chrome.runtime.Installed +=


                var Activate = default(Action);

                Activate =
                    async delegate
                    {
                        // context analysis injection?
                        //caller.name;

                        // need to run once only
                        Activate = delegate { };

                        // can we start here and allow Launched event to toggle?

                        // https://code.google.com/p/cassia/
                        // http://stackoverflow.com/questions/18052282/sending-message-from-one-application-to-another-application-in-the-same-terminal

                        var gport = 40804;

                        respawn = delegate
                        {

                            var cnn = new ScriptCoreLib.JavaScript.DOM.Notification(
                                title: "awaiting for TV.. " + gport,

                                options: new
                                {
                                    icon = new logo1().src,
                                    body = "what interests you?"
                                }
                            );

                            Task.Delay(10000).ContinueWith(x => cnn.close());
                        };

                        respawn();


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

                // make sure setup.exe and chrome.exe is closed if it seems stuck.

                // browser restart
                chrome.runtime.Startup +=
                    delegate { Activate(); };

                chrome.runtime.Installed +=
                    delegate { Activate(); };

                // if machine restarts, we never get launched?
                // Inspect views: background page (Inactive)

                chrome.app.runtime.Launched +=
                     async delegate
                     {
                         // re show the last data?

                         respawn();
                     };


            }

            #endregion

        }
    }
}